<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddDocument.aspx.cs" Inherits="VPR.WebApp.Transaction.AddDocument" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 217px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">      
<script type="text/javascript" language="javascript">
function SetMaxLength(obj, maxLen) {
return (obj.value.length < maxLen);
}

function AutoCompleteItemSelected(sender, e) {

if (sender._id == "AutoCompleteEx") {

var hdnPort = $get('<%=hdnPort.ClientID %>');
hdnPort.value = e.get_value();
//              alert(hdnPort.value);
}

}
    </script>
    <asp:HiddenField ID="hdnPort" runat="server" />
<div id="headercaption">
        ADD / EDIT DOCUMENT</div>
    <center>
        <div style="width: 80%">
            <fieldset style="width: 75%;">
                <legend>Add / Edit Document</legend>            
                        <div>
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 51%">
                                <tr>
                                    <td colspan="2" style="padding-top: 10px;">
                                        <table border="0" cellpadding="1" cellspacing="0" width="100%" class="custtable">
                                            <tr>
                                                <td class="style1">
                                                   Document Type:
                                                </td>
                                                <td>
                                                     <asp:DropDownList ID="ddlDocumentType" runat="server"  CssClass="dropdownlist">
                                                        </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvMailBody" runat="server" ControlToValidate="ddlDocumentType"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" InitialValue="0" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                   Document Name:
                                                </td>
                                                <td>
                                                   <asp:TextBox ID="txtDocumentName" runat="server" CssClass="textboxuppercase" MaxLength="100"
                                                        Width="250px" TabIndex="2"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDocumentName"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                    Port:
                                                </td>
                                                <td>
                                                       <asp:TextBox ID="txtPort" runat="server" CssClass="textboxuppercase" MaxLength="100"
                                                        Width="250px" TabIndex="2"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPort"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>   <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx" ID="autoComplete1"
                                    TargetControlID="txtPort" ServicePath="~/CustomControls/AutoComplete.asmx" ServiceMethod="GetPortList"
                                    MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="true" CompletionSetCount="20"
                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";, :"
                                    ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="AutoCompleteItemSelected">
                                    <Animations>
                                        <OnShow>
                                            <Sequence>
                                                <%-- Make the completion list transparent and then show it --%>
                                                <OpacityAction Opacity="0" />
                                                <HideAction Visible="true" />
                            
                                                <%--Cache the original size of the completion list the first time
                                                    the animation is played and then set it to zero --%>
                                                <ScriptAction Script="
                                                    // Cache the size and setup the initial size
                                                    var behavior = $find('AutoCompleteEx');
                                                    if (!behavior._height) {
                                                        var target = behavior.get_completionList();
                                                        behavior._height = target.offsetHeight - 2;
                                                        target.style.height = '0px';
                                                    }" />
                            
                                                <%-- Expand from 0px to the appropriate size while fading in --%>
                                                <Parallel Duration=".4">
                                                    <FadeIn />
                                                    <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx')._height" />
                                                </Parallel>
                                            </Sequence>
                                        </OnShow>
                                        <OnHide>
                                            <%-- Collapse down to 0px and fade out --%>
                                            <Parallel Duration=".4">
                                                <FadeOut />
                                                <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
                                            </Parallel>
                                        </OnHide>
                                    </Animations>
                                </cc1:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td class="style1">
                                                    Upload File
:
                                                </td>
                                                <td>
                                                      <asp:FileUpload ID="fileUpload" runat="server"></asp:FileUpload> 
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="fileUpload"
                                                        ErrorMessage="This field is required*" CssClass="errormessage" ValidationGroup="Save"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td colspan="2" style="padding-top: 10px;">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" 
                                            TabIndex="70" onclick="btnSave_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" CssClass="button" TabIndex="3"  OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;"
                                            Text="Back" />
                                        <br />
                                        <asp:Label ID="lblErr" runat="server" CssClass="errormessage"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
        </div>
    </center>
</asp:Content>
