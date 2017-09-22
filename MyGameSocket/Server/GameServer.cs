using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.WebSockets;
using System.Net;
using WebSocketSharp.Net.WebSockets;
using WebSocketSharp.Net;
using WebSocketSharp.Server;
using WebSocketSharp;
using MyGameSocket.Game;
using System.Xml;
using Newtonsoft.Json;
using Npgsql;
using Dapper;
using System.Configuration;
using System.Security.Cryptography;

namespace MyGameSocket.Server
{
    class GameServer
    {

        public WebSocketServer WSGameServer;

        public GameServer(string url)
        {
            WSGameServer = new WebSocketServer(url);
            WSGameServer.AddWebSocketService<PlayerActions>("/PlayerActions");
            WSGameServer.AddWebSocketService<AdminActions>("/AdminActions");
            Console.WriteLine();
        }

        public void Start()
        {
            this.WSGameServer.Start();
        }




    }

    class PlayerActions : WebSocketBehavior
    {
        protected override void OnOpen()
        {
            base.OnOpen();

        }
        protected override void OnMessage(MessageEventArgs e)
        {
            string[] message = e.Data.Split(' ');
            //Login
            if (message[0] == "LOGIN")
            {

                string loginStatus;
                Player player = new Player(message[1]);
                OnlinePlayers.AddPlayer(player, out loginStatus);
                //LoginCallback(loginStatus);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(OnlinePlayers.GetPlayers());
                Send(json);
                LoginCallback(loginStatus);
            }

            if (message[0] == "ADMIN")
            {
                GobangGame game = new GobangGame();
                game.Administrator = new Player(message[1]);
                OnlineGames.AddGame(game);
            }
            base.OnMessage(e);
        }
        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
        }
        public void Login(string[] message)
        {

            MyDel del = null;
            del = this.AddPlayerCallback;

            Player player = new Player(message[1]);
            OnlinePlayers.AddPlayer(player, del);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(OnlinePlayers.GetPlayers());
            Send(json);
        }
        public delegate void MyDel(string status);
        public void AddPlayerCallback(string status) { }
        public void LoginCallback(string status)
        {
            string json;
            switch (status)
            {
                case "SUCCESS":
                    Send("LOGIN_SUCCESS");
                    break;
                case "FAILED":
                    break;
                case "NULL":
                    break;
                case "NOTFOUND":
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(new LoginCallbackMessage("User not found!"));
                    Send(json);
                    break;
                case "EXISTED":
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(new LoginCallbackMessage("Already logged in!"));
                    Send(json);
                    break;
                default:
                    break;
            }
        }
        public void LoginSuccess()
        {
        }
    }

    class AdminActions : WebSocketBehavior
    {
        private Boolean TokenCorrect(string name, int token)
        {
            using (var connection = new Npgsql.NpgsqlConnection(ConfigurationManager.ConnectionStrings["users"].ConnectionString))
            {
                connection.Open();
                User input = new User();
                User correct = new User();
                input.Name = name;
                //input.Token = token;
                int inputToken = 0;
                inputToken = token;
                int correctToken = 0;
                var users = connection.Query<User>("SELECT * FROM users WHERE \"name\"='" + input.Name + "';");

                if (users.AsList<User>().Count == 0)
                {
                    return false;
                }
                else
                {
                    correct = users.AsList<User>()[0];
                    correctToken = (correct.Name + correct.Password).GetHashCode().GetHashCode();
                    if (inputToken == correctToken)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }



            }
        }

        protected override void OnMessage(MessageEventArgs e)
        {


            string[] message = e.Data.Split(' ');
            if (message[0] == "ADDGAME")
            {
                GobangGame game = new GobangGame();
                if (TokenCorrect(message[1], Convert.ToInt32(message[2])))
                {
                    
                    game.Administrator = new Player(message[1]);
                    OnlineGames.AddGame(game);
                    MainScreenMessage mainScreenMessage = new MainScreenMessage("Update");
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(mainScreenMessage);
                    Send(json);
                }
                else
                {
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(new LoginCallbackMessage("FAILED"));
                    Send(json);
                }
            }
            if (message[0] == "GETGAMES")
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(OnlineGames.GetGames());
                Send(json);
            }
            if (message[0] == "LOGIN")
            {
                using (var connection = new Npgsql.NpgsqlConnection(ConfigurationManager.ConnectionStrings["users"].ConnectionString))
                {
                    connection.Open();
                    User input = new User();
                    User correct = new User();
                    input.Name = message[1];
                    input.Password = message[2];
                    int inputToken = 0;
                    inputToken = (input.Name + input.Password).GetHashCode().GetHashCode();
                    Console.WriteLine(inputToken);
                    Boolean pass = false;
                    int correctToken = 0;
                    //LoginCallbackMessage message;

                    var users = connection.Query<User>("SELECT * FROM users WHERE \"name\"='" + input.Name + "';");
                    if (users.AsList<User>().Count == 0)
                    {
                        //NOT FOUND
                    }
                    else
                    {
                        correct = users.AsList<User>()[0];
                        correctToken = (correct.Name + correct.Password).GetHashCode().GetHashCode();
                        if (inputToken == correctToken)
                        {
                            pass = true;
                        }
                    }

                    if (pass == true)
                    {
                        string json = Newtonsoft.Json.JsonConvert.SerializeObject(new LoginCallbackMessage("TOKEN " + correctToken.ToString()));
                        Send(json);
                    }
                    else
                    {
                        string json = Newtonsoft.Json.JsonConvert.SerializeObject(new LoginCallbackMessage("FAILED"));
                        Send(json);
                    }

                    //correct= connection.QueryFirst<User>()



                    //if (message.Length >= 4)
                    //{
                    //    string token = "";
                    //    token = input.name + input.password;
                    //    inputToken = token.GetHashCode();
                    //}


                }

                //string json = Newtonsoft.Json.JsonConvert.SerializeObject(OnlineGames.GetGames());
                //Send(json);
            }




            //if (message[0] == "ADMIN")
            //{
            //    if (message[1] == "ADDGAME")
            //    {
            //        GobangGame game = new GobangGame();
            //        game.Administrator = new Player(message[1]);
            //        OnlineGames.AddGame(game);
            //    }
            //    if (message[1] == "GETGAMES")
            //    {
            //        string json = Newtonsoft.Json.JsonConvert.SerializeObject(OnlineGames.GetGames());
            //        Send(json);
            //    }


            //}
            base.OnMessage(e);
        }
    }

    class PlayActions : WebSocketBehavior
    {

    }



    public class LoginCallbackMessage
    {
        public LoginCallbackMessage(string messageString) => MessageString = messageString;
        public string MessageString;
        public int Token;
    }

    public class User
    {
        public int Id;
        public string Name;
        public string Type;
        public string Password;
        public string Token;
    }

    public class MainScreenMessage
    {
        public string Head { get; set; }
        public List<GobangGame> Games { get; set; }
        public List<Player> Players { get; set; }

        public MainScreenMessage(string head)
        {
            Head = head;
            this.Games = OnlineGames.GetGames();
            this.Players = OnlinePlayers.GetPlayers();
        }

    }

}
