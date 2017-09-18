using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Net;
using System.Threading;


namespace Socket
{
    class Program
    {


        static void Main(string[] args)
        {

            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8086);
            server.Start();
            Console.WriteLine("Server has started on 127.0.0.1:8086.{0}Waiting for a connection...", Environment.NewLine);

            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("A client connected.");
            Console.WriteLine("Hello World");
            Console.ReadLine();
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
