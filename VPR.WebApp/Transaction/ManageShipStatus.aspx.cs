using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPR.Entity;
using VPR.BLL;
using System.Data;
using VPR.Utilities;
using VPR.Common;

namespace VPR.WebApp.Transaction
{
    public partial class ManageShipStatus : System.Web.UI.Page
    {
        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        //private int _userLocation = 0;
        private bool _LocationSpecific = true;
        private int _userPort = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            _userId = UserBLL.GetLoggedInUserId();
            _LocationSpecific = UserBLL.GetUserLocationSpecific();
            _userPort = UserBLL.GetUserPort();

            if (_LocationSpecific == true)
            {
                _userPort = UserBLL.GetUserPort();
                //_userLocation = UserBLL.GetUserLoc(_userId);
            }
            else
            {
                _userPort = 0;
                //_userLocation = 0;
            }
            //_userLocation = UserBLL.GetUserLoc(_userId);
            //CheckUserAccess();

            if (!IsPostBack)
            {
                LoadGrid();
            }
        }

        private void LoadGrid()
        {
            List<VesselStatus> oLoadingList = new TransactionBLL().GetListVesselPosition("L", _userPort);
            gvwLoading.DataSource = oLoadingList;
            gvwLoading.DataBind();

            List<VesselStatus> oDischargingList = new TransactionBLL().GetListVesselPosition("D", _userPort);
            gvwDischarging.DataSource = oDischargingList;
            gvwDischarging.DataBind();

            List<VesselStatus> oAwaitingList = new TransactionBLL().GetListVesselPosition("A", _userPort);
            gvwAwaiting.DataSource = oAwaitingList;
            gvwAwaiting.DataBind();

            List<VesselStatus> oExpectingList = new TransactionBLL().GetListVesselPosition("E", _userPort);
            gvwExpecting.DataSource = oExpectingList;
            gvwExpecting.DataBind();
        }
        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();
            _LocationSpecific = UserBLL.GetUserLocationSpecific();

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

                //if (user.UserRole.Id != (int)UserRole.Admin)
                //{

                //    //ddlLocation.Enabled = false;
                //}
                //else
                //{
                //    _userLocation = 0;
                //    //ddlLocation.Enabled = true;
                //}

                if (!_canEdit)
                {
                    btnAwaPromote.Visible = false;
                    btnAwaRevert.Visible = false;
                    btnDisPromote.Visible = false;
                    btnDisRevert.Visible = false;
                    btnExpPromote.Visible = false;
                    btnLoaPromote.Visible = false;
                    btnLoaRevert.Visible = false;
                    btnSaveDisETC.Visible = false;
                    btnSaveETA.Visible = false;
                    btnSaveLoadETC.Visible = false;
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void gvwLoading_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                VesselStatus o = e.Row.DataItem as VesselStatus;
                DropDownList ddlBerth = (DropDownList)e.Row.FindControl("ddlBerth");

                DataTable dtCargo = new TransactionBLL().GetBerths(o.VesselId);
                DataRow dr = dtCargo.NewRow();
                dr["pk_BerthID"] = "0";
                dr["BerthName"] = "--None--";
                dtCargo.Rows.InsertAt(dr, 0);
                ddlBerth.DataTextField = "BerthName";
                ddlBerth.DataValueField = "pk_BerthID";
                ddlBerth.DataSource = dtCargo;
                ddlBerth.DataBind();

                ddlBerth.SelectedValue = o.BerthId.ToString();
    
            }
        }
        protected void gvwDischarging_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                VesselStatus o = e.Row.DataItem as VesselStatus;
                DropDownList ddlBerth = (DropDownList)e.Row.FindControl("ddlBerth");

                DataTable dtCargo = new TransactionBLL().GetBerths(o.VesselId);
                DataRow dr = dtCargo.NewRow();
                dr["pk_BerthID"] = "0";
                dr["BerthName"] = "--None--";
                dtCargo.Rows.InsertAt(dr, 0);
                ddlBerth.DataTextField = "BerthName";
                ddlBerth.DataValueField = "pk_BerthID";
                ddlBerth.DataSource = dtCargo;
                ddlBerth.DataBind();

                ddlBerth.SelectedValue = o.BerthId.ToString();

            }
        }
        protected void gvwAwaiting_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                VesselStatus o = e.Row.DataItem as VesselStatus;
                DropDownList ddlBerth = (DropDownList)e.Row.FindControl("ddlBerth");

                DataTable dtCargo = new TransactionBLL().GetBerths(o.VesselId);
                DataRow dr = dtCargo.NewRow();
                dr["pk_BerthID"] = "0";
                dr["BerthName"] = "--None--";
                dtCargo.Rows.InsertAt(dr, 0);
                ddlBerth.DataTextField = "BerthName";
                ddlBerth.DataValueField = "pk_BerthID";
                ddlBerth.DataSource = dtCargo;
                ddlBerth.DataBind();

                ddlBerth.SelectedValue = o.BerthId.ToString();
            }
        }
        protected void gvwExpecting_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                VesselStatus o = e.Row.DataItem as VesselStatus;
                DropDownList ddlBerth = (DropDownList)e.Row.FindControl("ddlBerth");

                DataTable dtCargo = new TransactionBLL().GetBerths(o.VesselId);
                DataRow dr = dtCargo.NewRow();
                dr["pk_BerthID"] = "0";
                dr["BerthName"] = "--None--";
                dtCargo.Rows.InsertAt(dr, 0);
                ddlBerth.DataTextField = "BerthName";
                ddlBerth.DataValueField = "pk_BerthID";
                ddlBerth.DataSource = dtCargo;
                ddlBerth.DataBind();

                ddlBerth.SelectedValue = o.BerthId.ToString();
            }
        }

        protected void chkLoading_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((CheckBox)sender).NamingContainer;
            DropDownList ddlBerth = (DropDownList)row.FindControl("ddlBerth");
            TextBox txtLoadingDate = (TextBox)row.FindControl("txtETC");

            if (((CheckBox)sender).Checked)
            {
                txtLoadingDate.Text = "";
                txtLoadingDate.Enabled = true;
                ddlBerth.Enabled = true;
                //txtLoadingDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
            else
            {
                txtLoadingDate.Enabled = false;
                txtLoadingDate.Text = string.Empty;
                ddlBerth.Enabled = false;
            }
        }

        protected void chkDischarging_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((CheckBox)sender).NamingContainer;
            DropDownList ddlBerth = (DropDownList)row.FindControl("ddlBerth");

            //TextBox txtArrivalDate = (TextBox)row.FindControl("txtArrivalDate");
            //TextBox txtBerthDate = (TextBox)row.FindControl("txtBerthDate");
            TextBox txtDischargeDate = (TextBox)row.FindControl("txtETC");
            //TextBox txtLoadingDate = (TextBox)row.FindControl("txtLoadingDate");

            if (((CheckBox)sender).Checked)
            {
                txtDischargeDate.Enabled = true;
                ddlBerth.Enabled = true;
                txtDischargeDate.Text = "";
                //txtDischargeDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
            else
            {
                txtDischargeDate.Enabled = false;
                txtDischargeDate.Text = string.Empty;
                ddlBerth.Enabled = false;
            }
        }

        protected void chkAwaiting_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((CheckBox)sender).NamingContainer;

            //TextBox txtArrivalDate = (TextBox)row.FindControl("txtArrivalDate");
            TextBox txtBerthDate = (TextBox)row.FindControl("txtBerthDate");
            //TextBox txtDischargeDate = (TextBox)row.FindControl("txtDischargeDate");
            //TextBox txtLoadingDate = (TextBox)row.FindControl("txtLoadingDate");
            DropDownList ddlBerth = (DropDownList)row.FindControl("ddlBerth");

            if (((CheckBox)sender).Checked)
            {
                ddlBerth.Enabled = true;
                //txtBerthDate.Enabled = true;
                //txtBerthDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
            else
            {
                ddlBerth.Enabled = false;
                txtBerthDate.Enabled = false;
                txtBerthDate.Text = string.Empty;
            }
        }

        protected void chkExpecting_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((CheckBox)sender).NamingContainer;

            TextBox txtArrivalDate = (TextBox)row.FindControl("txtArrivalDate");
            TextBox txtETA = (TextBox)row.FindControl("txtETA");
            TextBox txtBerthDate = (TextBox)row.FindControl("txtBerthDate");
            DropDownList ddlBerth = (DropDownList)row.FindControl("ddlBerth");

            if (((CheckBox)sender).Checked)
            {
                txtArrivalDate.Enabled = true;
                //txtArrivalDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                txtETA.Enabled = true;
                //txtBerthDate.Enabled = true;
                //txtBerthDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                ddlBerth.Enabled = true;
            }
            else
            {
                txtArrivalDate.Enabled = false;
                txtArrivalDate.Text = string.Empty;
                txtETA.Enabled = false;
                txtBerthDate.Enabled = false;
                txtBerthDate.Text = string.Empty;
                ddlBerth.Enabled = false;
            }
        }

        protected void ddlBerth_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((DropDownList)sender).NamingContainer;

            TextBox txtBerthDate = (TextBox)row.FindControl("txtBerthDate");
            DropDownList ddlBerth = (DropDownList)row.FindControl("ddlBerth");

            if (ddlBerth.SelectedIndex > 0)
            {
                txtBerthDate.Enabled = true;
                //txtBerthDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
            else
            {
                txtBerthDate.Enabled = false;
                txtBerthDate.Text = string.Empty;
            }

        }
        //Expecting
        protected void btnExpPromote_Click(object sender, EventArgs e)
        {
            DateTime? dte = null;
            List<VesselStatus> lstPromote = new List<VesselStatus>();
            int totalRows = gvwExpecting.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwExpecting.Rows[r];

                CheckBox chkExpecting = (CheckBox)thisGridViewRow.FindControl("chkExpecting");

                if (chkExpecting.Checked)
                {
                    HiddenField hdnVesselId = (HiddenField)thisGridViewRow.FindControl("hdnVesselId");
                    DropDownList ddlBerth = (DropDownList)thisGridViewRow.FindControl("ddlBerth");
                    TextBox txtArrivalDate = (TextBox)thisGridViewRow.FindControl("txtArrivalDate");
                    TextBox txtBerthDate = (TextBox)thisGridViewRow.FindControl("txtBerthDate");

                    lstPromote.Add(new VesselStatus
                    {
                        CreatedBy = _userId,
                        ModifiedBy = _userId,
                        BerthId = Convert.ToInt32(ddlBerth.SelectedValue),
                        VesselId = Convert.ToInt32(hdnVesselId.Value),
                        ArrivalDate = Convert.ToDateTime(txtArrivalDate.Text),
                        BerthDate = (txtBerthDate.Text != string.Empty) ? Convert.ToDateTime(txtBerthDate.Text) : dte,
                        Activity = "E"
                    });

                }
            }

            if (lstPromote.Count > 0)
            {
                //Save
                new TransactionBLL().PromoteVessels(lstPromote);
                //Reload Tabs
                LoadGrid();

                lblErr.Text = "Vessle(s) promoted successfully!";
            }
            else
            {
                lblErr.Text = "Nothing selected!";
            }
        }
        protected void btnSaveETA_Click(object sender, EventArgs e)
        {
            int totalRows = gvwExpecting.Rows.Count;
            bool IsSelected = false;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwExpecting.Rows[r];

                CheckBox chkExpecting = (CheckBox)thisGridViewRow.FindControl("chkExpecting");

                if (chkExpecting.Checked)
                {
                    HiddenField hdnVesselId = (HiddenField)thisGridViewRow.FindControl("hdnVesselId");
                    TextBox txtETA = (TextBox)thisGridViewRow.FindControl("txtETA");

                    new TransactionBLL().SaveETCorWTA(Convert.ToInt32(hdnVesselId.Value), Convert.ToDateTime(txtETA.Text.Trim()), true);
                    IsSelected = true;
                }
            }

            LoadGrid();

            if (IsSelected)
                lblErr.Text = "ETA(s) saved successfully!";
            else
                lblErr.Text = "Nothing selected!";
        }

        //Awaiting
        protected void btnAwaPromote_Click(object sender, EventArgs e)
        {
            DateTime? dte = null;
            List<VesselStatus> lstPromote = new List<VesselStatus>();
            int totalRows = gvwAwaiting.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwAwaiting.Rows[r];

                CheckBox chkAwaiting = (CheckBox)thisGridViewRow.FindControl("chkAwaiting");

                if (chkAwaiting.Checked)
                {
                    HiddenField hdnVesselId = (HiddenField)thisGridViewRow.FindControl("hdnVesselId");
                    DropDownList ddlBerth = (DropDownList)thisGridViewRow.FindControl("ddlBerth");
                    TextBox txtArrivalDate = (TextBox)thisGridViewRow.FindControl("txtArrivalDate");
                    TextBox txtBerthDate = (TextBox)thisGridViewRow.FindControl("txtBerthDate");

                    lstPromote.Add(new VesselStatus
                    {
                        CreatedBy = _userId,
                        ModifiedBy = _userId,
                        BerthId = Convert.ToInt32(ddlBerth.SelectedValue),
                        VesselId = Convert.ToInt32(hdnVesselId.Value),
                        ArrivalDate = Convert.ToDateTime(txtArrivalDate.Text),
                        BerthDate = Convert.ToDateTime(txtBerthDate.Text),
                        //BerthDate = (txtArrivalDate.Text != string.Empty) ? Convert.ToDateTime(txtArrivalDate.Text) : dte,
                        Activity = "A"
                    });

                }
            }

            if (lstPromote.Count > 0)
            {
                //Save
                new TransactionBLL().PromoteVessels(lstPromote);
                //Reload Tabs
                LoadGrid();

                lblErr.Text = "Vessle(s) promoted successfully!";
            }
            else
            {
                lblErr.Text = "Nothing selected!";
            }
        }
        protected void btnAwaRevert_Click(object sender, EventArgs e)
        {
            List<int> lstRevert = new List<int>();
            int totalRows = gvwAwaiting.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwAwaiting.Rows[r];

                CheckBox chkAwaiting = (CheckBox)thisGridViewRow.FindControl("chkAwaiting");

                if (chkAwaiting.Checked)
                {
                    HiddenField hdnStatusId = (HiddenField)thisGridViewRow.FindControl("hdnStatusId");

                    lstRevert.Add(Convert.ToInt32(hdnStatusId.Value));
                }
            }

            if (lstRevert.Count > 0)
            {
                //Save
                new TransactionBLL().RevertVessels(lstRevert);
                //Reload Tabs
                LoadGrid();

                lblErr.Text = "Vessle(s) promoted successfully!";
            }
            else
            {
                lblErr.Text = "Nothing selected!";
            }
        }

        //Discharging
        protected void btnDisPromote_Click(object sender, EventArgs e)
        {
            DateTime? dte = null;
            List<VesselStatus> lstPromote = new List<VesselStatus>();
            int totalRows = gvwDischarging.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwDischarging.Rows[r];

                CheckBox chkDischarging = (CheckBox)thisGridViewRow.FindControl("chkDischarging");

                if (chkDischarging.Checked)
                {
                    HiddenField hdnVesselId = (HiddenField)thisGridViewRow.FindControl("hdnVesselId");
                    DropDownList ddlBerth = (DropDownList)thisGridViewRow.FindControl("ddlBerth");
                    TextBox txtArrivalDate = (TextBox)thisGridViewRow.FindControl("txtArrivalDate");
                    TextBox txtBerthDate = (TextBox)thisGridViewRow.FindControl("txtBerthDate");
                    TextBox txtETC = (TextBox)thisGridViewRow.FindControl("txtETC");
                    HiddenField hdnActivity = (HiddenField)thisGridViewRow.FindControl("hdnActivity");
                    if (string.IsNullOrEmpty(txtETC.Text) == false)
                    {
                        if (txtETC.Text.ToDateTime() <= DateTime.Now && txtETC.Text.ToDateTime() >= txtBerthDate.Text.ToDateTime())
                        {
                            lstPromote.Add(new VesselStatus
                            {
                                CreatedBy = _userId,
                                ModifiedBy = _userId,
                                BerthId = Convert.ToInt32(ddlBerth.SelectedValue),
                                VesselId = Convert.ToInt32(hdnVesselId.Value),
                                ArrivalDate = Convert.ToDateTime(txtArrivalDate.Text),
                                BerthDate = (txtArrivalDate.Text != string.Empty) ? Convert.ToDateTime(txtArrivalDate.Text) : dte,
                                ETC = Convert.ToDateTime(txtETC.Text),
                                VActivity = hdnActivity.Value,
                                Activity = "D"
                            });
                        }
                    }

                }
            }

            if (lstPromote.Count > 0)
            {
                //Save
                new TransactionBLL().PromoteVessels(lstPromote);
                //Reload Tabs
                LoadGrid();

                lblErr.Text = "Vessle(s) promoted successfully!";
            }
            else
            {
                lblErr.Text = "Nothing selected!";
            }
        }
        protected void btnDisRevert_Click(object sender, EventArgs e)
        {
            List<int> lstRevert = new List<int>();
            int totalRows = gvwDischarging.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwDischarging.Rows[r];

                CheckBox chkDischarging = (CheckBox)thisGridViewRow.FindControl("chkDischarging");

                if (chkDischarging.Checked)
                {
                    HiddenField hdnStatusId = (HiddenField)thisGridViewRow.FindControl("hdnStatusId");

                    lstRevert.Add(Convert.ToInt32(hdnStatusId.Value));
                }
            }

            if (lstRevert.Count > 0)
            {
                //Save
                new TransactionBLL().RevertVessels(lstRevert);
                //Reload Tabs
                LoadGrid();

                lblErr.Text = "Vessle(s) promoted successfully!";
            }
            else
            {
                lblErr.Text = "Nothing selected!";
            }
        }
        protected void btnSaveDisETC_Click(object sender, EventArgs e)
        {
            int totalRows = gvwDischarging.Rows.Count;
            bool IsSelected = false;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwDischarging.Rows[r];

                CheckBox chkDischarging = (CheckBox)thisGridViewRow.FindControl("chkDischarging");

                if (chkDischarging.Checked)
                {
                    HiddenField hdnVesselId = (HiddenField)thisGridViewRow.FindControl("hdnVesselId");
                    TextBox txtETC = (TextBox)thisGridViewRow.FindControl("txtETC");

                    new TransactionBLL().SaveETCorWTA(Convert.ToInt32(hdnVesselId.Value), Convert.ToDateTime(txtETC.Text.Trim()), false);
                    IsSelected = true;
                }
            }

            LoadGrid();

            if (IsSelected)
                lblErr.Text = "ETC(s) saved successfully!";
            else
                lblErr.Text = "Nothing selected!";
        }

        //Loading
        protected void btnLoaPromote_Click(object sender, EventArgs e)
        {
            DateTime? dte = null;
            List<VesselStatus> lstPromote = new List<VesselStatus>();
            int totalRows = gvwLoading.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwLoading.Rows[r];

                CheckBox chkLoading = (CheckBox)thisGridViewRow.FindControl("chkLoading");

                if (chkLoading.Checked)
                {
                    HiddenField hdnVesselId = (HiddenField)thisGridViewRow.FindControl("hdnVesselId");
                    DropDownList ddlBerth = (DropDownList)thisGridViewRow.FindControl("ddlBerth");
                    TextBox txtArrivalDate = (TextBox)thisGridViewRow.FindControl("txtArrivalDate");
                    TextBox txtBerthDate = (TextBox)thisGridViewRow.FindControl("txtBerthDate");
                    TextBox txtETC = (TextBox)thisGridViewRow.FindControl("txtETC");
                    HiddenField hdnActivity = (HiddenField)thisGridViewRow.FindControl("hdnActivity");
                    if (string.IsNullOrEmpty(txtETC.Text) == false)
                    {
                        lstPromote.Add(new VesselStatus
                        {
                            CreatedBy = _userId,
                            ModifiedBy = _userId,
                            BerthId = Convert.ToInt32(ddlBerth.SelectedValue),
                            VesselId = Convert.ToInt32(hdnVesselId.Value),
                            ArrivalDate = Convert.ToDateTime(txtArrivalDate.Text),
                            BerthDate = (txtArrivalDate.Text != string.Empty) ? Convert.ToDateTime(txtArrivalDate.Text) : dte,
                            ETC = (txtETC.Text != string.Empty) ? Convert.ToDateTime(txtETC.Text) : DateTime.Now,
                            //ETC = Convert.ToDateTime(txtETC.Text),
                            VActivity = hdnActivity.Value,
                            Activity = "L"
                        });
                    }
                }
            }

            if (lstPromote.Count > 0)
            {
                //Save
                new TransactionBLL().PromoteVessels(lstPromote);
                //Reload Tabs
                LoadGrid();

                lblErr.Text = "Vessle(s) promoted successfully!";
            }
            else
            {
                lblErr.Text = "Nothing selected!";
            }
        }
        protected void btnLoaRevert_Click(object sender, EventArgs e)
        {
            List<int> lstRevert = new List<int>();
            int totalRows = gvwLoading.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwLoading.Rows[r];

                CheckBox chkLoading = (CheckBox)thisGridViewRow.FindControl("chkLoading");

                if (chkLoading.Checked)
                {
                    HiddenField hdnStatusId = (HiddenField)thisGridViewRow.FindControl("hdnStatusId");

                    lstRevert.Add(Convert.ToInt32(hdnStatusId.Value));
                }
            }

            if (lstRevert.Count > 0)
            {
                //Save
                new TransactionBLL().RevertVessels(lstRevert);
                //Reload Tabs
                LoadGrid();

                lblErr.Text = "Vessle(s) promoted successfully!";
            }
            else
            {
                lblErr.Text = "Nothing selected!";
            }
        }
        protected void btnSaveLoadETC_Click(object sender, EventArgs e)
        {
            int totalRows = gvwLoading.Rows.Count;
            bool IsSelected = false;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwLoading.Rows[r];

                CheckBox chkLoading = (CheckBox)thisGridViewRow.FindControl("chkLoading");

                if (chkLoading.Checked)
                {
                    HiddenField hdnVesselId = (HiddenField)thisGridViewRow.FindControl("hdnVesselId");
                    TextBox txtETC = (TextBox)thisGridViewRow.FindControl("txtETC");

                    new TransactionBLL().SaveETCorWTA(Convert.ToInt32(hdnVesselId.Value), Convert.ToDateTime(txtETC.Text.Trim()), false);
                }
            }

            LoadGrid();

            if (IsSelected)
                lblErr.Text = "ETC(s) saved successfully!";
            else
                lblErr.Text = "Nothing selected!";
        }
    }
}