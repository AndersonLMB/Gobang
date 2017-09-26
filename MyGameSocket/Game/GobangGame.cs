using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyGameSocket.Game
{
    public class GobangGame
    {
        //public int[] GameGrids;
        public string Name;
        public List<Grid> GameGrids = new List<Grid>();
        public List<Player> Players = new List<Player>();
        public Player Administrator;
        public List<WinLog> WinLogs;
        public int Time;
        //public int UnixTime;
        public List<Step> Steps = new List<Step>();
        public int X, Y;
        public int WinCount;

        public GobangGame()
        {
            X = 15;
            Y = 15;
            WinCount = 5;
            Time = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            for (var i = 0; i < X * Y; i++)
            {
                Grid grid = new Grid(-1, "null", DateTime.Now);
                GameGrids.Add(grid);
            }
            WinLogs = new List<WinLog>();
            Name = Time.ToString();
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }
        public void ExecuteStep(int x, int y, Player player, out string stepMessage)
        {
            stepMessage = "";
            Grid grid = GetGrid(x, y);
            if (grid.Value >= 0)
            {

            }
            else
            {
                int value = Players.IndexOf(player);

                if (Steps.Count == 0)
                {
                    SetGrid(x, y, value, player.Name);
                    Step step = new Step(player, DateTime.Now, value);
                    Steps.Add(step);
                }
                else
                {
                    if (value != Steps[Steps.Count - 1].Value)
                    {
                        SetGrid(x, y, value, player.Name);
                        Step step = new Step(player, DateTime.Now, value);
                        Steps.Add(step);
                    }
                }
                if (Win())
                {
                    stepMessage = "WIN";
                }
            }

        }
        public Grid GetGrid(int x, int y)
        {
            return GameGrids[X * y + x];
        }
        public void SetGrid(int x, int y, int value, string name)
        {
            this.GameGrids[X * y + x] = new Grid(value, name, DateTime.Now);
        }

        public Boolean Win()
        {
            if (RowWin() || ColumnWin() || BackslashWin() || SlashWin())
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        //RowWin
        private Boolean RowWin()
        {
            int value;
            bool win = false;
            for (var y = 0; y < Y; y++)
            {
                for (var x = 0; x <= X - WinCount; x++)
                {
                    Grid grid = GetGrid(x, y);
                    value = grid.Value;
                    if (value != -1)
                    {
                        Boolean tempWin = true;
                        for (var i = 0; i < 5; i++)
                        {
                            if (GetGrid(x + i, y).Value != value)
                            {
                                tempWin = false;
                            }
                        }
                        if (tempWin)
                        {
                            WinLog winLog = new WinLog();
                            winLog.WinGrid = grid;
                            winLog.WinType = "ROW";
                            WinLogs.Add(winLog);
                            win = tempWin;
                        }
                    }
                }
            }
            return win;
        }
        //ColumnWin
        private Boolean ColumnWin()
        {
            int value;
            bool win = false;
            for (var y = 0; y < Y - WinCount; y++)
            {
                for (var x = 0; x <= X; x++)
                {
                    Grid grid = GetGrid(x, y);
                    value = grid.Value;
                    if (value != -1)
                    {
                        Boolean tempWin = true;
                        for (var i = 0; i < 5; i++)
                        {
                            if (GetGrid(x, y + i).Value != value)
                            {
                                tempWin = false;
                            }
                        }
                        if (tempWin)
                        {
                            WinLog winLog = new WinLog();
                            winLog.WinGrid = grid;
                            winLog.WinType = "COLUMN";
                            WinLogs.Add(winLog);
                            win = tempWin;
                        }
                    }
                }
            }
            return win;
        }
        //BackslashWin
        private Boolean BackslashWin()
        {
            int value;
            bool win = false;
            for (var y = 0; y < Y - WinCount; y++)
            {
                for (var x = 0; x <= X - WinCount; x++)
                {
                    Grid grid = GetGrid(x, y);
                    value = grid.Value;
                    if (value != -1)
                    {
                        Boolean tempWin = true;
                        for (var i = 0; i < 5; i++)
                        {
                            if (GetGrid(x + i, y + i).Value != value)
                            {
                                tempWin = false;
                            }
                        }
                        if (tempWin)
                        {
                            WinLog winLog = new WinLog();
                            winLog.WinGrid = grid;
                            winLog.WinType = "BACKSLASH";
                            WinLogs.Add(winLog);
                            win = tempWin;
                        }
                    }
                }
            }
            return win;
        }
        //SlashWin
        private Boolean SlashWin()
        {
            int value;
            bool win = false;
            for (var y = 0; y < Y - WinCount; y++)
            {
                for (var x = 0; x <= X - WinCount; x++)
                {
                    Grid grid = GetGrid(x, y + WinCount - 1);
                    value = grid.Value;
                    if (value != -1)
                    {
                        Boolean tempWin = true;
                        for (var i = 0; i < 5; i++)
                        {
                            if (GetGrid(x + i, y + WinCount - i - 1).Value != value)
                            {
                                tempWin = false;
                            }
                        }
                        if (tempWin)
                        {
                            WinLog winLog = new WinLog();
                            winLog.WinGrid = grid;
                            winLog.WinType = "SLASH";
                            WinLogs.Add(winLog);
                            win = tempWin;
                        }
                    }
                }
            }
            return win;
        }

        public void Drawself()
        {
            Console.WriteLine();
            for (var y = 0; y < Y; y++)
            {
                for (var x = 0; x < X; x++)
                {
                    if (GetGrid(x, y).Value == -1)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(GetGrid(x, y).Value);
                    }
                }
                Console.WriteLine();
            }
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
        private DateTime Time;
        public int Value;
    }

    public class Player
    {
        public string Name;
        public Player(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
    }

    public class WinLog
    {
        public Grid WinGrid;
        public string WinType;

    }

    public class Step
    {
        public Player Player;
        public DateTime Time;
        public int Value;

        public Step(Player player, DateTime time, int value)
        {
            Player = player;
            Time = time;
            Value = value;
        }
    }
}
