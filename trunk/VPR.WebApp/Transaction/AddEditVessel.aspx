<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditVessel.aspx.cs" Inherits="VPR.WebApp.Transaction.AddEditVessel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="VPR.WebApp" Namespace="VPR.WebApp.CustomControls" TagPrefix="cc2" %>
<%@ Register Src="~/CustomControls/AC_Port.ascx" TagName="AC_Port" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="headercaption">
        ADD / EDIT VESSEL</div>
    <center>
        <div style="width: 100%">
            <fieldset style="width: 80%;">
                <legend>Add / Edit Vessel</legend>
                <asp:UpdatePanel ID="upExportBL" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td colspan="2" style="padding-top: 10px;">
                                        <table border="0" cellpadding="1" cellspacing="0" width="100%" class="custtable">
                                            <tr>
                                                <td>
                                                    Activity:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAcivity" runat="server" TabIndex="1" AutoPostBack = "true"
                                                        onselectedindexchanged="ddlAcivity_SelectedIndexChanged">
                                                        <asp:ListItem Value="L" Text="Load" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="D" Text="Discharge"></asp:ListItem>
                                                        <asp:ListItem Value="B" Text="Load & Discharge"></asp:ListItem>
                                                        <asp:ListItem Value="O" Text="Others"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    Vessel Name:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtVesselName" runat="server" CssClass="textboxuppercase" MaxLength="100"
                                                        Width="250px" TabIndex="2"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvMailBody" runat="server" ControlToValidate="txtVesselName"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Voyage No:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtVoyageNo" runat="server" Width="250px" CssClass="textboxuppercase"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="rfvGroupName" runat="server" ControlToValidate="txtVoyageNo"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <asp:Label ID="lblVoyage" runat="server" CssClass="errormessage1"></asp:Label>--%>
                                                </td>
                                                <td>
                                                    Port:
                                                </td>
                                                <td>
                                                    <uc1:AC_Port ID="txtPort" runat="server" />
                                                    <asp:Label ID="errPort" runat="server" CssClass="errormessage1"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Previous Port:
                                                </td>
                                                <td>
                                                    <uc1:AC_Port ID="txtPreviousPort" runat="server" />
                                                    <asp:Label ID="errPreviousPort" runat="server" CssClass="errormessage1"></asp:Label>
                                                </td>

                                                <td>
                                                    Next Port:
                                                </td>
                                                <td>
                                                    <uc1:AC_Port ID="txtNextPort" runat="server" />
                                                    <asp:Label ID="errNextPort" runat="server" CssClass="errormessage1"></asp:Label>
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                                <td>
                                                    ETA:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtArrivalDate" runat="server" Width="250px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="cbeArrivalDate" TargetControlID="txtArrivalDate" runat="server"
                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                    <asp:RequiredFieldValidator ID="rfvMailSendOn" runat="server" ControlToValidate="txtArrivalDate"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    ETC:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtETC" runat="server" Width="250px" Enabled="False"></asp:TextBox>
                                                    <%--<cc1:CalendarExtender ID="cbeETC" TargetControlID="txtETC" runat="server" Format="dd-MM-yyyy"
                                                        Enabled="True" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtETC"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                                </td>
                                            </tr>
                                            <tr>

                                                 <td>
                                                    LOA:
                                                </td>
                                                <td>
                                                    <cc2:CustomTextBox ID="txtLOA" runat="server" CssClass="numerictextbox" TabIndex="13"
                                                        Width="250px" Type="Decimal" MaxLength="15" Precision="12" Scale="2"></cc2:CustomTextBox>
                                                </td>
                                                <td>
                                                    Owner Name:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOwnerName" runat="server" Width="250px" CssClass="textboxuppercase"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtOwnerName"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Agent Name:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAgentName" runat="server">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="errormessage"
                                                        ErrorMessage="This field is required" ControlToValidate="ddlAgentName" InitialValue="0"
                                                        ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>

                                               
                                                <td>
                                                    Remarks:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="textboxuppercase" MaxLength="100"
                                                        Width="250px" TabIndex="2"></asp:TextBox>
<%--                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRemarks"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                                </td>
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
                                                        <asp:ListItem Value="N" Text="None" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="D" Text="Discharge"></asp:ListItem>
                                                        <asp:ListItem Value="L" Text="Loading"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity" SortExpression="Quantity" HeaderStyle-Width="100">
                                                    <ItemTemplate>
                                                        <cc2:CustomTextBox ID="txtQuantity" runat="server" Text='<%# Bind("Quantity", "{0:n3}") %>'
                                                            Style="text-align: right;" Width="80" BorderStyle="None" MaxLength="12" AutoPostBack="true"
                                                            Precision="8" Scale="3" Type="Decimal">
                                                        </cc2:CustomTextBox>
                                                        <asp:RequiredFieldValidator ID="rfvQuantiry" runat="server" ControlToValidate="txtQuantity"
                                                            Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="IsDeleted" HeaderText="Is Deleted" InsertVisible="False"
                                                    ReadOnly="True" SortExpression="IsDeleted" HeaderStyle-Width="120" Visible="false" />
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnRemove" runat="server" ImageUrl="~/Images/remove.png" Height="16"
                                                            Width="16" OnClick="btnRemove_Click" />
                                                        <asp:HiddenField ID="hdnCargoVesselId" runat="server" Value='<%# Eval("CargoVesselId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle ForeColor="#000066" />
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>
                                        <br />
                                        <asp:Button ID="btnAddNew" runat="server" Text="Add New Row" 
                                            onclick="btnAddNew_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-top: 10px;">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" 
                                            TabIndex="70" onclick="btnSave_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnBack" runat="server" CssClass="button" TabIndex="71" OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;"
                                            Text="Back" />
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
