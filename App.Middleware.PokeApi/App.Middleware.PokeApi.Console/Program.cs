
using App.Middleware.PokeApi.Console;
using App.Middleware.PokeApi.Service;
using App.Middleware.PokeApi.Service.Interfaces;

using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        services.AddSingleton<Executor, Executor>()
                .BuildServiceProvider()
                .GetService<Executor>()
                .Execute(args);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IPokemonService, PokemonService>();
    }
}

