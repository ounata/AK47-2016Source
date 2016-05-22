<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileWatchTest.aspx.cs" Inherits="Diagnostics.AppPoolCheck.FileWatchTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文件监测测试</title>
</head>
<body>
    <form id="serverForm" runat="server">
        <div>
            <h1>文件通知测试</h1>
        </div>
        <div>
            <p>
                该测试是为了验证依赖于文件更新的缓存项的有效性（一般和路径配置和文件访问权限有关）。在config的目录下要有一个notify.user文件。
            </p>
            <p>
                第一次访问页面时，代码中会初始化一个缓存项。界面上会显示尚未初始化。如果点击“接收通知”按钮，会显示“尚未初始化”
                每次点击“发送通知”按钮时，会显示发送的时间（这个时间也会写入文件）。这时如果点击“接收通知”，会显示“重新初始化：xxxxxx”。说明缓存被刷新了。
                如果仅点击“接收通知”，那么会一直显示缓存中的时间值。
            </p>
        </div>
        <div>
            <div>
                <asp:Button runat="server" ID="sendNotify" OnClick="sendNotify_Click" Text="发送通知" />
                <asp:Button runat="server" ID="receiveNotify" OnClick="receiveNotify_Click" Text="接收通知" />
            </div>
            <div>
                <div runat="server" id="sentMessage" />
                <div runat="server" id="receivedMessage" />
            </div>
        </div>
    </form>
</body>
</html>
