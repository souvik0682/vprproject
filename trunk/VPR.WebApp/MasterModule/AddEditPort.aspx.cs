﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPR.Utilities;

using VPR.Utilities.ResourceManager;
using VPR.Common;

namespace VPR.WebApp.MasterModule
{
    public partial class AddEditPort : System.Web.UI.Page
    {

        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;
        private string portId = "";
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        #endregion


        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
          
            //_userId = VPR.BLL.UserBLL.GetLoggedInUserId();

            if (!IsPostBack)
            {
                portId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
                ClearText();
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManagePort.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                txtPortName.Attributes["onkeypress"] = "javascript:return SetMaxLength(this, 200)";
                if (portId != "-1")
                    LoadData(portId);
            }
            RetriveParameters();
            CheckUserAccess(portId);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            portId = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
            SaveData(portId);

        }

        #endregion

        #region Private Methods

        private void RetriveParameters()
        {
            _userId = VPR.BLL.UserBLL.GetLoggedInUserId();

            //Get user permission.
            VPR.BLL.UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }

        private void CheckUserAccess(string xID)
        {
            if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (ReferenceEquals(user, null) || user.Id == 0)
                {
                    Response.Redirect("~/Login.aspx");
                }

                btnSave.Visible = true;

                if (!_canAdd && !_canEdit)
                    btnSave.Visible = false;
                else
                {

                    if (!_canEdit && xID != "-1")
                    {
                        btnSave.Visible = false;
                    }
                    else if (!_canAdd && xID == "-1")
                    {
                        btnSave.Visible = false;
                    }
                }

            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void LoadData(string portId)
        {
            ClearText();

            int intportId = 0;
            if (portId == "" || !Int32.TryParse(portId, out intportId))
                return;
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            System.Data.DataSet ds = dbinteract.GetPort(Convert.ToInt32(portId), "", "");

            if (!ReferenceEquals(ds, null) && ds.Tables[0].Rows.Count > 0)
            {
                txtPortName.Text = ds.Tables[0].Rows[0]["PortName"].ToString();
                txtPortCode.Text = ds.Tables[0].Rows[0]["PortCode"].ToString();
                ddlICD.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["ICDIndicator"]);
                txtPortAddressee.Text = ds.Tables[0].Rows[0]["PortAddressee"].ToString();
                txtAdd1.Text = ds.Tables[0].Rows[0]["Address2"].ToString();
                txtAdd2.Text = ds.Tables[0].Rows[0]["Address3"].ToString();
                txtExportPort.Text = ds.Tables[0].Rows[0]["ExportPort"].ToString();

            }
        }
        private void ClearText()
        {
            txtPortCode.Text = "";
            txtPortName.Text = "";
            txtPortAddressee.Text = "";
            txtAdd1.Text = "";
            txtAdd2.Text = "";
        }
        private void SaveData(string portId)
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            bool isedit = portId != "-1" ? true : false;
            if (!isedit)
                // if (dbinteract.GetPort(-1, txtPortCode.Text.Trim(), "").Tables[0].Rows.Count > 0)
                if (!dbinteract.IsUnique("DSR.dbo.mstPort", "PortCode", txtPortCode.Text.Trim()))
                {
                    GeneralFunctions.RegisterAlertScript(this, "Port Code must be unique. The given code has already been used for another port. Please try with another one.");
                    return;
                }
            int result = dbinteract.AddEditPort(_userId, Convert.ToInt32(portId), txtPortName.Text.Trim().ToUpper(), txtPortCode.Text.Trim().ToUpper(), ddlICD.SelectedIndex == 0 ? false : true, txtPortAddressee.Text.ToUpper(), txtAdd1.Text.ToUpper(), txtAdd2.Text.ToUpper(), txtExportPort.Text.ToUpper(), isedit);


            if (result > 0)
            {
                Response.Redirect("~/MasterModule/ManagePort.aspx");
            }
            else
            {
                GeneralFunctions.RegisterAlertScript(this, "Error Occured");
            }
        }


        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }
    }
}