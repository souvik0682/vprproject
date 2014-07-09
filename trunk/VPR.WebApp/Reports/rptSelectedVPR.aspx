<%@ Page Title=":: VPR :: Cargo Wise Vessel" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rptSelectedVPR.aspx.cs" Inherits="VPR.WebApp.Reports.rptSelectedVPR" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/CustomControls/AC_Port.ascx" TagName="AC_Port" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Scripts/Common.js" type="text/javascript"></script>
    <script type="text/javascript">
        LoadScript();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="headercaption">SELECTED VPR</div>
    <div id="dvSync" runat="server" style="padding: 5px 0px 5px 0px; display: none;">
        <table width="100%" class="synpanel">
            <tr>
                <td>
                    <div id="dvErrMsg" runat="server"></div>
                </td>
                <td style="text-align: right; width: 2%;">
                    <img alt="Click to close" src="../../Images/Close-Button.bmp" title="Click to close" onclick="closeErrorPanel()" />
                </td>
            </tr>
        </table>
    </div>
    <div style="width:900px;margin: 0 auto;">
        <fieldset style="width: 878px;">
            <legend>Report Criteria</legend>
            <div style="padding:10px;">
                <table border="0" cellpadding="0" cellspacing="0">
                    
                     <tr>
                        <td style="padding-right:30px;">Select Activity:<span class="errormessage">*</span></td> 
                        <td>                                        
                            <asp:DropDownList ID="ddlActivity" runat="server"  Width="150" TabIndex="1">
                                <asp:ListItem Value="E" Text="Expected Arrival"></asp:ListItem>
                                <asp:ListItem Value="A" Text="Awaiting Berth"></asp:ListItem>
                                <asp:ListItem Value="D" Text="Discharging"></asp:ListItem>
                                <asp:ListItem Value="L" Text="Loading"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="padding-right:30px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Select Port:<span class="errormessage">*</span></td> 
                        <%--<td>                                        
                            <asp:DropDownList ID="ddlPort" runat="server"  Width="150" AutoPostBack="true" 
                                onselectedindexchanged="ddlPort_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>--%>
                        <td>
                            <uc1:AC_Port ID="txtPort" runat="server" TabIndex="4"/>
                            <asp:Label ID="errPort" runat="server" CssClass="errormessage1"></asp:Label>
                        </td>
                        <td><asp:Button ID="btnShow" runat="server" Text="Show" CssClass="button" 
                                ValidationGroup="Show" OnClick="btnShow_Click" Width="71px" /></td>
                    </tr>
                   
                </table>
            </div>
        </fieldset>
    </div>
    <div class="reportpanel" style="width:894px;margin:0 auto;">
        <rsweb:ReportViewer ID="rptViewer" runat="server" CssClass="rptviewer" Width="100%">
        </rsweb:ReportViewer>
    </div>
</asp:Content>
