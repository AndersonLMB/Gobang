using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Net;
using System.Threading;


namespace MySocket
{
    class Program
    {


        static void Main(string[] args)
        {

        }
    }

    public static class PublicChess
    {
        public static List<int> ChessList;
        public static int value = 1;
        public static int GetValue()
        {
            return value;
        }
        public static void SetValue(int valueToSet)
        {
            value = valueToSet;
        }
    }
}
