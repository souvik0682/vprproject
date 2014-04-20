using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPR.Common;
using VPR.BLL;
using VPR.Entity;
using VPR.Utilities;
using System.Data;
using VPR.Utilities.ResourceManager;

namespace VPR.WebApp.MasterModule
{
    public partial class vendor_add_edit : System.Web.UI.Page
    {
        VendorEntity oVendorEntity;
        VendorBLL oVendorBll;
        UserEntity oUserEntity;

        #region Private Member Variables

        private int _userId = 0;
        private int _locId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //CheckUserAccess();
            //IUser user = (IUser)Session[Constants.SESSION_USER_INFO];
            //_userId = user == null ? 0 : user.Id;

            RetriveParameters();
            if (!Page.IsPostBack)
            {
                ListItem Li = null;
                Li = new ListItem("Select", "0");
                //PopulateDropDown((int)Enums.DropDownPopulationFor.VendorType, ddlVendorType, 0);
                //ddlVendorType.Items.Insert(0, Li);

                Li = new ListItem("Select", "0");
                PopulateDropDown();
                ddlCountry.Items.Insert(0, Li);

                if (hdnVendorID.Value != "0")
                    LoadData();
            }
            CheckUserAccess(hdnVendorID.Value);
        }

        //private void CheckUserAccess()
        //{
        //    if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
        //    {
        //        IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

        //        if (ReferenceEquals(user, null) || user.Id == 0)
        //        {
        //            Response.Redirect("~/Login.aspx");
        //        }

        //        if (user.UserRole.Id != (int)UserRole.Admin)
        //        {
        //            Response.Redirect("~/Unauthorized.aspx");
        //        }
        //    }
        //    else
        //    {
        //        Response.Redirect("~/Login.aspx");
        //    }
        //}

        private void LoadData()
        {
            VendorEntity oVendor = (VendorEntity)VendorBLL.GetVendor(Convert.ToInt32(hdnVendorID.Value));

            txtName.Text = oVendor.VendorName;
            txtAddress1.Text = oVendor.VendorAddress1;
            txtAddress2.Text = oVendor.VendorAddress2;
            txtPhone.Text = oVendor.Phone;
            txtCity.Text = oVendor.City;
            txtState.Text = oVendor.State;
            ddlCountry.SelectedValue = oVendor.fk_CountryID.ToString();
            TxtEmail.Text = oVendor.EmailID;
            TxtMob.Text = oVendor.Mobile;
            
        }

        void PopulateDropDown()
        {


            DataTable dt = new CommonBLL().GetAllCountry();
            DataRow dr = dt.NewRow();

            dr["pk_countryID"] = "0";
            dr["CountryName"] = "--Select--";
            dt.Rows.InsertAt(dr, 0);
            ddlCountry.DataValueField = "pk_countryID";
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataSource = dt;
            ddlCountry.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                oVendorBll = new VendorBLL();
                oVendorEntity = new VendorEntity();
                //oUserEntity = (UserEntity)Session[Constants.SESSION_USER_INFO]; // This section has been commented temporarily

                oVendorEntity.VendorName = txtName.Text.Trim();
                oVendorEntity.VendorAddress1 = txtAddress1.Text.Trim();
                oVendorEntity.VendorAddress2 = txtAddress2.Text.Trim();
                oVendorEntity.VendorActive = true;
                oVendorEntity.State = txtState.Text;
                oVendorEntity.City = txtCity.Text;
                oVendorEntity.EmailID = TxtEmail.Text;
                oVendorEntity.Phone = txtPhone.Text;
                oVendorEntity.Mobile = TxtMob.Text;
                oVendorEntity.fk_CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
     
                if (hdnVendorID.Value == "0") // Insert
                {
                    oVendorEntity.CreatedBy = _userId;// oUserEntity.Id;
                    oVendorEntity.CreatedOn = DateTime.Today.Date;
                    oVendorEntity.ModifiedBy = _userId;// oUserEntity.Id;
                    oVendorEntity.ModifiedOn = DateTime.Today.Date;

                    switch (oVendorBll.AddEditVndor(oVendorEntity))
                    {
                        case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                            break;
                        case 1: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00009");
                            ClearAll();
                            break;
                    }
                }
                else // Update
                {
                    oVendorEntity.VendorId = Convert.ToInt32(hdnVendorID.Value);
                    oVendorEntity.ModifiedBy = _userId;// oUserEntity.Id;
                    oVendorEntity.ModifiedOn = DateTime.Today.Date;

                    switch (oVendorBll.AddEditVndor(oVendorEntity))
                    {
                        case 0: lblMessage.Text = ResourceManager.GetStringWithoutName("ERR00011");
                            break;
                        case 1: Response.Redirect("~/MasterModule/vendor-list.aspx");
                            break;
                    }
                }


            }
        }

        private void RetriveParameters()
        {
            //_userId = UserBLL.GetLoggedInUserId();

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _locId);
                hdnVendorID.Value = _locId.ToString();
            }
            _userId = VPR.BLL.UserBLL.GetLoggedInUserId();

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

                    if (!_canEdit && xID != "0")
                    {
                        btnSave.Visible = false;
                    }
                    else if (!_canAdd && xID == "0")
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MasterModule/vendor-list.aspx");
        }

        void ClearAll()
        {
            ddlCountry.SelectedIndex = 0;

            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtName.Text = string.Empty;
            hdnVendorID.Value = "0";
            txtCity.Text = string.Empty;
            txtState.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            TxtMob.Text = string.Empty;
           

        }
    }
}