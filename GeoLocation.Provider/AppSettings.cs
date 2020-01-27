using System.Collections.Generic;

namespace GeoLocation.Provider
{
    public class AppSettings
    {
        public ApiConfig GetAeoApi { get; set; }
        public List<string> AllowedCountryCodes { get; set; } = new List<string>();
    }

    public class ApiConfig
    {
        public string Url { get; set; }
        public string ApiKey { get; set; }
    }
}
