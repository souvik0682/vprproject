using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPR.Utilities;
using VPR.Common;
using VPR.BLL;
using System.Data;
using System.Text;
using System.Collections.Specialized;

namespace VPR.WebApp
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            InitialMethod();
        }

        private void InitialMethod()
        {
            //Clears the application cache.
            GeneralFunctions.ClearApplicationCache();

            SetUserAccess();

            if (!Request.Path.Contains("ChangePassword.aspx"))
            {
                if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
                {
                    IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                    if (ReferenceEquals(user, null) || user.Id == 0)
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                    SetAttributes(user);
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
            else
            {
                if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
                {
                    IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                    if (!ReferenceEquals(user, null) && user.Id > 0)
                    {
                        SetAttributes(user);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            Page.Header.DataBind();
            this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "Common", this.ResolveClientUrl("~/Scripts/Common.js"));
            this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "CustomTextBox", this.ResolveClientUrl("~/Scripts/CustomTextBox.js"));

            GenerateUserSpecificMenu(UserBLL.GetLoggedInUserId());
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }

        protected void lnkPwd_Click(object sender, EventArgs e)
        {

        }

        private void SetAttributes(IUser user)
        {
            lblUserName.Text = "Welcome " + user.UserFullName;
        }

        private void SetUserAccess()
        {
            if (Request.QueryString["mid"] != null)
            {
                int menuId = 0;
                int userId = 0;

                userId = UserBLL.GetLoggedInUserId();
                menuId = Convert.ToInt32(VPR.Utilities.GeneralFunctions.DecryptQueryString(Request.QueryString["mid"]));
                IUserPermission userPermission = UserBLL.GetMenuAccessByUser(userId, menuId);
                Session[Constants.SESSION_USER_PERMISSION] = userPermission;
            }           
            
        }

        private void GenerateUserSpecificMenu(int UserId)
        {
            DataTable MenuTable = UserBLL.GetUserSpecificMenuList(UserId);

            StringBuilder StringMenu = new StringBuilder();

            //Filter for 1st level menu
            DataRow[] PRows = MenuTable.Select("PID = 0");
            StringMenu.Append("<ul>");
            GenerateList(MenuTable, StringMenu, PRows);
            StringMenu.Append("</ul>");

            navbar.InnerHtml = StringMenu.ToString();
        }

        private string GenerateList(DataTable MenuTable, StringBuilder StringMenu, DataRow[] PRows)
        {
            foreach (DataRow pRow in PRows)
            {
                //Checking whether child exist or not
                DataRow[] FLCRows = MenuTable.Select("PID = " + pRow["MenuID"].ToString());

                if (pRow["Name"].ToString() == "Home" || pRow["Name"].ToString() == "Change Password" || pRow["IsParent"].ToString() == "0")
                {
                    if (pRow["CanView"].ToString() == "1")
                    {
                        StringMenu.Append("<li>");
                        switch (pRow["Name"].ToString())
                        {
                            case "Voyage":
                                if (pRow["Navigation"].ToString().IndexOf('?') >= 0)
                                    StringMenu.Append("<a href='" + Page.ResolveClientUrl(pRow["Navigation"].ToString() + "&p=" + VPR.Utilities.GeneralFunctions.EncryptQueryString("master")) + "&mid=" + VPR.Utilities.GeneralFunctions.EncryptQueryString(pRow["MenuID"].ToString()) + "'>" + pRow["Name"].ToString() + "</a>");
                                else
                                    StringMenu.Append("<a href='" + Page.ResolveClientUrl(pRow["Navigation"].ToString() + "?p=" + VPR.Utilities.GeneralFunctions.EncryptQueryString("master")) + "&mid=" + VPR.Utilities.GeneralFunctions.EncryptQueryString(pRow["MenuID"].ToString()) + "'>" + pRow["Name"].ToString() + "</a>");
                                break;

                            case "Voyage Edit":
                                if (pRow["Navigation"].ToString().IndexOf('?') >= 0)
                                    StringMenu.Append("<a href='" + Page.ResolveClientUrl(pRow["Navigation"].ToString() + "&p=" + VPR.Utilities.GeneralFunctions.EncryptQueryString("import")) + "&mid=" + VPR.Utilities.GeneralFunctions.EncryptQueryString(pRow["MenuID"].ToString()) + "'>" + pRow["Name"].ToString() + "</a>");
                                else
                                    StringMenu.Append("<a href='" + Page.ResolveClientUrl(pRow["Navigation"].ToString() + "?p=" + VPR.Utilities.GeneralFunctions.EncryptQueryString("import")) + "&mid=" + VPR.Utilities.GeneralFunctions.EncryptQueryString(pRow["MenuID"].ToString()) + "'>" + pRow["Name"].ToString() + "</a>");
                                break;

                            default:
                                if (pRow["Navigation"].ToString().IndexOf('?') >= 0)
                                    StringMenu.Append("<a href='" + Page.ResolveClientUrl(pRow["Navigation"].ToString()) + "&mid=" + VPR.Utilities.GeneralFunctions.EncryptQueryString(pRow["MenuID"].ToString()) + "'>" + pRow["Name"].ToString() + "</a>");
                                else
                                    StringMenu.Append("<a href='" + Page.ResolveClientUrl(pRow["Navigation"].ToString()) + "?mid=" + VPR.Utilities.GeneralFunctions.EncryptQueryString(pRow["MenuID"].ToString()) + "'>" + pRow["Name"].ToString() + "</a>");
                                break;
                        }
                        StringMenu.Append("</li>");
                    }
                }
                else
                {
                    //StringMenu.Append("<li>");
                    //StringMenu.Append("<a href='" + Page.ResolveClientUrl(pRow["Navigation"].ToString()) + "'>" + pRow["Name"].ToString() + "</a>");

                    if (FLCRows.Length > 0)
                    {
                        StringMenu.Append("<li>");
                        StringMenu.Append("<a href='" + Page.ResolveClientUrl(pRow["Navigation"].ToString()) + "'>" + pRow["Name"].ToString() + "</a>");

                        StringMenu.Append("<ul>");
                        GenerateList(MenuTable, StringMenu, FLCRows);
                        StringMenu.Append("</ul>");

                        StringMenu.Append("</li>");
                    }
                    //StringMenu.Append("</li>");
                }
            }

            return StringMenu.ToString();
        }

    }
}