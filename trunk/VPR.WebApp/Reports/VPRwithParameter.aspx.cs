using System;
using System.Collections.Generic;
using System.Configuration;
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
    public partial class VPRwithParameter : System.Web.UI.Page
    {
        #region Private Member Variables
        List<VesselPosition> VesselPosition = null;

        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());
        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;

        #endregion
        //List<VesselPosition> AllVesselPosition = null;
        //List<VesselPosition> DistinctVesselPosition = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RetriveParameters();
                CheckUserAccess();
                //LoadDDLs();
            }
        }

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


        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateReport();
            }
            catch (Exception ex)
            {
                //ReportBAL.HandleException(ex, this.Server.MapPath(this.Request.ApplicationPath).Replace("/", "\\"));
                //ToggleErrorPanel(true, ex.Message);
            }
        }

        //private void SetDefaultSearchCriteria(SearchCriteria criteria)
        //{
        //    criteria.portID = Convert.ToInt32(ViewState["PORTID"]);  // Convert.ToInt32(ddlPort.SelectedValue);
        //    criteria.ActivityName = ddlActivity.SelectedItem.ToString();
        //    //criteria.BLDate = txtMovementDate.toda();
        //}

        
        private void GenerateReport()
        {
            var cls = new ReportBAL();
            LocalReportManager reportManager = new LocalReportManager(rptViewer, "VPRwithParameter", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            ReportCriteria criteria = new ReportCriteria();
            BuildCriteria(criteria);

            VesselPosition = ReportBAL.GetVPR(criteria);
            ReportDataSource dsGeneral = new ReportDataSource("dsSelectedVPR", VesselPosition);
            //reportManager.AddParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"]));
            //reportManager.AddParameter("FromDate", txtFromDt.Text.Trim());
            //reportManager.AddParameter("ToDate", txtToDt.Text.Trim());
            //reportManager.AddParameter("Cargo", Convert.ToString(ddlCargo.SelectedItem));
            //reportManager.AddParameter("Country", Convert.ToString(ddlCountry.SelectedItem));

            reportManager.AddParameter("Activity", Convert.ToString(ddlActivity.SelectedItem));
            //criteria.PortId = Convert.ToInt32(ViewState["PORTID"]);
            reportManager.AddParameter("PortNo", Convert.ToString(ViewState["PORTID"]));


            //rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"])));
            reportManager.AddDataSource(dsGeneral);
            reportManager.Show();
        }

        //private void LoadDDLs()
        //{
        //    DataTable dt = new TransactionBLL().GetPortWithTransaction();
        //    DataRow dr = dt.NewRow();
        //    dr["pk_PortID"] = "0";
        //    dr["PortName"] = "--Select--";
        //    dt.Rows.InsertAt(dr, 0);
        //    ddlPort.DataValueField = "pk_PortID";
        //    ddlPort.DataTextField = "PortName";
        //    ddlPort.DataSource = dt;
        //    ddlPort.DataBind();
        //}

        private void BuildCriteria(ReportCriteria criteria)
        {
            //criteria.TransactionType = ddlTxnType.SelectedValue;
            //criteria.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
            //criteria.CargoId = Convert.ToInt32(ddlCargo.SelectedValue);
            //criteria.CargoGroupId = Convert.ToInt32(ddlCargoGroup.SelectedValue);
            criteria.PortId = Convert.ToInt32(ViewState["PORTID"]);  // Convert.ToInt32(ddlPort.SelectedValue);
            criteria.Activity = ddlActivity.SelectedValue;
            //if (ddlCountry.SelectedIndex == 0)
            //    criteria.CountryName = "0";
            //else
            //    criteria.CountryName = ddlCountry.SelectedItem.ToString().Substring(0, 2);
        }

        void txtPort_TextChanged(object sender, EventArgs e)
        {
            string port = ((TextBox)txtPort.FindControl("txtPort")).Text;

            if (port != string.Empty)
            {
                if (port.Split('|').Length > 1)
                {
                    string portCode = port.Split('|')[1].Trim();

                    int portId = new TransactionBLL().GetPortId(portCode);

                    ViewState["PORTID"] = portId;
                }
                else
                {
                    ViewState["PORTID"] = null;
                }
            }
            else
            {
                ViewState["PORTID"] = null;
            }


        }

    }
}