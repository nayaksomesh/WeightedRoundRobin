namespace Assignment.Api.Request
{
    public class ServerRequest
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public int CpuThreshold { get; set; }
        public int CpuUsage { get; set; }
    }
}