using UnityEngine;

public class PokemonTeam : MonoBehaviour
{
    public const int maxTeam = 6;
    [SerializeField] PokemonChoice[] pokemonsChoices;
    Pokemon[] pokemons = new Pokemon[maxTeam];
    
    public Pokemon[] Pokemons => pokemons;
    public int Lenght => pokemons.Length;

    public Pokemon this[int index]
    {
        get => pokemons[index];
    }
    private void Start()
    {
        for (int i = 0; i < pokemonsChoices.Length; i++)
        {
            Pokemon _p = PokemonManager.Instance.GeneratePokemon(Random.Range(7, 12), pokemonsChoices[i]);
            pokemons[i] = _p;
        }
    }

    public Pokemon GetFirstLivingPokemon()
    {
        for (int i = 0; i < pokemons.Length; ++i)
        {
            if (!pokemons[i].Fainted)
                return pokemons[i];
        }
        return null;
    }
    public bool HavePokemonLeft()
    {
        for (int i = 0; i < pokemons.Length; ++i)
        {
            if (!pokemons[i].Fainted)
                return true;
        }
        return false;
    }
    public void SwapPokemonPlacement(int _source, int _destination)
    {
        if (_destination >= maxTeam || _destination < 0) return;
        if (_source >= maxTeam || _source < 0) return;
        Pokemon _pokemonToSwap = pokemons[_source];
        pokemons[_source] = pokemons[_destination];
        pokemons[_destination] = _pokemonToSwap;
    }
}
