using System.Net;

namespace WeightedRoundRobin
{
    public class Server
    {
        public int Id;
        //public IPAddress IP;
        public int Weight;
        public int CpuThreshold;
        public int CpuUsage;
    }
}
