<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageEmail.aspx.cs" Inherits="VPR.WebApp.MasterModule.ManageEmail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <div id="headercaption">
        MANAGE EMAIL</div>
    <center>
        <div style="width: 850px; ">
            <fieldset style="width: 100%;">
                <legend>Search</legend>
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                                FilterMode="InvalidChars" ValidChars=" " TargetControlID="txtName">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtName"
                                WatermarkText="Name">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmailId" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                                FilterMode="InvalidChars" ValidChars=" " TargetControlID="txtEmailId">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtEmailId"
                                WatermarkText="Email ID">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCargoGroup" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                                FilterMode="InvalidChars" ValidChars=" " TargetControlID="txtCargoGroup">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtCargoGroup"
                                WatermarkText="Cargo Group">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        
                        <td>
                            <asp:TextBox ID="txtCompany" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                                FilterMode="InvalidChars" ValidChars=" " TargetControlID="txtCompany">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" runat="server" TargetControlID="txtCompany"
                                WatermarkText="Company">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCategory" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                                FilterMode="InvalidChars" ValidChars=" " TargetControlID="txtCompany">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtCategory"
                                WatermarkText="Category">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
<%--                    </tr>
                    
                    <tr>--%>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" 
                                Width="100px" onclick="btnSearch_Click"/>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button" 
                                Width="100px" onclick="btnReset_Click"/>
                        </td>
                    </tr>
                    </table>
            </fieldset>
            <asp:UpdateProgress ID="uProgressLoc" runat="server" AssociatedUpdatePanelID="upBL">
                <ProgressTemplate>
                    <div class="progress">
                        <div id="image">
                            <img src="../../Images/PleaseWait.gif" alt="" /></div>
                        <div id="text">
                            Please Wait...</div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <fieldset id="fsList" runat="server" style="width: 100%; min-height: 100px;">
                <legend>Email List</legend>
                <div style="float: right; padding-bottom: 5px;">
                    Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="30" Value="30" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="Add New Email" Width="150"
                        OnClick="btnAdd_Click" />
                </div>
                <div>
                    <span class="errormessage">&nbsp;</span>
                </div>
                <br />
                <div>
                    <asp:UpdatePanel ID="upBL" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="ddlPaging" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:GridView ID="gvImportBL" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                BorderStyle="None" BorderWidth="0" Width="100%" 
                                onpageindexchanging="gvImportBL_PageIndexChanging" 
                                onrowcommand="gvImportBL_RowCommand" onrowdatabound="gvImportBL_RowDataBound">
                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                <PagerStyle CssClass="gridviewpager" />
                                <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                <EmptyDataTemplate>
                                    No Record(s) Found</EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="12%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkName" runat="server" CommandName="Sort" CommandArgument="Name" Text="Name"></asp:LinkButton>
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="15%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkEmail" runat="server" CommandName="Sort" CommandArgument="EmailId" Text="Email ID"></asp:LinkButton>
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="10%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkCompany" runat="server" CommandName="Sort" CommandArgument="Company" Text="Company"></asp:LinkButton>
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="10%" />
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkCategory" runat="server" CommandName="Sort" CommandArgument="CompanyAbbr" Text="Category"></asp:LinkButton>
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit.png"
                                                Height="16" Width="16" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle CssClass="gridviewheader" />
                                        <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/remove.png"
                                                Height="16" Width="16" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </fieldset>
        </div>
    </center>
</asp:Content>
