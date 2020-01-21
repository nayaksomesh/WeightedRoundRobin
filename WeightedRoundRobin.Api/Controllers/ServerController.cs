using Assignment.Api.Request;
using Assignment.Api.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeightedRoundRobin;
using WeightedRoundRobin.Api.Extensions;

namespace Assignment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServerController : ControllerBase
    {
        [HttpGet("list")]
        public IActionResult GetList()
        {
            var server = Algorithm.GetList().ToResponseModelList();
            return Ok(server);
        }

        public IActionResult Get()
        {
            var response = new BaseResponse();
            var server = Algorithm.GetServer();
            if (server == null)
            {
                response.Errors.Add("No server found");
                return BadRequest(response);
            }

            response.Messages.Add($"New connection from Server{server.Id}");
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add([FromBody]List<ServerRequest> servers)
        {
            var response = new BaseResponse();

            servers?.RemoveAll(o => o == null);
            if (servers?.Any() != true)
            {
                response.Errors.Add("No servers found in request.");
                return BadRequest(response);
            }

            for (var i = 0; i < servers.Count; i++)
            {
                if (servers[i].CpuThreshold <= 0)
                    response.Errors.Add($"Server {i + 1} CpuThreshold cannot be less than 0.");
                if (servers[i].Weight <= 0)
                    response.Errors.Add($"Server {i + 1} Weight cannot be less than 0.");
                if (servers[i].CpuUsage < 0)
                    response.Errors.Add($"Server {i + 1} CpuUsage cannot be less than 0.");
            }

            if (response.Errors.Any())
            {
                return BadRequest(response);
            }

            foreach (var server in servers)
            {
                var serverId = Algorithm.RegisterServer(server.ToData());
                if (serverId > 0)
                    response.Messages.Add($"Server{serverId} added successfully");
            }
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update([FromBody]ServerRequest serverRequest)
        {
            var response = new BaseResponse();
            try
            {
                Algorithm.Update(serverRequest.ToData());
            }
            catch (ArgumentException ex)
            {
                response.Errors.Add(ex.Message);
            }
            catch (Exception)
            {
                response.Errors.Add("Internal server error");
            }
            return Ok(response);
        }
    }
}
