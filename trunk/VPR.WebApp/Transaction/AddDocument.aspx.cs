using System;
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
using System.Configuration;

namespace VPR.WebApp.Transaction
{
    public partial class AddDocument : System.Web.UI.Page
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
            _userPort = UserBLL.GetUserPort();
            _LocationSpecific = UserBLL.GetUserLocationSpecific();

            CheckUserAccess();
            if (!IsPostBack) 
            {
                LoadDefault();
                if (!ReferenceEquals(Request.QueryString["DocId"], null))
                {
                    int DocId = 0;
                    DocId = GeneralFunctions.DecryptQueryString(Request.QueryString["DocId"].ToString()).ToInt();
                    btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageDocument.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                    if (DocId > 0)
                    {
                        ViewState["DocId"] = DocId;
                        LoadForEdit(DocId);
                    }
                    else
                    {
                        ViewState["DocId"] = 0;
                    }
                }
                else
                {
                    ViewState["DocId"] = 0;
                }
                
            }
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
                    _userPort = 0;
                    //ddlLocation.Enabled = true;
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
                    hdnPort.Value = _userPort.ToString();
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

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();
            //Get user permission.
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);
        }

        private void LoadDefault()
        {
            var documentTypes = DocumentBAL.GetDocumentType();
            ddlDocumentType.DataSource = documentTypes;
            ddlDocumentType.DataTextField = "DocumentType";
            ddlDocumentType.DataValueField = "pk_DocumentTypeID";
            ddlDocumentType.DataBind();
            ddlDocumentType.Items.Insert(0, new ListItem("--Document Type--", "0"));
            ddlDocumentType.SelectedIndex = 0;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transaction/ManageDocument.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int DocId = 0;
            DocId = GeneralFunctions.DecryptQueryString(Request.QueryString["DocId"].ToString()).ToInt();
            if (DocId == -1)
            {
                if (fileUpload.HasFile)
                {
                    var fileName = fileUpload.FileName;
                    var path = Server.MapPath("~/Documents");
                    var newFileName = Guid.NewGuid().ToString();

                    if (!string.IsNullOrEmpty(path))
                    {
                        path += @"\" + newFileName + System.IO.Path.GetExtension(fileName);
                    }
                    if (ValidateSave(path))
                    {
                        fileUpload.PostedFile.SaveAs(path);
                        if (DocumentBAL.SaveDocument(Convert.ToInt32(ddlDocumentType.SelectedValue), txtDocumentName.Text, Convert.ToInt32(hdnPort.Value), path, _userId, ddlScope.SelectedValue) > 0)
                        {
                            lblErr.Text = "Record saved successfully";
                            Reset();
                        }
                        else
                        {
                            lblErr.Text = "Sorry! Couldn't save data. Please try again later!";
                        }
                    }
                }
            }
            else
            {
                if (DocumentBAL.EditSaveDocument(Convert.ToInt32(ddlDocumentType.SelectedValue), txtDocumentName.Text, Convert.ToInt32(hdnPort.Value), ddlScope.SelectedValue, DocId) > 0)
                {
                    lblErr.Text = "Record updated successfully";
                    Response.Redirect("~/Transaction/ManageDocument.aspx");
                }
                else
                {
                    lblErr.Text = "Sorry! Couldn't save data. Please try again!";
                }
            }
        }

        private void Reset() 
        {
            txtDocumentName.Text = string.Empty;
            txtPort.Text = string.Empty;
            ddlDocumentType.SelectedIndex = 0;
            hdnPort.Value = "";
        }

        private bool ValidateSave(string path)
        {
            bool IsValid = true;

            if (ddlDocumentType.SelectedIndex == 0 || string.IsNullOrEmpty(txtDocumentName.Text) || string.IsNullOrEmpty(hdnPort.Value) || string.IsNullOrEmpty(path) || path.Length == 0)
            {
                IsValid = false;
                lblErr.Text = "Please provide fields properly";
            }
            return IsValid;
        }

        private void LoadForEdit(int DoclId)
        {
            var src = DocumentBAL.GetDocuments(DocumentID: DoclId);
            txtDocumentName.Text = Convert.ToString(src.Tables[0].Rows[0]["DocumentName"]);
            ddlDocumentType.SelectedValue = Convert.ToString(src.Tables[0].Rows[0]["fk_DocumentTypeID"]);
            ddlScope.SelectedValue = Convert.ToString(src.Tables[0].Rows[0]["Scope"]);
            //ViewState["PortID"] = Convert.ToInt32(src.Tables[0].Rows[0]["fk_PortID"]);
            fileUpload.Visible = false;
            lblUpload.Visible = false;
            hdnPort.Value = Convert.ToString(src.Tables[0].Rows[0]["fk_PortID"]);
            rfvFileUpload.Enabled = false;

            string port = new TransactionBLL().GetPortNameById(Convert.ToInt32(src.Tables[0].Rows[0]["fk_PortID"]));

            ((TextBox)txtPort.FindControl("txtPort")).Text = port;

            fileUpload.Enabled = false;

        }
    }
}
