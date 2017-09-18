using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameSocket.Game
{
    public class GobangGame
    {
        //public int[] GameGrids;

        public List<Grid> GameGrids = new List<Grid>();
        public List<Player> Players = new List<Player>();

        public int X, Y;

        public GobangGame()
        {
            GameGrids.Capacity = 255;
            this.X = 15;
            this.Y = 15;
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }
        public void ExecuteStep(int x, int y, Player player)
        {
            Grid grid = GetGrid(x, y);
            int value = Players.IndexOf(player);
            SetGrid(x, y, value, player.Name);
        }
        public Grid GetGrid(int x, int y)
        {
            return GameGrids[X * y + x];
        }
        public void SetGrid(int x, int y, int value, string name)
        {
            this.GameGrids[X * y + x] = new Grid(value, name, DateTime.Now);
        }
    }

    public class Grid
    {
        public Grid(int value, string playerName, DateTime time)
        {
            this.PlayerName = playerName;
            this.Value = value;
            this.Time = time;
        }
        public string PlayerName;
        public DateTime Time;
        public int Value;
    }

    public class Player
    {
        public string Name;
        public Player(string name)
        {
            Name = name;
        }
    }
}
