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

var gridElement = function (options) {
    this.geometry = options.geometry;
    this.coordinate = options.coordinate;
    this.radius = options.radius;
    this.grid = options.value;
}
gridElement.prototype.boolContainXY = function (x, y) {
    if ((Math.pow((x - this.coordinate[0]), 2) + Math.pow((y - this.coordinate[1]), 2)) <= Math.pow(this.radius, 2)) {
        return true;
    }
    else {
        return false;
    }
}


var renderChessboardListens = function (ctx, game, options) {
    var pieces = [];
    var x, y;
    for (y = 0; y < game.Y; y++) {
        for (x = 0; x < game.X; x++) {
            var element = new gridElement({
                geometry: "circle",
                coordinate: [(x + 1) * options.scale, (y + 1) * options.scale],
                radius: 0.4 * options.scale,
                value: game.getGrid(x, y),
            });
            switch (element.grid) {
                case -1:
                    element.type = "empty";
                case 0:
                    element.type = "full";
                case 1:
                    element.type = "full";
            }
            pieces.push(element);
        }
    }
    jQuery(ctx.canvas).unbind("click");
    jQuery(ctx.canvas).unbind("mousemove");
    //ctx.canvas.removeEventListener("click");
    //ctx.canvas.removeEventListener("mousemove");
    ctx.canvas.addEventListener("click", function (evt) {
        pieces.forEach(function (piece) {
            if (piece.boolContainXY(evt.layerX, evt.layerY)) {

                console.log(piece);
            }
        });
    });
    //ctx.canvas.addEventListener("mousemove", function (evt) {
    //    pieces.forEach(function (piece) {
    //        if (piece.boolContainXY(evt.layerX, evt.layerY)) {
    //            if (piece.grid.Value >= 0) {

    //            }
    //            else {
    //                //ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height);
    //                //renderGame(ctx, game, options);
    //                //var newOptions = JSON.parse(JSON.stringify(options));
    //                //newOptions.pieceType=
    //                //var pieceOptions = { pieceType: "empty", };
    //                //renderChessboardPiece(ctx, game, options, piece);

    //            }
    //        }
    //    });
    //});
}

var renderChessboardPiece = function (ctx, game, options, piece) {
    ctx.strokeStyle = "rgb(0,0,0)";
    ctx.fillStyle = "rgba(100,100,100,0.5)";
    ctx.lineWidth = 2;
    ctx.beginPath();
    ctx.arc(piece.coordinate[0], piece.coordinate[1], 0.4 * options.scale, 0, Math.PI * 2, true);
    ctx.stroke();
    ctx.fill();
};


var renderGame = function (ctx, game, options) {
    //render ChessboardGrids
    game.getGrid = function (x, y) {
        return this.GameGrids[x + this.X * y];
    };
    game.getXYByIndex = function () { };
    renderChessboardGrids(ctx, game, options);
    renderChessboardPieces(ctx, game, options);
    if (options.addListen === true) {
        renderChessboardListens(ctx, game, options);
    }
};




var addGame = function (game, context, options) {
    var html = template("gameTemplate", game);
    $("#games").append(html);
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
    });
};

var updateGames = function (Games, options) {
    clearGamesList();
    addGamesInList(Games, options);
};
