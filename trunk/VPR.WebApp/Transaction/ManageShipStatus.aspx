<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageShipStatus.aspx.cs" Inherits="VPR.WebApp.Transaction.ManageShipStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="headercaption">
        MANAGE SHIP STATUS</div>
    <center>
        <div style="width: 100%">
            <fieldset style="width: 80%;">
                <legend>Manage Ship Status</legend>
                <asp:UpdatePanel ID="upImportBL" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td colspan="2" style="padding-top: 10px;">
                                        <cc1:TabContainer ID="tcPP" runat="server" ActiveTabIndex="0">
                                            <!-- Loading Tab-->
                                            <cc1:TabPanel ID="tpLoading" runat="server">
                                                <HeaderTemplate>
                                                    Loading</HeaderTemplate>
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvwLoading" runat="server" AutoGenerateColumns="False"
                                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                        CellPadding="3" DataKeyNames="VesselId" 
                                                        OnRowDataBound="gvwLoading_RowDataBound">
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" oncheckedchanged="chkSelect_CheckedChanged" AutoPostBack="true" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Activity" HeaderText="Activity" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="Activity" />
                                                            <asp:BoundField DataField="Vessel" HeaderText="Vessel" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="Vessel" />
                                                            <asp:BoundField DataField="LOA" HeaderText="LOA" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="LOA" />
                                                            <asp:BoundField DataField="BirthNo" HeaderText="Birth No" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="BirthNo" />
                                                            <asp:TemplateField HeaderText="Arrival Date" SortExpression="ArrivalDate">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtArrivalDate" runat="server" Text='<%# Bind("ArrivalDate","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceArrivalDate" TargetControlID="txtArrivalDate" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvArrivalDate" runat="server" ControlToValidate="txtArrivalDate"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtArrivalDate"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Berth Date" SortExpression="BerthDate">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtBerthDate" runat="server" Text='<%# Bind("BerthDate","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceBerthDate" TargetControlID="txtBerthDate" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvBerthDate" runat="server" ControlToValidate="txtBerthDate"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtBerthDate"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Discharge Date" SortExpression="DischargeDate">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDischargeDate" runat="server" Text='<%# Bind("DischargeDate","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceDischargeDate" TargetControlID="txtDischargeDate" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvDischargeDate" runat="server" ControlToValidate="txtDischargeDate"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtDischargeDate"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Loading Date" SortExpression="LoadingDate">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtLoadingDate" runat="server" Text='<%# Bind("LoadingDate","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceLoadingDate" TargetControlID="txtLoadingDate" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvLoadingDate" runat="server" ControlToValidate="txtLoadingDate"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="txtLoadingDate"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Cargo" HeaderText="Cargo" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="Cargo" />
                                                            <asp:BoundField DataField="CargoQuantity" HeaderText="Cargo Quantity" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="CargoQuantity" />
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnRemove" runat="server" OnClick="btnRemove_Click" ImageUrl="~/Images/remove.png"
                                                                        Height="16" Width="16" />
                                                                    <%--<asp:HiddenField ID="hdnContainerId" runat="server" Value='<%# Eval("ContainerId") %>' />--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                    <br />
                                                    <asp:Button ID="btnLoaPromote" runat="server" Text="Promote" />
                                                    <asp:Button ID="btnLoaRevert" runat="server" Text="Revert" />
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <!-- Dscharging Tab-->
                                            <cc1:TabPanel ID="tpDischarging" runat="server">
                                                <HeaderTemplate>
                                                    Dscharging</HeaderTemplate>
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvwDischarging" runat="server" AutoGenerateColumns="False"
                                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                        CellPadding="3" DataKeyNames="VesselId">
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkDischarging" runat="server" oncheckedchanged="chkDischarging_CheckedChanged" AutoPostBack="true" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Activity" HeaderText="Activity" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="Activity" />
                                                            <asp:BoundField DataField="Vessel" HeaderText="Vessel" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="Vessel" />
                                                            <asp:BoundField DataField="LOA" HeaderText="LOA" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="LOA" />
                                                            <asp:BoundField DataField="BirthNo" HeaderText="Birth No" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="BirthNo" />
                                                            <asp:TemplateField HeaderText="Arrival Date" SortExpression="ArrivalDate">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtArrivalDate" runat="server" Text='<%# Bind("ArrivalDate","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceArrivalDate" TargetControlID="txtArrivalDate" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvArrivalDate" runat="server" ControlToValidate="txtArrivalDate"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtArrivalDate"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Berth Date" SortExpression="BerthDate">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtBerthDate" runat="server" Text='<%# Bind("BerthDate","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceBerthDate" TargetControlID="txtBerthDate" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvBerthDate" runat="server" ControlToValidate="txtBerthDate"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtBerthDate"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Discharge Date" SortExpression="DischargeDate">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDischargeDate" runat="server" Text='<%# Bind("DischargeDate","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceDischargeDate" TargetControlID="txtDischargeDate" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvDischargeDate" runat="server" ControlToValidate="txtDischargeDate"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtDischargeDate"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Loading Date" SortExpression="LoadingDate">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtLoadingDate" runat="server" Text='<%# Bind("LoadingDate","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceLoadingDate" TargetControlID="txtLoadingDate" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvLoadingDate" runat="server" ControlToValidate="txtLoadingDate"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="txtLoadingDate"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Cargo" HeaderText="Cargo" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="Cargo" />
                                                            <asp:BoundField DataField="CargoQuantity" HeaderText="Cargo Quantity" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="CargoQuantity" />
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnRemove" runat="server" OnClick="btnRemove_Click" ImageUrl="~/Images/remove.png"
                                                                        Height="16" Width="16" />
                                                                    <%--<asp:HiddenField ID="hdnContainerId" runat="server" Value='<%# Eval("ContainerId") %>' />--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                    <br />
                                                    <asp:Button ID="btnDisPromote" runat="server" Text="Promote" />
                                                    <asp:Button ID="btnDisRevert" runat="server" Text="Revert" />
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <!-- Awating Tab-->
                                            <cc1:TabPanel ID="tpAwating" runat="server">
                                                <HeaderTemplate>
                                                    Awating</HeaderTemplate>
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvwAwaiting" runat="server" AutoGenerateColumns="False"
                                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                        CellPadding="3" DataKeyNames="VesselId">
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkAwaiting" runat="server" oncheckedchanged="chkAwaiting_CheckedChanged" AutoPostBack="true" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Activity" HeaderText="Activity" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="Activity" />
                                                            <asp:BoundField DataField="Vessel" HeaderText="Vessel" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="Vessel" />
                                                            <asp:BoundField DataField="LOA" HeaderText="LOA" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="LOA" />
                                                            <asp:BoundField DataField="BirthNo" HeaderText="Birth No" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="BirthNo" />
                                                            <asp:TemplateField HeaderText="Arrival Date" SortExpression="ArrivalDate">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtArrivalDate" runat="server" Text='<%# Bind("ArrivalDate","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceArrivalDate" TargetControlID="txtArrivalDate" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvArrivalDate" runat="server" ControlToValidate="txtArrivalDate"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtArrivalDate"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Berth Date" SortExpression="BerthDate">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtBerthDate" runat="server" Text='<%# Bind("BerthDate","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceBerthDate" TargetControlID="txtBerthDate" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvBerthDate" runat="server" ControlToValidate="txtBerthDate"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtBerthDate"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Discharge Date" SortExpression="DischargeDate">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDischargeDate" runat="server" Text='<%# Bind("DischargeDate","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceDischargeDate" TargetControlID="txtDischargeDate" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvDischargeDate" runat="server" ControlToValidate="txtDischargeDate"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtDischargeDate"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Loading Date" SortExpression="LoadingDate">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtLoadingDate" runat="server" Text='<%# Bind("LoadingDate","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceLoadingDate" TargetControlID="txtLoadingDate" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvLoadingDate" runat="server" ControlToValidate="txtLoadingDate"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="txtLoadingDate"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Cargo" HeaderText="Cargo" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="Cargo" />
                                                            <asp:BoundField DataField="CargoQuantity" HeaderText="Cargo Quantity" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="CargoQuantity" />
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnRemove" runat="server" OnClick="btnRemove_Click" ImageUrl="~/Images/remove.png"
                                                                        Height="16" Width="16" />
                                                                    <%--<asp:HiddenField ID="hdnContainerId" runat="server" Value='<%# Eval("ContainerId") %>' />--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                    <br />
                                                    <asp:Button ID="btnAwaPromote" runat="server" Text="Promote" />
                                                    <asp:Button ID="btnAwaRevert" runat="server" Text="Revert" />
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <!-- Expecting Tab-->
                                            <cc1:TabPanel ID="tpExpecting" runat="server">
                                                <HeaderTemplate>
                                                    Expecting</HeaderTemplate>
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvwExpecting" runat="server" AutoGenerateColumns="False"
                                                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                        CellPadding="3" DataKeyNames="VesselId">
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkExpecting" runat="server" oncheckedchanged="chkExpecting_CheckedChanged" AutoPostBack="true" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Activity" HeaderText="Activity" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="Activity" />
                                                            <asp:BoundField DataField="Vessel" HeaderText="Vessel" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="Vessel" />
                                                            <asp:BoundField DataField="LOA" HeaderText="LOA" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="LOA" />
                                                            <asp:BoundField DataField="BirthNo" HeaderText="Birth No" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="BirthNo" />
                                                            <asp:TemplateField HeaderText="Arrival Date" SortExpression="ArrivalDate">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtArrivalDate" runat="server" Text='<%# Bind("ArrivalDate","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceArrivalDate" TargetControlID="txtArrivalDate" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvArrivalDate" runat="server" ControlToValidate="txtArrivalDate"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtArrivalDate"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Berth Date" SortExpression="BerthDate">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtBerthDate" runat="server" Text='<%# Bind("BerthDate","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceBerthDate" TargetControlID="txtBerthDate" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvBerthDate" runat="server" ControlToValidate="txtBerthDate"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtBerthDate"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Discharge Date" SortExpression="DischargeDate">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDischargeDate" runat="server" Text='<%# Bind("DischargeDate","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceDischargeDate" TargetControlID="txtDischargeDate" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvDischargeDate" runat="server" ControlToValidate="txtDischargeDate"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtDischargeDate"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Loading Date" SortExpression="LoadingDate">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtLoadingDate" runat="server" Text='<%# Bind("LoadingDate","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceLoadingDate" TargetControlID="txtLoadingDate" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvLoadingDate" runat="server" ControlToValidate="txtLoadingDate"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="txtLoadingDate"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Cargo" HeaderText="Cargo" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="Cargo" />
                                                            <asp:BoundField DataField="CargoQuantity" HeaderText="Cargo Quantity" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="CargoQuantity" />
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemStyle CssClass="gridviewitem" Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnRemove" runat="server" OnClick="btnRemove_Click" ImageUrl="~/Images/remove.png"
                                                                        Height="16" Width="16" />
                                                                    <%--<asp:HiddenField ID="hdnContainerId" runat="server" Value='<%# Eval("ContainerId") %>' />--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                    <br />
                                                    <asp:Button ID="btnExpPromote" runat="server" Text="Promote" />
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                        </cc1:TabContainer>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
            <asp:UpdateProgress ID="uProgressBL" runat="server" AssociatedUpdatePanelID="upImportBL">
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
