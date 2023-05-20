using UnityEngine;

public class PokemonTeam : MonoBehaviour
{
    public const int maxTeam = 6;
    [SerializeField] PokemonChoice[] pokemonsChoices;
    Pokemon[] pokemons = new Pokemon[maxTeam];
    public Pokemon this[int index]
    {
        get => pokemons[index];
    }
    private void Start()
    {
        pokemons[0] = Pokemon.GetRandomPokemon(10, PokemonDataSO.GetPokemon(pokemonsChoices[0]));
    }
}
