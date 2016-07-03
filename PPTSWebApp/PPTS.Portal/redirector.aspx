<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="redirector.aspx.cs" Inherits="PPTS.Portal.redirector" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>重定向...</title>
</head>
<body>
    <input type="hidden" runat="server" id="rd" />
    <script>
        var url = document.getElementById("rd");

        if (url && url != "") {
            window.location.replace(url.value);
        }
    </script>
</body>
</html>
