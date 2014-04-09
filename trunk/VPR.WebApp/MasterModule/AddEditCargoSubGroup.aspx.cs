using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPR.Utilities;
using VPR.Entity;
using VPR.BLL;
using System.Data;
using VPR.Utilities.ResourceManager;
using VPR.Common;

namespace VPR.WebApp.MasterModule
{
    public partial class AddEditCargoSubGroup : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;
        private string CargoSubGroupID = "";
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
                LoadCargoGroupDDL();
                CargoSubGroupID = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
                ClearText();
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageCargoSubGroup.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                txtCargoSubGroupName.Attributes["onkeypress"] = "javascript:return SetMaxLength(this, 200)";
                if (CargoSubGroupID != "-1")
                    LoadData(CargoSubGroupID);
            }
            RetriveParameters();
            CheckUserAccess(CargoSubGroupID);
            

        }

         protected void btnSave_Click(object sender, EventArgs e)
        {
            CargoSubGroupID = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
            SaveData(CargoSubGroupID);

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
            System.Data.DataSet ds = dbinteract.GetCargoSubGroup(Convert.ToInt32(portId), "", "");

            if (!ReferenceEquals(ds, null) && ds.Tables[0].Rows.Count > 0)
            {
                txtCargoSubGroupName.Text = ds.Tables[0].Rows[0]["CargoSubGroupName"].ToString();
                ddlCargoGroup.SelectedValue = ds.Tables[0].Rows[0]["fk_CargoGroupID"].ToString();
            }
        }
        private void ClearText()
        {
            txtCargoSubGroupName.Text = "";
        }
        private void SaveData(string portId)
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();
            bool isedit = portId != "-1" ? true : false;
            if (!isedit)
                // if (dbinteract.GetPort(-1, txtPortCode.Text.Trim(), "").Tables[0].Rows.Count > 0)
                if (!dbinteract.IsUnique("mstCargoSubGroup", "CargoSubGroupName", txtCargoSubGroupName.Text.Trim()))
                {
                    GeneralFunctions.RegisterAlertScript(this, "Cargo Name must be unique. Please try with another one.");
                    return;
                }
            int result = dbinteract.AddEditCargoSubGroup(_userId, Convert.ToInt32(portId), txtCargoSubGroupName.Text.Trim().ToUpper(), isedit, ddlCargoGroup.SelectedValue.ToInt());


            if (result > 0)
            {
                Response.Redirect("~/MasterModule/ManageCargoSubGroup.aspx");
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

        private void LoadCargoGroupDDL()
        {
            DataTable dt = new CommonBLL().GetAllGroup();
            DataRow dr = dt.NewRow();
            dr["pk_CargoGroupID"] = "0";
            dr["CargoGroupName"] = "--Select--";
            dt.Rows.InsertAt(dr, 0);
            ddlCargoGroup.DataValueField = "pk_CargoGroupID";
            ddlCargoGroup.DataTextField = "CargoGroupName";
            ddlCargoGroup.DataSource = dt;
            ddlCargoGroup.DataBind();
        }

        protected void ddlCargoGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}