using Newtonsoft.Json;

namespace App.Middleware.PokeApi.Common.Dto
{
    public class AreaResponse
    {
        [JsonProperty("location_area")]
        public LocationArea LocationArea { get; set; }
    }

    public class LocationArea
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
