<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VPRwithParameter.aspx.cs" Inherits="VPR.WebApp.Reports.VPRwithParameter" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 21%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
<center>

        <div id="headercaption">
           Total Vessel Position
        </div>
        <center>
            <asp:Label ID="lblError" runat="server" Text="Port of Discharge cannot be left blank"
                Style="color: Red; display: none"></asp:Label>
            <fieldset style="width: 1020px;">
                <legend> Total Vessel Position </legend>
                <table width="100%">
                    <tr>
                        <td class="style1">
                            Activity Status:<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlActivity" runat="server" TabIndex="1">
<%--                                <asp:ListItem Value="0" Text="All" Selected="True"></asp:ListItem>--%>
                                <asp:ListItem Value="E" Text="Expected Arrival"></asp:ListItem>
                                <asp:ListItem Value="B" Text="Awaiting Berth"></asp:ListItem>
                                <asp:ListItem Value="D" Text="Discharging"></asp:ListItem>
                                <asp:ListItem Value="L" Text="Loading"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            Port:<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPort" runat="server" 
                                onselectedindexchanged="ddlPort_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                    </tr>
     
                    <tr>            
                        <td style="text-align: right; " class="style1">
                            <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show Report"  />
                        </td>
                    </tr>
                 </table>
            </fieldset>
            <fieldset id="Fieldset1" style="width:1020px;" runat="server">
                <legend> Vessel Position</legend>
                <div style="padding: 10px;">
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                      <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%" Font-Names="Verdana" Visible="false"
                            Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt"></rsweb:ReportViewer>
                </div>
            </fieldset>
    </center>
</asp:Content>
