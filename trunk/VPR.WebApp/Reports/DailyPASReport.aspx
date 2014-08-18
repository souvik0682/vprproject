<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DailyPASReport.aspx.cs" Inherits="VPR.WebApp.Reports.DailyPASReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/CustomControls/AC_Port.ascx" TagName="AC_Port" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <center>

        <div id="headercaption">
           Daily PAS Report
        </div>
        <center>
            <asp:Label ID="lblError" runat="server" Text="Port of Discharge cannot be left blank"
                Style="color: Red; display: none"></asp:Label>
            <fieldset style="width: 1020px;">
                <legend> DAILY PAS REPORT </legend>
                <table width="100%">
                    <tr>
                    
                        <td>
                            Activity Start:<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMovementDate1" runat="server" Width="250px"></asp:TextBox>
                            <cc1:CalendarExtender ID="ceMovementDate" TargetControlID="txtMovementDate1" runat="server"
                                Format="dd-MM-yyyy" Enabled="True" />
                            
                        </td>
                        <td>
                            Activity End:<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMovementDate2" runat="server" Width="250px"></asp:TextBox>
                            <cc1:CalendarExtender ID="ceMovementDate1" TargetControlID="txtMovementDate2" runat="server"
                                Format="dd-MM-yyyy" Enabled="True" />
                        </td>
                        
                    </tr>
                    <tr>
                        <td style="padding-right:15px;">Select Cargo:<span class="errormessage">*</span></td> 
                        <td>                                        
                            <asp:DropDownList ID="ddlCargo" runat="server"  Width="150" AutoPostBack="true" onselectedindexchanged="ddlCargo_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="padding-right:15px;">Select Country:<span class="errormessage">*</span></td> 
                        <td>                                        
                            <asp:DropDownList ID="ddlCountry" runat="server"  Width="150" AutoPostBack="true" 
                                onselectedindexchanged="ddlCountry_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>          
                    <tr>     
                        <td style="padding-right:15px;">Port:<span class="errormessage">*</span></td> 
                        <td>
                            <uc1:AC_Port ID="txtPort" runat="server" TabIndex="4"/>
                            <asp:Label ID="errPort" runat="server" CssClass="errormessage1"></asp:Label>
                        </td>
                        <td style="padding-right:15px;">Vessel Status:<span class="errormessage">*</span></td> 
                        <td>
                            <asp:DropDownList ID="ddlActivity" runat="server" TabIndex="1">
                                <asp:ListItem Value="O" Text="Open" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="S" Text="Sailed"></asp:ListItem>
                                <asp:ListItem Value="A" Text="All"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                     </tr>  
                     <tr>
<%--                        <td>                                        
                            <asp:DropDownList ID="ddlPort" runat="server"  Width="150" AutoPostBack="true" 
                                onselectedindexchanged="ddlPort_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>--%>
                       
<%--                        <td><asp:Button ID="Button1" runat="server" Text="Show" CssClass="button" ValidationGroup="Show" OnClick="btnShow_Click" /></td>--%>
                        <%--<td><asp:Button ID="Button1" runat="server" Text="Show" CssClass="button" ValidationGroup="Show" OnClick="btnShow_Click" /></td>--%>
 
          
                        <td style="text-align: right; width: 5%;">
                            <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Generate Excel"  />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td><asp:RequiredFieldValidator ID="rfvMovementDate" runat="server" CssClass="errormessage" ControlToValidate="txtMovementDate1" Display="Dynamic" ForeColor="" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator></td>
                        <td><asp:RequiredFieldValidator ID="rfvMovementDate1" runat="server" CssClass="errormessage" ControlToValidate="txtMovementDate2" Display="Dynamic" ForeColor="" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator></td>

                    </tr>
                 </table>
            </fieldset>
<%--            <fieldset id="Fieldset1" style="width:1020px;" runat="server">
                <legend> DAILY PAS REPORT</legend>
                <div style="padding: 10px;">
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                      <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%" Font-Names="Verdana" Visible="false"
                            Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt"></rsweb:ReportViewer>
                </div>
            </fieldset>--%>
    </center>
</asp:Content>
