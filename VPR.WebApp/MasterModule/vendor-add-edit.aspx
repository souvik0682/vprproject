<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vendor-add-edit.aspx.cs"
    MasterPageFile="~/Site.Master" Inherits="VPR.WebApp.MasterModule.vendor_add_edit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div>
        <div id="headercaption">
            ADD / EDIT VENDOR</div>
        <center>
            <fieldset style="width: 600px;">
                <legend>Add / Edit Vendor</legend>
                <table border="0" cellpadding="2" cellspacing="3">
                    
                    <tr>
                        <td>
                            Name<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" Width="250" Style="text-transform: uppercase;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Please enter name"
                                Display="None" ControlToValidate="txtName" ValidationGroup="vgVendor"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="rfvName"
                                WarningIconImageUrl="">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Address Line1 :
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddress1" runat="server" TextMode="MultiLine" Width="300" Style="text-transform: uppercase;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Address Line1 :
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddress2" runat="server" TextMode="MultiLine" Width="300" Style="text-transform: uppercase;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            City :
                        </td>
                        <td>
                            <asp:TextBox ID="txtCity" runat="server" Width="300" Style="text-transform: uppercase;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            State :
                        </td>
                        <td>
                            <asp:TextBox ID="txtState" runat="server" Width="300" Style="text-transform: uppercase;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Country Code :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCountry" runat="server" Width="255" Enabled="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Phone :
                        </td>
                        <td>
                            <asp:TextBox ID="txtPhone" runat="server" Width="300" Style="text-transform: uppercase;"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            Mobile No :
                        </td>
                        <td>
                            <asp:TextBox ID="TxtMob" runat="server" Width="300" Style="text-transform: uppercase;"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            e-Mail ID :
                        </td>
                        <td>
                            <asp:TextBox ID="TxtEmail" runat="server" Width="300" Style="text-transform: uppercase;"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnVendorID" runat="server" Value="0" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="vgVendor" />&nbsp;&nbsp;<asp:Button
                                ID="btnBack" runat="server" CssClass="button" Text="Back" ValidationGroup="vgUnknown"
                                OnClick="btnBack_Click" OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;" /><asp:Label
                                    ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </center>
    </div>
</asp:Content>
