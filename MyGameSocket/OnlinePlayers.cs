using System;
using System.Collections.Generic;
using MyGameSocket.Game;
using System.Configuration;
using Dapper;
using MyGameSocket.Server;
using System.Linq;

namespace MyGameSocket
{
    public static class OnlinePlayers
    {
        public static List<Player> Players = new List<Player>();
        public static void AddPlayer(Player player)
        {
            var exist = Players.Find(x => x.Name == player.Name);
            Console.WriteLine(exist != null ? true : false);

            if (exist == null)
            {
                Players.Add(player);
            }
            DisplayAllPlayer();
        }
        public static void AddPlayer(Player player, out string status)
        {
            status = "NULL";
            var exist = Players.Find(x => x.Name == player.Name);
            Console.WriteLine(exist != null ? true : false);

            if (exist != null)
            {
                status = "EXISTED";
            }
            else
            {
                using (Npgsql.NpgsqlConnection connection = new Npgsql.NpgsqlConnection(ConfigurationManager.ConnectionStrings["users"].ConnectionString))
                {
                    connection.Open();
                    //Console.WriteLine("SELECT * FROM \"users\" WHERE \"name\"='" + player.Name + "';");
                    var users = connection.Query<User>("SELECT * FROM users WHERE \"name\"='" + player.Name + "';");
                    if (users.AsList<User>().Count == 0)
                    {
                        status = "NOTFOUND";
                    }
                    else
                    {
                        Players.Add(player);
                        //for (var i = 0; i < OnlinePlayers.GetPlayers().AsList<Player>().Count; i++)
                        //{
                        //    Console.WriteLine(OnlinePlayers.GetPlayers().AsList<Player>()[i].Name);
                        //}
                    }
                    connection.Close();
                }
            }

            //if (exist == null)
            //{
            //    status = "SUCCESS";
            //    Players.Add(player);
            //}
            //else
            //{
            //    //status = "FAILED";
            //}
            DisplayAllPlayer();
        }
        public static void AddPlayer(Player player, Delegate del)
        {
            var exist = Players.Find(x => x.Name == player.Name);
            Console.WriteLine(exist != null ? true : false);

            if (exist == null)
            {

                Players.Add(player);

            }


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

        public static Player GetPlayer(string name)
        {
            return Players.Find(player => player.Name == name);
        }


        public static void DisplayAllPlayer()
        {
            for (var i = 0; i < Players.Count; i++)
            {
                Console.Write(i.ToString() + ":" + Players[i].Name + " ");
            }
            Console.WriteLine();
        }

        //public List<Player> GetPlayers()
        //{
        //    return Players;
        //}


    }
}
