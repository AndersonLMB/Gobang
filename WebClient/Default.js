socket = new WebSocket("ws://127.0.0.1:1836/AdminActions");
var token;
var user;

//var clearGamesList = function () {

//};
//var addGamesInList = function (Games) {
//    Games.forEach(function (game) {

//        console.log(game.GameGrids);
//    });
//};
//var updateGames = function (Games) {
//    clearGamesList();
//    addGamesInList(Games);
//};
//var updateGames = function () {
//    updateGamesList();
//}
socket.addGame = function () {
    this.send("ADDGAME " + user + " " + token);
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
    console.log(e);
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
};
$("#loginButton").click(function () {
    socket.login();
});
$("#addGameButton").click(function () {
    socket.addGame();
});
$("#refreshGamesButton").click(function () {
    socket.refresh();
});
