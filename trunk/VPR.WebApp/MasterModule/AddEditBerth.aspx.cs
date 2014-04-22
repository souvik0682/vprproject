using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VPR.Entity;
using VPR.BLL;
using VPR.Utilities;
using VPR.Utilities.ResourceManager;
using VPR.Common;

namespace VPR.WebApp.MasterModule
{
    public partial class AddEditBerth : System.Web.UI.Page
    {
        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private int _userLocation = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            _userId = UserBLL.GetLoggedInUserId();
            _userLocation = UserBLL.GetUserLocation();
            CheckUserAccess();

            if (!IsPostBack)
            {
              
                if (!ReferenceEquals(Request.QueryString["BerthId"], null))
                {
                    int BerthId = 0;
                    BerthId = GeneralFunctions.DecryptQueryString(Request.QueryString["BerthId"].ToString()).ToInt();
                    btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageBerth.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                    if (BerthId > 0)
                    {
                        ViewState["BerthId"] = BerthId;
                        LoadForEdit(BerthId);
                    }
                    else
                    {
                        ViewState["BerthId"] = 0;
                    }
                }
                else
                {
                    ViewState["BerthId"] = 0;
                }
            }

            txtPort.TextChanged += new EventHandler(txtPort_TextChanged);
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

        private void LoadForEdit(int VesselId)
        {
            VesselEntity o = new VesselEntity();
            o = new TransactionBLL().GetVessel(VesselId);

            txtBerthName.Text = o.BerthName;

            string port = new TransactionBLL().GetPortNameById(o.PortId);
            ViewState["PORTID"] = o.PortId;
            ((TextBox)txtPort.FindControl("txtPort")).Text = port;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateSave())
            {
                BerthEntity o = new BerthEntity();

                if (!ReferenceEquals(ViewState["BerthId"], null))
                    o.BerthId = Convert.ToInt32(ViewState["BerthId"]);

                o.BerthName = txtBerthName.Text.Trim();
                o.PortId = Convert.ToInt32(ViewState["PORTID"]);

                new CommonBLL().SaveBerth(o);

                lblErr.Text = "Record saved successfully";
            }
        }

        private bool ValidateSave()
        {
            bool IsValid = true;

            if (Convert.ToString(ViewState["PORTID"]) == string.Empty || Convert.ToString(ViewState["PORTID"]) == "0")
            {
                IsValid = false;
                errPort.Text = "This field is required";
            }

            return IsValid;
        }
    }
}