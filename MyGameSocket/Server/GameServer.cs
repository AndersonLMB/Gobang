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


namespace MyGameSocket.Server
{
    class GameServer
    {

        public WebSocketServer WSGameServer;

        public GameServer(string url)
        {
            WSGameServer = new WebSocketServer(url);
            WSGameServer.AddWebSocketService<PlayerActions>("/PlayerActions");
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

    public class LoginCallbackMessage
    {
        public LoginCallbackMessage(string messageString) => MessageString = messageString;
        public string MessageString;
    }

    public class User
    {
        public int id;
        public string name;
        public string type;
        public string password;
    }

}
