<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="PPTS.Portal.index" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title>学大教育PPTS</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <link href="/favicon.ico" rel="shortcut icon" />
    <script src="build/ppts.startup.min.js"></script>
    <script>
        mcs.g.loadCss([
            //<!--#bootstrap基础样式-->
            'libs/bootstrap-3.3.5/css/bootstrap.min',
            //<!--#boostrap组件样式-->
            'libs/bootstrap-3.3.5/css/style',
            'libs/bootstrap-3.3.5/css/style-responsive',
            //<!--#font awesome字体样式-->
            'libs/font-awesome-4.5.0/css/font-awesome.min',
            //<!--#ace admin基础样式-->
            'libs/ace-1.2.3/ace.min',
            //<!--#blockUI样式-->
            'libs/angular-block-ui-0.2.2/dist/angular-block-ui',
            //<!--#全局组件样式-->
            'libs/mcs-jslib-1.0.0/component/mcs.component',
            //<!--下拉框样式-->
            'libs/angular-ui-select-0.13.2/dist/select.min',
            'libs/angular-ui-select-0.13.2/dist/themes/selectize.default',
            'libs/angular-dialog-service/dist/dialogs.min'
        ], [
            //<!--#网站主样式-->
            'assets/styles/site'
        ]);
        mcs.g.loadRequireJs(
            'libs/requirejs-2.1.22/require',
            './app/common/config/require.config');
    </script>
</head>
<body class="no-skin" ng-controller="appController as vm">
    <input runat="server" type="hidden" id="portalParameters" />
    <div ui-view></div>
</body>
</html>
