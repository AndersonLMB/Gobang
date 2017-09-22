<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebClient.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Lib/jquery-3.2.1.js" type="text/javascript"></script>

</head>
<body>

    <div>
        <input id="usernameInput" type="text" />
        <input id="passwordInput" type="password" />
        <button id="loginButton">Log in</button>
        <button id="exitButton">Exit</button>
    </div>

    <div>
        <span>Operations:
        </span>
        <span>
            <button id="addGameButton">Add Game</button>
            <button id="refreshGamesButton">Refresh</button>
        </span>
    </div>

    <div id="gamesList">
    </div>
    <script src="Default.js" type="text/javascript"></script>
</body>
</html>
