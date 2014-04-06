<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    CodeBehind="AddEditEmailGroup.aspx.cs" Inherits="VPR.WebApp.MasterModule.AddEditEmailGroup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="VPR.WebApp" Namespace="VPR.WebApp.CustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div>
        <div id="headercaption">
            ADD/EDIT EMAIL GROUP
        </div>
        <center>
            <fieldset style="width: 85%;">
                <legend>Add / Edit Email Group</legend>
                <asp:UpdatePanel ID="upInvoice" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            Group Name<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGroupName" runat="server" Width="600px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvGroupName" runat="server" ControlToValidate="txtGroupName"
                                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblGroupName" runat="server" CssClass="errormessage1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Country<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCountry" runat="server" CssClass="errormessage"
                                            ErrorMessage="This field is required" ControlToValidate="ddlCountry" InitialValue="0"
                                            ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Mail Subject<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMailSubject" runat="server" Width="600px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMailSubject" runat="server" ControlToValidate="txtMailSubject"
                                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Mail Body<span class="errormessage1">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMailBody" runat="server" Width="600px" TextMode="MultiLine" Rows="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMailBody" runat="server" ControlToValidate="txtMailBody"
                                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Attachment
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAttachment" runat="server">
                                <asp:ListItem Value="N" Text="None" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="L" Text="Link"></asp:ListItem>
                                <asp:ListItem Value="A" Text="Attachment"></asp:ListItem>
                                <asp:ListItem Value="B" Text="Both"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Mail Frequency
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMailFrequency" runat="server" AutoPostBack="true"
                                onselectedindexchanged="ddlMailFrequency_SelectedIndexChanged">
                                <asp:ListItem Value="D" Text="Daily" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="W" Text="Weekly"></asp:ListItem>
                                <asp:ListItem Value="M" Text="Monthly"></asp:ListItem>
                                <asp:ListItem Value="Y" Text="Yearly"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Mail Send Time
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlHour" runat="server">
                                <asp:ListItem Value="0" Text="00" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="01"></asp:ListItem>
                                <asp:ListItem Value="2" Text="02"></asp:ListItem>
                                <asp:ListItem Value="3" Text="03"></asp:ListItem>
                                <asp:ListItem Value="4" Text="04"></asp:ListItem>
                                <asp:ListItem Value="5" Text="05"></asp:ListItem>
                                <asp:ListItem Value="6" Text="06"></asp:ListItem>
                                <asp:ListItem Value="7" Text="07"></asp:ListItem>
                                <asp:ListItem Value="8" Text="08"></asp:ListItem>
                                <asp:ListItem Value="9" Text="09"></asp:ListItem>
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlMinutes" runat="server">
                                <asp:ListItem Value="00" Text="00" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                <asp:ListItem Value="45" Text="45"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMailSend" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMailSendOn" runat="server" Width="250px"></asp:TextBox>
                            <cc1:CalendarExtender ID="cbeBookingDate" TargetControlID="txtMailSendOn" runat="server"
                                Format="dd-MM-yyyy" Enabled="True" />
                            <asp:RequiredFieldValidator ID="rfvMailSendOn" runat="server" ControlToValidate="txtMailSendOn"
                                            ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                            
                            <asp:DropDownList ID="ddlDayOfMonth" runat="server">
                                <asp:ListItem Value="1" Text="01" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="2" Text="02"></asp:ListItem>
                                <asp:ListItem Value="3" Text="03"></asp:ListItem>
                                <asp:ListItem Value="4" Text="04"></asp:ListItem>
                                <asp:ListItem Value="5" Text="05"></asp:ListItem>
                                <asp:ListItem Value="6" Text="06"></asp:ListItem>
                                <asp:ListItem Value="7" Text="07"></asp:ListItem>
                                <asp:ListItem Value="8" Text="08"></asp:ListItem>
                                <asp:ListItem Value="9" Text="09"></asp:ListItem>
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                <asp:ListItem Value="31" Text="31"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlDayOfWeek" runat="server">
                                <asp:ListItem Value="1" Text="Monday" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Tuesday"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Wednesday"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Thursday"></asp:ListItem>
                                <asp:ListItem Value="5" Text="Friday"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Saturday"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Sunday"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-top: 10; border: none;">
                            <fieldset style="width: 95%;">
                                <legend>Tag Email Ids</legend>
                                <table>
                                    <tr>
                                        <td>
                                            Available Emails
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
                                            Tagged Emails
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
