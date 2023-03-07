using System.Text;

namespace App.Middleware.PokeApi.Common.Dto
{
    public class PokemonDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Weight { get; set; }

        public int Height { get; set; }

        public string Types { get; set; }

        public string TypeNames { get; set; }

        public string RelatedPokemon { get; set; }

        public string Moves { get; set; }

        public string Img1 { get; set; }

        public string Img2 { get; set; }

        public string AreaName { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Id: {this.Id}");
            sb.AppendLine($"Name: {this.Name}");
            sb.AppendLine($"Height: {this.Height}");
            sb.AppendLine($"Weight: {this.Weight}");
            sb.AppendLine($"Types: {this.Types}");
            sb.AppendLine($"Type Name: {this.TypeNames}");
            sb.AppendLine($"Moves: {this.Moves}");
            sb.AppendLine($"Img1: {this.Img1}");
            sb.AppendLine($"Img2: {this.Img2}");
            sb.AppendLine($"Nombre Area: {this.AreaName}");
            return sb.ToString();
        }
    }
}
