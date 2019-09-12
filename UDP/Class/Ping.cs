using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP
{
    public class Ping
    {
        public static void Send(Socket socket, string ip, int port = 60000)
        {
            string hr = "Heartbeat Request";

            var address = IPAddress.Parse(ip);
            var bytes = Encoding.UTF8.GetBytes(hr);
            var ep = new IPEndPoint(address, port);
            socket.SendTo(bytes, ep);
            Console.WriteLine($"{ip} \t Sent Message - {hr}");
        }
    }
}
