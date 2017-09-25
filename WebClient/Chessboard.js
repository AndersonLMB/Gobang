socket = new WebSocket("ws://127.0.0.1:1836/PlayerActions");
var token;
var user;

var clearGamesList = function () {
    $(".gameContainer").remove();
};

var renderChessboardGrids = function (ctx, game) {
    ctx.strokeStyle = "rgb(150,150,150)";
    var i;
    //Draw verticle lines
    for (i = 0; i < game.X; i++) {
        ctx.beginPath();
        ctx.moveTo(10 * (i + 1), 10);
        ctx.lineTo(10 * (i + 1), 10 * game.Y);
        ctx.stroke();
    }
    //Draw horizonal lines
    for (i = 0; i < game.Y; i++) {
        ctx.beginPath();
        ctx.moveTo(10, 10 * (i + 1));
        ctx.lineTo(10 * game.X, 10 * (i + 1));
        ctx.stroke();
    }
};

var renderGame = function (ctx, game) {
    //render ChessboardGrids
    renderChessboardGrids(ctx, game);


    console.log(game);
};

var addGame = function (game, context) {
    var html = template("gameTemplate", game);
    $("#games").append(html);
    console.log(html);
    var canvas = document.getElementById("gameCanvas-" + game.Name);
    var ctx = canvas.getContext("2d");
    renderGame(ctx, game);
};

var addGamesInList = function (Games) {
    Games.forEach(function (game) {
        addGame(game, document.getElementById("games"));
        console.log(game);
    });
};

var updateGames = function (Games) {
    clearGamesList();
    addGamesInList(Games);
};

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
        updateGames(data.Games);
    }
    if (data.Head === "GAMES") {
        updateGames(data.Body);
    }
};

$("#loginButton").click(function () {
    socket.login();
});

$("#refreshGamesButton").click(function () {
    socket.refresh();
});
