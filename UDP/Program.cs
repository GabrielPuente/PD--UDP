using System;
using System.Net.Sockets;
using System.Threading;

namespace UDP
{
    public class Program
    {
        public static void Main()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var thread = new Thread(Service.ReceiveMessage);
            var port = 60000;
            Console.Write("IP: ");
            var ips = Console.ReadLine();
            thread.Start();
            //string [] ips = { "172.18.3.101", "172.18.0.6", "172.18.0.7", "172.18.3.51", "172.18.0.19" };
            //Console.Write("Port: ");
            //var port = Console.ReadLine();
            while (true)
            {
                foreach (string item in ips.Split(';'))
                {
                    Ping.Send(socket, item, port);
                }
                Thread.Sleep(500);
            }
        }
    }
}