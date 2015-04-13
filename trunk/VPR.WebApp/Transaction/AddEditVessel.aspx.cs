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

namespace VPR.WebApp.Transaction
{
    public partial class AddEditVessel : System.Web.UI.Page
    {
        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private int _userPort = 0;
        private bool _LocationSpecific = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            _userId = UserBLL.GetLoggedInUserId();
            _LocationSpecific = UserBLL.GetUserLocationSpecific();
            _userPort = UserBLL.GetUserPort();

            CheckUserAccess();

            if (!IsPostBack)
            {
                LoadDDLs();

                if (!ReferenceEquals(Request.QueryString["VesselId"], null))
                {
                    int VesselId = 0;
                    VesselId = GeneralFunctions.DecryptQueryString(Request.QueryString["VesselId"].ToString()).ToInt();
                    btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageVessel.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                    if (VesselId > 0)
                    {
                        ViewState["VesselId"] = VesselId;
                        LoadForEdit(VesselId);
                    }
                    else
                    {
                        ddlAcivity.SelectedIndex = -1;
                        BindGrid(0);
                        ViewState["VesselId"] = 0;
                    }
                }
                else
                {
                    ddlAcivity.SelectedIndex = -1;
                    BindGrid(0);
                    ViewState["VesselId"] = 0;
                }
            }

            txtPort.TextChanged += new EventHandler(txtPort_TextChanged);
            txtNextPort.TextChanged += new EventHandler(txtNextPort_TextChanged);
            txtPreviousPort.TextChanged += new EventHandler(txtPreviousPort_TextChanged);
        }

        protected void gvwCargo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CargoDetails cargo = e.Row.DataItem as CargoDetails;
                DropDownList ddlCargo = (DropDownList)e.Row.FindControl("ddlCargo");
                DropDownList ddlActType = (DropDownList)e.Row.FindControl("ddlActType");


                DataTable dtCargo = new TransactionBLL().GetAllCargo();
                DataRow dr = dtCargo.NewRow();
                dr["pk_cargoID"] = "0";
                dr["CargoName"] = "--Select--";
                dtCargo.Rows.InsertAt(dr, 0);
                ddlCargo.DataTextField = "CargoName";
                ddlCargo.DataValueField = "pk_cargoID";
                ddlCargo.DataSource = dtCargo;
                ddlCargo.DataBind();

                ddlCargo.SelectedValue = cargo.CargoId.ToString();
                ddlActType.SelectedValue = cargo.ActType;
                if (ddlAcivity.SelectedValue.ToString() == "B")
                    ddlActType.Enabled = true;
                else
                    ddlActType.Enabled = false;
                

                //if (ddlAcivity.SelectedValue.ToString() == "L")
                //{
                //    ddlActType.SelectedValue = "L";
                //    ddlActType.Enabled = false;
                //}
                //else if (ddlAcivity.SelectedValue.ToString() == "D")
                //{
                //    ddlActType.SelectedValue = "D";
                //    ddlActType.Enabled = false;
                //}

                //else if (ddlAcivity.SelectedValue.ToString() == "B")
                //{
                //    ddlActType.SelectedValue = "D";
                //    ddlActType.Enabled = true;
                //}

                //else if (ddlAcivity.SelectedValue.ToString() == "O")
                //{
                //    ddlActType.SelectedValue = "N";
                //    ddlActType.Enabled = true;
                //}


                ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                btnRemove.ImageUrl = "~/Images/remove.png";
                btnRemove.Attributes.Add("onclick", "javascript:return confirm('Are you sure about delete?');");
                btnRemove.CommandName = "Delete";
                btnRemove.ToolTip = "Delete";
            }
        }

        void txtPreviousPort_TextChanged(object sender, EventArgs e)
        {
            string previousPort = ((TextBox)txtPreviousPort.FindControl("txtPort")).Text;

            if (previousPort != string.Empty)
            {
                if (previousPort.Split('|').Length > 1)
                {
                    string portCode = previousPort.Split('|')[1].Trim();

                    int portId = new TransactionBLL().GetPortId(portCode);

                    ViewState["PREVIOUSPORTID"] = portId;
                }
                else
                {
                    ViewState["PREVIOUSPORTID"] = null;
                }
            }
            else
            {
                ViewState["PREVIOUSPORTID"] = null;
            }
        }

        void txtNextPort_TextChanged(object sender, EventArgs e)
        {
            string nextPort = ((TextBox)txtNextPort.FindControl("txtPort")).Text;

            if (nextPort != string.Empty)
            {
                if (nextPort.Split('|').Length > 1)
                {
                    string portCode = nextPort.Split('|')[1].Trim();

                    int portId = new TransactionBLL().GetPortId(portCode);

                    ViewState["NEXTPORTID"] = portId;
                }
                else
                {
                    ViewState["NEXTPORTID"] = null;
                }
            }
            else
            {
                ViewState["NEXTPORTID"] = null;
            }
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

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            List<CargoDetails> lstData = ViewState["DataSource"] as List<CargoDetails>;

            int totalRows = gvwCargo.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwCargo.Rows[r];

                HiddenField hdnCargoVesselId = (HiddenField)thisGridViewRow.FindControl("hdnCargoVesselId");
                DropDownList ddlCargo = (DropDownList)thisGridViewRow.FindControl("ddlCargo");
                DropDownList ActType = (DropDownList)thisGridViewRow.FindControl("ddlActType");
                TextBox txtQuantity = (TextBox)thisGridViewRow.FindControl("txtQuantity");

                lstData.Where(d => d.CargoVesselId == Convert.ToInt64(hdnCargoVesselId.Value))
                    .Select(d =>
                    {
                        d.CargoId = Convert.ToInt32(ddlCargo.SelectedValue);
                        d.Quantity = Convert.ToDecimal(txtQuantity.Text.Trim());
                        return d;
                    }).ToList();
            }

            ImageButton btnRemove = (ImageButton)sender;
            GridViewRow gvContainerRow = (GridViewRow)btnRemove.Parent.Parent;
            HiddenField hdnCargoVesselId2 = gvContainerRow.FindControl("hdnCargoVesselId") as HiddenField;

            lstData.Where(d => d.CargoVesselId == Convert.ToInt64(hdnCargoVesselId2.Value))
                    .Select(d =>
                    {
                        d.IsDeleted = true;
                        return d;
                    }).ToList();

            for (int i = lstData.Count - 1; i >= 0; i--)
            {
                if (lstData[i].IsDeleted == true && lstData[i].IsNew == true)
                    lstData.RemoveAt(i);
            }

            ViewState["DataSource"] = lstData;
            BindGrid(-1);
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

                if (_LocationSpecific == true)
                //if (user.UserRole.Id != (int)UserRole.Admin)
                {
                    txtPort.EnableViewState = false;
                    string port = new TransactionBLL().GetPortNameById(_userPort);
                    ViewState["PORTID"] = _userPort;
                    ((TextBox)txtPort.FindControl("txtPort")).Text = port;
                    txtPort.Visible = false;
                    txtPortText.Visible = true;
                    txtPortText.Text = port;
                     //ddlLocation.Enabled = false;
                }
                else
                {
                    _userPort = 0;
                    txtPort.EnableViewState = true;
                    //_userLocation = 0;
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

        private void LoadDDLs()
        {
            DataTable dt = new TransactionBLL().GetAllAgent();
            DataRow dr = dt.NewRow();
            dr["pk_AgentID"] = "0";
            dr["AgentName"] = "--Select--";
            dt.Rows.InsertAt(dr, 0);
            ddlAgentName.DataValueField = "pk_AgentID";
            ddlAgentName.DataTextField = "AgentName";
            ddlAgentName.DataSource = dt;
            ddlAgentName.DataBind();

            //DataTable dt2 = new TransactionBLL().GetAllLocation();
            ////DataRow dr2 = dt2.NewRow();
            ////dr2["pk_LocID"] = "0";
            ////dr2["LocName"] = "--Select--";
            ////dt2.Rows.InsertAt(dr, 0);
            //ddlLocation.DataValueField = "pk_LocID";
            //ddlLocation.DataTextField = "LocName";
            //ddlLocation.DataSource = dt2;
            //ddlLocation.DataBind();
            //ddlLocation.SelectedIndex = 0;

            DataTable dt1 = new TransactionBLL().GetAllVesselPrefix();
            //DataRow dr1 = dt.NewRow();
            //dr1["pk_AgentID"] = "0";
            //dr1["AgentName"] = "--Select--";
            //dt.Rows.InsertAt(dr, 0);
            ddlVesselPrefix.DataValueField = "pk_VesselPrefixID";
            ddlVesselPrefix.DataTextField = "VesselPrefix";
            ddlVesselPrefix.DataSource = dt1;
            ddlVesselPrefix.DataBind();
            //DataTable dt2 = new TransactionBLL().GetAllBerth();
            //DataRow dr2 = dt2.NewRow();
            //dr2["pk_BerthID"] = "0";
            //dr2["BerthName"] = "--Select--";
            //dt2.Rows.InsertAt(dr2, 0);
            //ddlBerth.DataValueField = "pk_BerthID";
            //ddlBerth.DataTextField = "BerthName";
            //ddlBerth.DataSource = dt2;
            //ddlBerth.DataBind();
        }

        private bool ValidateSave()
        {
            bool IsValid = true;

            if (Convert.ToString(ViewState["PORTID"]) == string.Empty || Convert.ToString(ViewState["PORTID"]) == "0")
            {
                IsValid = false;
                errPort.Text = "This field is required";
            }

            if (Convert.ToString(ViewState["PREVIOUSPORTID"]) == string.Empty || Convert.ToString(ViewState["PREVIOUSPORTID"]) == "0")
            {
                IsValid = false;
                errPreviousPort.Text = "This field is required";
            }

            if (Convert.ToString(ViewState["NEXTPORTID"]) == string.Empty || Convert.ToString(ViewState["NEXTPORTID"]) == "0")
            {
                IsValid = false;
                errNextPort.Text = "This field is required";
            }

            if (gvwCargo.Rows.Count == 1)
            {
                List<CargoDetails> lstData = ViewState["DataSource"] as List<CargoDetails>;
                GridViewRow thisGridViewRow = gvwCargo.Rows[0];
                DropDownList ddlCargo = (DropDownList)thisGridViewRow.FindControl("ddlCargo");
                if (ddlCargo.SelectedIndex == 0)
                {
                    IsValid = false;
                    lblErr.Text = "Cargo is compulsory";
                }
            }
            return IsValid;
        }

        private void BindGrid(int VesselId)
        {
            List<CargoDetails> oList = new List<CargoDetails>();

            if (VesselId > 0) //Edit
            {
                oList = new TransactionBLL().GetListOfCargo(VesselId);
                ViewState["DataSource"] = oList;
            }
            else if (VesselId == 0)//Add
            {
                oList.Add(new CargoDetails { CargoVesselId = -1, CargoId = 0, IsNew = true });
                ViewState["DataSource"] = oList;
            }

            if (!ReferenceEquals(ViewState["DataSource"], null))
            {
                oList = ViewState["DataSource"] as List<CargoDetails>;
                gvwCargo.DataSource = oList.Where(i => i.IsDeleted == false).ToList();
                gvwCargo.DataBind();
            }
        }

        private void SetActivity()
        {

        }

        private void AddNewRow()
        {
            List<CargoDetails> oList = new List<CargoDetails>();

            if (!ReferenceEquals(ViewState["DataSource"], null))
            {
                oList = ViewState["DataSource"] as List<CargoDetails>;

                int totalRows = gvwCargo.Rows.Count;

                for (int r = 0; r < totalRows; r++)
                {
                    GridViewRow thisGridViewRow = gvwCargo.Rows[r];

                    HiddenField hdnCargoVesselId = (HiddenField)thisGridViewRow.FindControl("hdnCargoVesselId");
                    DropDownList ddlCargo = (DropDownList)thisGridViewRow.FindControl("ddlCargo");
                    TextBox txtQuantity = (TextBox)thisGridViewRow.FindControl("txtQuantity");
                    DropDownList ddlActType = (DropDownList)thisGridViewRow.FindControl("ddlActType");

                    oList.Where(d => d.CargoVesselId == Convert.ToInt64(hdnCargoVesselId.Value))
                        .Select(d =>
                        {
                            d.CargoId = Convert.ToInt32(ddlCargo.SelectedValue);
                            d.Quantity = Convert.ToDecimal(txtQuantity.Text.Trim());
                            d.ActType = Convert.ToString(ddlActType.SelectedValue);
                            return d;
                        }).ToList();
                }

                var min = oList.Min(i => i.CargoVesselId);

                oList.Add(new CargoDetails { CargoVesselId = (min.ToInt() - 1), CargoId = 0, IsNew = true, ActType = ddlAcivity.SelectedValue });

                gvwCargo.DataSource = oList;
                gvwCargo.DataBind();
            }
        }

        private void LoadForEdit(int VesselId)
        {
            VesselEntity o = new VesselEntity();
            o = new TransactionBLL().GetVessel(VesselId);

            ddlAcivity.SelectedValue = o.Activity;
            if (!string.IsNullOrEmpty(o.ActivityStatus))
                ddlAcivity.Enabled = false;

            txtVesselName.Text = o.VesselName;
            ddlVesselPrefix.SelectedValue = o.VesselPrefix.ToString();

            string port = new TransactionBLL().GetPortNameById(o.PortId);
            ViewState["PORTID"] = o.PortId;
            ((TextBox)txtPort.FindControl("txtPort")).Text = port;

            string prevPort = new TransactionBLL().GetPortNameById(o.PrevPortId);
            ViewState["PREVIOUSPORTID"] = o.PrevPortId;
            ((TextBox)txtPreviousPort.FindControl("txtPort")).Text = prevPort;

            string nxtPort = new TransactionBLL().GetPortNameById(o.NextPortId);
            ViewState["NEXTPORTID"] = o.NextPortId;
            ((TextBox)txtNextPort.FindControl("txtPort")).Text = nxtPort;

            //ddlBerth.SelectedValue = o.BerthId.ToString();
            txtLOA.Text = o.LOA.ToString();
            txtArrivalDate.Text = o.ETA.ToString("dd-MM-yyyy");

            //if (o.BerthDate.HasValue)
            //    txtBerthDate.Text = o.BerthDate.Value.ToString("dd-MM-yyyy");
            if (o.ETC.HasValue)
                txtETC.Text = o.ETC.Value.ToString("dd-MM-yyyy");

            if (o.BerthDate.HasValue)
                txtBerthDate.Text = o.BerthDate.Value.ToString("dd-MM-yyyy");

            if (o.SailDate.HasValue)
                txtSailDate.Text = o.SailDate.Value.ToString("dd-MM-yyyy");

            //ddlLocation.SelectedValue = o.LocID.ToString();
            txtOwnerName.Text = o.Owner;
            ddlAgentName.SelectedValue = o.AgentId.ToString();
            txtRemarks.Text = o.Remarks;
            txtSailDate.Enabled = true;
            
            if (o.ActivityStatus == "S")
            {
                txtBerthDate.Enabled = true;
                txtArrivalDate.Enabled = true;
                //txtSailDate.Enabled = true;
                txtETC.Enabled = true;
                ddlAcivity.Enabled = false;
                txtPort.EnableViewState = false;
                
            }
            
            BindGrid(VesselId);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateSave())
            {
                VesselEntity o = new VesselEntity();

                if (!ReferenceEquals(ViewState["VesselId"], null))
                    o.VesselId = Convert.ToInt32(ViewState["VesselId"]);

                o.VesselName = txtVesselName.Text.Trim();
                o.VesselPrefix = ddlVesselPrefix.SelectedValue.ToInt();
                //o.VoyageNo = txtVoyageNo.Text.Trim();
                o.VPRorPAS = "V";
                o.Activity = ddlAcivity.SelectedValue;
                o.AgentId = Convert.ToInt32(ddlAgentName.SelectedValue);
                o.ETA = Convert.ToDateTime(txtArrivalDate.Text.Trim());
                //o.BerthDate = Convert.ToDateTime(txtBerthDate.Text.Trim());
                //o.BerthId = Convert.ToInt32(ddlBerth.SelectedValue);
                o.CreatedBy = 0;
                //o.ETC = Convert.ToDateTime(txtETC.Text.Trim());
                if (txtETC.Text.Trim() != "")
                    o.ETC = Convert.ToDateTime(txtETC.Text.Trim());
                if (txtBerthDate.Text.Trim() != "")
                    o.BerthDate = Convert.ToDateTime(txtBerthDate.Text.Trim());
                if (txtSailDate.Text.Trim() != "")
                    o.SailDate = Convert.ToDateTime(txtSailDate.Text.Trim());
                o.LOA = Convert.ToDecimal(txtLOA.Text.Trim());
                o.ModifiedBy = 0;
                o.NextPortId = Convert.ToInt32(ViewState["NEXTPORTID"]);
                o.Owner = txtOwnerName.Text.Trim();
                o.PortId = Convert.ToInt32(ViewState["PORTID"]);
                o.PrevPortId = Convert.ToInt32(ViewState["PREVIOUSPORTID"]);
                o.Remarks = txtRemarks.Text.Trim();
                //o.LocID =  Convert.ToInt32(ddlLocation.SelectedValue);

                //Add Cargo Details
                List<CargoDetails> lstData = ViewState["DataSource"] as List<CargoDetails>;

                int totalRows = gvwCargo.Rows.Count;

                for (int r = 0; r < totalRows; r++)
                {
                    GridViewRow thisGridViewRow = gvwCargo.Rows[r];

                    HiddenField hdnCargoVesselId = (HiddenField)thisGridViewRow.FindControl("hdnCargoVesselId");
                    DropDownList ddlCargo = (DropDownList)thisGridViewRow.FindControl("ddlCargo");
                    DropDownList ddlActType = (DropDownList)thisGridViewRow.FindControl("ddlActType");
                    TextBox txtQuantity = (TextBox)thisGridViewRow.FindControl("txtQuantity");

                    lstData.Where(d => d.CargoVesselId == Convert.ToInt64(hdnCargoVesselId.Value))
                        .Select(d =>
                        {
                            d.CargoId = Convert.ToInt32(ddlCargo.SelectedValue);
                            d.Quantity = Convert.ToDecimal(txtQuantity.Text.Trim());
                            d.ActType = Convert.ToString(ddlActType.SelectedValue);
                            return d;
                        }).ToList();
                    ddlActType.Enabled = false;
                }

                new TransactionBLL().SaveVesselCargo(o, lstData);

                if (Convert.ToInt32(ViewState["VesselId"]) > 0)
                {
                    Response.Redirect("~/Transaction/ManageVessel.aspx");
                }
                else
                {
                    string encryptedId = GeneralFunctions.EncryptQueryString("-1");
                    Response.Redirect("~/Transaction/AddEditVessel.aspx?VesselId=" + encryptedId);
                }

                lblErr.Text = "Record saved successfully";
                ddlAcivity.SelectedIndex = -1;
        
            }
        }

        protected void ddlAcivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<CargoDetails> lstData = ViewState["DataSource"] as List<CargoDetails>;

            int totalRows = gvwCargo.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvwCargo.Rows[r];
                DropDownList ddlActType = (DropDownList)thisGridViewRow.FindControl("ddlActType");

                if (ddlAcivity.SelectedValue.ToString() == "L")
                {
                    ddlActType.SelectedValue = "L";
                    ddlActType.Enabled = false;
                }
                else if (ddlAcivity.SelectedValue.ToString() == "D")
                {
                    ddlActType.SelectedValue = "D";
                    ddlActType.Enabled = false;
                }

                else if (ddlAcivity.SelectedValue.ToString() == "B")
                {
                    //ddlActType.SelectedValue = "D";
                    ddlActType.Enabled = true;
                }

                else if (ddlAcivity.SelectedValue.ToString() == "O")
                {
                    ddlActType.SelectedValue = "N";
                    ddlActType.Enabled = false;
                }
            }
        }
    }
}