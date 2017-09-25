<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chessboard.aspx.cs" Inherits="WebClient.Chessboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Player Page</title>
    <link type="text/css" rel="stylesheet" href="Styles/Chessboard.css" />
    <script src="Lib/jquery-3.2.1.js" type="text/javascript"></script>
    <script src="Lib/template.js" type="text/javascript"></script>
    <script id="gameTemplate" type="text/html">
        <div id="gameContainer-{{Name}}" class="gameContainer">
            <div><span class="gameName">{{Name}}<button id="join{{Name}}">Join</button></span></div>
            <div><span>Administrator:{{Administrator.Name}}</span> | <span>Time:{{Time}}</span></div>
            <canvas id="gameCanvas-{{Name}}" width="200" height="200"></canvas>
        </div>
    </script>
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
            <button id="refreshGamesButton">Refresh</button>
        </span>
    </div>
    <div id="content">
        <div id="games" class="games">
        </div>
        <div id="mainGame" class="mainGame">
            <canvas id="mainGameCanvas" width="500" height="500"></canvas>
        </div>
    </div>


    <script src="Scripts/game.js" type="text/javascript"></script>
    <script src="Chessboard.js" type="text/javascript"></script>
</body>
</html>
