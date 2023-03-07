using App.Middleware.PokeApi.Service.Interfaces;

namespace App.Middleware.PokeApi.Console
{
    public class Executor
    {
        private readonly IPokemonService _pokemonService;
        public Executor(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        public void Execute(string[] args)
        {
            bool salir = false;

            while (!salir)
            {
                try
                {
                    System.Console.WriteLine("1. Listar Movimientos");
                    System.Console.WriteLine("2. Consultar Pokemons");
                    System.Console.WriteLine("3. Salir");
                    System.Console.WriteLine("Elige una de las opciones");
                    int opcion = Convert.ToInt32(System.Console.ReadLine());
                    System.Console.WriteLine();

                    switch (opcion)
                    {
                        case 1:
                            System.Console.WriteLine("Has elegido la opción: Listar Movimientos");
                            var moves = _pokemonService.GetMoves().Result;
                            int index = 1;
                            moves.ToList().ForEach(move =>
                            {
                                System.Console.WriteLine($"Movimiento {index}: {move.Name} ");
                                index++;
                            });
                            break;
                        case 2:
                            System.Console.WriteLine("Has elegido la Consultar Pokemons");
                            System.Console.WriteLine();
                            System.Console.WriteLine("Ingresa los ids de los Pokemon a consultar");
                            System.Console.WriteLine("Ejemplo: 1,2,3,4,5,6...");
                            string? ids = System.Console.ReadLine();
                            var pokemonList = _pokemonService.GetPokemons(Array.ConvertAll(ids.Split(','), int.Parse)).Result;
                            pokemonList.ForEach(pokemon =>
                            {
                                System.Console.WriteLine(pokemon.ToString());
                                System.Console.WriteLine("------------------------------------------");
                            });
                            break;
                        case 3:
                            System.Console.WriteLine("Has elegido salir de la aplicación");
                            salir = true;
                            break;
                        default:
                            System.Console.WriteLine("Elige una opcion entre 1 y 3");
                            break;
                    }

                }
                catch (FormatException e)
                {
                    System.Console.WriteLine(e.Message);
                }

                System.Console.WriteLine("\n\t");
            }

            System.Console.ReadLine();
        }
    }
}
