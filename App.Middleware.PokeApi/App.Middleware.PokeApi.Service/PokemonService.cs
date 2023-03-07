using App.Middleware.PokeApi.Common.Dto;

using App.Middleware.PokeApi.Service.Interfaces;

using Newtonsoft.Json;

using System.Collections.Concurrent;

namespace App.Middleware.PokeApi.Service
{
    public class PokemonService : IPokemonService
    {
        public ConcurrentBag<BaseResponse> MovesList { get; set; }

        public ConcurrentBag<PokemonResponse> PokemonList { get; set; }


        public PokemonService()
        {
            MovesList = new ConcurrentBag<BaseResponse>();
            PokemonList = new ConcurrentBag<PokemonResponse>();
        }

        public async Task<ConcurrentBag<BaseResponse>> GetMoves()
        {
            int limit = 20;
            const int OFF_SET = 0;
            int totalIterations = 0;
            int totalThreads = 10;
            string urlBase = "https://pokeapi.co/api/v2/move?offset={0}&limit={1}";

            GenericResponse serviceResponse = GetRestService(string.Format(urlBase, OFF_SET * limit, limit)).Result;
            if (serviceResponse != null && serviceResponse.Results != null && serviceResponse.Results.Count > 0)
            {
                serviceResponse.Results.ForEach(m =>
                {
                    MovesList.Add(m);
                });

                totalIterations = serviceResponse.Count / limit;
                Parallel.For(1, totalIterations, new ParallelOptions { MaxDegreeOfParallelism = totalThreads },
                async (index) =>
                {
                    serviceResponse = GetRestService(string.Format(urlBase, index * 20, limit)).GetAwaiter().GetResult();
                    if (serviceResponse != null && serviceResponse.Results != null && serviceResponse.Results.Count > 0)
                    {
                        serviceResponse.Results.ForEach(m =>
                        {
                            MovesList.Add(m);
                        });
                    }
                });
            }

            return MovesList;
        }


        public async Task<List<PokemonDto>> GetPokemons(int[] idArray)
        {
            string urlBase = "https://pokeapi.co/api/v2/pokemon/{0}/";
            List<BaseResponse> typesList = GetRestService("https://pokeapi.co/api/v2/type").Result.Results;

            Parallel.ForEach(idArray, async (id) =>
            {
                PokemonResponse pokemon = GetPokemonRestService(string.Format(urlBase, id)).GetAwaiter().GetResult();
                var pokemonAreas = GetAreaRestService(pokemon.LocationAreaEncounters).GetAwaiter().GetResult().FirstOrDefault();
                if (pokemonAreas != null && pokemonAreas.LocationArea != null)
                    pokemon.Area = pokemonAreas.LocationArea;

                PokemonList.Add(pokemon);
            });

            List<PokemonDto> listPokemon = PokemonList.Select(p => new PokemonDto
            {
                Id = p.Id,
                Name = p.Name,
                Height = p.Height,
                Weight = p.Weight,
                Types = String.Join(", ", typesList.Select(t => t.Name).ToList()),
                TypeNames = String.Join(", ", p.Types.Select(t => t.TypeObject.Name).ToList()),
                Moves = String.Join(", ", p.Moves.Select(t => t.MoveObject.Name).Take(3).ToList()),
                Img1 = p.Sprites.FrontDefault,
                Img2 = p.Sprites.BackDefault,
                AreaName = p.Area != null ? p.Area.Name : string.Empty

            }).ToList();

            return listPokemon;
        }

        private static async Task<PokemonResponse> GetPokemonRestService(string url)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string? content = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(content))
                return JsonConvert.DeserializeObject<PokemonResponse>(content);

            return null;
        }

        private static async Task<List<AreaResponse>> GetAreaRestService(string url)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string? content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<AreaResponse>>(content);
        }

        private static async Task<GenericResponse> GetRestService(string url)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string? content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GenericResponse>(content);
        }
    }
}
