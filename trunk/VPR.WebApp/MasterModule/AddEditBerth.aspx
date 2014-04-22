<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditBerth.aspx.cs" Inherits="VPR.WebApp.MasterModule.AddEditBerth" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="VPR.WebApp" Namespace="VPR.WebApp.CustomControls" TagPrefix="cc2" %>
<%@ Register Src="~/CustomControls/AC_Port.ascx" TagName="AC_Port" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="headercaption">
        ADD / EDIT BERTH</div>
    <center>
        <div style="width: 100%">
            <fieldset style="width: 80%;">
                <legend>Add / Edit Berth</legend>
                <asp:UpdatePanel ID="upExportBL" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div>
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 51%">
                                <tr>
                                    <td colspan="2" style="padding-top: 10px;">
                                        <table border="0" cellpadding="1" cellspacing="0" width="100%" class="custtable">
                                            <tr>
                                                <td>
                                                    Berth Name:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBerthName" runat="server" CssClass="textboxuppercase" MaxLength="100"
                                                        Width="250px" TabIndex="2"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvMailBody" runat="server" ControlToValidate="txtBerthName"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>
                                                    Port:
                                                </td>
                                                <td>
                                                    <uc1:AC_Port ID="txtPort" runat="server" />
                                                    <asp:Label ID="errPort" runat="server" CssClass="errormessage1"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td colspan="2" style="padding-top: 10px;">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" 
                                            TabIndex="70" onclick="btnSave_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnBack" runat="server" CssClass="button" TabIndex="3" OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;"
                                            Text="Back" />
                                        <br />
                                        <asp:Label ID="lblErr" runat="server" CssClass="errormessage"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
            <asp:UpdateProgress ID="uProgressBL" runat="server" AssociatedUpdatePanelID="upExportBL">
                <ProgressTemplate>
                    <div class="progress">
                        <div id="image">
                            <img src="../Images/PleaseWait.gif" alt="" /></div>
                        <div id="text">
                            Please Wait...</div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </center>
</asp:Content>
