﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AC_Port.ascx.cs" Inherits="VPR.WebApp.CustomControls.AC_Port" %>
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
    <asp:TextBox runat="server" ID="txtPort" autocomplete="off" ForeColor="#747862"
        MaxLength="50" Width="250px" CssClass="textboxuppercase"
        OnTextChanged="txtPort_TextChanged" AutoPostBack="true" />
   <%-- <cc1:textboxwatermarkextender id="txtWMEName6" runat="server" targetcontrolid="txtPort"
        watermarktext="TYPE PORT" watermarkcssclass="watermark1"></cc1:textboxwatermarkextender>--%>
 
    <cc1:autocompleteextender runat="server"  ID="AutoPort"
        targetcontrolid="txtPort" servicepath="AutoComplete.asmx" servicemethod="GetPortListBL"
        minimumprefixlength="1" completioninterval="1000" enablecaching="true" completionsetcount="20"
        completionlistcssclass="autocomplete_completionListElement" completionlistitemcssclass="autocomplete_listItem"
        completionlisthighlighteditemcssclass="autocomplete_highlightedListItem" delimitercharacters=";,:"
        showonlycurrentwordincompletionlistitem="true">
    </cc1:autocompleteextender>
 
</div>