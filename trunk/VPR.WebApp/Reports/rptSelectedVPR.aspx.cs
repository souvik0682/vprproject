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
    public partial class rptSelectedVPR : System.Web.UI.Page
    {
        #region Private Member Variables
        List<VesselPosition> lstcargoReport = null;

        private IFormatProvider _culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"].ToString());
        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    RetriveParameters();
                    CheckUserAccess();
                }
                catch (Exception ex)
                {
                    CommonBLL.HandleException(ex, this.Server.MapPath(this.Request.ApplicationPath).Replace("/", "\\"));
                }
            }
            txtPort.TextChanged += new EventHandler(txtPort_TextChanged);
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

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

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
            LocalReportManager reportManager = new LocalReportManager(rptViewer, "VPRwithParameter", ConfigurationManager.AppSettings["ReportNamespace"].ToString(), ConfigurationManager.AppSettings["ReportPath"].ToString());
            ReportCriteria criteria = new ReportCriteria();
            BuildCriteria(criteria);

            lstcargoReport = ReportBAL.GetVPR(criteria);
            ReportDataSource dsGeneral = new ReportDataSource("dsSelectedVPR", lstcargoReport);
            
            string Port = ((TextBox)txtPort.FindControl("txtPort")).Text;
            string PortName = "";
            if (Port == string.Empty)
                PortName = "All Ports";
            else
            {
                int portid = Convert.ToInt32(ViewState["PORTID"]);
                PortName = new TransactionBLL().GetOnlyPortNameById(portid);
            }
            reportManager.AddParameter("Activity", Convert.ToString(ddlActivity.SelectedItem));
            reportManager.AddParameter("PortNo", PortName);

            reportManager.AddDataSource(dsGeneral);
            reportManager.Show();
        }

        private void BuildCriteria(ReportCriteria criteria)
        {
            string Port = ((TextBox)txtPort.FindControl("txtPort")).Text;
            if (Port == string.Empty)
                criteria.PortId = 0;
            else
                criteria.PortId = Convert.ToInt32(ViewState["PORTID"]);  // Convert.ToInt32(ddlPort.SelectedValue);
            criteria.Activity = ddlActivity.SelectedValue;

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