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

            string[] ips = { "172.18.3.101", "172.18.0.6", "172.18.0.7", "172.18.3.51", "172.18.0.19", "172.18.0.10", "172.18.3.71" };

            thread.Start();
            while (true)
            {
                Console.WriteLine("\n");
                foreach (string item in ips)
                {
                    Ping.Send(socket, item, port);
                }
                Thread.Sleep(5000);
            }
        }
    }
}
