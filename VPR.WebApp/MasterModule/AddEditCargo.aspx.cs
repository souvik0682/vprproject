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
    public partial class AddEditCargo : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private int _userLocation = 0;
        #endregion

        #region Protected Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            _userId = UserBLL.GetLoggedInUserId();
            _userLocation = UserBLL.GetUserLocation();
            CheckUserAccess();

            if (!IsPostBack)
            {
                LoadCargoGroupDDL();

                rfvCargoSubGroup.Visible = false;
                rfvCargoSubGroup.Enabled = false;
 
                if (!ReferenceEquals(Request.QueryString["Id"], null))
                {
                    int CargoId = 0;
                    CargoId = GeneralFunctions.DecryptQueryString(Request.QueryString["Id"].ToString()).ToInt();
                    btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageCargo.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                    if (CargoId > 0)
                    {
                        ViewState["CargoId"] = CargoId;
                        LoadForEdit(CargoId);
                    }
                    else
                    {
                        ViewState["CargoId"] = null;
                    }
                }
                else
                {
                    ViewState["CargoId"] = null;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                CargoGroupEntity Cargo = new CargoGroupEntity();

                if (!ReferenceEquals(ViewState["CargoId"], null))
                    Cargo.pk_CargoID = Convert.ToInt32(ViewState["CargoId"]);
                else
                    Cargo.pk_CargoID = 0;

                Cargo.pk_CargoGroupID = ddlCargoGroup.SelectedValue.ToInt();
                Cargo.pk_CargoSubGroupID = ddlCargoSubGroup.SelectedValue.ToInt();
                Cargo.CargoName = txtCargoName.Text.Trim();
                Cargo.CreatedBy = _userId;
                Cargo.ModifiedBy = _userId;

                new CargoBLL().SaveCargo(Cargo);
                ClearText();
                lblMessage.Text = "Cargo Saved Successfully";
                txtCargoName.Focus();
            }

        }

        #endregion

        #region Private Methods

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            //Get user permission.
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }

        private void CheckUserAccess()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (ReferenceEquals(user, null) || user.Id == 0)
                {
                    Response.Redirect("~/Login.aspx");
                }

                if (_canView == false)
                {
                    Response.Redirect("~/Unauthorized.aspx");
                }

                if (user.UserRole.Id != (int)UserRole.Admin)
                {

                    //ddlLocation.Enabled = false;
                }
                else
                {
                    _userLocation = 0;
                    //ddlLocation.Enabled = true;
                }

                if (!_canEdit)
                    btnSave.Visible = false;
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private bool IsValid()
        {
            bool isValid = true;
            lblCargoName.Text = "";

            if (ReferenceEquals(ViewState["CargoId"], null))
            {
                if (new EmailBLL().IsEmailGroupExists(txtCargoName.Text.Trim()))
                {
                    isValid = false;
                    lblCargoName.Text = "Cargo Name not available!";
                }
            }

            return isValid;
        }

        private void LoadForEdit(int CargoId)
        {
            ICargoGroup objGroup = new CargoBLL().GetCargo(CargoId);

            txtCargoName.Text = objGroup.CargoName;
            ddlCargoGroup.SelectedValue = objGroup.fk_CargoGroupID.ToString();
            ddlCargoSubGroup.Items.Clear();
            LoadCargoSubGroupDDL();
            ddlCargoSubGroup.SelectedValue = objGroup.fk_CargoSubGroupID.ToString();
            //ddlCountry_SelectedIndexChanged(this, EventArgs.Empty);

        }

        private void ClearText()
        {
            txtCargoName.Text = "";
            ddlCargoGroup.SelectedIndex = -1;
            ddlCargoSubGroup.SelectedIndex = -1;

        }

        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        //private void LoadCargoGroupDDL()
        //{
        //    DataTable dt = new CommonBLL().GetAllGroup();
        //    DataRow dr = dt.NewRow();
        //    dr["pk_CargoGroupID"] = "0";
        //    dr["CargoGroupName"] = "--Select--";
        //    dt.Rows.InsertAt(dr, 0);
        //    ddlCargoGroup.DataValueField = "pk_CargoGroupID";
        //    ddlCargoGroup.DataTextField = "CargoGroupName";
        //    ddlCargoGroup.DataSource = dt;
        //    ddlCargoGroup.DataBind();
        //}

        private void LoadCargoGroupDDL()
        {
            DataTable dt = new EmailBLL().GetAllCargoGroup();
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
            ddlCargoSubGroup.Items.Clear();
            LoadCargoSubGroupDDL();
            //DataTable dt = new CargoBLL().GetAllCargoSubGroup(ddlCargoGroup.SelectedValue.ToInt());
            //DataRow dr = dt.NewRow();
            //dr["pk_CargoSubGroupID"] = "0";
            //dr["CargoSubGroupName"] = "--Select--";
            //dt.Rows.InsertAt(dr, 0);
            //ddlCargoSubGroup.DataValueField = "pk_CargoSubGroupID";
            //ddlCargoSubGroup.DataTextField = "CargoSubGroupName";
            //ddlCargoSubGroup.DataSource = dt;
            //ddlCargoSubGroup.DataBind();

        }

        protected void LoadCargoSubGroupDDL()
        {
            DataTable dt = new CargoBLL().GetAllCargoSubGroup(ddlCargoGroup.SelectedValue.ToInt());
            DataRow dr = dt.NewRow();
            dr["pk_CargoSubGroupID"] = "0";
            dr["CargoSubGroupName"] = "--Select--";
            dt.Rows.InsertAt(dr, 0);
            ddlCargoSubGroup.DataValueField = "pk_CargoSubGroupID";
            ddlCargoSubGroup.DataTextField = "CargoSubGroupName";
            ddlCargoSubGroup.DataSource = dt;
            ddlCargoSubGroup.DataBind();

        }
    }
}