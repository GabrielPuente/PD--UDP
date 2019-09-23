using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using UDP.Class;

namespace UDP
{
    public class Program
    {
        private static List<User> UserList = new List<User>
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

            thread.Start();
            while (true)
            {
                Console.WriteLine("\n");
                foreach (var item in UserList)
                {
                    if (item.Retry > 2)
                    {
                        Dead(item);

                        if (item.Leader)
                            SwapLeader();

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

        public static bool HasPriority(User user)
        {
            return user.Priority < FindCurrentLeader().Priority;
        }

        public static User FindNewLeader()
        {
            return UserList.First(x => x.Priority == UserList.Min(y => y.Priority) && x.Alive);
        }

        public static User FindCurrentLeader()
        {
            return UserList.Find(u => u.Leader);
        }

        public static User FindUser(string ip)
        {
            return UserList.Find(u => u.Ip == ip);
        }

        public static void SwapLeader()
        {
            FindCurrentLeader().Leader = false;
            FindNewLeader().Leader = true;
        }

        public static void Reset(User user)
        {
            user.Retry = 0;
            user.Alive = true;
        }
        
        private static void Dead(User user)
        {
            user.Alive = false;
        }
    }
}