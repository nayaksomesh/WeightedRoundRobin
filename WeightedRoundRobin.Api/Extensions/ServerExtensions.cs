using Assignment.Api.Request;
using Assignment.Api.Response;
using System.Collections.Generic;
using System.Linq;

namespace WeightedRoundRobin.Api.Extensions
{
    public static class ServerExtensions
    {
        public static Server ToData(this ServerRequest request)
        {
            if (request == null)
                return null;

            return new Server()
            {
                Id = request.Id,
                CpuThreshold = request.CpuThreshold,
                CpuUsage = request.CpuUsage,
                Weight = request.Weight
            };
        }

        public static ServerRequest ToModel(this Server server)
        {
            if (server == null)
                return null;

            return new ServerRequest()
            {
                Id = server.Id,
                CpuThreshold = server.CpuThreshold,
                CpuUsage = server.CpuUsage,
                Weight = server.Weight
            };
        }

        public static ServerResponse ToResponseModel(this Server server)
        {
            if (server == null)
                return null;

            return new ServerResponse()
            {
                Id = server.Id,
                CpuThreshold = server.CpuThreshold,
                CpuUsage = server.CpuUsage,
                Weight = server.Weight
            };
        }
        public static List<ServerResponse> ToResponseModelList(this List<Server> serverList)
        {
            var serverResponseList = new List<ServerResponse>();
            if (serverList?.Any() != true)
                return serverResponseList;

            foreach (var server in serverList)
            {
                var serverResponse = server.ToResponseModel();

                if (serverResponse != null)
                    serverResponseList.Add(serverResponse);
            }
            return serverResponseList;
        }
    }
}
