﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPR.Utilities;
using VPR.Common;
using VPR.BLL;
using System.Configuration;
using VPR.Entity;
using VPR.Utilities.ResourceManager;

namespace VPR.WebApp.Transaction
{
    public partial class ManageVessel : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _roleId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private bool _LocationSpecific = true;
        //private int _userLocation = 0;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            _userId = UserBLL.GetLoggedInUserId();
           
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                RetrieveSearchCriteria();
                LoadEmailGroup();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SaveNewPageIndex(0);
            LoadEmailGroup();
            upBL.Update();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtAgent.Text = "";
            txtPort.Text = "";
            txtVesselName.Text = "";
            ddlVesselStatus.SelectedValue = "S";

            SaveNewPageIndex(0);
            LoadEmailGroup();
            upBL.Update();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RedirecToAddEditPage(-1);
        }

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newPageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            SaveNewPageSize(newPageSize);
            LoadEmailGroup();
            upBL.Update();
        }

        protected void gvImportBL_RowCommand(object sender, GridViewCommandEventArgs e)
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

                LoadEmailGroup();
            }
            else if (e.CommandName == "Edit")
            {
                RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                DeleteEmailGroup(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvImportBL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 3);
                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "VesselName"));
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Activity"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PortName"));
                //e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BerthName"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LOA"));
                e.Row.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ArrivalDate")).Split(' ')[0];
                e.Row.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "AgentName"));
                //e.Row.Cells[7].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ETC"));

                //Edit Link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00070");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "VesselId"));

                //Delete link
                if (_canDelete == true)
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = true;
                    btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00080");
                    btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "VesselId"));
                    btnRemove.Attributes.Add("onclick", "javascript:return confirm('Are you sure about delete?');");
                }
                else
                {
                    ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = false;
                }
            }
        }

        protected void gvImportBL_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newIndex = e.NewPageIndex;
            gvImportBL.PageIndex = e.NewPageIndex;
            SaveNewPageIndex(e.NewPageIndex);
            LoadEmailGroup();
        }


        #region Private Methods

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

                    if (_canView == false)
                    {
                        Response.Redirect("~/Unauthorized.aspx");
                    }

                    if (_canAdd == false)
                    {
                        btnAdd.Visible = false;
                    }
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();
            _LocationSpecific = UserBLL.GetUserLocationSpecific();


            IUser user = new UserBLL().GetUser(_userId);

            if (!ReferenceEquals(user, null))
            {
                if (!ReferenceEquals(user.UserRole, null))
                {
                    _roleId = user.UserRole.Id;
                    UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
                }
            }
        }

        private void SetAttributes()
        {
            if (!IsPostBack)
            {
                gvImportBL.PagerSettings.PageButtonCount = Convert.ToInt32(ConfigurationManager.AppSettings["PageButtonCount"]);
            }
        }

        private void LoadEmailGroup()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
                {
                    SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                    if (!ReferenceEquals(searchCriteria, null))
                    {
                        BuildSearchCriteria(searchCriteria, user.PortID);

                        gvImportBL.PageIndex = searchCriteria.PageIndex;

                        if (searchCriteria.PageSize > 0) gvImportBL.PageSize = searchCriteria.PageSize;

                        gvImportBL.DataSource = new TransactionBLL().GetVessles(searchCriteria);

                        gvImportBL.DataBind();
                    }
                }
            }
        }

        private void DeleteEmailGroup(int vesselId)
        {
            new TransactionBLL().DeleteVessel(vesselId);

            LoadEmailGroup();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('Email has been deleted successfully!');</script>", false);
        }

        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());
            Response.Redirect("~/Transaction/AddEditVessel.aspx?VesselId=" + encryptedId);
        }

        private void BuildSearchCriteria(SearchCriteria criteria, int PortID)
        {
            string sortExpression = string.Empty;
            string sortDirection = string.Empty;

            int roleId = UserBLL.GetLoggedInUserRoleId();

            if (!ReferenceEquals(ViewState[Constants.SORT_EXPRESSION], null) && !ReferenceEquals(ViewState[Constants.SORT_DIRECTION], null))
            {
                sortExpression = Convert.ToString(ViewState[Constants.SORT_EXPRESSION]);
                sortDirection = Convert.ToString(ViewState[Constants.SORT_DIRECTION]);
            }

            if (_LocationSpecific == true)
            {
                criteria.portID = PortID;
                //criteria.LocationID = UserBLL.GetUserLoc(_userId);
            }
            else
            {
                criteria.portID = 0;
                //criteria.LocationID = 0;
            }
            criteria.UserId = _userId;
            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;

            criteria.VesselName = (txtVesselName.Text == "") ? string.Empty : txtVesselName.Text.Trim();
            criteria.Port = (txtPort.Text == "") ? string.Empty : txtPort.Text.Trim();
            criteria.AgentName = (txtAgent.Text == "") ? string.Empty : txtAgent.Text.Trim();
            criteria.VesselStatus = ddlVesselStatus.SelectedValue;

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
                    if (criteria.CurrentPage != PageName.EmailGroup)
                    {
                        criteria.Clear();
                        SetDefaultSearchCriteria(criteria);
                    }
                    else
                    {
                        txtVesselName.Text = criteria.VesselName;
                        txtPort.Text = criteria.Port;
                        txtAgent.Text = criteria.AgentName;
                        ddlVesselStatus.SelectedValue = criteria.VesselStatus;

                        gvImportBL.PageIndex = criteria.PageIndex;
                        gvImportBL.PageSize = criteria.PageSize;
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
            string sortExpression = "ArrivalDate";
            string sortDirection = "DESC";

            criteria.CurrentPage = PageName.EmailGroup;
            criteria.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;

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

        #endregion
    }
}