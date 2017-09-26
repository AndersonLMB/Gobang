socket = new WebSocket("ws://127.0.0.1:1836/PlayerActions");
var token;
var user;

socket.login = function () {
    var username = document.getElementById("usernameInput");
    var password = document.getElementById("passwordInput");
    socket.send("LOGIN " + username.value + " " + password.value);
    user = username.value;
};
socket.refresh = function () {
    socket.send("GETGAMES");
};

socket.onmessage = function (e) {
    //console.log(e);
    var data = e.data;
    data = JSON.parse(data);

    if (data.Head === "LOGIN_SUCCESS") {
        token = data.Body;
    }
    if (data.Head === "Update") {
        updateGames(data.Games, { scale: 10 });
    }
    if (data.Head === "GAMES") {
        updateGames(data.Body, { scale: 10 });
    }
    if (data.Head === "MAINGAME") {
        renderMainGame(data.Body, { scale: 25, addListen: true });
    }
    if (data.Head === "UPDATEMAINGAME") {
        renderMainGame(data.Body, { scale: 25 });
    }
    if (data.Head === "WIN") {
        alert("GAME OVER");
    }
};

$("#loginButton").click(function () {
    socket.login();
});

$("#refreshGamesButton").click(function () {
    socket.refresh();
});
var activateGame = function (name, user, token, options) {
    socket.send("JOINGAME " + name + " " + user + " " + token);
    socket.send("GETGAME " + name);
    console.log(arguments);
};

var tryStep = function (user, token, stepOptions) {

    console.log(arguments);
    socket.send("TRYSTEP " + user + " " + token + " " + stepOptions.game.Name + " " + stepOptions.piece.gridX + " " + stepOptions.piece.gridY);
};

var renderMainGame = function (game, options) {
    var canvas = document.getElementById("mainGameCanvas");
    var ctx = canvas.getContext("2d");
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    renderGame(ctx, game, options);

    console.log(game);
};