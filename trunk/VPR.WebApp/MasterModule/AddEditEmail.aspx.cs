using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPR.Common;
using VPR.BLL;
using VPR.Utilities;
using System.Data;
using VPR.Entity;

namespace VPR.WebApp.MasterModule
{
    public partial class AddEditEmail : System.Web.UI.Page
    {
        List<ICargoGroup> dynList;
        List<ICargoGroup> dynList2;
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
                LoadCountryDDL();
                
                if (!ReferenceEquals(Request.QueryString["EmailId"], null))
                {
                    int EmailId = 0;
                    EmailId = GeneralFunctions.DecryptQueryString(Request.QueryString["EmailId"].ToString()).ToInt();

                    if (EmailId > 0)
                    {
                        ViewState["EmailId"] = EmailId;
                        BindListBox(Convert.ToInt32(ViewState["EmailId"]));
                        LoadForEdit(EmailId);
                    }
                    else
                    {
                        BindListBox(0);
                        ViewState["EmailId"] = null;
                    }
                }
                else
                {
                    BindListBox(0);
                    ViewState["EmailId"] = null;
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

        private void LoadCountryDDL()
        {
            DataTable dt = new CommonBLL().GetAllCountry();
            DataRow dr = dt.NewRow();
            dr["pk_countryID"] = "0";
            dr["CountryName"] = "--Select--";
            dt.Rows.InsertAt(dr, 0);
            ddlCountry.DataValueField = "pk_countryID";
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataSource = dt;
            ddlCountry.DataBind();
        }

        private void LoadForEdit(int EmailId)
        {
            IEmail objGroup = new EmailBLL().GetEmail(EmailId);

            txtSuffix.Text = objGroup.Suffix;
            txtSalutation.Text = objGroup.Salutation;
            txtReceiverName.Text = objGroup.Name;
            txtEmailId.Text = objGroup.EmailId;
            txtEmailId1.Text = objGroup.EmailId1;
            txtEmailId2.Text = objGroup.EmailId2;
            txtEmailId3.Text = objGroup.EmailId3;
            txtCompanyName.Text = objGroup.Company;
            txtCompanyAbbr.Text = objGroup.CompanyAbbr;
            ddlCountry.SelectedValue = objGroup.CountryId.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                Email emailGroup = new Email();

                if (!ReferenceEquals(ViewState["EmailId"], null))
                    emailGroup.Id = Convert.ToInt32(ViewState["EmailId"]);

                emailGroup.Suffix = txtSuffix.Text.Trim();
                emailGroup.Salutation = txtSalutation.Text.Trim();
                emailGroup.Name = txtReceiverName.Text.Trim();
                emailGroup.EmailId = txtEmailId.Text.Trim();
                emailGroup.EmailId1 = txtEmailId1.Text.Trim();
                emailGroup.EmailId2 = txtEmailId2.Text.Trim();
                emailGroup.EmailId3 = txtEmailId3.Text.Trim();
                emailGroup.Company = txtCompanyName.Text.Trim();
                emailGroup.CompanyAbbr = txtCompanyAbbr.Text.Trim();
                emailGroup.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);

                if (!ReferenceEquals(ViewState["dynList2"], null))
                    emailGroup.CargoGroupList = (List<ICargoGroup>)ViewState["dynList2"];

                new EmailBLL().SaveEmail(emailGroup);

                lblMessage.Text = "Email Saved Successfully";
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            MoveItems(true);// true since we add
        }
        protected void ButtonRemove_Click(object sender, EventArgs e)
        {
            MoveItems(false); // false since we remove
        }
        protected void ButtonAddAll_Click(object sender, EventArgs e)
        {
            MoveAllItems(true); // true since we add all
        }

        protected void ButtonRemoveAll_Click(object sender, EventArgs e)
        {
            MoveAllItems(false); // false means re remove all
        }

        private void MoveItems(bool isAdd)
        {
            if (isAdd)// means if you add items to the right box
            {
                for (int i = ListBox1.Items.Count - 1; i >= 0; i--)
                {
                    string cargoGroupName = string.Empty;

                    if (ListBox1.Items[i].Selected)
                    {
                        cargoGroupName = ListBox1.Items[i].Text;

                        ListBox2.Items.Add(ListBox1.Items[i]);
                        //ListBox2.ClearSelection();
                        ListBox1.Items.Remove(ListBox1.Items[i]);

                        List<ICargoGroup> list1 = (List<ICargoGroup>)ViewState["dynList"];
                        List<ICargoGroup> list2 = (List<ICargoGroup>)ViewState["dynList2"];

                        if (list2.Any(l => l.CargoGroupName == cargoGroupName))
                        {
                            //list2 = list2.Where(l => l.EmailId == ListBox2.Items[i].Text).Select(l => { l.IsRemoved = true; l.IsAdded = true; return l; }).ToList();
                            var toUpdate = list2.SingleOrDefault(l => l.CargoGroupName == cargoGroupName);
                            toUpdate.IsRemoved = false;
                            toUpdate.IsAdded = false;

                            list1.RemoveAll(l => l.CargoGroupName == cargoGroupName);
                        }
                        else
                        {
                            //list1 = list1.Where(l => l.EmailId == ListBox1.Items[i].Text).Select(l => { l.IsRemoved = true; l.IsAdded = false; return l; }).ToList();
                            var toUpdate = list1.SingleOrDefault(l => l.CargoGroupName == cargoGroupName);
                            toUpdate.IsRemoved = true;
                            toUpdate.IsAdded = false;

                            //list2 = list2.Where(l => l.EmailId == ListBox2.Items[i].Text).Select(l => { l.IsRemoved = false; l.IsAdded = true; return l; }).ToList();
                            var item = list1.SingleOrDefault(l => l.CargoGroupName == cargoGroupName);
                            list2.Add(new CargoGroupEntity { CargoGroupID = item.CargoGroupID, CargoGroupName = item.CargoGroupName, GroupStatus = item.GroupStatus, IsAdded = item.IsAdded, IsRemoved = item.IsRemoved });
                            var toUpdate2 = list2.SingleOrDefault(l => l.CargoGroupName == cargoGroupName);
                            toUpdate2.IsRemoved = false;
                            toUpdate2.IsAdded = true;
                        }

                        ViewState["dynList"] = list1;
                        ViewState["dynList2"] = list2;


                    }
                }
            }
            else // means if you remove items from the right box and add it back to the left box
            {
                for (int i = ListBox2.Items.Count - 1; i >= 0; i--)
                {
                    string cargoGroupName = string.Empty;

                    if (ListBox2.Items[i].Selected)
                    {
                        cargoGroupName = ListBox2.Items[i].Text;

                        ListBox1.Items.Add(ListBox2.Items[i]);
                        //ListBox1.ClearSelection();
                        ListBox2.Items.Remove(ListBox2.Items[i]);

                        List<ICargoGroup> list1 = (List<ICargoGroup>)ViewState["dynList"];
                        List<ICargoGroup> list2 = (List<ICargoGroup>)ViewState["dynList2"];

                        //list2 = list2.Where(l => l.Id == Convert.ToInt32(ListBox2.Items[i].Value)).Select(l => { l.IsRemoved = true; l.IsAdded = false; return l; }).ToList();
                        //list1 = list1.Where(l => l.Id == Convert.ToInt32(ListBox1.Items[i].Value)).Select(l => { l.IsRemoved = false; l.IsAdded = true; return l; }).ToList();

                        if (list1.Any(l => l.CargoGroupName == cargoGroupName))
                        {
                            //list2 = list2.Where(l => l.EmailId == ListBox2.Items[i].Text).Select(l => { l.IsRemoved = true; l.IsAdded = true; return l; }).ToList();
                            var toUpdate = list1.SingleOrDefault(l => l.CargoGroupName == cargoGroupName);
                            toUpdate.IsRemoved = false;
                            toUpdate.IsAdded = false;

                            list2.RemoveAll(l => l.CargoGroupName == cargoGroupName);
                        }
                        else
                        {
                            //list1 = list1.Where(l => l.EmailId == ListBox1.Items[i].Text).Select(l => { l.IsRemoved = true; l.IsAdded = false; return l; }).ToList();
                            var toUpdate = list2.SingleOrDefault(l => l.CargoGroupName == cargoGroupName);
                            toUpdate.IsRemoved = true;
                            toUpdate.IsAdded = false;

                            //list2 = list2.Where(l => l.EmailId == ListBox2.Items[i].Text).Select(l => { l.IsRemoved = false; l.IsAdded = true; return l; }).ToList();
                            var item = list2.SingleOrDefault(l => l.CargoGroupName == cargoGroupName);
                            list1.Add(new CargoGroupEntity { CargoGroupID = item.CargoGroupID, CargoGroupName = item.CargoGroupName, GroupStatus = item.GroupStatus, IsAdded = item.IsAdded, IsRemoved = item.IsRemoved });
                            var toUpdate2 = list1.SingleOrDefault(l => l.CargoGroupName == cargoGroupName);
                            toUpdate2.IsRemoved = false;
                            toUpdate2.IsAdded = true;
                        }

                        ViewState["dynList"] = list1;
                        ViewState["dynList2"] = list2;


                    }
                }
            }
        }

        private void MoveAllItems(bool isAddAll)
        {
            if (isAddAll)// means if you add ALL items to the right box
            {
                for (int i = ListBox1.Items.Count - 1; i >= 0; i--)
                {
                    //ListBox2.Items.Add(ListBox1.Items[i]);
                    //ListBox2.ClearSelection();
                    //ListBox1.Items.Remove(ListBox1.Items[i]);

                    string cargoGroupName = string.Empty;

                    cargoGroupName = ListBox1.Items[i].Text;

                    ListBox2.Items.Add(ListBox1.Items[i]);
                    //ListBox2.ClearSelection();
                    ListBox1.Items.Remove(ListBox1.Items[i]);

                    List<ICargoGroup> list1 = (List<ICargoGroup>)ViewState["dynList"];
                    List<ICargoGroup> list2 = (List<ICargoGroup>)ViewState["dynList2"];

                    if (list2.Any(l => l.CargoGroupName == cargoGroupName))
                    {
                        //list2 = list2.Where(l => l.EmailId == ListBox2.Items[i].Text).Select(l => { l.IsRemoved = true; l.IsAdded = true; return l; }).ToList();
                        var toUpdate = list2.SingleOrDefault(l => l.CargoGroupName == cargoGroupName);
                        toUpdate.IsRemoved = false;
                        toUpdate.IsAdded = false;

                        list1.RemoveAll(l => l.CargoGroupName == cargoGroupName);
                    }
                    else
                    {
                        //list1 = list1.Where(l => l.EmailId == ListBox1.Items[i].Text).Select(l => { l.IsRemoved = true; l.IsAdded = false; return l; }).ToList();
                        var toUpdate = list1.SingleOrDefault(l => l.CargoGroupName == cargoGroupName);
                        toUpdate.IsRemoved = true;
                        toUpdate.IsAdded = false;

                        //list2 = list2.Where(l => l.EmailId == ListBox2.Items[i].Text).Select(l => { l.IsRemoved = false; l.IsAdded = true; return l; }).ToList();
                        var item = list1.SingleOrDefault(l => l.CargoGroupName == cargoGroupName);
                        list2.Add(new CargoGroupEntity { CargoGroupID = item.CargoGroupID, CargoGroupName = item.CargoGroupName, GroupStatus = item.GroupStatus, IsAdded = item.IsAdded, IsRemoved = item.IsRemoved });
                        var toUpdate2 = list2.SingleOrDefault(l => l.CargoGroupName == cargoGroupName);
                        toUpdate2.IsRemoved = false;
                        toUpdate2.IsAdded = true;
                    }

                    ViewState["dynList"] = list1;
                    ViewState["dynList2"] = list2;
                }
            }
            else // means if you remove ALL items from the right box and add it back to the left box
            {
                for (int i = ListBox2.Items.Count - 1; i >= 0; i--)
                {
                    //ListBox1.Items.Add(ListBox2.Items[i]);
                    //ListBox1.ClearSelection();
                    //ListBox2.Items.Remove(ListBox2.Items[i]);

                    string cargoGroupName = string.Empty;

                    cargoGroupName = ListBox2.Items[i].Text;

                    ListBox1.Items.Add(ListBox2.Items[i]);
                    //ListBox1.ClearSelection();
                    ListBox2.Items.Remove(ListBox2.Items[i]);

                    List<ICargoGroup> list1 = (List<ICargoGroup>)ViewState["dynList"];
                    List<ICargoGroup> list2 = (List<ICargoGroup>)ViewState["dynList2"];

                    //list2 = list2.Where(l => l.Id == Convert.ToInt32(ListBox2.Items[i].Value)).Select(l => { l.IsRemoved = true; l.IsAdded = false; return l; }).ToList();
                    //list1 = list1.Where(l => l.Id == Convert.ToInt32(ListBox1.Items[i].Value)).Select(l => { l.IsRemoved = false; l.IsAdded = true; return l; }).ToList();

                    if (list1.Any(l => l.CargoGroupName == cargoGroupName))
                    {
                        //list2 = list2.Where(l => l.EmailId == ListBox2.Items[i].Text).Select(l => { l.IsRemoved = true; l.IsAdded = true; return l; }).ToList();
                        var toUpdate = list1.SingleOrDefault(l => l.CargoGroupName == cargoGroupName);
                        toUpdate.IsRemoved = false;
                        toUpdate.IsAdded = false;

                        list2.RemoveAll(l => l.CargoGroupName == cargoGroupName);
                    }
                    else
                    {
                        //list1 = list1.Where(l => l.EmailId == ListBox1.Items[i].Text).Select(l => { l.IsRemoved = true; l.IsAdded = false; return l; }).ToList();
                        var toUpdate = list2.SingleOrDefault(l => l.CargoGroupName == cargoGroupName);
                        toUpdate.IsRemoved = true;
                        toUpdate.IsAdded = false;

                        //list2 = list2.Where(l => l.EmailId == ListBox2.Items[i].Text).Select(l => { l.IsRemoved = false; l.IsAdded = true; return l; }).ToList();
                        var item = list2.SingleOrDefault(l => l.CargoGroupName == cargoGroupName);
                        list1.Add(new CargoGroupEntity { CargoGroupID = item.CargoGroupID, CargoGroupName = item.CargoGroupName, GroupStatus = item.GroupStatus, IsAdded = item.IsAdded, IsRemoved = item.IsRemoved });
                        var toUpdate2 = list1.SingleOrDefault(l => l.CargoGroupName == cargoGroupName);
                        toUpdate2.IsRemoved = false;
                        toUpdate2.IsAdded = true;
                    }

                    ViewState["dynList"] = list1;
                    ViewState["dynList2"] = list2;
                }
            }
        }

        private void BindListBox(int EmailId)
        {
            dynList = new List<ICargoGroup>();
            dynList = new EmailBLL().GetListOfAvailableCargoGroup(EmailId, true);
            //dynList.Add(new Email { Id = 1, EmailId = "Elevator", Company = "AAA", IsRemoved = false, IsAdded = false });
            //dynList.Add(new Email { Id = 2, EmailId = "Stairs", Company = "BBB", IsRemoved = false, IsAdded = false });

            ListBox1.DataSource = dynList;
            ListBox1.DataValueField = "CargoGroupName";
            ListBox1.DataTextField = "CargoGroupName";
            ListBox1.DataBind();

            ViewState["dynList"] = dynList;

            dynList2 = new List<ICargoGroup>();
            dynList2 = new EmailBLL().GetListOfTaggedCargoGroup(EmailId, true);
            //dynList2.Add(new Email { Id = 1, EmailId = "Souvik", Company = "Wipro", IsRemoved = false, IsAdded = false });
            //dynList2.Add(new Email { Id = 2, EmailId = "Tapas", Company = "CTS", IsRemoved = false, IsAdded = false });

            ListBox2.DataSource = dynList2;
            ListBox2.DataValueField = "CargoGroupName";
            ListBox2.DataTextField = "CargoGroupName";
            ListBox2.DataBind();

            ViewState["dynList2"] = dynList2;
        }

        private bool IsValid()
        {
            bool isValid = true;
            lblGroupName.Text = "";

            if (ReferenceEquals(ViewState["EmailId"], null))
            {
                if (new EmailBLL().IsEmailExists(txtEmailId.Text.Trim()))
                {
                    isValid = false;
                    lblGroupName.Text = "Email ID not available!";
                }
            }

            return isValid;
        }
    }
}