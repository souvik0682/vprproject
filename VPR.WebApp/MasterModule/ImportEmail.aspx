<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportEmail.aspx.cs" Inherits="VPR.WebApp.MasterModule.ImportData" MasterPageFile="~/Site.Master" Title=":: VPR::Import Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="dvAsync" style="padding: 5px; display: none;">
        <div class="asynpanel">
            <div id="dvAsyncClose">
                <img alt="" src="../../Images/Close-Button.bmp" style="cursor: pointer;" onclick="ClearErrorState()" /></div>
            <div id="dvAsyncMessage"></div>
        </div>
    </div>
    <div id="headercaption">IMPORT EMAILS</div>
    <center>
    <div style="width:950px;">        
        <fieldset style="width:100%;">
            <legend>Import Email IDs</legend>
            <table>
                <tr>
                    <td>
                        Select File: <asp:FileUpload ID="fuShipSoft" runat="server" />
                    </td>
                    <td><asp:Button ID="btnImport" runat="server" Text="Import" Width="100px" OnClick="btnImport_Click" /></td>
                    <td><asp:Button ID="btnExport" runat="server" Text="Export" Width="100px" OnClick="btnExport_Click" /></td>
                </tr>
            </table>
        </fieldset>
        
    </div>
    </center>
</asp:Content>
