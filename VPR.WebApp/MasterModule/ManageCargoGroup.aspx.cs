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
using VPR.Utilities.ResourceManager;

namespace VPR.WebApp.MasterModule
{
    public partial class ManageCargoGroup : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private bool _hasEditAccess = true;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;

        #endregion

        #region Protected Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {
            SetAttributes();
            RetriveParameters();
            CheckUserAccess();

            if (!IsPostBack)
            {
                RetrieveSearchCriteria();
                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];
                LoadData(searchCriteria.SortExpression, searchCriteria.SortDirection);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RedirecToAddEditPage(-1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SaveNewPageIndex(0);
            SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];
            LoadData(searchCriteria.SortExpression, searchCriteria.SortDirection);
            upLoc.Update();
        }

        protected void gvwLoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newIndex = e.NewPageIndex;
            gvwLoc.PageIndex = e.NewPageIndex;
            SaveNewPageIndex(e.NewPageIndex);
            SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];
            LoadData(searchCriteria.SortExpression, searchCriteria.SortDirection);
        }
        protected void gvwLoc_RowCommand(object sender, GridViewCommandEventArgs e)
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

                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];
                LoadData(searchCriteria.SortExpression, searchCriteria.SortDirection);
            }
            else if (e.CommandName == "Edit")
            {
                RedirecToAddEditPage(Convert.ToInt32(e.CommandArgument));
            }
            else if (e.CommandName == "Remove")
            {
                DeleteCargoGroup(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void gvwLoc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GeneralFunctions.ApplyGridViewAlternateItemStyle(e.Row, 3);

                ScriptManager sManager = ScriptManager.GetCurrent(this);

                e.Row.Cells[0].Text = ((gvwLoc.PageSize * gvwLoc.PageIndex) + e.Row.RowIndex + 1).ToString();
                e.Row.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_CargoGroupID"));
                e.Row.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "GroupType"));
                e.Row.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "CargoGroupName"));

                // Edit link
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.ToolTip = ResourceManager.GetStringWithoutName("ERR00013");
                btnEdit.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_CargoGroupID"));

                //Delete link
                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00012");
                btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pk_CargoGroupID"));

                if (_canDelete == true)
                {
                    //ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = true;
                    btnRemove.ToolTip = ResourceManager.GetStringWithoutName("ERR00007");
                    //btnRemove.CommandArgument = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BLID"));

                }
                else
                {
                    //ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                    btnRemove.Visible = false;
                }

                if (_hasEditAccess)
                {
                    btnRemove.OnClientClick = "javascript:return confirm('" + ResourceManager.GetStringWithoutName("ERR00014") + "');";
                }
                else
                {
                    btnEdit.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00008") + "');return false;";
                    btnRemove.OnClientClick = "javascript:alert('" + ResourceManager.GetStringWithoutName("ERR00008") + "');return false;";
                }
            }
        }

        protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newPageSize = Convert.ToInt32(ddlPaging.SelectedValue);
            SaveNewPageSize(newPageSize);
            SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];
            LoadData(searchCriteria.SortExpression, searchCriteria.SortDirection);
            upLoc.Update();
        }
        #endregion
        protected void gvwLoc_Sorting(object sender, GridViewSortEventArgs e)
        {
            SearchCriteria criteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];
            if (criteria.SortDirection == "ASC")
            {
                criteria.SortDirection = "DESC";
                LoadData(criteria.SortExpression, criteria.SortDirection);
            }
            else
            {
                criteria.SortDirection = "ASC";
                LoadData(criteria.SortExpression, criteria.SortDirection);
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            txtPrdGroup.Text = "";
            txtCargoType.Text = "";
            btnSearch_Click(sender, e);
        }

         #region Private Methods

        private void SetAttributes()
        {
            // txtPortCode.Attributes.Add("OnFocus", "javascript:js_waterMark_Focus('" + txtPortCode.ClientID + "', 'Type CountryId')");
            // txtPortCode.Attributes.Add("OnBlur", "javascript:js_waterMark_Blur('" + txtPortCode.ClientID + "', 'Type CountryId')");
            // //txtPortCode.Text = "Type CountryId";

            // txtPortName.Attributes.Add("OnFocus", "javascript:js_waterMark_Focus('" + txtPortName.ClientID + "', 'Type Country Name')");
            // txtPortName.Attributes.Add("OnBlur", "javascript:js_waterMark_Blur('" + txtPortName.ClientID + "', 'Type Country Name')");
            //// txtPortName.Text = "Type Country Name";

            txtWMEPrdGroup.WatermarkText = "TYPE CARGO GROUP";
            gvwLoc.PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            gvwLoc.PagerSettings.PageButtonCount = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageButtonCount"]);
        }

        private void LoadData(string SortExp, string direction)
        {
            string CargoGroup = string.IsNullOrEmpty(txtPrdGroup.Text) ? "" : txtPrdGroup.Text.Trim();
            string CargoType = string.IsNullOrEmpty(txtCargoType.Text) ? "" : txtCargoType.Text.Trim();

            lblErrorMsg.Text = "";
            if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            {
                SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

                if (!ReferenceEquals(searchCriteria, null))
                {
                    BuildSearchCriteria(searchCriteria);

                    gvwLoc.PageIndex = searchCriteria.PageIndex;
                    if (searchCriteria.PageSize > 0) gvwLoc.PageSize = searchCriteria.PageSize;

                    BLL.DBInteraction dbinteract = new BLL.DBInteraction();
                   
                    try
                    {
                        System.Data.DataSet ds = dbinteract.GetCargoGroup(-1, CargoGroup);
                        System.Data.DataView dv = new System.Data.DataView(ds.Tables[0]);
                        if (!string.IsNullOrEmpty(SortExp) && !string.IsNullOrEmpty(direction) )
                            dv.Sort = SortExp + " " + direction;
                        gvwLoc.DataSource = dv;
                       
                    }
                    catch (Exception ex)
                    {

                        gvwLoc.DataSource = null;
                        lblErrorMsg.Text = "Error Occured.Please try again.";
                    }

                    gvwLoc.DataBind();
                }
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
                    //Response.Redirect("~/Unauthorized.aspx");
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

        private void DeleteCargoGroup(int portId)
        {
            DBInteraction dinteract = new DBInteraction();
            dinteract.DeleteCargoGroup(portId);
            SearchCriteria searchCriteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];
            LoadData(searchCriteria.SortExpression, searchCriteria.SortDirection);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "<script>javascript:void alert('" + ResourceManager.GetStringWithoutName("ERR00010") + "');</script>", false);
        }

        private void RedirecToAddEditPage(int id)
        {
            string encryptedId = GeneralFunctions.EncryptQueryString(id.ToString());

            Response.Redirect("~/MasterModule/AddEditCargoGroup.aspx?id=" + encryptedId);
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
           

            criteria.SortExpression = sortExpression;
            criteria.SortDirection = sortDirection;
            criteria.CargoGroup = (txtPrdGroup.Text == "TYPE CARGO GROUP") ? string.Empty : txtPrdGroup.Text.Trim();
            criteria.CargoGroupType = (txtCargoType.Text == "TYPE CARGO TYPE") ? string.Empty : txtCargoType.Text.Trim();
            Session[Constants.SESSION_SEARCH_CRITERIA] = criteria;
        }

        private void RetrieveSearchCriteria()
        {
            bool isCriteriaExists = false;

            //if (!ReferenceEquals(Session[Constants.SESSION_SEARCH_CRITERIA], null))
            //{
            //    SearchCriteria criteria = (SearchCriteria)Session[Constants.SESSION_SEARCH_CRITERIA];

            //    if (!ReferenceEquals(criteria, null))
            //    {
            //        if (criteria.CurrentPage != PageName.LocationMaster)
            //        {
            //            criteria.Clear();
            //            SetDefaultSearchCriteria(criteria);
            //        }
            //        else
            //        {
            //            txtPortCode.Text = criteria.LocAbbr;
            //            txtPortName.Text = criteria.LocName;
            //            gvwLoc.PageIndex = criteria.PageIndex;
            //            gvwLoc.PageSize = criteria.PageSize;
            //            ddlPaging.SelectedValue = criteria.PageSize.ToString();
            //            isCriteriaExists = true;
            //        }
            //    }
            //}

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
            //criteria.CurrentPage = PageName.LocationMaster;
            criteria.PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
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