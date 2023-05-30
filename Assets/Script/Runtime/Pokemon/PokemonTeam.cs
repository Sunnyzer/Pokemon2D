using UnityEngine;

public class PokemonTeam : MonoBehaviour
{
    public const int maxTeam = 6;
    [SerializeField] PokemonChoice[] pokemonsChoices;
    [SerializeField] Pokemon[] pokemons = new Pokemon[maxTeam];
    public Pokemon this[int index]
    {
        get => pokemons[index];
    }
    private void Start()
    {
        pokemons[0] = PokemonManager.Instance.GeneratePokemon(10, PokemonManager.Instance.GetPokemonDataByPokemonChoice(pokemonsChoices[0]));
        pokemons[1] = PokemonManager.Instance.GeneratePokemon(10, PokemonManager.Instance.GetPokemonDataByPokemonChoice(pokemonsChoices[1]));
    }
}
