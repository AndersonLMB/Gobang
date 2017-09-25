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

//var renderChessboardPiece = function () {

//};

var renderChessboardPieces = function (ctx, game) {
    var x, y;
    for (y = 0; y < game.Y; y++) {
        for (x = 0; x < game.X; x++) {
            var value = game.getGrid(x, y).Value;
            if (value >= 0) {
                if (value == 0) {
                    ctx.strokeStyle = "rgb(0,0,0)";
                    ctx.fillStyle = "rgb(0,0,0)";
                }
                if (value == 1) {
                    ctx.strokeStyle = "rgb(0,0,0)";
                    ctx.fillStyle = "rgb(255,255,255)";
                }
                ctx.beginPath();
                ctx.arc((x + 1) * 10, (y + 1) * 10, 4, 0, Math.PI * 2, true);
                ctx.lineWidth = 2;
                ctx.stroke();
                ctx.fill();
            }
        }
    }
};

var renderGame = function (ctx, game) {
    //render ChessboardGrids
    renderChessboardGrids(ctx, game);
    renderChessboardPieces(ctx, game);
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
        game.getGrid = function (x, y) {
            return this.GameGrids[x + this.X * y];
        };
        addGame(game, document.getElementById("games"));
        console.log(game);
    });
};

var updateGames = function (Games) {
    clearGamesList();
    addGamesInList(Games);
};
