using UnityEngine;

public class PokemonManager : Singleton<PokemonManager>
{
    [SerializeField] PokemonDataSO pokemonDataSO;
    //public PokemonDataSO AllPokemon => pokemonDataSO;
    public PokemonData GetPokemonData(int _index)
    {
        return pokemonDataSO.AllPokemon[_index];
    }
    public Pokemon GeneratePokemon(int _level, PokemonData _pokemonSpecies)
    {
        return new Pokemon(_level, _pokemonSpecies, Stat.GetRandomIV(), Nature.GetRandomNature());
    }
}
