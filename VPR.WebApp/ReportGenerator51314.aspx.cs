using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;
using VPR.Entity;
using VPR.BLL;
using System.Configuration;

namespace VPR.WebApp
{
    public partial class ReportGenerator51314 : System.Web.UI.Page
    {
        List<VesselPosition> AllVesselPosition = null;
        List<VesselPosition> DistinctVesselPosition = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblResponse.Text = Generate();
        }
        
        protected void SubreportEventHandler(object sender, SubreportProcessingEventArgs e)
        {


            try
            {
                e.DataSources.Clear();
                var portName = e.Parameters[0].Values[0];
                var VesselPositionLoading = AllVesselPosition.Where(x => x.ActivityStatus == "L" && x.PortName == portName).ToList();
                var VesselPositionDischarge = AllVesselPosition.Where(x => x.ActivityStatus == "D" && x.PortName == portName).ToList();
                var VesselPositionAwaiting = AllVesselPosition.Where(x => x.ActivityStatus == "A" && x.PortName == portName).ToList();
                var VesselPositionArrive = AllVesselPosition.Where(x => (x.ActivityStatus == string.Empty || x.ActivityStatus == "") && x.PortName == portName).ToList();
                e.DataSources.Add(new ReportDataSource("VesselPositionLoading", VesselPositionLoading));
                e.DataSources.Add(new ReportDataSource("VesselPositionDischarge", VesselPositionDischarge));
                e.DataSources.Add(new ReportDataSource("VesselPositionAwaiting", VesselPositionAwaiting));
                e.DataSources.Add(new ReportDataSource("VesselPositionArrive", VesselPositionArrive));

            }
            catch { }

        }
        public string Generate()
        {
            var reportBAL = new ReportBAL();
            AllVesselPosition = reportBAL.GetVesselPosition(null);
            if (AllVesselPosition == null)
            {
                return string.Empty;
            }
            DistinctVesselPosition = reportBAL.GetDistinctPortsFromVesselPosition(AllVesselPosition);
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            ReportViewer rptViewer = new ReportViewer();
            string rptName = "MainVesselPosition.rdlc";
            rptViewer.Reset();
            rptViewer.LocalReport.Dispose();
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.ShowDetailedSubreportMessages = true;
            rptViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportEventHandler);
            rptViewer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath) + ConfigurationManager.AppSettings["ReportPath1"].ToString() + "/" + rptName;
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("DistinctPortName", DistinctVesselPosition));
            rptViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", "BEN LINE AGENCIES (INDIA) PVT. LTD."));
            rptViewer.LocalReport.SetParameters(new ReportParameter("Address", "VESSEL LINE UP AS ON DT. - " + DateTime.Now.ToString("dd/MM/yyyy")));
            byte[] bytes = rptViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            string fileName = "VesselPosition-" + DateTime.Now.ToString("dd-MM-yyyy") + ".pdf";
            string filePath = HttpContext.Current.Server.MapPath("DailyReport") + @"\" + fileName;

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
            return fileName;
        }
    }
}