using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Linq;
using UDP.Class;

namespace UDP
{
    public class Program
    {
        public static List<User> UserList = new List<User>
            {
                new User { Priority = 0, Ip = "172.18.0.7", Leader = true },
                new User { Priority = 1, Ip = "172.18.1.116"},
                new User { Priority = 3, Ip = "172.18.0.23"},
                new User { Priority = 4, Ip = "172.18.3.51"},
                new User { Priority = 5, Ip = "172.18.0.19"},
                new User { Priority = 6, Ip = "172.18.0.15"},
                new User { Priority = 7, Ip = "172.18.0.29"},
            };
        public static void Main()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var thread = new Thread(Service.ReceiveMessage);
            var port = 60000;


            //string[] ips = {"172.18.0.7", "172.18.3.51", "172.18.0.19", "172.18.1.116", "172.18.0.23", "172.18.0.29", "172.18.0.15" };

            thread.Start();
            while (true)
            {
                Console.WriteLine("\n");
                foreach (var item in UserList)
                {
                    if (item.Retry > 2)
                    {
                        item.Alive = false;

                        if (item.Leader)
                            ChangeLeader();

                        continue;
                    }
                    if (item.Alive)
                    {
                        item.Retry++;
                        Ping.Send(socket, item.Ip, port);
                    }
                }
                Thread.Sleep(5000);
            }



        }

        private static void ChangeLeader()
        {
            //UserList.Find(u => u.Alive == true && u.Priority);

            //foreach (var item in collection)
            //{

            //}
        }

        //public static void Reset(string ip)
        //{
        //    var objip = UserList.Find(u => u.Ip == ip);
        //    if (objip.Retry > 1)
        //        objip.Retry = 0;
        //}
    }
}
