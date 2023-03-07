using Newtonsoft.Json;

namespace App.Middleware.PokeApi.Common.Dto
{
    public class PokemonResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("base_experience")]
        public int BaseExperience { get; set; }

        [JsonProperty("is_default")]
        public bool IsDefault { get; set; }

        [JsonProperty("types")]
        public List<Type> Types { get; set; }

        [JsonProperty("moves")]
        public List<Move> Moves { get; set; }

        [JsonProperty("sprites")]
        public Sprites Sprites { get; set; }

        [JsonProperty("location_area_encounters")]
        public string LocationAreaEncounters { get; set; }

        [JsonProperty("area")]
        public LocationArea? Area { get; set; }
    }

    public class Type
    {
        [JsonProperty("slot")]
        public int Slot { get; set; }

        [JsonProperty("type")]
        public BaseResponse TypeObject { get; set; }
    }

    public class Move
    {
        [JsonProperty("move")]
        public BaseResponse MoveObject { get; set; }
    }

    public class Sprites
    {
        [JsonProperty("back_default")]
        public string BackDefault { get; set; }

        [JsonProperty("front_default")]
        public string FrontDefault { get; set; }
    }

}
