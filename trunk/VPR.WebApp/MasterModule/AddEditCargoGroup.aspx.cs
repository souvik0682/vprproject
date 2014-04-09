using System;
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
    public partial class AddEditCargoGroup : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;
        private string CargoGroupID = "";
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        #endregion

        #region Protected Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargoGroupID = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
                ClearText();
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageCargoGroup.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                txtCargoGroupName.Attributes["onkeypress"] = "javascript:return SetMaxLength(this, 200)";
                if (CargoGroupID != "-1")
                    LoadData(CargoGroupID);
            }
            RetriveParameters();
            CheckUserAccess(CargoGroupID);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            CargoGroupID = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
            SaveData(CargoGroupID);

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
            System.Data.DataSet ds = dbinteract.GetCargoGroup(Convert.ToInt32(portId), "");

            if (!ReferenceEquals(ds, null) && ds.Tables[0].Rows.Count > 0)
            {
                txtCargoGroupName.Text = ds.Tables[0].Rows[0]["CargoGroupName"].ToString();
            }
        }
        private void ClearText()
        {
            txtCargoGroupName.Text = "";
        }
        private void SaveData(string portId)
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            bool isedit = portId != "-1" ? true : false;
            if (!isedit)
                // if (dbinteract.GetPort(-1, txtPortCode.Text.Trim(), "").Tables[0].Rows.Count > 0)
                if (!dbinteract.IsUnique("mstCargoGroup", "CargoGroupName", txtCargoGroupName.Text.Trim()))
                {
                    GeneralFunctions.RegisterAlertScript(this, "Cargo Name must be unique. Please try with another one.");
                    return;
                }
            int result = dbinteract.AddEditCargoGroup(_userId, Convert.ToInt32(portId), txtCargoGroupName.Text.Trim().ToUpper(), isedit);


            if (result > 0)
            {
                Response.Redirect("~/MasterModule/ManageCargoGroup.aspx");
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