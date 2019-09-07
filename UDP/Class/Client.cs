using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP
{
    public class Client
    {
        public static void Send(Socket socket, string ip, int port = 60000)
        {
            //Console.Write("Digite: ");
            //var message = Console.ReadLine();
            //var address = IPAddress.Parse(ip);
            //var bytes = Encoding.UTF8.GetBytes(message);
            //var ep = new IPEndPoint(address, port);
            //socket.SendTo(bytes, ep);
            //port = 60000; 

            var address = IPAddress.Parse(ip);
            var bytes = Encoding.UTF8.GetBytes("Heartbeat Reply");
            var ep = new IPEndPoint(address, port);
            socket.SendTo(bytes, ep);
        }
    }
}
