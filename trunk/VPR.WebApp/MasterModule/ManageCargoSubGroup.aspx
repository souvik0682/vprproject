﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageCargoSubGroup.aspx.cs" Inherits="VPR.WebApp.MasterModule.ManageCargoSubGroup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
 <div id="dvAsync" style="padding: 5px; display: none;">
        <div class="asynpanel">
            <div id="dvAsyncClose">
                <img alt="" src="../../Images/Close-Button.bmp" style="cursor: pointer;" onclick="ClearErrorState()" /></div>
            <div id="dvAsyncMessage">
            </div>
        </div>
    </div>
    <div id="headercaption">MANAGE SUB GROUP</div>
    <center>
    <div style="width:850px;">        
        <fieldset style="width:100%;">
            <legend>Search Sub Group</legend>
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="txtCargoGroup" runat="server" CssClass="watermark" ForeColor="#747862" ></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtWMEPrdGroup" runat="server" TargetControlID="txtCargoGroup" WatermarkText="Type Cargo Group" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSubGroup" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtWMESubGroup" runat="server" TargetControlID="txtSubGroup" WatermarkText="Type Cargo Sub Group" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                    </td>
                    <td><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" Width="100px" OnClick="btnSearch_Click" />
                     <asp:Button ID="btnRefresh" runat="server" Text="Reset" CssClass="button" Width="100px" onclick="btnRefresh_Click"  /></td>
                </tr>
            </table>              
        </fieldset>
        <asp:UpdateProgress ID="uProgressLoc" runat="server" AssociatedUpdatePanelID="upLoc">
            <ProgressTemplate>
                <div class="progress">
                    <div id="image">
                        <img src="../../Images/PleaseWait.gif" alt="" /></div>
                    <div id="text">
                        Please Wait...</div>
                </div>
            </ProgressTemplate>        
        </asp:UpdateProgress>
        <fieldset id="fsList" runat="server" style="width:100%;min-height:100px;">
            <legend>Sub Group List</legend>
            <div style="float:right;padding-bottom:5px;margin-top: -10px">
                Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="30" Value="30" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>&nbsp;&nbsp;
                <asp:Button ID="btnAdd" runat="server" Text="Add New Group" Width="130px" OnClick="btnAdd_Click" />
            </div>
          <br />            
            <div>
                <asp:UpdatePanel ID="upLoc" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="ddlPaging" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                    <asp:Label runat="server" ID="lblErrorMsg" Text=""></asp:Label>
                        <asp:GridView ID="gvwLoc" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                BorderStyle="None" BorderWidth="0" OnPageIndexChanging="gvwLoc_PageIndexChanging" AllowSorting="true" onsorting="gvwLoc_Sorting"
                OnRowDataBound="gvwLoc_RowDataBound" OnRowCommand="gvwLoc_RowCommand" Width="100%">
                <pagersettings mode="NumericFirstLast" position="TopAndBottom" />
                <pagerstyle cssclass="gridviewpager" />
                <emptydatarowstyle cssclass="gridviewemptydatarow" />
                <emptydatatemplate>No Cargo Sub Group(s) Found</emptydatatemplate>
                <columns>
                                <asp:TemplateField HeaderText="Sl#">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="5%" />                                    
                                </asp:TemplateField>
                              
                            
                                <asp:TemplateField HeaderText="id" Visible="false">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="2%" />                                       
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sub Group Name" SortExpression="CargoGroupName">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="20%" />                                       
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Group" SortExpression="CargoSubGroupName">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="20%" />                                       
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />                                    
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit.png" Height="16" Width="16" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />                                    
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/remove.png" Height="16" Width="16" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </fieldset>
    </div>
    </center>
</asp:Content>
