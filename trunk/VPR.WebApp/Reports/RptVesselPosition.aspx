<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RptVesselPosition.aspx.cs" Inherits="VPR.WebApp.Reports.RptVesselPosition" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
<center>

        <div id="headercaption">
           Vessel Position
        </div>
        <center>
            <asp:Label ID="lblError" runat="server" Text="Port of Discharge cannot be left blank"
                Style="color: Red; display: none"></asp:Label>
            <fieldset style="width: 1020px;">
                <legend> Vessel Position </legend>
                <table width="100%">
                    <tr>
                                                
                        <td style="text-align: right; width: 5%;">
                            <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show Report"  />
                        </td>
                    </tr>
                 </table>
            </fieldset>
            <fieldset style="width:1020px;" runat="server">
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
