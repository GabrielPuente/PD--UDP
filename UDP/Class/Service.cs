using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP
{
    public class Service
    {
        private const int port = 60000;
        public static void ReceiveMessage()
        {
            string hr = "heartbeat request";
            var client = new UdpClient(port);
            var ip = new IPEndPoint(IPAddress.Any, port);
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            try
            {
                while (true)
                {
                    byte[] bytes = client.Receive(ref ip);
                    var message = $"\n{ip.Address.ToString()}:\t{Encoding.UTF8.GetString(bytes, 0, bytes.Length)}";
                    Console.WriteLine(message);

                    if (message.ToLower().Contains(hr))
                        Client.Send(socket, ip.Address.ToString());
                }             
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                client.Close();
            }
        }
    }
}
