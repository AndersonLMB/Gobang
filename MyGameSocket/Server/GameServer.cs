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


namespace MyGameSocket.Server
{
    class GameServer
    {

        public WebSocketServer WSGameServer;

        public GameServer(string url)
        {
            WSGameServer = new WebSocketServer(url);
            WSGameServer.AddWebSocketService<PlayerLogin>("/PlayerLogin");
        }

        public void Start()
        {
            this.WSGameServer.Start();
        }




    }

    class PlayerLogin : WebSocketBehavior
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
                Player player = new Player(message[1]);
                OnlinePlayers.AddPlayer(player);
            }

            if (message[0] == "ADMIN")
            {
                GobangGame game = new GobangGame();
                game.Administrator = new Player(message[1]);
                OnlineGames.AddGame(game);

                //Player player = new Player(message[1]);
                //OnlinePlayers.AddPlayer(player);
            }



            base.OnMessage(e);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
        }
    }

}
