using GeoLocation.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace WeightedRoundRobin.Api.Middlewares
{
    public class CountryRestrictionMiddleware
    {
        private const string NullIpAddress = "::1";

        private readonly RequestDelegate _next;
        private readonly GeoLocation.Provider.GeoApi.Communicator _communicator;
        public readonly AppSettings _appSettings;

        public CountryRestrictionMiddleware(RequestDelegate next, GeoLocation.Provider.GeoApi.Communicator communicator, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _communicator = communicator;
            _appSettings = appSettings.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress.ToString();

            if (ipAddress != NullIpAddress)
            {
                var geoLocation = await _communicator.GetGeoLocationAsync(ipAddress);
                if (_appSettings.AllowedCountryCodes?.Contains(geoLocation?.country?.code?.ToUpper()) != true)
                {
                    context.Response.Clear();
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                    await context.Response.WriteAsync($"Access from {geoLocation.country.name} [{geoLocation.country.code}] country not authorized.");
                }
            }
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}