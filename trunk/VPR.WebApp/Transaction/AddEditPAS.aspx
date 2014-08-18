<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditPAS.aspx.cs" Inherits="VPR.WebApp.Transaction.AddEditPAS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="VPR.WebApp" Namespace="VPR.WebApp.CustomControls" TagPrefix="cc2" %>
<%@ Register Src="~/CustomControls/AC_Port.ascx" TagName="AC_Port" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="headercaption">
        ADD / EDIT PAS</div>
    <center>
        <div style="width: 100%">
            <fieldset style="width: 80%;">
                <legend>Add / Edit PAS</legend>
                <asp:UpdatePanel ID="upExportBL" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td colspan="2" style="padding-top: 10px;">
                                        <table border="0" cellpadding="1" cellspacing="0" width="100%" class="custtable">
                                            <tr>
                                                <td>
                                                    Activity Date:<span class="errormessage1">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMovementDate" runat="server" Width="250px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtMovementDate" runat="server"
                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                    <asp:RequiredFieldValidator ID="rfvMovementDate" runat="server" ControlToValidate="txtMovementDate"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    Vessel:<span class="errormessage1">*</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlVessel" runat="server" TabIndex="1" AutoPostBack = "true"
                                                        onselectedindexchanged="ddlVessel_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>
                                                    Vessel Activity:<span class="errormessage1">*</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlActivity" runat="server" TabIndex="1">
                                                        <asp:ListItem Value="D" Text="Discharge" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="L" Text="Load"></asp:ListItem>
                                                        <asp:ListItem Value="B" Text="Load & Discharge"></asp:ListItem>
                                                        <asp:ListItem Value="O" Text="Others"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>


                                                 <td>
                                                    Port:<span class="errormessage1">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPortName" runat="server" Width="250px" MaxLength="100" CssClass="textboxuppercase"></asp:TextBox>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>
                                                    Movement:<span class="errormessage1">*</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlMovement" runat="server" TabIndex="1">
      <%--                                                  <asp:ListItem Value="1" Text="Arrival" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Berthing"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Discharge"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Load"></asp:ListItem>
                                                        <asp:ListItem Value="5" Text="Sailed"></asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </td>
 <%--                                               <td>
                                                    Movement Type:<span class="errormessage1">*</span>
                                                </td>
                                                <td>
                                                     <asp:DropDownList ID="ddlMovementType" runat="server" TabIndex="1">
                                                        <asp:ListItem Value="E" Text="Expected" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="A" Text="Actual"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>--%>
                                                
                                            </tr>

                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-top: 10px;">
                                        <asp:GridView ID="gvwCargo" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="3" DataKeyNames="CargoVesselId" OnRowDataBound="gvwCargo_RowDataBound">
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Cargo" HeaderStyle-Width="300">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlCargo" runat="server">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="ActType" HeaderStyle-Width="80">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlActType" runat="server">
                                                        <asp:ListItem Value="D" Text="Discharge" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="L" Text="Load"></asp:ListItem>
                                                        <asp:ListItem Value="N" Text="None"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty Loaded" SortExpression="Quantity" HeaderStyle-Width="100">
                                                    <ItemTemplate>
                                                        <cc2:CustomTextBox ID="txtQuantity" runat="server" Text='<%# Bind("Quantity", "{0:n3}") %>'
                                                            Style="text-align: right;" Width="80" BorderStyle="None" MaxLength="12" AutoPostBack="true"
                                                            Precision="8" Scale="3" Type="Decimal">
                                                        </cc2:CustomTextBox>
                                                        <asp:RequiredFieldValidator ID="rfvQuantiry" runat="server" ControlToValidate="txtQuantity"
                                                            Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                        <asp:HiddenField ID="hdnCargoVesselId" runat="server" Value='<%# Eval("CargoVesselId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="IsDeleted" HeaderText="Is Deleted" InsertVisible="False"
                                                    ReadOnly="True" SortExpression="IsDeleted" HeaderStyle-Width="120" Visible="false" />
                                                <%--<asp:TemplateField HeaderText="Delete">
                                                    <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnRemove" runat="server" ImageUrl="~/Images/remove.png" Height="16"
                                                            Width="16" OnClick="btnRemove_Click" />
                                                        <asp:HiddenField ID="hdnCargoVesselId" runat="server" Value='<%# Eval("CargoVesselId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <RowStyle ForeColor="#000066" />
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>
                                        <br />
<%--                                        <asp:Button ID="btnAddNew" runat="server" Text="Add New Row" 
                                            onclick="btnAddNew_Click" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-top: 10px;">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" 
                                            TabIndex="70" onclick="btnSave_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnBack" runat="server" CssClass="button" TabIndex="71" OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;"
                                            Text="Back" onclick="btnBack_Click" />
                                        <br />
                                        <asp:Label ID="lblErr" runat="server" CssClass="errormessage"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
            <asp:UpdateProgress ID="uProgressBL" runat="server" AssociatedUpdatePanelID="upExportBL">
                <ProgressTemplate>
                    <div class="progress">
                        <div id="image">
                            <img src="../Images/PleaseWait.gif" alt="" /></div>
                        <div id="text">
                            Please Wait...</div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </center>
</asp:Content>

