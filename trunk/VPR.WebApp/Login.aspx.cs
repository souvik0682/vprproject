﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPR.BLL;
using VPR.Common;
using VPR.Entity;
using VPR.Utilities;
using VPR.Utilities.Cryptography;
using VPR.Utilities.ResourceManager;

namespace VPR.WebApp
{
    public partial class Login : System.Web.UI.Page
    {
        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetAttributes();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (ValidateControl())
            {
                IUser user = new UserEntity();
                user.Name = txtUserName.Text.Trim();
                user.Password = Encryption.Encrypt(txtPassword.Text.Trim());

                try
                {
                    bool valid = new UserBLL().ValidateUser(user);

                    if (valid)
                    {
                        Session[Constants.SESSION_USER_INFO] = user;
                        //Session[Constants.SESSION_ROLE_LOCATIONSPECIFIC] = user;
                        Response.Redirect("~/View/Home.aspx");
                    }
                    else
                    {
                        lblMsg.Text = ResourceManager.GetStringWithoutName("ERR00003");
                        lblMsg.Visible = true;
                    }

                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    CommonBLL.HandleException(ex, this.Server.MapPath(this.Request.ApplicationPath).Replace("/", "\\"));

                    switch (ex.Number)
                    {
                        case -1:
                            lblMsg.Text = ResourceManager.GetStringWithoutName("ERR00004");
                            break;
                        case 4060:
                            lblMsg.Text = ResourceManager.GetStringWithoutName("ERR00005");
                            break;
                        case 18456:
                            lblMsg.Text = ResourceManager.GetStringWithoutName("ERR00006");
                            break;
                        default:
                            lblMsg.Text = ResourceManager.GetStringWithoutName("ERR00007");
                            break;
                    }

                    lblMsg.Visible = true;
                }
                catch (Exception ex)
                {
                    CommonBLL.HandleException(ex, this.Server.MapPath(this.Request.ApplicationPath).Replace("/", "\\"));
                    lblMsg.Text = ResourceManager.GetStringWithoutName("ERR00007");
                    lblMsg.Visible = true;
                }
            }
        }

        #endregion

        #region Private Methods

        private void SetAttributes()
        {
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            txtUserName.Focus();
            lblMsg.Visible = false;
            lblMsgUsername.Visible = false;
            lblMsgPassword.Visible = false;
            txtPassword.Attributes.Add("autocomplete", "off");
            rfvName.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00001");
            rfvPwd.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00002");
        }

        private bool ValidateControl()
        {
            bool isValid = true;

            if (txtUserName.Text.Trim() == string.Empty)
            {
                isValid = false;
                lblMsgUsername.Visible = true;
                lblMsgUsername.Text = ResourceManager.GetStringWithoutName("ERR00001");
            }

            if (txtPassword.Text.Trim() == string.Empty)
            {
                isValid = false;
                lblMsgPassword.Visible = true;
                lblMsgPassword.Text = ResourceManager.GetStringWithoutName("ERR00002");
            }

            return isValid;
        }

        #endregion
    }
}