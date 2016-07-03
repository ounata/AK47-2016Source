<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="PPTS.Portal.index" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title>学大教育-PPTS2016</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <link href="favicon.ico" rel="shortcut icon" />
    <script src="app/common/config/ppts.startup.js"></script>
</head>
<body class="no-skin navbar-fixed" ng-controller="appController as vm">
    <!-- BEGIN PAGE SPINNER -->
    <div id="ppts-spinner-loading" class="spinner">
        <div class="bounce1"></div>
        <div class="bounce2"></div>
        <div class="bounce3"></div>
    </div>
    <input runat="server" type="hidden" id="configData"/>
    <div ui-view></div>
</body>
</html>