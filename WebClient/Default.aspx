<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebClient.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Administrator Page</title>
    <script src="Lib/jquery-3.2.1.js" type="text/javascript"></script>
    <script src="Lib/template.js" type="text/javascript"></script>
    <script id="gameTemplate" type="text/html">
        <div id="gameContainer-{{Name}}" class="gameContainer">
            <div><span class="gameName" >{{Name}}</span></div>
            <div><span>Administrator:{{Administrator.Name}}</span> | <span>Time:{{Time}}</span></div>
            <canvas id="gameCanvas-{{Name}}" width="200" height="200"></canvas>
        </div>
    </script>
</head>
<body>

    <div>
        <input id="usernameInput" type="text" value="Jiang" />
        <input id="passwordInput" type="password" value="123" />
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

    <div id="games">
    </div>
    <script src="Scripts/game.js" type="text/javascript"></script>
    <script src="Default.js" type="text/javascript"></script>
</body>
</html>
