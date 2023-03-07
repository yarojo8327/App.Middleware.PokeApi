using App.Middleware.PokeApi.Common.Dto;

using System.Collections.Concurrent;

namespace App.Middleware.PokeApi.Service.Interfaces
{
    public interface IPokemonService
    {
        Task<ConcurrentBag<BaseResponse>> GetMoves();

        Task<List<PokemonDto>> GetPokemons(int[] idArray);
    }
}
