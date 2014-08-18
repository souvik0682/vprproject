<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEditPASVessel.aspx.cs" Inherits="VPR.WebApp.Transaction.AddEditPASVessel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="VPR.WebApp" Namespace="VPR.WebApp.CustomControls" TagPrefix="cc2" %>
<%@ Register Src="~/CustomControls/AC_Port.ascx" TagName="AC_Port" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="headercaption">
        ADD / EDIT PAS VESSEL</div>
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
                                                    Activity:<span class="errormessage1">*</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAcivity" runat="server" TabIndex="1" AutoPostBack = "true"
                                                        onselectedindexchanged="ddlAcivity_SelectedIndexChanged">
                                                        <asp:ListItem Value="D" Text="Discharge" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="L" Text="Load"></asp:ListItem>
                                                        <asp:ListItem Value="B" Text="Load & Discharge"></asp:ListItem>
                                                        <asp:ListItem Value="O" Text="Others"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    Vessel Name:<span class="errormessage1">*</span>
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
                                                    Job:<span class="errormessage1">*</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlJob" runat="server" TabIndex="3">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvJob" runat="server" CssClass="errormessage"
                                                        ErrorMessage="This field is required" ControlToValidate="ddlJob" InitialValue="0"
                                                        ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    Port:<span class="errormessage1">*</span>
                                                </td>
                                                <td>
                                                    <uc1:AC_Port ID="txtPort" runat="server" TabIndex="4"/>
                                                    <asp:Label ID="errPort" runat="server" CssClass="errormessage1"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Previous Port:<span class="errormessage1">*</span>
                                                </td>
                                                <td>
                                                    <uc1:AC_Port ID="txtPreviousPort" runat="server" TabIndex="5"/>
                                                    <asp:Label ID="errPreviousPort" runat="server" CssClass="errormessage1"></asp:Label>
                                                </td>

                                                <td>
                                                    Next Port:<span class="errormessage1">*</span>
                                                </td>
                                                <td>
                                                    <uc1:AC_Port ID="txtNextPort" runat="server" TabIndex="6"/>
                                                    <asp:Label ID="errNextPort" runat="server" CssClass="errormessage1"></asp:Label>
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                                <td>
                                                    ETA:<span class="errormessage1">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtArrivalDate" runat="server" Width="250px" TabIndex="7"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="cbeArrivalDate" TargetControlID="txtArrivalDate" runat="server"
                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                    <asp:RequiredFieldValidator ID="rfvMailSendOn" runat="server" ControlToValidate="txtArrivalDate"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                               <td>
                                                    Arrival Date:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtActArr" runat="server" Width="250px" TabIndex="8" Enabled="False"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="cbeActArr" TargetControlID="txtActArr" runat="server"
                                                        Format="dd-MM-yyyy" Enabled="true" />
<%--                                                    <asp:RequiredFieldValidator ID="rfvActArr" runat="server" ControlToValidate="txtActArr"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    ETB:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBerthDate" runat="server" Width="250px"  Enabled="true" ></asp:TextBox>
                                                    <cc1:CalendarExtender ID="cbeBerth" TargetControlID="txtBerthDate" runat="server"
                                                        Format="dd-MM-yyyy"/>
       <%--                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBerthDate"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                                </td>
                                                <td>
                                                    Berth Date:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtActBerth" runat="server" Width="250px"  Enabled="False" ></asp:TextBox>
                                                    <cc1:CalendarExtender ID="cbeActBerth" TargetControlID="txtActBerth" runat="server"
                                                        Format="dd-MM-yyyy"/>
       <%--                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBerthDate"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    ETC:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtETC" runat="server" Width="250px" Enabled="true"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="cbeETC" TargetControlID="txtETC" runat="server" Format="dd-MM-yyyy"
                                                        Enabled="True" />
<%--                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtETC"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                                </td>
                                                <td>
                                                    Sail Date:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtActSail" runat="server" Width="250px" Enabled="False"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="cbeActSail" TargetControlID="txtActSail" runat="server" Format="dd-MM-yyyy"
                                                        Enabled="false" />
<%--                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtETC"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    OPA (If Any):
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOPA" runat="server" Width="250px" Enabled="True" CssClass="textboxuppercase"></asp:TextBox>
                                                    <%--<cc1:CalendarExtender ID="cbeETC" TargetControlID="txtETC" runat="server" Format="dd-MM-yyyy"
                                                        Enabled="True" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtETC"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                                </td>
                                            </tr>
                                            <tr>

                                                 <td>
                                                    Nominating Company:<span class="errormessage1">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNom" runat="server" Width="250px" MaxLength="100" CssClass="textboxuppercase"></asp:TextBox>
<%--                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLOA"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic">
                                                     </asp:RequiredFieldValidator>--%>
                                                </td>
                                                <td>
                                                    Country of Nom Company:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlNCountry" runat="server" TabIndex="5"  Width="250px">
                                                     <asp:ListItem Value="0" Text="--None--"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
 
                                            </tr>
                                            <tr>
                                                <td>
                                                    Appointing Company:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAppointing" runat="server" Width="250px" MaxLength="100" CssClass="textboxuppercase"></asp:TextBox>
<%--                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtOwnerName"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                                </td>
                                                <td>
                                                    Country of App Company:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlACountry" runat="server" TabIndex="6"  Width="250px">
                                                    <asp:ListItem Value="0" Text="--None--"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
 
                                            </tr>
                                            <tr>
                                                
                                                <td>
                                                    Shipper/Receiver:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtShipper" runat="server" Width="250px" MaxLength="100" CssClass="textboxuppercase"></asp:TextBox>
<%--                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtOwnerName"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>--%>
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
                                                        <asp:ListItem Value="D" Text="Discharge" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="L" Text="Load"></asp:ListItem>
                                                        <asp:ListItem Value="N" Text="None"></asp:ListItem>
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
