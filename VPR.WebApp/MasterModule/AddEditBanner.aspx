<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditBanner.aspx.cs" Inherits="VPR.WebApp.MasterModule.AddEditBanner" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="VPR.WebApp" Namespace="VPR.WebApp.CustomControls" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function SetMaxLength(obj, maxLen) {
            return (obj.value.length < maxLen);
        }    
    </script>
    <style type="text/css">
        .style1
        {
            width: 203px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">
        ADD / EDIT FLASH NEWS</div>
    <center>
        <fieldset style="width:600px;">
            <legend>Add / Edit Flash News</legend>
            <table border="0" cellpadding="2" cellspacing="3">
                <tr>
                    <td class="style1">Flash News :<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtBanner" runat="server" CssClass="textboxuppercase" MaxLength="50" Width="250"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtBanner" Display="Dynamic" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td>
                        Type
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                            <asp:ListItem Value="C" Text="Continuous" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="D" Text="Date Range"></asp:ListItem>
                        </asp:DropDownList>
                    </td>

                </tr>
                <tr>
                    <td>
                        Start Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartDate" runat="server" Width="250px"></asp:TextBox>
                        <cc1:CalendarExtender ID="cbeStartDate" TargetControlID="txtStartDate" runat="server"
                            Format="dd-MM-yyyy" Enabled="true" />
                        <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr>
                    <td>
                        End Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndDate" runat="server" Width="250px"></asp:TextBox>
                        <cc1:CalendarExtender ID="cbeEndDate" TargetControlID="txtEndDate" runat="server"
                            Format="dd-MM-yyyy" Enabled="true" />
                        <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="txtEndDate"
                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Upload File:
                    </td>
                    <td>
                        <asp:FileUpload ID="fileUpload" runat="server"></asp:FileUpload> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="fileUpload"
                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button 
                            ID="btnBack" runat="server" CssClass="button" Text="Back" 
                            onclick="btnBack_Click" />
                        <asp:Label ID="lblErr" runat="server" CssClass="errormessage"></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
    </center>
</asp:Content>
