﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPR.BLL;
using VPR.Common;
using VPR.Entity;
using VPR.Utilities;
using VPR.Utilities.ResourceManager;

namespace VPR.WebApp.View
{
    public partial class ManageUser : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;

        #endregion

        #region Protected Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                RetrieveSearchCriteria();
                LoadUser();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RedirecToAddEditPage(-1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SaveNewPageIndex(0);
            LoadUser();
            upUser.Update();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtUserName.Text = string.Empty;
            txtFName.Text = string.Empty;

            SaveNewPageIndex(0);
            LoadUser();
            upUser.Update();
        }

        protected void gvwUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newIndex = e.NewPageIndex;
            gvwUser.PageIndex = e.NewPageIndex;
            SaveNewPageIndex(e.NewPageIndex);
            LoadUser();
        }
        protected void gvwUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Sort"))
            {
                if (ViewState[Constants.SORT_EXPRESSION] == null)
                {
                    ViewState[Constants.SORT_EXPRESSION] = e.CommandArgument.ToString();
                    ViewState[Constants.SORT_DIRECTION] = "ASC";
                }
                else
                {
                    if (ViewState[Constants.SORT_EXPRESSION].ToString() == e.CommandArgument.ToString())
                    {
                        if (ViewState[Constants.SORT_DIRECTION].ToString() == "ASC")
                            ViewState[Constants.SORT_DIRECTION] = "DESC";
                        else
                            ViewState[Constants.SORT_DIRECTION] = "ASC";
                    }
                    else
                    {
                        ViewState[Constants.SORT_DIRECTION] = "ASC";
                        ViewState[Constants.SORT_EXPRESSION] = e.CommandArgument.ToString();
                    }
                }

                LoadUser();
            }
            else if (e.CommandName == "Edit")
            {
                RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                DeleteUser(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "ChangePwd")
            {
                ResetUserPassword(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvwUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 9);

                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = ((gvwUser.PageSize * gvwUser.PageIndex) + e.Row.RowIndex + 1).ToString();
                //e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Name"));

                if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsActive")))
                {
                    ((Label)e.Row.FindControl("lblName")).Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Name"));
                    ((Label)e.Row.FindControl("lblInActive")).Style["display"] = "none";
                }
                else
                {
                    ((Label)e.Row.FindControl("lblName")).Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Name"));
                    ((Label)e.Row.FindControl("lblInActive")).Style["display"] = "";
                }

                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "UserRole.Name"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FirstName"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LastName"));
                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "UserLocation.Name"));

                //Pwd Button
                LinkButton lnkPwd = (LinkButton)e.Row.FindControl("lnkPwd");
                lnkPwd.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Id"));
                lnkPwd.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00058") + "');";

                // Edit link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00013");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Id"));

                //Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00012");
                btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Id"));

                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Id")) == "1")
                {
                    btnRemove.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00059") + "');return false;";
                }
                else
                {
                    if (_canDelete)
                    {
                        btnRemove.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00014") + "');";
                    }
                    else
                    {
                        //btnEdit.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00008") + "');return false;";
                        btnRemove.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00008") + "');return false;";
                    }
                }
            }
        }

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newPageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            SaveNewPageSize(newPageSize);
            LoadUser();
            upUser.Update();
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

                if (user.UserRole.Id != (int)UserRole.Admin)
                {
                    Response.Redirect("~/Unauthorized.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!_canView)
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                txtWMEUserName.WatermarkText = ResourceManager.GetStringWithoutName("ERR00032");
                txtWMEFName.WatermarkText = ResourceManager.GetStringWithoutName("ERR00033");
                //gvwUser.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
                gvwUser.PagerSettings.PageButtonCount = Convert.ToInt32(ConfigurationManager.AppSettings["PageButtonCount"]);
            }
        }

        private void DeleteUser(int userId)
        {
            UserBLL userBll = new UserBLL();
            userBll.DeleteUser(userId, _userId);
            LoadUser();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00010") + "');</script>", false);
        }

        private void LoadUser()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(searchCriteria, null))
                {
                    BuildSearchCriteria(searchCriteria);
                    UserBLL userBll = new UserBLL();

                    gvwUser.PageIndex = searchCriteria.PageIndex;
                    if (searchCriteria.PageSize > 0) gvwUser.PageSize = searchCriteria.PageSize;

                    gvwUser.DataSource = userBll.GetAllUserList(searchCriteria);
                    gvwUser.DataBind();
                }
            }
        }

        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());
            Response.Redirect("~/View/AddEditUser.aspx?id=" + encryptedId);
        }

        private void BuildSearchCriteria(SearchCriteria criteria)
        {
            string sortExpression = string.Empty;
            string sortDirection = string.Empty;

            if (!ReferenceEquals(ViewState[Constants.SORT_EXPRESSION], null) && !ReferenceEquals(ViewState[Constants.SORT_DIRECTION], null))
            {
                sortExpression = Convert.ToString(ViewState[Constants.SORT_EXPRESSION]);
                sortDirection = Convert.ToString(ViewState[Constants.SORT_DIRECTION]);
            }
            else
            {
                sortExpression = "UserName";
                sortDirection = "ASC";
            }

            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;
            criteria.UserName = (txtUserName.Text == ResourceManager.GetStringWithoutName("ERR00032")) ? string.Empty : txtUserName.Text.Trim();
            criteria.FirstName = (txtFName.Text == ResourceManager.GetStringWithoutName("ERR00033")) ? string.Empty : txtFName.Text.Trim();
            Session[Constants.SESSION_SEARCH_CRITERIA] = criteria;
        }

        private void RetrieveSearchCriteria()
        {
            bool isCriteriaExists = false;

            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria criteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(criteria, null))
                {
                    if (criteria.CurrentPage != PageName.UserMaster)
                    {
                        criteria.Clear();
                        SetDefaultSearchCriteria(criteria);
                    }
                    else
                    {
                        txtUserName.Text = criteria.UserName;
                        txtFName.Text = criteria.FirstName;
                        gvwUser.PageIndex = criteria.PageIndex;
                        gvwUser.PageSize = criteria.PageSize;
                        ddlPaging.SelectedValue = criteria.PageSize.ToString();
                        isCriteriaExists = true;
                    }
                }
            }

            if (!isCriteriaExists)
            {
                SearchCriteria newcriteria = new SearchCriteria();
                SetDefaultSearchCriteria(newcriteria);
            }
        }

        private void SetDefaultSearchCriteria(SearchCriteria criteria)
        {
            string sortExpression = string.Empty;
            string sortDirection = string.Empty;

            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;
            criteria.CurrentPage = PageName.UserMaster;
            criteria.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            Session[Constants.SESSION_SEARCH_CRITERIA] = criteria;
        }

        private void SaveNewPageIndex(int newIndex)
        {
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria criteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(criteria, null))
                {
                    criteria.PageIndex = newIndex;
                }
            }
        }

        private void SaveNewPageSize(int newPageSize)
        {
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria criteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(criteria, null))
                {
                    criteria.PageSize = newPageSize;
                }
            }
        }

        private void ResetUserPassword(int uId)
        {
            IUser user = new UserEntity();
            user.Id = uId;
            new UserBLL().ResetPassword(user, _userId);
            SendResetPwdEmail(uId);
        }

        private void SendResetPwdEmail(int uId)
        {
            IUser user = new UserBLL().GetUser(uId);

            if (!ReferenceEquals(user, null))
            {
                string url = Convert.ToString(ConfigurationManager.AppSettings["ApplicationUrl"]) + "/Security/ChangePassword.aspx?id=" + GeneralFunctions.EncryptQueryString(uId.ToString());
                string msgBody = "Hello " + user.UserFullName + "<br/>We have received new password request for your account " + user.Name + ". Your temporary password is:" + Constants.DEFAULT_PASSWORD + " <br/>If this request was initiated by you, please click on following link and change your password:<br/><a href='" + url + "'>" + url + "</a>";

                try
                {
                    CommonBLL.SendMail(Convert.ToString(ConfigurationManager.AppSettings["Sender"]), Convert.ToString(ConfigurationManager.AppSettings["DisplayName"]), user.EmailId, string.Empty, "Request for change password", msgBody, Convert.ToString(ConfigurationManager.AppSettings["MailServerIP"]), Convert.ToString(ConfigurationManager.AppSettings["MailUserAccount"]), Convert.ToString(ConfigurationManager.AppSettings["MailUserPwd"]));
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00023") + "');</script>", false);
                }
                catch (Exception ex)
                {
                    CommonBLL.HandleException(ex, this.Server.MapPath(this.Request.ApplicationPath).Replace("/", "\\"));
                }
            }
        }

        #endregion
    }
}