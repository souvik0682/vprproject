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
    public partial class CargoReport : System.Web.UI.Page
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
                    RetriveParameters();
                    CheckUserAccess();
                    SetAttributes();
                    LoadCargo();
                    LoadCountry();
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
            }
            catch (Exception ex)
            {
                //ReportBAL.HandleException(ex, this.Server.MapPath(this.Request.ApplicationPath).Replace("/", "\\"));
                //ToggleErrorPanel(true, ex.Message);
            }
        }

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                btnShow.ToolTip = ResourceManager.GetStringWithoutName("R00058");
                ceFromDt.Format = Convert.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                ceToDt.Format = Convert.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                rfvFromDt.ErrorMessage = ResourceManager.GetStringWithoutName("R00062");
                rfvToDt.ErrorMessage = ResourceManager.GetStringWithoutName("R00063");
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

        private void GenerateReport()
        {
            var cls = new ReportBAL();
            LocalReportManager reportManager = new LocalReportManager(rptViewer, "CargoReport", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            ReportCriteria criteria = new ReportCriteria();
            BuildCriteria(criteria);

            lstcargoReport = ReportBAL.GetCargoReport(criteria);
            ReportDataSource dsGeneral = new ReportDataSource("DataSetCargoReport", lstcargoReport);
            reportManager.AddParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"]));
            reportManager.AddParameter("FromDate", txtFromDt.Text.Trim());
            reportManager.AddParameter("ToDate", txtToDt.Text.Trim());
            reportManager.AddParameter("Cargo", Convert.ToString(ddlCargo.SelectedItem));
            reportManager.AddParameter("Country", Convert.ToString(ddlCountry.SelectedItem));

            //rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", Convert.ToString(ConfigurationManager.AppSettings["CompanyName"])));
            reportManager.AddDataSource(dsGeneral);
            reportManager.Show();
        }

        private void BuildCriteria(ReportCriteria criteria)
        {
            if (txtFromDt.Text.Trim() != string.Empty) criteria.FromDate = Convert.ToDateTime(txtFromDt.Text, _culture);
            if (txtToDt.Text.Trim() != string.Empty) criteria.ToDate = Convert.ToDateTime(txtToDt.Text, _culture);
            //criteria.TransactionType = ddlTxnType.SelectedValue;
            criteria.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
        }

        private void LoadCargo()
        {
            DataTable dt = new ReportBAL().GetAllCargo();
            //DataRow dr = dt.NewRow();
            //dr["pk_CargoGroupId"] = "0";
            //dr["CargoGroupName"] = "All Counters";
            //dt.Rows.InsertAt(dr, 0);
            ddlCargo.DataValueField = "pk_CargoGroupId";
            ddlCargo.DataTextField = "CargoGroupName";
            ddlCargo.DataSource = dt;
            ddlCargo.DataBind();
        }

        private void LoadCountry()
        {

            DataTable dt = new ReportBAL().GetAllCountry();
            //DataRow dr = dt.NewRow();
            //dr["pk_CountryId"] = "0";
            //dr["CountryName"] = "All Counters";
            //dt.Rows.InsertAt(dr, 0);
            ddlCountry.DataValueField = "pk_CountryId";
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataSource = dt;
            ddlCountry.DataBind();
        }

        protected void ddlCargo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //private void ToggleErrorPanel(bool isVisible, string errorMessage)
        //{
        //    if (isVisible)
        //    {
        //        dvSync.Style["display"] = "";
        //        dvErrMsg.InnerHtml = GeneralFunctions.FormatErrorMessage(errorMessage);
        //    }
        //    else
        //    {
        //        dvSync.Style["display"] = "none";
        //        dvErrMsg.InnerHtml = string.Empty;
        //    }
        //}
    }
}