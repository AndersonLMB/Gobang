adminSocket = new WebSocket("ws://127.0.0.1:1836/AdminActions");
var token;
var user;

var clearGamesList = function () {

};
var addGamesInList = function (Games) {
    Games.forEach(function (game) {
        
        console.log(game.GameGrids);
    })
};
var updateGames = function (Games) {
    clearGamesList();
    addGamesInList(Games);
};
//var updateGames = function () {
//    updateGamesList();
//}
adminSocket.addGame = function () {
    this.send("ADDGAME " + user + " " + token);
};
adminSocket.login = function () {
    var username = document.getElementById("usernameInput");
    var password = document.getElementById("passwordInput");
    adminSocket.send("LOGIN " + username.value + " " + password.value);
    user = username.value;
};
adminSocket.refresh = function () {
    adminSocket.send("GETGAMES");
}

adminSocket.onmessage = function (e) {
    console.log(e);
    var data = e.data;
    data = JSON.parse(data);

    if (data.Head === "LOGIN_SUCCESS") {
        token = data.Body;
    };
    if (data.Head === "Update") {
        updateGames(data.Games);
    };
};
$("#loginButton").click(function () {
    adminSocket.login();
});
$("#addGameButton").click(function () {
    adminSocket.addGame();
});
$("#refreshGamesButton").click(function () {
    adminSocket.refresh();
});
