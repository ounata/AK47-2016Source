<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UdpTest.aspx.cs" Inherits="Diagnostics.AppPoolCheck.UdpTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Udp发送和接受测试</title>
</head>
<body>
    <form id="serverForm" runat="server">
        <div>
            <h1>Udp通知测试</h1>
        </div>
        <div>
            <p>
                该测试是为了验证依赖于UDP通知的缓存项的有效性（一般和网络配置和防火墙有关）。
            </p>
            <p>
                每次点击“发送通知”按钮后，再点击“接收通知”，应该看到接收的时间值和发送的应该一致。
            </p>
        </div>
        <div>
            <asp:Button runat="server" ID="sendNotify" OnClick="sendNotify_Click" Text="发送通知" />
            <asp:Button runat="server" ID="receiveNotify" OnClick="receiveNotify_Click" Text="接收通知" />
        </div>
        <div>
            <div runat="server" id="sentMessage" />
            <div runat="server" id="receivedMessage" />
        </div>
    </form>
</body>
</html>
