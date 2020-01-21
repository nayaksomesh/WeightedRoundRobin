using System;
using System.Collections.Generic;

namespace Accops_Assignment
{
    class Program
    {
        static readonly List<int> Options = new List<int>()
            {
                1,  //Update CPU Usage
                2   //Get server
            };

        static void Main(string[] args)
        {
            string serverCountInput;
            int serverCount;
            do
            {
                Console.WriteLine("Enter No Of Sever # : ");
                serverCountInput = Console.ReadLine();
            } while (!int.TryParse(serverCountInput, out serverCount));

            for (var i = 1; i <= serverCount; i++)
            {
                string serverWeightInput;
                string serverCPUThresholdInput;

                int serverWeight;
                do
                {
                    Console.WriteLine($"Server {i} Weight # ");
                    serverWeightInput = Console.ReadLine();
                } while (!int.TryParse(serverWeightInput, out serverWeight));

                int serverCPUThreshold;
                do
                {
                    Console.WriteLine($"Server {i} CPU Threshold # ");
                    serverCPUThresholdInput = Console.ReadLine();
                } while (!int.TryParse(serverCPUThresholdInput, out serverCPUThreshold));

                WeightedRoundRobin.Algorithm.RegisterServer(serverWeight, serverCPUThreshold);
            }

            Console.WriteLine("Option 1 to Update CPU Usage");
            Console.WriteLine("Option 2 to get server");
            Console.WriteLine();

            string selectedOptionInput;
            int selectedOption;
            while (true)
            {
                do
                {
                    Console.WriteLine("Enter Option :");
                    selectedOptionInput = Console.ReadLine();
                } while (!int.TryParse(selectedOptionInput, out selectedOption) && !Options.Contains(selectedOption));

                switch (selectedOption)
                {
                    case 1:
                        string serverIndexInput;
                        int serverId;
                        do
                        {
                            Console.WriteLine("Enter Server # : ");
                            serverIndexInput = Console.ReadLine();
                        } while (!int.TryParse(serverIndexInput, out serverId) && serverId >= 0 && serverId <= serverCount);

                        string cpuThresholdInput;
                        int cpuThreshold;
                        do
                        {
                            Console.WriteLine("Enter CPU Usage : ");
                            cpuThresholdInput = Console.ReadLine();
                        } while (!int.TryParse(cpuThresholdInput, out cpuThreshold) && cpuThreshold >= 0);
                        try
                        {
                            WeightedRoundRobin.Algorithm.UpdateServerCPUThreshold(serverId, cpuThreshold);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Internal server error");
                        }
                        break;
                    case 2:
                        var server = WeightedRoundRobin.Algorithm.GetServer();
                        Console.WriteLine($"New connection from Server{server.Id}");
                        //Console.WriteLine($"Id: {server.Id}, IP: {server.IP}, CpuThreshold: {server.CpuThreshold}, Cpu Usage: {server.CpuUsage}, Weight {server.Weight}");
                        break;
                }
            }
        }
    }
}
