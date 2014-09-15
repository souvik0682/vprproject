using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPR.Utilities;
using VPR.BLL;
using System.Configuration;

using VPR.Utilities.ResourceManager;
using VPR.Common;

namespace VPR.WebApp.MasterModule
{
    public partial class AddEditBanner : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;
        private string BannerID = "";
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

                BannerID = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
                ClearText();
                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageBanner.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                txtBanner.Attributes["onkeypress"] = "javascript:return SetMaxLength(this, 300)";
                if (BannerID != "-1")
                    LoadData(BannerID);
                else
                {
                    rfvStartDate.Enabled = false;
                    rfvEndDate.Enabled = false;
                    txtStartDate.Enabled = false;
                    txtEndDate.Enabled = false;
                }
            }
            RetriveParameters();
            CheckUserAccess(BannerID);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BannerID = GeneralFunctions.DecryptQueryString(Request.QueryString["id"]);
            SaveData(BannerID);

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

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
            System.Data.DataSet ds = dbinteract.GetBanners(Convert.ToInt32(portId), "");

            if (!ReferenceEquals(ds, null) && ds.Tables[0].Rows.Count > 0)
            {
                txtBanner.Text = ds.Tables[0].Rows[0]["Banner"].ToString();
                ddlType.SelectedValue = ds.Tables[0].Rows[0]["BannerType"].ToString();
                if (ddlType.SelectedIndex == 1)
                {
                    txtStartDate.Text = ds.Tables[0].Rows[0]["StartDate"].ToString();
                    txtEndDate.Text = ds.Tables[0].Rows[0]["EndDate"].ToString();
                    txtStartDate.Enabled = true;
                    txtEndDate.Enabled = true;
                }
                else
                {
                    txtStartDate.Enabled = false;
                    txtEndDate.Enabled = false;
                    rfvStartDate.Enabled = false;
                    rfvEndDate.Enabled = false;

                }
            }
        }
        private void ClearText()
        {
            txtBanner.Text = "";
            txtEndDate.Text = "";
            txtStartDate.Text = "";
            ddlType.SelectedIndex = -1;

        }
        private void SaveData(string portId)
        {
            BLL.DBInteraction dbinteract = new BLL.DBInteraction();

            bool isedit = portId != "-1" ? true : false;
            if (!isedit)
                // if (dbinteract.GetPort(-1, txtPortCode.Text.Trim(), "").Tables[0].Rows.Count > 0)
                if (!dbinteract.IsUnique("mstBanner", "Banner", txtBanner.Text.Trim()))
                {
                    GeneralFunctions.RegisterAlertScript(this, "Banner text must be unique. Please try with another one.");
                    return;
                }

            if (fileUpload.HasFile)
            {
                var fileName = fileUpload.FileName;
                var path = Server.MapPath("~/Documents");
                var path1 = Convert.ToString(ConfigurationManager.AppSettings["ApplicationUrl"]) + "/Documents";
                var newFileName = Guid.NewGuid().ToString();

                if (!string.IsNullOrEmpty(path))
                {
                    path += @"\" + newFileName + System.IO.Path.GetExtension(fileName);
                    path1 += @"\" + newFileName + System.IO.Path.GetExtension(fileName);
                }
                if (ValidateSave(path))
                {
                    fileUpload.PostedFile.SaveAs(path);

                    int result = dbinteract.SaveBanner(_userId, Convert.ToInt32(portId), ddlType.SelectedValue.ToString(), txtBanner.Text.Trim().ToUpper(), txtStartDate.Text, txtEndDate.Text, path1, isedit);
                    if (result > 0)
                    {
                        Response.Redirect("~/MasterModule/ManageBanner.aspx");
                    }
                    else
                    {
                        GeneralFunctions.RegisterAlertScript(this, "Error Occured");
                    }
                }
                else
                    GeneralFunctions.RegisterAlertScript(this, "Error Occured");
            }
        }

  
        #endregion

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedIndex == 1)
            {
                rfvStartDate.Enabled = true;
                rfvEndDate.Enabled = true;
                txtStartDate.Enabled = true;
                txtEndDate.Enabled = true;
            }
            else
            {
                rfvStartDate.Enabled = false;
                rfvEndDate.Enabled = false;
                txtStartDate.Enabled = false;
                txtEndDate.Enabled = false;
            }

        }

        private bool ValidateSave(string path)
        {
            bool IsValid = true;

            if (string.IsNullOrEmpty(path) || path.Length == 0)
            {
                IsValid = false;
                lblErr.Text = "Please provide fields properly";
            }
            return IsValid;
        }
    }
}