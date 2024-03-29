﻿using System;
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
                    LoadCargoGroup();
                    LoadCargo(0, 0);
                    LoadCountry();
                    //LoadPort("");
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
            reportManager.AddParameter("CargoGroup", Convert.ToString(ddlCargoGroup.SelectedItem));
            if (ddlPort.Items.Count > 0)
                reportManager.AddParameter("Port", Convert.ToString(ddlPort.SelectedItem));
            else
                reportManager.AddParameter("Port", "All Ports");

            if (ddlSubGroup.Items.Count > 0)
                reportManager.AddParameter("SubGroup", Convert.ToString(ddlSubGroup.SelectedItem));
            else
                reportManager.AddParameter("SubGroup", "All Sub Group");
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
            criteria.CargoId = Convert.ToInt32(ddlCargo.SelectedValue);
            criteria.CargoGroupId = Convert.ToInt32(ddlCargoGroup.SelectedValue);
            if (ddlSubGroup.Items.Count > 0)
                criteria.SubGroupID = Convert.ToInt32(ddlSubGroup.SelectedValue);
            else
                criteria.SubGroupID = 0;

            if (ddlPort.Items.Count > 0)
                criteria.PortId = Convert.ToInt32(ddlPort.SelectedValue);
            else
                criteria.PortId = 0;
            criteria.Activity = ddlActivity.SelectedValue;
            if (ddlCountry.SelectedIndex == 0)
                criteria.CountryName = "0";
            else
                criteria.CountryName = ddlCountry.SelectedItem.ToString().Substring(0, 2);
        }

        private void LoadCargoGroup()
        {
            DataTable dt = new ReportBAL().GetAllCargoGroup();
            DataRow dr = dt.NewRow();
            dr["pk_CargoGroupId"] = "0";
            dr["CargoGroupName"] = "All Groups";
            dt.Rows.InsertAt(dr, 0);
            ddlCargoGroup.DataValueField = "pk_CargoGroupId";
            ddlCargoGroup.DataTextField = "CargoGroupName";
            ddlCargoGroup.DataSource = dt;
            ddlCargoGroup.DataBind();
        }

        private void LoadCargo(int GroupID, int SubGroupID)
        {
            DataTable dt = new ReportBAL().GetAllCargo(GroupID, SubGroupID);
            DataRow dr = dt.NewRow();
            dr["pk_CargoId"] = "0";
            dr["CargoName"] = "All Cargo";
            dt.Rows.InsertAt(dr, 0);
            ddlCargo.DataValueField = "pk_CargoId";
            ddlCargo.DataTextField = "CargoName";
            ddlCargo.DataSource = dt;
            ddlCargo.DataBind();
        }

        private void LoadPort(string Country)
        {
            DataTable dt = new ReportBAL().GetAllPorts(Country);
            DataRow dr = dt.NewRow();
            dr["pk_PortId"] = "0";
            dr["PortName"] = "All Ports";
            dt.Rows.InsertAt(dr, 0);
            ddlPort.DataValueField = "pk_PortId";
            ddlPort.DataTextField = "PortName";
            ddlPort.DataSource = dt;
            ddlPort.DataBind();
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

        private void LoadCargoSubGroup(int CargoGroupID)
        {
            DataTable dt = new ReportBAL().GetAllCargoSubGroup(CargoGroupID);
            DataRow dr = dt.NewRow();
            dr["pk_CargoSubGroupId"] = "0";
            dr["CargoSubGroupName"] = "All Sub Groups";
            dt.Rows.InsertAt(dr, 0);
            ddlSubGroup.DataValueField = "pk_CargoSubGroupId";
            ddlSubGroup.DataTextField = "CargoSubGroupName";
            ddlSubGroup.DataSource = dt;
            ddlSubGroup.DataBind();
        }

        protected void ddlCargo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPort(ddlCountry.SelectedItem.ToString().Substring(0,2));
        }

        protected void ddlPort_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCargoGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCargoSubGroup(Convert.ToInt16(ddlCargoGroup.SelectedValue));
            LoadCargo(Convert.ToInt32(ddlCargoGroup.SelectedValue), 0);
        }

        protected void ddlSubGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCargo(Convert.ToInt32(ddlCargoGroup.SelectedValue), Convert.ToInt32(ddlSubGroup.SelectedValue));
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