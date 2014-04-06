﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vendor-add-edit.aspx.cs"
    MasterPageFile="~/Site.Master" Inherits="VPR.WebApp.MasterModule.vendor_add_edit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div>
        <div id="headercaption">
            ADD / EDIT VENDOR</div>
        <center>
            <fieldset style="width: 400px;">
                <legend>Add / Edit Vendor</legend>
                <table border="0" cellpadding="2" cellspacing="3">
                    <tr>
                        <td style="width: 140px;">
                            Type<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlVendorType" runat="server" Width="255" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlVendorType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvType" runat="server" ErrorMessage="Please select vendor type"
                                Display="None" ControlToValidate="ddlVendorType" ValidationGroup="vgVendor" InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvType"
                                WarningIconImageUrl="">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Location ID<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLocationID" runat="server" Width="255" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlLocationID_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ErrorMessage="Please select location"
                                Display="None" ControlToValidate="ddlLocationID" ValidationGroup="vgVendor" InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvLocation"
                                WarningIconImageUrl="">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Salutation<span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSalutation" runat="server" Width="255">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSalutation" runat="server" ErrorMessage="Please select salutation"
                                Display="None" ControlToValidate="ddlSalutation" ValidationGroup="vgVendor" InitialValue="0"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvSalutation"
                                WarningIconImageUrl="">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
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
                            Address :
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="250" Style="text-transform: uppercase;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Customs Code <span class="errormessage1">*</span> :
                        </td>
                        <td>
                            <asp:TextBox ID="txtCfsCode" runat="server" Width="250" Enabled="false" Style="text-transform: uppercase;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCfdCode" runat="server" ErrorMessage="Please enter CFS Code"
                                ControlToValidate="txtCfsCode" Display="None" ValidationGroup="vgVendor" Enabled="false"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="rfvCfdCode">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Terminal Code :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTerminalCode" runat="server" Width="255" Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>

                     <tr>
                        <td>
                            Contact Person :
                        </td>
                        <td>
                            <asp:TextBox ID="TxtCP" runat="server" Width="250" Style="text-transform: uppercase;"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            Mobile No :
                        </td>
                        <td>
                            <asp:TextBox ID="TxtMob" runat="server" Width="250" Style="text-transform: uppercase;"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            e-Mail ID :
                        </td>
                        <td>
                            <asp:TextBox ID="TxtEmail" runat="server" Width="250" Style="text-transform: uppercase;"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            PAN :
                        </td>
                        <td>
                            <asp:TextBox ID="TxtPAN" runat="server" Width="250" Style="text-transform: uppercase;"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            TAN :
                        </td>
                        <td>
                            <asp:TextBox ID="TxtTAN" runat="server" Width="250" Style="text-transform: uppercase;"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            IEC :
                        </td>
                        <td>
                            <asp:TextBox ID="TxtIEC" runat="server" Width="250" Style="text-transform: uppercase;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            BIN :
                        </td>
                        <td>
                            <asp:TextBox ID="TxtBIN" runat="server" Width="250" Style="text-transform: uppercase;"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            Bank Name :
                        </td>
                        <td>
                            <asp:TextBox ID="TxtBankName" runat="server" Width="250" Style="text-transform: uppercase;"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            Account No :
                        </td>
                        <td>
                            <asp:TextBox ID="TxtACNo" runat="server" Width="250" Style="text-transform: uppercase;"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            A/c Type :
                        </td>
                        <td>
                            <asp:TextBox ID="TxtAcType" runat="server" Width="250" Style="text-transform: uppercase;"></asp:TextBox>
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
