<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddEditEmail.aspx.cs" Inherits="VPR.WebApp.MasterModule.AddEditEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
<div>
        <div id="headercaption">
            ADD/EDIT EMAIL
        </div>
        <center>
            <fieldset style="width: 85%;">
                <legend>Add / Edit Email</legend>
                <asp:UpdatePanel ID="upInvoice" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            Suffix
                        </td>
                        <td>
                            <asp:TextBox ID="txtSuffix" runat="server" Width="150px"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvSuffix" runat="server" ControlToValidate="txtSuffix"
                                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Salutation<span class="errormessage1">*</span>
                        </td>
                        <td>
                           <asp:TextBox ID="txtSalutation" runat="server" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSalutation" runat="server" ControlToValidate="txtSalutation"
                                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Receiver Name<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtReceiverName" runat="server" Width="350px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvReceiverName" runat="server" ControlToValidate="txtReceiverName"
                                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Email ID<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmailId" runat="server" Width="350px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmailId" runat="server" ControlToValidate="txtEmailId"
                                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                            ControlToValidate="txtEmailId" Display="Dynamic" CssClass="errormessage"
                                            ErrorMessage="Invalid email format" ValidationGroup="Save"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            <asp:Label ID="lblGroupName" runat="server" CssClass="errormessage1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Email ID 1
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmailId1" runat="server" Width="350px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                            ControlToValidate="txtEmailId1" Display="Dynamic" CssClass="errormessage"
                                            ErrorMessage="Invalid email format" ValidationGroup="Save"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Email ID 2
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmailId2" runat="server" Width="350px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                            ControlToValidate="txtEmailId2" Display="Dynamic" CssClass="errormessage"
                                            ErrorMessage="Invalid email format" ValidationGroup="Save"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Email ID 3
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmailId3" runat="server" Width="350px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                            ControlToValidate="txtEmailId3" Display="Dynamic" CssClass="errormessage"
                                            ErrorMessage="Invalid email format" ValidationGroup="Save"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Company Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtCompanyName" runat="server" Width="350px"></asp:TextBox>
<%--                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCompanyName"
                                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Category
                        </td>
                        <td>
                            <asp:TextBox ID="txtCompanyAbbr" runat="server" Width="350px"></asp:TextBox>
             <%--               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCompanyAbbr"
                                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Country<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCountry" runat="server" >
                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCountry" runat="server" CssClass="errormessage"
                                            ErrorMessage="This field is required" ControlToValidate="ddlCountry" InitialValue="0"
                                            ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Attachment
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAttachment" runat="server" 
                                onselectedindexchanged="ddlAttachment_SelectedIndexChanged">
                                <asp:ListItem Value="N" Text="None" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="L" Text="Link"></asp:ListItem>
                                <asp:ListItem Value="A" Text="Attachment"></asp:ListItem>
                                <asp:ListItem Value="B" Text="Both"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-top: 10; border: none;">
                            <fieldset style="width: 95%;">
                                <legend>Tag Cargo Group(s)</legend>
                                <table>
                                    <tr>
                                        <td>
                                            Available Cargo Group(s)
                                            <br />
                                            <asp:ListBox ID="ListBox1" runat="server" Height="150px" Width="150px" SelectionMode="Multiple">
                                            </asp:ListBox>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="ButtonAdd" runat="server" Text=">" Width="50px" OnClick="ButtonAdd_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="ButtonRemove" runat="server" Text="<" Width="50px" OnClick="ButtonRemove_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="ButtonAddAll" runat="server" Text=">>>" Width="50px" OnClick="ButtonAddAll_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="ButtonRemoveAll" runat="server" Text="<<<" Width="50px" OnClick="ButtonRemoveAll_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            Tagged Cargo Group(s)
                                            <br />
                                            <asp:ListBox ID="ListBox2" runat="server" Height="150px" Width="150px" SelectionMode="Multiple">
                                            </asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lbltxt" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="Save" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back" OnClick="btnBack_Click" />
                            <br />
                            <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                    </tr>
                </table>
                </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
            <asp:UpdateProgress ID="uProgressInvoice" runat="server" AssociatedUpdatePanelID="upInvoice">
                <ProgressTemplate>
                    <div class="progress">
                        <div id="image">
                            <img src="../Images/PleaseWait.gif" alt="" /></div>
                        <div id="text">
                            Please Wait...</div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </center>
    </div>
</asp:Content>
