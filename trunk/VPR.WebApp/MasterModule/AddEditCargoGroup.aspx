﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditCargoGroup.aspx.cs" Inherits="VPR.WebApp.MasterModule.AddEditCargoGroup" %>

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
        ADD / EDIT CARGO GROUP</div>
    <center>
        <fieldset style="width:600px;">
            <legend>Add / Edit Port</legend>
            <table border="0" cellpadding="2" cellspacing="3">
                <tr>
                    <td class="style1">Cargo Group Name:<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtCargoGroupName" runat="server" CssClass="textboxuppercase" MaxLength="50" Width="250"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="errormessage" 
                            ControlToValidate="txtCargoGroupName" Display="Dynamic" Text="This field is Required" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>

                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button 
                            ID="btnBack" runat="server" CssClass="button" Text="Back" 
                            onclick="btnBack_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </center>
</asp:Content>