var clearGamesList = function () {
    $(".gameContainer").remove();
};

var renderChessboardGrids = function (ctx, game, options) {
    ctx.strokeStyle = "rgb(150,150,150)";
    var i;
    //Draw verticle lines
    for (i = 0; i < game.X; i++) {
        ctx.beginPath();
        ctx.moveTo(options.scale * (i + 1), options.scale);
        ctx.lineTo(options.scale * (i + 1), options.scale * game.Y);
        ctx.stroke();
    }
    //Draw horizonal lines
    for (i = 0; i < game.Y; i++) {
        ctx.beginPath();
        ctx.moveTo(options.scale, options.scale * (i + 1));
        ctx.lineTo(options.scale * game.X, options.scale * (i + 1));
        ctx.stroke();
    }
};

//var renderChessboardPiece = function () {

//};

var renderChessboardPieces = function (ctx, game, options) {
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
                ctx.arc((x + 1) * options.scale, (y + 1) * options.scale, 0.4 * options.scale, 0, Math.PI * 2, true);
                ctx.lineWidth = 2;
                ctx.stroke();
                ctx.fill();
            }
        }
    }
};

var renderChessboardListens = function (ctx, game, options) {
    
}

var renderGame = function (ctx, game, options) {
    //render ChessboardGrids
    game.getGrid = function (x, y) {
        return this.GameGrids[x + this.X * y];
    };
    renderChessboardGrids(ctx, game, options);
    renderChessboardPieces(ctx, game, options);
    if (options.addListen === true) {
        renderChessboardListens(ctx, game, options);
    }

    console.log(game);
};




var addGame = function (game, context, options) {
    var html = template("gameTemplate", game);
    $("#games").append(html);
    console.log(html);
    var canvas = document.getElementById("gameCanvas-" + game.Name);
    var ctx = canvas.getContext("2d");
    renderGame(ctx, game, options);
    $("#join" + game.Name).click(function () {
        activateGame(game.Name, user, token, {});
    });
};

var addGamesInList = function (Games, options) {
    Games.forEach(function (game) {
        //game.getGrid = function (x, y) {
        //    return this.GameGrids[x + this.X * y];
        //};
        addGame(game, document.getElementById("games"), options);
        console.log(game);
    });
};

var updateGames = function (Games, options) {
    clearGamesList();
    addGamesInList(Games, options);
};
