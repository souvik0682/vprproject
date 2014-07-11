<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="forceSendMail.aspx.cs" Inherits="VPR.WebApp.Reports.forceSendMail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div>
        <div id="headercaption">
            SEND EMAIL</div>
        <center>
            <fieldset style="width: 60%;">
                <legend>Send Email</legend>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="2" cellspacing="3" width="100%">
                          <tr>
                            <td style="padding-right:15px;">Country:<span class="errormessage">*</span></td> 
                            <td>                                        
                            <asp:DropDownList ID="ddlCountry" runat="server"  Width="150" AutoPostBack="true" 
                                onselectedindexchanged="ddlCountry_SelectedIndexChanged">
                            </asp:DropDownList>
                            </td>
                            <td style="padding-right:15px;">Group:<span class="errormessage">*</span></td> 
                            <td>                                        
                                <asp:DropDownList ID="ddlCargoGroup" runat="server"  Width="150" AutoPostBack="true" onselectedindexchanged="ddlCargoGroup_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                 

                            <td style="padding-right:15px;">Sub Group:<span class="errormessage">*</span></td> 
                            <td>                                        
                                <asp:DropDownList ID="ddlSubGroup" runat="server"  Width="150" AutoPostBack="true" onselectedindexchanged="ddlSubGroup_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                   
                            <td style="padding-right:15px;">Email Group:<span class="errormessage">*</span></td> 
                            <td>                                        
                            <asp:DropDownList ID="ddlEmailGroup" runat="server"  Width="150" AutoPostBack="true" 
                                onselectedindexchanged="ddlddlEmailGroup_SelectedIndexChanged">
                            </asp:DropDownList>
                            </td>

                         </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table border="0" cellpadding="2" cellspacing="3" width="100%">
                    <tr>
                        <td>
                            <div style="margin-left: 40%;">
                                <asp:Button ID="btnShow" runat="server" Text="Show Email IDs" OnClick="btnShow_Click"
                                    ValidationGroup="vgContainer" />
                            </div>
                            <asp:Button ID="Button1" runat="server" Style="display: none;" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1"
                                PopupControlID="pnlContainer" Drag="true" BackgroundCssClass="ModalPopupBG" CancelControlID="btnCancel">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="pnlContainer" runat="server" Style="background-color: White;" 
                                Height="270px" Width="577px">
                                <center style="height: 253px; width: 551px">
                                    <fieldset>
                                        <div style="overflow: auto; height: 180px; width: 487px;">
                                            <asp:GridView ID="gvMail" runat="server" AutoGenerateColumns="false" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Email ID" HeaderStyle-Width="30%">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnEmailId" runat="server" Value='<%# Eval("pk_EmailId") %>' />
<%--                                                            <asp:HiddenField ID="hdnStatus" runat="server" Value='<%# Eval("Status") %>' />
                                                            <asp:HiddenField ID="hdnLandingDate" runat="server" Value='<%# Eval("LandingDate") %>' />
                                                            <asp:HiddenField ID="hdnLMDT" runat="server" Value='<%# Eval("LMDT") %>' />--%>
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

                                                    <asp:TemplateField HeaderText="Select" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkContainer" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" BackColor="GrayText" />
                                                <RowStyle Wrap="true" />
                                            </asp:GridView>
                                        </div>
                                        <br />
                                        <asp:Button ID="btnProceed" runat="server" Text="Proceed" OnClick="btnProceed_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                    </fieldset>
                                </center>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </center>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
