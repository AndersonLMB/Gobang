using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGameSocket.Server;
using Npgsql;
using Npgsql.Schema;
using MyGameSocket.Game;


namespace MyGameSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            GameServer gameServer = new GameServer("ws://127.0.0.1:1836");
            gameServer.Start();
            #region TEST 1 
            GobangGame game = new GobangGame();
            Player player1 = new Player("Jiang");
            Player player2 = new Player("Xi");
            game.AddPlayer(player1);
            game.AddPlayer(player2);
            game.ExecuteStep(7, 2, player1);
            game.ExecuteStep(6, 3, player1);
            game.ExecuteStep(10, 10, player2);
            game.ExecuteStep(5, 4, player1);
            game.ExecuteStep(4, 5, player1);
            game.ExecuteStep(1, 6, player1);
            Player player = new Player("Deng");
            game.Administrator = player;
            game.Drawself();
            Console.WriteLine(game.Win());
            game.ExecuteStep(3, 6, player1);
            OnlineGames.AddGame(game);
            //game.Drawself();
            //Console.WriteLine(game.Win());
            #endregion
            Console.ReadLine();

        }
    }
}
