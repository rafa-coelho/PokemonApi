namespace Pokedex.Data.Model;
public class PokemonModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string[] Evolutions { get; set; }
    public string Sprite { get; set; }
}
