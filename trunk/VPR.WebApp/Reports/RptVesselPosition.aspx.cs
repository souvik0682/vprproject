using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using VPR.BLL;
using Microsoft.Reporting.WebForms;
using VPR.Entity;

using System.Configuration;
namespace VPR.WebApp.Reports
{
    public partial class RptVesselPosition : System.Web.UI.Page
    {
        List<VesselPosition> AllVesselPosition = null;
        List<VesselPosition> DistinctVesselPosition = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadDDLs();
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            var reportBAL = new ReportBAL();
            SearchCriteria newcriteria = new SearchCriteria();
            SetDefaultSearchCriteria(newcriteria);

            AllVesselPosition = reportBAL.GetVesselPosition(newcriteria);
            if (AllVesselPosition != null)
            {
                DistinctVesselPosition = reportBAL.GetDistinctPortsFromVesselPosition(AllVesselPosition);// vesselPositions.Select(x => x.PortName).Distinct();
                GenerateReport();
                lblMsg.Visible = false;
                rptViewer.Visible = true;
            }
            else
            {
               lblMsg.Text= "No Data Found";
               lblMsg.Visible = true;
               rptViewer.Visible = false;
            }
        }

        private void SetDefaultSearchCriteria(SearchCriteria criteria)
        {
            criteria.portID = Convert.ToInt32(ddlPort.SelectedValue);
            criteria.ActivityName = ddlActivity.SelectedItem.ToString();
            //criteria.BLDate = txtMovementDate.toda();
        }

        static int  index=0;
        protected void SubreportEventHandler(object sender, SubreportProcessingEventArgs e)
        {

            if (DistinctVesselPosition.Count > index)
            {
                try
                {
                    e.DataSources.Clear();
                    var VesselPositionLoading = AllVesselPosition.Where(x => x.ActivityStatus == "L" && x.PortName == DistinctVesselPosition[index].PortName).ToList();
                    var VesselPositionDischarge = AllVesselPosition.Where(x => x.ActivityStatus == "D" && x.PortName == DistinctVesselPosition[index].PortName).ToList();
                    var VesselPositionAwaiting = AllVesselPosition.Where(x => x.ActivityStatus == "A" && x.PortName == DistinctVesselPosition[index].PortName).ToList();
                    var VesselPositionArrive = AllVesselPosition.Where(x => (x.ActivityStatus == string.Empty || x.ActivityStatus == "") && x.PortName == DistinctVesselPosition[index].PortName).ToList();
                    e.DataSources.Add(new ReportDataSource("VesselPositionLoading", VesselPositionLoading));
                    e.DataSources.Add(new ReportDataSource("VesselPositionDischarge", VesselPositionDischarge));
                    e.DataSources.Add(new ReportDataSource("VesselPositionAwaiting", VesselPositionAwaiting));
                    e.DataSources.Add(new ReportDataSource("VesselPositionArrive", VesselPositionArrive));
                    
                }
                catch { }
                index++;
            }

        }

        private void GenerateReport()
        {
            string rptName = "MainVesselPosition.rdlc";
            rptViewer.Reset();
            index = 0;
            rptViewer.LocalReport.Dispose();
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.ShowDetailedSubreportMessages = true;
            rptViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportEventHandler);
            rptViewer.LocalReport.ReportPath = this.Server.MapPath(this.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath1"].ToString() + "/" + rptName;

            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("DistinctPortName", DistinctVesselPosition));
            
            rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", "BEN LINE AGENCIES (INDIA) PVT. LTD."));
            rptViewer.LocalReport.SetParameters(new ReportParameter("Address", "VESSEL LINE UP AS ON DT. - "+DateTime.Now.ToString("dd/MM/yyyy")));
          //  rptViewer.LocalReport.SetParameters(new ReportParameter("PortNo",DistinctVesselPosition[0].PortName));
            rptViewer.LocalReport.Refresh();
        }

        private void LoadDDLs()
        {
            DataTable dt = new TransactionBLL().GetPortWithTransaction();
            DataRow dr = dt.NewRow();
            dr["pk_PortID"] = "0";
            dr["PortName"] = "All Ports";
            dt.Rows.InsertAt(dr, 0);
            ddlPort.DataValueField = "pk_PortID";
            ddlPort.DataTextField = "PortName";
            ddlPort.DataSource = dt;
            ddlPort.DataBind();
        }

    }
}
