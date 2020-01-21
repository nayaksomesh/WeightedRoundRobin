using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

namespace WeightedRoundRobin
{
    public static class Algorithm
    {
        private static readonly List<Server> _servers = new List<Server>();

        private static int GetGcd(List<Server> servers)
        {
            var serverIndex = lastChoosenServer;
            if (serverIndex < 0)
                serverIndex = 0;

            int a = servers[serverIndex].Weight;

            if (serverIndex >= servers.Count - 1)
                serverIndex = -1;

            int b = servers[serverIndex + 1].Weight;
            while (b != 0)
            {
                var t = b;
                b = a % b;
                a = t;
            }
            return a;
        }

        private static int GetGcd(Server server1, Server server2)
        {
            int a = server1.Weight;
            int b = server2.Weight;
            while (b != 0)
            {
                var t = b;
                b = a % b;
                a = t;
            }
            return a;
        }

        private static int GetMaxWeight(List<Server> servers)
        {
            int max = 0;
            foreach (var s in servers)
            {
                if (s.Weight > max)
                    max = s.Weight;
            }
            Console.WriteLine($"Max weight: {max}");
            return max;
        }

        private static int lastChoosenServer = -1;
        private static int gcd = 0;
        private static int currentDispatchWeight = -1;
        private static int max = 0;

        private static SpinLock _sLock = new SpinLock(true);

        private static IPAddress GenerateRandomIp()
        {
            var data = new byte[4];
            new Random().NextBytes(data);
            return new IPAddress(data);
        }

        public static Server RegisterServer(int weight, int cpuThreshold, int cpuUsage = -1, IPAddress ipAddress = null)
        {
            Server server = null;
            if (weight > 0)
            {
                server = new Server()
                {
                    Id = _servers.Count + 1,
                    //IP = ipAddress,// ?? GenerateRandomIp(),
                    Weight = weight,
                    CpuThreshold = cpuThreshold >= 0 ? cpuThreshold : 0
                };
                if (cpuUsage >= 0)
                    server.CpuUsage = cpuUsage;

                _servers.Add(server);
            }
            max = GetMaxWeight(_servers);
            return server;
        }

        public static int RegisterServer(Server server)
        {
            if (server.Weight > 0 && server.CpuThreshold > 0)
            {
                server.Id = _servers.Count + 1;

                _servers.Add(server);
                max = GetMaxWeight(_servers);
                return server.Id;
            }
            return 0;
        }

        public static void Update(Server server)
        {
            if (server == null)
            {
                var message = $"Server cannot be null";
                throw new ArgumentException(message);
            }
            if (server.CpuUsage < 0)
            {
                var message = $"Invalid CPU usage";
                throw new ArgumentException(message);
            }
            else
            {
                var selectedServer = _servers.FirstOrDefault(o => o.Id == server.Id);
                if (selectedServer != null)
                {
                    selectedServer.CpuUsage = server.CpuUsage;
                }
                else
                {
                    var message = $"Invalid Server {server.Id}";
                    throw new ArgumentException(message);
                }
            }
        }

        public static void UpdateServerCPUThreshold(int serverId, int cpuUsage)
        {
            if (cpuUsage < 0)
            {
                var message = $"Invalid CPU usage";
                throw new ArgumentException(message);
            }
            else
            {
                if (serverId > 0 && serverId <= _servers.Count)
                {
                    _servers[serverId - 1].CpuUsage = cpuUsage;
                }
                else
                {
                    var message = $"Invalid Server {serverId}";
                    throw new ArgumentException(message);
                }
            }
        }

        public static List<Server> GetList()
        {
            return _servers;
        }

        public static Server GetServer()
        {
            if (_servers?.Any() != true)
                return null;

            bool isLocked = false;
            _sLock.Enter(ref isLocked);

            do
            {
                do
                {
                    gcd = GetGcd(_servers);

                    lastChoosenServer = (lastChoosenServer + 1) % _servers.Count;
                    if (lastChoosenServer == 0)
                    {
                        currentDispatchWeight -= gcd;
                        if (currentDispatchWeight <= 0)
                        {
                            currentDispatchWeight = max;
                            if (currentDispatchWeight == 0)
                                return null;
                        }
                    }
                } while (_servers[lastChoosenServer].Weight < currentDispatchWeight);
                //Console.WriteLine($"Selected server{lastChoosenServer + 1}, CurrentDispatchWeight: {currentDispatchWeight}");
            } while (_servers[lastChoosenServer].CpuThreshold < _servers[lastChoosenServer].CpuUsage);

            if (isLocked)
            {
                _sLock.Exit(true);
            }
            //for (var i = 0; i < _servers.Count; i++)
            //Console.WriteLine($"Id: {_servers[i].Id}, IP: {_servers[i].IP}, CpuThreshold: {_servers[i].CpuThreshold}, Cpu Usage: {_servers[i].CpuUsage}, Weight {_servers[i].Weight}");
            return _servers[lastChoosenServer];
        }
    }
}
