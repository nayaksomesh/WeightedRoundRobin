namespace GeoLocation.Provider.GeoApi.Proxy
{
    public class IpLocationResponse
    {
        public Country country { get; set; }
        public Time time { get; set; }

        //public Area area { get; set; }
        //public Asn asn { get; set; }
        //public City city { get; set; }
        //public Continent continent { get; set; }
        //public Currency currency { get; set; }
        //public string ip { get; set; }
        //public Location location { get; set; }
        //public string postcode { get; set; }
        //public Security security { get; set; }
        //public string status { get; set; }
        //public string type { get; set; }

        //public class Area
        //{
        //    public string code { get; set; }
        //    public string name { get; set; }
        //}

        //public class Asn
        //{
        //    public string number { get; set; }
        //    public string organisation { get; set; }
        //}

        //public class City
        //{
        //    public int geonameid { get; set; }
        //    public string name { get; set; }
        //    public int population { get; set; }
        //}

        //public class Continent
        //{
        //    public string code { get; set; }
        //    public int geonameid { get; set; }
        //    public string name { get; set; }
        //}

        public class Country
        {
            public string code { get; set; }
            public string name { get; set; }

            //public string area_size { get; set; }
            //public string capital { get; set; }
            //public int geonameid { get; set; }
            //public bool is_in_eu { get; set; }
            //public Languages languages { get; set; }
            //public string phone_code { get; set; }
            //public int population { get; set; }
        }

        //public class Languages
        //{
        //    public string _as { get; set; }
        //    public string bh { get; set; }
        //    public string bn { get; set; }
        //    public string doi { get; set; }
        //    public string en { get; set; }
        //    public string fr { get; set; }
        //    public string gu { get; set; }
        //    public string hi { get; set; }
        //    public string inc { get; set; }
        //    public string kn { get; set; }
        //    public string kok { get; set; }
        //    public string ks { get; set; }
        //    public string lus { get; set; }
        //    public string ml { get; set; }
        //    public string mni { get; set; }
        //    public string mr { get; set; }
        //    public string ne { get; set; }
        //    public string or { get; set; }
        //    public string pa { get; set; }
        //    public string sa { get; set; }
        //    public string sat { get; set; }
        //    public string sd { get; set; }
        //    public string sit { get; set; }
        //    public string ta { get; set; }
        //    public string te { get; set; }
        //    public string ur { get; set; }
        //}

        //public class Currency
        //{
        //    public string code { get; set; }
        //    public string name { get; set; }
        //}

        //public class Location
        //{
        //    public float latitude { get; set; }
        //    public float longitude { get; set; }
        //}

        //public class Security
        //{
        //    public bool is_crawler { get; set; }
        //    public bool is_proxy { get; set; }
        //    public bool is_thread { get; set; }
        //    public bool is_tor { get; set; }
        //}

        public class Time
        {
            public int? gtm_offset { get; set; }
            public string time { get; set; }
            public string timezone { get; set; }
        }
    }
}
