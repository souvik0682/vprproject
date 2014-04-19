using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPR.Entity;

namespace VPR.WebApp.Transaction
{
    public partial class ManageShipStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGrid();
            }
        }

        protected void gvwLoading_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                VesselStatus o = e.Row.DataItem as VesselStatus;
            }
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((CheckBox)sender).NamingContainer;

            //TextBox txtArrivalDate = (TextBox)row.FindControl("txtArrivalDate");
            //TextBox txtBerthDate = (TextBox)row.FindControl("txtBerthDate");
            //TextBox txtDischargeDate = (TextBox)row.FindControl("txtDischargeDate");
            TextBox txtLoadingDate = (TextBox)row.FindControl("txtLoadingDate");

            if (((CheckBox)sender).Checked)
            {
                txtLoadingDate.Enabled = true;
                txtLoadingDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
            else
            {
                txtLoadingDate.Enabled = false;
                txtLoadingDate.Text = string.Empty;
            }
        }

        protected void chkDischarging_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((CheckBox)sender).NamingContainer;

            //TextBox txtArrivalDate = (TextBox)row.FindControl("txtArrivalDate");
            //TextBox txtBerthDate = (TextBox)row.FindControl("txtBerthDate");
            TextBox txtDischargeDate = (TextBox)row.FindControl("txtDischargeDate");
            //TextBox txtLoadingDate = (TextBox)row.FindControl("txtLoadingDate");

            if (((CheckBox)sender).Checked)
            {
                txtDischargeDate.Enabled = true;
                txtDischargeDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
            else
            {
                txtDischargeDate.Enabled = false;
                txtDischargeDate.Text = string.Empty;
            }
        }

        protected void chkAwaiting_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((CheckBox)sender).NamingContainer;

            //TextBox txtArrivalDate = (TextBox)row.FindControl("txtArrivalDate");
            TextBox txtBerthDate = (TextBox)row.FindControl("txtBerthDate");
            //TextBox txtDischargeDate = (TextBox)row.FindControl("txtDischargeDate");
            //TextBox txtLoadingDate = (TextBox)row.FindControl("txtLoadingDate");

            if (((CheckBox)sender).Checked)
            {
                txtBerthDate.Enabled = true;
                txtBerthDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
            else
            {
                txtBerthDate.Enabled = false;
                txtBerthDate.Text = string.Empty;
            }
        }

        protected void chkExpecting_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((CheckBox)sender).NamingContainer;

            TextBox txtArrivalDate = (TextBox)row.FindControl("txtArrivalDate");
            //TextBox txtBerthDate = (TextBox)row.FindControl("txtBerthDate");
            //TextBox txtDischargeDate = (TextBox)row.FindControl("txtDischargeDate");
            //TextBox txtLoadingDate = (TextBox)row.FindControl("txtLoadingDate");

            if (((CheckBox)sender).Checked)
            {
                txtArrivalDate.Enabled = true;
                txtArrivalDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
            else
            {
                txtArrivalDate.Enabled = false;
                txtArrivalDate.Text = string.Empty;
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
        }

        private void LoadGrid()
        {
            List<VesselStatus> oList = new List<VesselStatus>();
            oList.Add(new VesselStatus { VesselId=0, Activity = "ACT001", BirthNo = "BN001", LOA = "LOA001", Vessel = "VSL001", Cargo = "CG001", CargoQuantity = "1000" });

            gvwLoading.DataSource = oList;
            gvwLoading.DataBind();

            List<VesselStatus> oDischargingList = new List<VesselStatus>();
            oDischargingList.Add(new VesselStatus { VesselId = 0, Activity = "ACT001", BirthNo = "BN001", LOA = "LOA001", Vessel = "VSL001", Cargo = "CG001", CargoQuantity = "1000" });

            gvwDischarging.DataSource = oDischargingList;
            gvwDischarging.DataBind();

            List<VesselStatus> oAwaitingList = new List<VesselStatus>();
            oAwaitingList.Add(new VesselStatus { VesselId = 0, Activity = "ACT001", BirthNo = "BN001", LOA = "LOA001", Vessel = "VSL001", Cargo = "CG001", CargoQuantity = "1000" });

            gvwAwaiting.DataSource = oAwaitingList;
            gvwAwaiting.DataBind();

            List<VesselStatus> oExpectingList = new List<VesselStatus>();
            oExpectingList.Add(new VesselStatus { VesselId = 0, Activity = "ACT001", BirthNo = "BN001", LOA = "LOA001", Vessel = "VSL001", Cargo = "CG001", CargoQuantity = "1000" });

            gvwExpecting.DataSource = oExpectingList;
            gvwExpecting.DataBind();
        }
    }
}