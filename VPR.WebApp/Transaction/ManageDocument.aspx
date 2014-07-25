<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageDocument.aspx.cs" Inherits="VPR.WebApp.Transaction.ManageDocument" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/CustomControls/AC_Port.ascx" TagName="AC_Port" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
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
    <div id="headercaption">MANAGE DOCUMENT</div>
    <center>
    <div style="width:850px;">      
        <fieldset style="width:100%;">
            <legend>Search Document</legend>
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="txtDocumentName" runat="server" CssClass="watermark" ForeColor="#747862" ></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="wmtxtDocumentName" runat="server" TargetControlID="txtDocumentName" WatermarkText="Document Name" 
                        WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                    </td>
                    <td>
                       <asp:DropDownList ID="ddlDocumentType" runat="server" >
                       </asp:DropDownList>
                    </td>
                     <td>
                        <asp:TextBox ID="txtPort" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="wmtxtPort" runat="server" TargetControlID="txtPort" WatermarkText="Port"
                         WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                         
                                <cc1:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx" ID="autoComplete1"
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
                    <td><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" Width="100px" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnRefresh" runat="server" Text="Reset" CssClass="button" Width="100px" onclick="btnRefresh_Click"  /></td>
                </tr>
            </table>              
        </fieldset>        
        <fieldset id="fsList" runat="server" style="width:100%;min-height:100px;">
            <legend>Document List</legend>
            <div style="float:right;padding-bottom:5px;margin-top: -10px">
                Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="30" Value="30" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>&nbsp;&nbsp;
                <asp:Button ID="btnAdd" runat="server" Text="Add New Document" Width="130px" OnClick="btnAdd_Click" />
                
            </div>
          <br />            
            <div>
                    <asp:Label runat="server" ID="lblErrorMsg" Text=""></asp:Label>
                        <asp:GridView ID="gvwLoc" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                BorderStyle="None" BorderWidth="0" OnPageIndexChanging="gvwLoc_PageIndexChanging" AllowSorting="true"
                                DataKeyNames="pk_DocumentID"
                                OnRowDataBound="gvwLoc_RowDataBound" OnRowCommand="gvwLoc_RowCommand" Width="100%" onsorting="gvwLoc_Sorting">
                                <pagersettings mode="NumericFirstLast" position="TopAndBottom" />
                                <pagerstyle cssclass="gridviewpager" />
                                <emptydatarowstyle cssclass="gridviewemptydatarow" />
                                <emptydatatemplate>No Document(s) Found</emptydatatemplate>
                                <columns>
                                <asp:TemplateField HeaderText="Sl#">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="5%" />                                    
                                </asp:TemplateField>
                              
                            
                                <asp:TemplateField HeaderText="id" Visible="false">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="2%" />                                       
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Doc Type" SortExpression="DocType" >
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="15%" />                                       
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Port" SortExpression="Port">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="20%" />      
                                   <%-- <HeaderTemplate><asp:LinkButton ID="lnkHMan" runat="server" CommandName="Sort" CommandArgument="Manager" Text="Location Manager"></asp:LinkButton></HeaderTemplate>                                                                 --%>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Document Name" SortExpression="DocumentName" >
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="20%" />                                       
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Scope" SortExpression="Scope" >
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="10%" />                                       
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Download">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="5%"   VerticalAlign="Middle" HorizontalAlign="Center"/><ItemTemplate>   
                                                             
                                        <asp:ImageButton ID="btnDownload" runat="server" CommandName="Download" ImageUrl="~/Images/FileDownloads.ico" Height="16" Width="16" />
                                    </ItemTemplate>                                       
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Edit">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit.png"
                                            Height="16" Width="16" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Remove">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle"  />                                    
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/remove.png" Height="16" Width="16" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </columns>
                    </asp:GridView>
                
            </div>
        </fieldset>
    </div>
    </center>
</asp:Content>