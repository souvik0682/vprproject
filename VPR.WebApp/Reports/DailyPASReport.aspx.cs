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
    public partial class DailyPASReport : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                try
                {
                    RetriveParameters();
                    CheckUserAccess();
                    SetAttributes();
                    LoadCargo(0);
                    LoadCountry();
                }
                catch (Exception ex)
                {
                    CommonBLL.HandleException(ex, this.Server.MapPath(this.Request.ApplicationPath).Replace("/", "\\"));
                }
            }
            txtPort.TextChanged += new EventHandler(txtPort_TextChanged);

        }

        private void SetAttributes()
        {
            if (!IsPostBack)
            {

                ceMovementDate.Format = Convert.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                ceMovementDate1.Format = Convert.ToString(ConfigurationManager.AppSettings["DateFormat"]);
                rfvMovementDate.ErrorMessage = ResourceManager.GetStringWithoutName("R00062");
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

        protected void btnShow_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void LoadCargo(int GroupID)
        {
            DataTable dt = new ReportBAL().GetAllCargo(GroupID, 0);
            DataRow dr = dt.NewRow();
            dr["pk_CargoId"] = "0";
            dr["CargoName"] = "All Cargo";
            dt.Rows.InsertAt(dr, 0);
            ddlCargo.DataValueField = "pk_CargoId";
            ddlCargo.DataTextField = "CargoName";
            ddlCargo.DataSource = dt;
            ddlCargo.DataBind();
        }

        private void LoadCountry()
        {

            DataTable dt = new ReportBAL().GetAllCountry();
            DataRow dr = dt.NewRow();
            dr["pk_CountryId"] = "0";
            dr["CountryName"] = "All Country";
            dt.Rows.InsertAt(dr, 0);
            ddlCountry.DataValueField = "pk_CountryId";
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataSource = dt;
            ddlCountry.DataBind();
        }

        private void GenerateReport()
        {
            ReportBAL cls = new ReportBAL();
            try
            {
                DateTime dt1 = DateTime.Today;
                DateTime dt2 = DateTime.Today;
                DataTable dtExcel = new DataTable();
                string CountryAbbr;

                dt1 = Convert.ToDateTime(txtMovementDate1.Text.Trim());
                dt2 = Convert.ToDateTime(txtMovementDate2.Text.Trim());
                if (ddlCountry.SelectedIndex == 0)
                    CountryAbbr = "0";
                else
                    CountryAbbr = ddlCountry.SelectedItem.ToString().Substring(0, 2);

                string Port = ((TextBox)txtPort.FindControl("txtPort")).Text;
                int PortID = 0;
                if (Port != string.Empty)
                {
                    PortID = Convert.ToInt32(ViewState["PORTID"]);
                }
                dtExcel = cls.GetPASExcelReport(dt1, dt2, Convert.ToInt32(ddlCargo.SelectedValue), PortID, CountryAbbr, ddlActivity.SelectedValue);

                ExporttoExcel(dtExcel);
            }
            catch (Exception)
            {
                //Response.Write(ex.Message.ToString());
            }

        }
        private void ExporttoExcel(DataTable table)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=DailyPASReport.xls");
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");

            for (int j = 0; j < table.Columns.Count; j++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(table.Columns[j].ColumnName);
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            try
            {
                HttpContext.Current.Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        protected void ddlCargo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadPort(ddlCountry.SelectedItem.ToString().Substring(0, 2));
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