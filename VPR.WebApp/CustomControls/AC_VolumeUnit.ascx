﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AC_VolumeUnit.ascx.cs" Inherits="EMS.WebApp.CustomControls.AC_VolumeUnit" %>

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
    <asp:TextBox runat="server" ID="txtVolUnit" autocomplete="off" ForeColor="#747862"
        MaxLength="50" Width="250px" CssClass="textboxuppercase"
        OnTextChanged="txtVolUnit_TextChanged" AutoPostBack="true" />
    <%--<cc1:textboxwatermarkextender id="txtWMEName1" runat="server" targetcontrolid="txtVolUnit"
        watermarktext="TYPE VOLUME UNIT" watermarkcssclass="watermark1"></cc1:textboxwatermarkextender>--%>
 
    <cc1:autocompleteextender runat="server"  ID="AutoPort"
        targetcontrolid="txtVolUnit" servicepath="AutoComplete.asmx" servicemethod="GetVolumeUnitList"
        minimumprefixlength="2" completioninterval="1000" enablecaching="true" completionsetcount="20"
        completionlistcssclass="autocomplete_completionListElement" completionlistitemcssclass="autocomplete_listItem"
        completionlisthighlighteditemcssclass="autocomplete_highlightedListItem" delimitercharacters=";,:"
        showonlycurrentwordincompletionlistitem="true">
    </cc1:autocompleteextender>
 
</div>