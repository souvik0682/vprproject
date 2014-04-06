﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AC_Consignee.ascx.cs" Inherits="EMS.WebApp.CustomControls.AC_Consignee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<link href="../CustomControls/StyleSheet.css" rel="stylesheet" type="text/css" />

<style type="text/css">
    .watermark1
    {
        text-transform: uppercase;
        color: #747862;
        height: 20px;
        border: 0.5;
        padding: 1px 1px;
        margin-bottom: 0px;
    }
</style>

<div>
    <asp:TextBox runat="server" ID="txtConsignee" autocomplete="off" ForeColor="#747862"
        MaxLength="250" Width="450px" CssClass="textboxuppercase"
        AutoPostBack="true" ontextchanged="txtConsignee_TextChanged" />
    <%--<cc1:textboxwatermarkextender id="txtWMEName6" runat="server" targetcontrolid="txtConsignee"
        watermarktext="TYPE CONSIGNEE" watermarkcssclass="watermark1"></cc1:textboxwatermarkextender>--%>
 
    <cc1:autocompleteextender runat="server"  ID="AutoPort"
        targetcontrolid="txtConsignee" servicepath="AutoComplete.asmx" servicemethod="GetConsignee"
        minimumprefixlength="1" completioninterval="1000" enablecaching="true" completionsetcount="20"
        completionlistcssclass="autocomplete_completionListElement" completionlistitemcssclass="autocomplete_listItem"
        completionlisthighlighteditemcssclass="autocomplete_highlightedListItem" delimitercharacters=";,:"
        showonlycurrentwordincompletionlistitem="true" UseContextKey="true">
    </cc1:autocompleteextender>
 
</div>