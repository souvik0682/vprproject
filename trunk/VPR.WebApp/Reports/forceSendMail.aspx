<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="forceSendMail.aspx.cs" Inherits="VPR.WebApp.Reports.forceSendMail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">

        <ContentTemplate>
            <div>
                <div id="headercaption">
                    SEND EMAIL</div>
                <center>
                    <fieldset style="width: 60%;">
                        <legend>Send Email</legend>
                        <table border="0" cellpadding="2" cellspacing="3" width="100%">
                            <tr>
                                <td style="padding-right: 15px;">
                                    Country:<span class="errormessage">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCountry" runat="server" Width="150" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Text="-- Select --" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="padding-right: 15px;">
                                    Group:<span class="errormessage">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCargoGroup" runat="server" Width="150" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlCargoGroup_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Text="-- Select --" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 15px;">
                                    Sub Group:<span class="errormessage">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSubGroup" runat="server" Width="150" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlSubGroup_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Text="-- Select --" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="padding-right: 15px;">
                                    Email Group:<span class="errormessage">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlEmailGroup" runat="server" Width="150" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlEmailGroup_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Text="-- Select --" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rfvEmailGroup" runat="server" CssClass="errormessage"
                                        ErrorMessage="This field is required" ControlToValidate="ddlEmailGroup" InitialValue="0"
                                        ValidationGroup="vgSave" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblUpload" runat="server" Text="Attach File :">
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:FileUpload ID="fileUpload" runat="server"></asp:FileUpload> 
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnShow" runat="server" Text="Show Email IDs" OnClick="btnShow_Click"
                                        ValidationGroup="vgSave" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Panel ID="pnlContainer" runat="server" Style="background-color: White;" Height="270px"
                                        Width="577px">
                                        <%--<center style="height: 253px; width: 551px">--%>
                                        <%--<fieldset>--%>
                                        <%--<div style="overflow: auto; height: 180px; width: 487px;">--%>
                                        <asp:GridView ID="gvMail" runat="server" AutoGenerateColumns="false" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Email ID" HeaderStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnEmailId" runat="server" Value='<%# Eval("pk_EmailId") %>' />
                                                        <asp:Label ID="lblEmailID" runat="server" Text='<%# Eval("EmailIDActive")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="50%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("ReceiverName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Country" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContainerSize" runat="server" Text='<%# Eval("CountryName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="40px">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkboxSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" BackColor="GrayText" />
                                            <RowStyle Wrap="true" />
                                        </asp:GridView>
                                        <%--</div>--%>
                                        <br />
                                        <asp:Button ID="btnProceed" runat="server" Text="Proceed" OnClick="btnProceed_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                        <br />
                                        <asp:Label ID="lblError" runat="server" Text="" CssClass="errormessage"></asp:Label>
                                        <%--</fieldset>--%>
                                        <%--</center>--%>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lblMessage" runat="server" Text="No Record(s) found!" CssClass="errormessage"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </center>
            </div>
        </ContentTemplate>

</asp:Content>
