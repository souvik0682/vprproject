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
    public partial class AddEditPAS : System.Web.UI.Page
    {
        private int _userId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;
        private int _userLocation = 0;
        private int PASTranId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            _userId = UserBLL.GetLoggedInUserId();
            _userLocation = UserBLL.GetUserLocation();
            CheckUserAccess();

            if (!IsPostBack)
            {
                LoadDDLs();

                if (!ReferenceEquals(Request.QueryString["PASTranId"], null))
                {
                    //int PASTranId = 0;
                    ddlActivity.Enabled = false;
                    PASTranId = GeneralFunctions.DecryptQueryString(Request.QueryString["PASTranId"].ToString()).ToInt();
                    btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManagePAS.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                    if (PASTranId > 0)
                    {
                        ViewState["PASTranId"] = PASTranId;
                        LoadForEdit(0, PASTranId);
                    }
                    else
                    {
                        ddlActivity.SelectedIndex = -1;
                        //BindGrid(0);
                        ViewState["PASTranId"] = 0;
                    }
                }
                else
                {
                    ddlActivity.SelectedIndex = -1;
                    //BindGrid(0);
                    ViewState["PASTranId"] = 0;
                }
            }

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
                ddlCargo.Enabled = false;
                if (ddlActivity.SelectedValue.ToString() == "B")
                    ddlActType.Enabled = true;
                else
                    ddlActType.Enabled = false;


                //ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnRemove");
                //btnRemove.ImageUrl = "~/Images/remove.png";
                //btnRemove.Attributes.Add("onclick", "javascript:return confirm('Are you sure about delete?');");
                //btnRemove.CommandName = "Delete";
                //btnRemove.ToolTip = "Delete";
            }
        }

        //protected void btnAddNew_Click(object sender, EventArgs e)
        //{
        //    AddNewRow();
        //}

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
            BindGrid(0, -1);
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

        private void LoadDDLs()
        {

            DataTable dt = new TransactionBLL().GetPASVesselList();
            DataRow dr = dt.NewRow();
            dr["pk_vesselID"] = "0";
            dr["VesselName"] = "--Select--";
            dt.Rows.InsertAt(dr, 0);
            ddlVessel.DataValueField = "pk_vesselID";
            ddlVessel.DataTextField = "VesselName";
            ddlVessel.DataSource = dt;
            ddlVessel.DataBind();

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

        private void LoadMovement()
        {

            DataTable dt = new TransactionBLL().GetMovementList();
            //DataRow dr = dt.NewRow();
            //dr["pk_vesselID"] = "0";
            //dr["VesselName"] = "--Select--";
            //dt.Rows.InsertAt(dr, 0);
            ddlMovement.DataValueField = "pk_MovementID";
            ddlMovement.DataTextField = "MovementName";
            ddlMovement.DataSource = dt;
            ddlMovement.DataBind();

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


            return IsValid;
        }

        private void BindGrid(int VesselId, int PASTranId)
        {
            List<CargoDetails> oList = new List<CargoDetails>();

            if (VesselId > 0 && PASTranId > 0) //Edit
            {
                oList = new TransactionBLL().GetListOfCargo(VesselId);
                ViewState["DataSource"] = oList;
            }
            else if (PASTranId == 0 && VesselId > 0)//Add
            {
                oList = new TransactionBLL().GetListOfCargo(VesselId);
                for (int i = oList.Count - 1; i >= 0; i--)
                {
                    oList[i].IsNew = true;

                }


                //oList.Add(new CargoDetails { CargoVesselId = -1, CargoId = 0, IsNew = true });
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

        //private void AddNewRow()
        //{
        //    List<CargoDetails> oList = new List<CargoDetails>();

        //    if (!ReferenceEquals(ViewState["DataSource"], null))
        //    {
        //        oList = ViewState["DataSource"] as List<CargoDetails>;

        //        int totalRows = gvwCargo.Rows.Count;

        //        for (int r = 0; r < totalRows; r++)
        //        {
        //            GridViewRow thisGridViewRow = gvwCargo.Rows[r];

        //            HiddenField hdnCargoVesselId = (HiddenField)thisGridViewRow.FindControl("hdnCargoVesselId");
        //            DropDownList ddlCargo = (DropDownList)thisGridViewRow.FindControl("ddlCargo");
        //            TextBox txtQuantity = (TextBox)thisGridViewRow.FindControl("txtQuantity");
        //            DropDownList ddlActType = (DropDownList)thisGridViewRow.FindControl("ddlActType");

        //            oList.Where(d => d.CargoVesselId == Convert.ToInt64(hdnCargoVesselId.Value))
        //                .Select(d =>
        //                {
        //                    d.CargoId = Convert.ToInt32(ddlCargo.SelectedValue);
        //                    d.Quantity = Convert.ToDecimal(txtQuantity.Text.Trim());
        //                    d.ActType = Convert.ToString(ddlActType.SelectedValue);
        //                    return d;
        //                }).ToList();
        //        }

        //        var min = oList.Min(i => i.CargoVesselId);

        //        oList.Add(new CargoDetails { CargoVesselId = (min.ToInt() - 1), CargoId = 0, IsNew = true });

        //        gvwCargo.DataSource = oList;
        //        gvwCargo.DataBind();
        //    }
        //}

        private void LoadForEdit(int VesselId, int PASTranId)
        {
            VesselMovementEntity o = new VesselMovementEntity();
            o = new TransactionBLL().GetPASMovement(PASTranId);
            LoadMovement();

            ddlActivity.SelectedValue = o.VesselActivity;
            
            txtPortName.Text = o.PortName;
            txtMovementDate.Text = o.MovementDate.Value.ToString("dd-MM-yyyy");
            ddlVessel.SelectedValue = o.VesselId.ToString();
            ddlMovement.SelectedValue = o.Movement.ToString();
            ddlMovementType.SelectedValue = o.MovementType.ToString();
            ddlMovement.Enabled = false;

            if (o.Movement != "1" && o.Movement != "2")
                BindGrid(o.VesselId.ToInt(), PASTranId);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateSave())
            {
                VesselMovementEntity o = new VesselMovementEntity();

                if (!ReferenceEquals(ViewState["PASTranId"], null))
                    o.PASTranID = Convert.ToInt32(ViewState["PASTranId"]);

                o.MovementDate = Convert.ToDateTime(txtMovementDate.Text.Trim());
                o.MovementType = Convert.ToString(ddlMovementType.SelectedValue);
                o.Movement = Convert.ToString(ddlMovement.SelectedValue);
                o.VesselId = Convert.ToInt32(ddlVessel.SelectedValue);
              
                //o.BerthDate = Convert.ToDateTime(txtBerthDate.Text.Trim());
                //o.BerthId = Convert.ToInt32(ddlBerth.SelectedValue);
                o.CreatedBy = 0;
                //o.ETC = Convert.ToDateTime(txtETC.Text.Trim());
                //o.LOA = Convert.ToDecimal(txtLOA.Text.Trim());
                o.ModifiedBy = 0;
                
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

                new TransactionBLL().SavePAS(o, lstData);

                if (Convert.ToInt32(ViewState["PASTranId"]) > 0)
                {
                    Response.Redirect("~/Transaction/ManagePAS.aspx");
                }
                else
                {
                    string encryptedId = GeneralFunctions.EncryptQueryString("-1");
                    Response.Redirect("~/Transaction/AddEditPAS.aspx?PASTranId=" + encryptedId);
                }

                lblErr.Text = "Record saved successfully";
                ddlActivity.SelectedIndex = -1;

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

                if (ddlActivity.SelectedValue.ToString() == "L")
                {
                    ddlActType.SelectedValue = "L";
                    ddlActType.Enabled = false;
                }
                else if (ddlActivity.SelectedValue.ToString() == "D")
                {
                    ddlActType.SelectedValue = "D";
                    ddlActType.Enabled = false;
                }

                else if (ddlActivity.SelectedValue.ToString() == "B")
                {
                    //ddlActType.SelectedValue = "D";
                    ddlActType.Enabled = true;
                }

                else if (ddlActivity.SelectedValue.ToString() == "O")
                {
                    ddlActType.SelectedValue = "N";
                    ddlActType.Enabled = false;
                }
            }
        }

        protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CurMovement = 0;
            LoadMovement();
            DataSet ds = new TransactionBLL().GetPortNameByVesselID(ddlVessel.SelectedValue.ToInt(), PASTranId);
            txtPortName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            ddlActivity.SelectedValue = ds.Tables[0].Rows[0]["Activity"].ToString();
            ddlMovement.SelectedValue = ds.Tables[1].Rows[0]["NextMove"].ToString();
            CurMovement = ds.Tables[1].Rows[0]["NextMove"].ToInt();
            for (int r = 0; r < CurMovement - 1; r++)
            {
                //ddlMovement.Items.RemoveAt(r);
                ddlMovement.Items[r].Attributes.Add("disabled", "disabled");
            }
            //txtPortName.Text = new TransactionBLL().GetPortNameByVesselID(ddlVessel.SelectedValue.ToInt());
            if (CurMovement != 1 && CurMovement != 2)
                BindGrid(ddlVessel.SelectedValue.ToInt(), 0);

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }
      
    }
}