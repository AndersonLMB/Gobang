<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebClient.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Lib/jquery-3.2.1.js" type="text/javascript"></script>
    <%--<script src="Lib/bootstrap.js" type="text/javascript"></script>--%>
    <%--<link rel="stylesheet" type="text/css" href="Styles/bootstrap.css" />--%>
</head>
<body>

    <div>
        <button id="loginButton">Log in</button>
        <button id="exitButton">Exit</button>
    </div>

    <div>
        <span>Operations:
        </span>
        <span>
            <button id="addGameButton">Add Game</button>
        </span>
    </div>

    <div>

    </div>
    <script>
        var adminSocket = new WebSocket("ws://127.0.0.1:1836/AdminActions");

    </script>
</body>
</html>
