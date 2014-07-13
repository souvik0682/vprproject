using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPR.BLL;
using Microsoft.Reporting.WebForms;
using VPR.Entity;
using System.Configuration;
using VPR.Utilities.ReportManager;
using VPR.Utilities.ResourceManager;
using VPR.Common;
using System.Data;

namespace VPR.WebApp.Reports
{
    public partial class forceSendMail : System.Web.UI.Page
    {
        #region Private Member Variables
        List<CargoReportEntity> lstcargoReport = null;

        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());
        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;

        #endregion

        //List<CargoReport> AllVesselPosition = null;
        //List<CargoReport> DistinctVesselPosition = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                try
                {
                    //RetriveParameters();
                    //CheckUserAccess();
                    //SetAttributes();
                    LoadCargoGroup();
                    LoadCountry();
                    pnlContainer.Attributes.CssStyle["display"] = "none";
                    lblMessage.Attributes.CssStyle["display"] = "none";
                }
                catch (Exception ex)
                {
                    CommonBLL.HandleException(ex, this.Server.MapPath(this.Request.ApplicationPath).Replace("/", "\\"));
                    //ToggleErrorPanel(true, ex.Message);
                }
            }

        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateReport();
                //ModalPopupExtender1.Show();
            }
            catch (Exception ex)
            {
                //ReportBAL.HandleException(ex, this.Server.MapPath(this.Request.ApplicationPath).Replace("/", "\\"));
                //ToggleErrorPanel(true, ex.Message);
            }
        }

        //private void SetAttributes()
        //{
        //    if (!IsPostBack)
        //    {
        //        btnShow.ToolTip = ResourceManager.GetStringWithoutName("R00058");
        //        ceFromDt.Format = Convert.ToString(ConfigurationManager.AppSettings["DateFormat"]);
        //        ceToDt.Format = Convert.ToString(ConfigurationManager.AppSettings["DateFormat"]);
        //        rfvFromDt.ErrorMessage = ResourceManager.GetStringWithoutName("R00062");
        //        rfvToDt.ErrorMessage = ResourceManager.GetStringWithoutName("R00063");
        //    }
        //}

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            //Get user permission.
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }

        private void CheckUserAccess()
        {
            if (!_canView)
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }

        private void GenerateReport()
        {
            ReportCriteria criteria = new ReportCriteria();
            BuildCriteria(criteria);
            DataTable dtFilteredContainer = new DataTable();
            ReportBAL emailBLL = new ReportBAL();

            dtFilteredContainer = emailBLL.GetEmailIDs(criteria);

            if (dtFilteredContainer != null && dtFilteredContainer.Rows.Count > 0)
            {
                gvMail.DataSource = dtFilteredContainer;
                gvMail.DataBind();
                pnlContainer.Attributes.CssStyle["display"] = "";
                lblMessage.Attributes.CssStyle["display"] = "none";
            }
            else
            {
                gvMail.DataSource = null;
                gvMail.DataBind();
                pnlContainer.Attributes.CssStyle["display"] = "none";
                lblMessage.Attributes.CssStyle["display"] = "";
            }
        }

        private void BuildCriteria(ReportCriteria criteria)
        {
            //criteria.TransactionType = ddlTxnType.SelectedValue;
            criteria.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
            //criteria.CargoId = Convert.ToInt32(ddlCargo.SelectedValue);
            criteria.CargoGroupId = Convert.ToInt32(ddlCargoGroup.SelectedValue);
            if (ddlSubGroup.Items.Count > 0)
                criteria.SubGroupID = Convert.ToInt32(ddlSubGroup.SelectedValue);
            else
                criteria.SubGroupID = 0;

            if (ddlEmailGroup.Items.Count > 0)
                criteria.EmailGroupID = Convert.ToInt32(ddlEmailGroup.SelectedValue);
            else
                criteria.EmailGroupID = 0;
        }

        private void LoadCargoGroup()
        {
            DataTable dt = new ReportBAL().GetAllCargoGroup();
            DataRow dr = dt.NewRow();
            dr["pk_CargoGroupId"] = "0";
            dr["CargoGroupName"] = "-- Select --";
            dt.Rows.InsertAt(dr, 0);
            ddlCargoGroup.DataValueField = "pk_CargoGroupId";
            ddlCargoGroup.DataTextField = "CargoGroupName";
            ddlCargoGroup.DataSource = dt;
            ddlCargoGroup.DataBind();
        }

        private void LoadEmailGroup(int SubGroupID)
        {
            DataTable dt = new ReportBAL().GetAllMailGroup(SubGroupID);
            DataRow dr = dt.NewRow();
            dr["pk_EmailGroupId"] = "0";
            dr["GroupName"] = "-- Select --";
            dt.Rows.InsertAt(dr, 0);
            ddlEmailGroup.DataValueField = "pk_EmailGroupId";
            ddlEmailGroup.DataTextField = "GroupName";
            ddlEmailGroup.DataSource = dt;
            ddlEmailGroup.DataBind();
        }

        private void LoadCountry()
        {

            DataTable dt = new ReportBAL().GetAllCountry();
            DataRow dr = dt.NewRow();
            dr["pk_CountryId"] = "0";
            dr["CountryName"] = "-- Select --";
            dt.Rows.InsertAt(dr, 0);
            ddlCountry.DataValueField = "pk_CountryId";
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataSource = dt;
            ddlCountry.DataBind();
        }

        private void LoadCargoSubGroup(int CargoGroupID)
        {
            DataTable dt = new ReportBAL().GetAllCargoSubGroup(CargoGroupID);
            DataRow dr = dt.NewRow();
            dr["pk_CargoSubGroupId"] = "0";
            dr["CargoSubGroupName"] = "-- Select --";
            dt.Rows.InsertAt(dr, 0);
            ddlSubGroup.DataValueField = "pk_CargoSubGroupId";
            ddlSubGroup.DataTextField = "CargoSubGroupName";
            ddlSubGroup.DataSource = dt;
            ddlSubGroup.DataBind();
        }

     
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadPort(ddlCountry.SelectedItem.ToString().Substring(0, 2));
        }

        protected void ddlCargoGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCargoSubGroup(Convert.ToInt16(ddlCargoGroup.SelectedValue));
            //LoadCargo(Convert.ToInt32(ddlCargoGroup.SelectedValue), 0);
        }

        protected void ddlSubGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEmailGroup(Convert.ToInt16(ddlSubGroup.SelectedValue));
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";

                string attachmentFileName = string.Empty;
                string emailIds = string.Empty;
                int emailGroupId = Convert.ToInt32(ddlEmailGroup.SelectedValue);

                foreach (GridViewRow row in gvMail.Rows)
                {
                    CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkEmp");

                    if (ChkBoxRows.Checked == true)
                    {
                        HiddenField hdnEmailId = (HiddenField)row.FindControl("hdnEmailId");
                        emailIds += "," + hdnEmailId.Value;
                    }
                }

                if (!string.IsNullOrEmpty(emailIds))
                {
                    emailIds = emailIds.Remove(0, 1);

                    //Send Email : EmailGroupId, EmailIds, AttachmentFile
                    new ReportBAL().SendForceEmail(emailGroupId, emailIds, attachmentFileName);
                    lblError.Text = "Email(s) sent successfully!";
                    lblError.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblError.Text = "Please select Email(s)!";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error while sending Email(s)!";
                lblError.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void ddlEmailGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvMail.DataSource = null;
            gvMail.DataBind();
            pnlContainer.Attributes.CssStyle["display"] = "none";
            //lblMessage.Attributes.CssStyle["display"] = "";
        }

        //void fillContainer(int EmptyYardId)
        //{

        //    ContainerTranBLL oContainerTranBLL = new ContainerTranBLL();
        //    dtFilteredContainer = oContainerTranBLL.GetContainerTransactionListFiltered(Convert.ToInt16(ddlFromStatus.SelectedValue), EmptyYardId, Convert.ToDateTime(txtDate.Text), Convert.ToInt16(ddlLine.SelectedValue));
        //    ViewState["Container"] = dtFilteredContainer;
        //    gvContainer.DataSource = dtFilteredContainer;
        //    gvContainer.DataBind();
        //}

        protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)gvMail.HeaderRow.FindControl("chkboxSelectAll");
            foreach (GridViewRow row in gvMail.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkEmp");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }
    }
}