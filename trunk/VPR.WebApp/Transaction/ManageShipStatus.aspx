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
                <%--<asp:UpdatePanel ID="upImportBL" runat="server" UpdateMode="Always">
                    <ContentTemplate>--%>
                <div>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td colspan="2" style="padding-top: 10px;">
                                <cc1:TabContainer ID="tcPP" runat="server" ActiveTabIndex="0" AutoPostBack="true"
                                    OnActiveTabChanged="tcPP_ActiveTabChanged">
                                    <!-- Expecting Tab-->
                                    <cc1:TabPanel ID="tpExpecting" runat="server">
                                        <HeaderTemplate>
                                            Expecting
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="upExpecting" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvwExpecting" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="VesselId"
                                                        OnRowDataBound="gvwExpecting_RowDataBound">
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkExpecting" runat="server" OnCheckedChanged="chkExpecting_CheckedChanged"
                                                                        AutoPostBack="true" />
                                                                    <asp:HiddenField ID="hdnVesselId" runat="server" Value='<%# Eval("VesselId") %>' />
                                                                    <asp:HiddenField ID="hdnActivity" runat="server" Value='<%# Eval("VActivity") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="25px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Activity" HeaderText="Activity" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="Activity">
                                                                <HeaderStyle Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Vessel" HeaderText="Vessel" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="Vessel">
                                                                <HeaderStyle Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="LOA" HeaderText="LOA" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="LOA" />
                                                            <asp:TemplateField HeaderText="ETA" SortExpression="ETA">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtETA" runat="server" Text='<%# Bind("ETA","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceETADate" TargetControlID="txtETA" runat="server" Format="dd-MM-yyyy"
                                                                        Enabled="True" />
                                                                    <asp:CompareValidator ID="cvETA" runat="server" ControlToValidate="txtETA" Operator="LessThanEqual"
                                                                        Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfvETA" runat="server" ControlToValidate="txtETA"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
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
                                                            <asp:TemplateField HeaderText="Berth No" SortExpression="BirthNo">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlBerth" runat="server" Enabled="false" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlBerth_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ETB" SortExpression="BerthDate">
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
                                                            <asp:TemplateField HeaderText="ETC/Sail Date" SortExpression="ETC">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtETC" runat="server" Text='<%# Bind("ETC","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceDischargeDate" TargetControlID="txtETC" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvDischargeDate" runat="server" ControlToValidate="txtETC"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtETC" Display="Dynamic"
                                                                        ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Cargo" HeaderText="Cargo/Quantity" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="Cargo">
                                                                <HeaderStyle Width="150px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <RowStyle ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                    <br />
                                                    <asp:Button ID="btnExpPromote" runat="server" Text="Promote" OnClick="btnExpPromote_Click" />
                                                    <asp:Button ID="btnSaveETA" runat="server" Text="Save ETA" OnClick="btnSaveETA_Click" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="uProgressBL" runat="server" AssociatedUpdatePanelID="upExpecting">
                                                <ProgressTemplate>
                                                    <div class="progress">
                                                        <div id="image">
                                                            <img src="../Images/PleaseWait.gif" alt="" /></div>
                                                        <div id="text">
                                                            Please Wait...</div>
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <!-- Awating Tab-->
                                    <cc1:TabPanel ID="tpAwating" runat="server">
                                        <HeaderTemplate>
                                            Awating
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="upAwaiting" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvwAwaiting" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        OnRowDataBound="gvwAwaiting_RowDataBound" BorderColor="#CCCCCC" BorderStyle="None"
                                                        BorderWidth="1px" CellPadding="3" DataKeyNames="VesselId">
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkAwaiting" runat="server" OnCheckedChanged="chkAwaiting_CheckedChanged"
                                                                        AutoPostBack="true" />
                                                                    <asp:HiddenField ID="hdnVesselId" runat="server" Value='<%# Eval("VesselId") %>' />
                                                                    <asp:HiddenField ID="hdnStatusId" runat="server" Value='<%# Eval("StatusId") %>' />
                                                                    <asp:HiddenField ID="hdnActivity" runat="server" Value='<%# Eval("VActivity") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="25px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Activity" HeaderText="Activity" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="Activity" HeaderStyle-Width="100px" />
                                                            <asp:BoundField DataField="Vessel" HeaderText="Vessel" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="Vessel" HeaderStyle-Width="100px" />
                                                            <asp:BoundField DataField="LOA" HeaderText="LOA" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="LOA" />
                                                            <asp:TemplateField HeaderText="ETA" SortExpression="ETA">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtETA" runat="server" Text='<%# Bind("ETA","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceETADate" TargetControlID="txtETA" runat="server" Format="dd-MM-yyyy"
                                                                        Enabled="True" />
                                                                    <asp:CompareValidator ID="cvETA" runat="server" ControlToValidate="txtETA" Operator="LessThanEqual"
                                                                        Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfvETA" runat="server" ControlToValidate="txtETA"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
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
                                                            <asp:TemplateField HeaderText="Berth No" SortExpression="BirthNo">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlBerth" runat="server" Enabled="false" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlBerth_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ETB" SortExpression="BerthDate">
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
                                                            <asp:TemplateField HeaderText="ETC/Sail Date" SortExpression="ETC">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtETC" runat="server" Text='<%# Bind("ETC","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceDischargeDate" TargetControlID="txtETC" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvDischargeDate" runat="server" ControlToValidate="txtETC"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtETC" Display="Dynamic"
                                                                        ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Cargo" HeaderText="Cargo/Quantity" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="Cargo" HeaderStyle-Width="100px" />
                                                        </Columns>
                                                        <RowStyle ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                    <br />
                                                    <asp:Button ID="btnAwaPromote" runat="server" Text="Promote" OnClick="btnAwaPromote_Click" />
                                                    <asp:Button ID="btnAwaRevert" runat="server" Text="Revert" OnClick="btnAwaRevert_Click" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upAwaiting">
                                                <ProgressTemplate>
                                                    <div class="progress">
                                                        <div id="image">
                                                            <img src="../Images/PleaseWait.gif" alt="" /></div>
                                                        <div id="text">
                                                            Please Wait...</div>
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <!-- Dscharging Tab-->
                                    <cc1:TabPanel ID="tpDischarging" runat="server">
                                        <HeaderTemplate>
                                            Discharging
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="upDischarging" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvwDischarging" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        OnRowDataBound="gvwDischarging_RowDataBound" BorderColor="#CCCCCC" BorderStyle="None"
                                                        BorderWidth="1px" CellPadding="3" DataKeyNames="VesselId">
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkDischarging" runat="server" OnCheckedChanged="chkDischarging_CheckedChanged"
                                                                        AutoPostBack="true" />
                                                                    <asp:HiddenField ID="hdnVesselId" runat="server" Value='<%# Eval("VesselId") %>' />
                                                                    <asp:HiddenField ID="hdnStatusId" runat="server" Value='<%# Eval("StatusId") %>' />
                                                                    <asp:HiddenField ID="hdnActivity" runat="server" Value='<%# Eval("VActivity") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="25px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Activity" HeaderText="Activity" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="Activity" HeaderStyle-Width="100px" />
                                                            <asp:BoundField DataField="Vessel" HeaderText="Vessel" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="Vessel" HeaderStyle-Width="100px" />
                                                            <asp:BoundField DataField="LOA" HeaderText="LOA" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="LOA" />
                                                            <asp:TemplateField HeaderText="ETA" SortExpression="ETA">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtETA" runat="server" Text='<%# Bind("ETA","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceETADate" TargetControlID="txtETA" runat="server" Format="dd-MM-yyyy"
                                                                        Enabled="True" />
                                                                    <asp:CompareValidator ID="cvETA" runat="server" ControlToValidate="txtETA" Operator="LessThanEqual"
                                                                        Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfvETA" runat="server" ControlToValidate="txtETA"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
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
                                                            <asp:TemplateField HeaderText="Berth No" SortExpression="BirthNo">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlBerth" runat="server" Enabled="false">
                                                                    </asp:DropDownList>
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
                                                            <asp:TemplateField HeaderText="ETC/Sail Date" SortExpression="ETC">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtETC" runat="server" Text='<%# Bind("ETC","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceDischargeDate" TargetControlID="txtETC" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvDischargeDate" runat="server" ControlToValidate="txtETC"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" CssClass="errormessage" ErrorMessage="This field is required"
                                                                        ControlToValidate="txtETC" InitialValue="" ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <%--                                                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtETC"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>--%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Cargo" HeaderText="Cargo/Quantity" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="Cargo" HeaderStyle-Width="100px" />
                                                        </Columns>
                                                        <RowStyle ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                    <br />
                                                    <asp:Button ID="btnDisPromote" runat="server" Text="Promote" OnClick="btnDisPromote_Click" />
                                                    <asp:Button ID="btnDisRevert" runat="server" Text="Revert" OnClick="btnDisRevert_Click" />
                                                    <asp:Button ID="btnSaveDisETC" runat="server" Text="Save ETC" OnClick="btnSaveDisETC_Click" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upDischarging">
                                                <ProgressTemplate>
                                                    <div class="progress">
                                                        <div id="image">
                                                            <img src="../Images/PleaseWait.gif" alt="" /></div>
                                                        <div id="text">
                                                            Please Wait...</div>
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <!-- Loading Tab-->
                                    <cc1:TabPanel ID="tpLoading" runat="server">
                                        <HeaderTemplate>
                                            Loading
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="upLoading" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvwLoading" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="VesselId"
                                                        OnRowDataBound="gvwLoading_RowDataBound">
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkLoading" runat="server" OnCheckedChanged="chkLoading_CheckedChanged"
                                                                        AutoPostBack="true" />
                                                                    <asp:HiddenField ID="hdnVesselId" runat="server" Value='<%# Eval("VesselId") %>' />
                                                                    <asp:HiddenField ID="hdnStatusId" runat="server" Value='<%# Eval("StatusId") %>' />
                                                                    <asp:HiddenField ID="hdnActivity" runat="server" Value='<%# Eval("VActivity") %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="25px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Activity" HeaderText="Activity" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="Activity" HeaderStyle-Width="100px" />
                                                            <asp:BoundField DataField="Vessel" HeaderText="Vessel" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="Vessel" HeaderStyle-Width="100px" />
                                                            <asp:BoundField DataField="LOA" HeaderText="LOA" InsertVisible="False" ReadOnly="True"
                                                                SortExpression="LOA" />
                                                            <asp:TemplateField HeaderText="ETA" SortExpression="ETA">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtETA" runat="server" Text='<%# Bind("ETA","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceETADate" TargetControlID="txtETA" runat="server" Format="dd-MM-yyyy"
                                                                        Enabled="True" />
                                                                    <asp:CompareValidator ID="cvETA" runat="server" ControlToValidate="txtETA" Operator="LessThanEqual"
                                                                        Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfvETA" runat="server" ControlToValidate="txtETA"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
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
                                                            <asp:TemplateField HeaderText="Berth No" SortExpression="BirthNo">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlBerth" runat="server" Enabled="false">
                                                                    </asp:DropDownList>
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
                                                            <asp:TemplateField HeaderText="ETC/Sail Date" SortExpression="ETC">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtETC" runat="server" Text='<%# Bind("ETC","{0:dd-MM-yyyy}") %>'
                                                                        Width="80" BorderStyle="None" MaxLength="10" Enabled="false">
                                                                    </asp:TextBox>
                                                                    <cc1:CalendarExtender ID="ceDischargeDate" TargetControlID="txtETC" runat="server"
                                                                        Format="dd-MM-yyyy" Enabled="True" />
                                                                    <asp:CompareValidator ID="cvDischargeDate" runat="server" ControlToValidate="txtETC"
                                                                        Operator="LessThanEqual" Display="Dynamic" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"
                                                                        Type="Date" ToolTip="Date should be less than equals to current date!" ValidationGroup="Save">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" CssClass="errormessage" ErrorMessage="This field is required"
                                                                        ControlToValidate="txtETC" InitialValue="" ValidationGroup="Save" Display="Dynamic">
                                                                    </asp:RequiredFieldValidator>
                                                                    <%--                                                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtETC"
                                                                        Display="Dynamic" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>--%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Cargo" HeaderText="Cargo/Quantity" InsertVisible="False"
                                                                ReadOnly="True" SortExpression="Cargo" HeaderStyle-Width="100px" />
                                                        </Columns>
                                                        <RowStyle ForeColor="#000066" />
                                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    </asp:GridView>
                                                    <br />
                                                    <asp:Button ID="btnLoaPromote" runat="server" Text="Promote" OnClick="btnLoaPromote_Click" />
                                                    <asp:Button ID="btnLoaRevert" runat="server" Text="Revert" OnClick="btnLoaRevert_Click" />
                                                    <asp:Button ID="btnSaveLoadETC" runat="server" Text="Save ETC" OnClick="btnSaveLoadETC_Click" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="upLoading">
                                                <ProgressTemplate>
                                                    <div class="progress">
                                                        <div id="image">
                                                            <img src="../Images/PleaseWait.gif" alt="" /></div>
                                                        <div id="text">
                                                            Please Wait...</div>
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                </cc1:TabContainer>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-top: 10px;">
                                <asp:Label ID="lblErr" runat="server" CssClass="errormessage"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </fieldset>
            <%--<asp:UpdateProgress ID="uProgressBL" runat="server" AssociatedUpdatePanelID="upImportBL">
                <ProgressTemplate>
                    <div class="progress">
                        <div id="image">
                            <img src="../Images/PleaseWait.gif" alt="" /></div>
                        <div id="text">
                            Please Wait...</div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
        </div>
    </center>
</asp:Content>
