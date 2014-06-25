<%@ Page Title=":: VPR :: Cargo Wise Vessel" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rptCargoWeekly.aspx.cs" Inherits="VPR.WebApp.Reports.rptCargoWeekly" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Scripts/Common.js" type="text/javascript"></script>
    <script type="text/javascript">
        LoadScript();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="headercaption">PERIODIC VESSEL SUMMARY</div>
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
            <legend>Search Criteria</legend>
            <div style="padding:10px;">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="padding-right:15px;">Start Date:<span class="errormessage">*</span></td>
                        <td style="padding-right:30px;">
                            <asp:TextBox ID="txtFromDt" runat="server" Width="150" MaxLength="10"></asp:TextBox>
                            <cc1:calendarextender ID="ceFromDt" runat="server" TargetControlID="txtFromDt"></cc1:calendarextender>                                                                        
                        </td>
                        <td style="padding-right:15px;">End Date:<span class="errormessage">*</span></td>
                        <td style="padding-right:10px;">
                            <asp:TextBox ID="txtToDt" runat="server" Width="150" MaxLength="10"></asp:TextBox>
                            <cc1:calendarextender ID="ceToDt" runat="server" TargetControlID="txtToDt"></cc1:calendarextender>   
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlActivity" runat="server" TabIndex="1">
                                <asp:ListItem Value="0" Text="All" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="D" Text="Discharge"></asp:ListItem>
                                <asp:ListItem Value="L" Text="Load"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-right:15px;">Select Cargo Group:<span class="errormessage">*</span></td> 
                        <td>                                        
                            <asp:DropDownList ID="ddlCargoGroup" runat="server"  Width="150" AutoPostBack="true" onselectedindexchanged="ddlCargoGroup_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                       
                        <td style="padding-right:15px;">Select Country:<span class="errormessage">*</span></td> 
                        <td>                                        
                            <asp:DropDownList ID="ddlCountry" runat="server" Width="150" AutoPostBack="true" 
                                onselectedindexchanged="ddlCountry_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                      
                    </tr>
                    <tr>
                        <td style="padding-right:15px;">Select Cargo:<span class="errormessage">*</span></td> 
                        <td>                                        
                            <asp:DropDownList ID="ddlCargo" runat="server" Width="150" AutoPostBack="true" onselectedindexchanged="ddlCargo_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="padding-right:15px;">Select Port:<span class="errormessage">*</span></td> 
                        <td>                                        
                            <asp:DropDownList ID="ddlPort" runat="server"  Width="150" AutoPostBack="true" 
                                onselectedindexchanged="ddlPort_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td><asp:Button ID="btnShow" runat="server" Text="Show" CssClass="button" ValidationGroup="Show" OnClick="btnShow_Click" /></td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                        <td><asp:RequiredFieldValidator ID="rfvFromDt" runat="server" CssClass="errormessage" ControlToValidate="txtFromDt" Display="Dynamic" ForeColor="" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator></td>
                        <td>&nbsp;</td>
                        <td colspan="2"><asp:RequiredFieldValidator ID="rfvToDt" runat="server" CssClass="errormessage" ControlToValidate="txtToDt" Display="Dynamic" ForeColor="" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator></td>
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
