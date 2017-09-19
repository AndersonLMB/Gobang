using System;
using System.Collections.Generic;
using MyGameSocket.Game;


namespace MyGameSocket
{
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
}
