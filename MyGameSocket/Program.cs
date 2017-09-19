using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGameSocket.Game;
using MyGameSocket.Server;

namespace MyGameSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            GameServer gameServer = new GameServer("ws://127.0.0.1:1836");
            gameServer.Start();

            GobangGame game = new GobangGame();
            Player player1 = new Player("Jiang");
            game.AddPlayer(player1);
            game.ExecuteStep(7, 2, player1);
            game.ExecuteStep(6, 3, player1);
            game.ExecuteStep(5, 4, player1);
            game.ExecuteStep(4, 5, player1);
            game.ExecuteStep(1, 6, player1);
            game.Drawself();
            Console.WriteLine(game.Win());
            game.ExecuteStep(3, 6, player1);
            game.Drawself();
            Console.WriteLine(game.Win());
            Console.ReadLine();

        }
    }




    public static class OnlineGames
    {
        public static List<GobangGame> Games = new List<GobangGame>();
        public static void AddGame(GobangGame game)
        {
            Games.Add(game);
            Console.WriteLine("Game Added! Administrated by {0}", game.Administrator);
        }

        public static GobangGame GetGame(int index)
        {
            return Games[index];
        }
        //public static List<GobangGame> games = new List<GobangGame>(20);


        //public static void AddGame()
        //{

        //}

        //public static void RemoveGame()
        //{

        //}

    }

    public static class OnlinePlayers
    {
        public static List<Player> Players = new List<Player>();
        public static void AddPlayer(Player player)
        {
            Players.Add(player);
            DisplayAllPlayer();

        }
        public static void AddPlayer(string name)
        {
            Player player = new Player(name);
            AddPlayer(player);
        }

        public static List<Player> GetPlayers()
        {
            return Players;
        }

        public static Player GetPlayer(int index)
        {
            return Players[index];
        }

        public static void DisplayAllPlayer()
        {
            for (var i = 0; i < Players.Count; i++)
            {
                Console.Write(i.ToString() + ":" + Players[i].Name + " ");
            }
            Console.WriteLine();
        }


    }
}
