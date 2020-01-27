using GeoLocation.Provider.GeoApi.Proxy;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeoLocation.Provider.GeoApi
{
    public class Communicator
    {
        private readonly IHttpClientFactory _httpWebClient;
        public readonly AppSettings _appSettings;

        public Communicator(IHttpClientFactory httpWebClient, IOptions<AppSettings> appSettings)
        {
            _httpWebClient = httpWebClient;
            _appSettings = appSettings.Value;
        }

        public async Task<IpLocationResponse> GetGeoLocationAsync(string ipAddress)
        {
            IpLocationResponse result = null;
            try
            {
                if (string.IsNullOrEmpty(_appSettings.GetAeoApi.Url) || string.IsNullOrEmpty(_appSettings.GetAeoApi.ApiKey))
                    return result;

                var url = $"{_appSettings.GetAeoApi.Url}{ipAddress}?api_key={_appSettings.GetAeoApi.ApiKey}";

                using (var httpClient = _httpWebClient.CreateClient())
                {
                    var response = await httpClient.GetAsync(url);

                    var data = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(data))
                    {
                        result = JsonConvert.DeserializeObject<IpLocationResponse>(data);
                    }
                }
            }
            catch
            {
                throw new Exception("Error getting ip geo location");
            }
            return result;
        }
    }
}
