﻿<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="SimpleWaitPage.aspx.cs" Inherits="AsyncWebFormTest.AsyncPages.SimpleWaitPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>简单延迟的异步页面</title>
</head>
<body>
    <form id="serverForm" runat="server">
        <asp:TextBox runat="server" ID="outputText" TextMode="MultiLine" Width="600" Height="600"></asp:TextBox>
    </form>
</body>
</html>
