using System.Collections.Generic;
using UnityEngine;

public class PokemonManager : Singleton<PokemonManager>
{
    [SerializeField] PokemonDataSO pokemonDataSO;
    public PokemonData GetPokemonDataByIndex(int _index)
    {
        return pokemonDataSO.AllPokemon[_index];
    }
    public PokemonData GetPokemonDataByPokemonChoice(PokemonChoice _pokemonChoice)
    {
        return GetPokemonDataByIndex(_pokemonChoice.IndexPokemon);
    }
    public Pokemon GeneratePokemon(int _level, PokemonData _pokemonSpecies)
    {
        List<Move> _moves = MoveManager.Instance.GenerateMoveByLevel(_level, _pokemonSpecies);
        Pokemon _pokemon = new Pokemon(_level, _pokemonSpecies, Stat.GetRandomIV(), Nature.GetRandomNature(), _moves);
        return _pokemon;
    }
    public Pokemon GeneratePokemon(int _level, PokemonChoice _pokemonChoice)
    {
        PokemonData _pokemonSpecies = GetPokemonDataByPokemonChoice(_pokemonChoice);
        List<Move> _moves = MoveManager.Instance.GenerateMoveByLevel(_level, _pokemonSpecies);
        Pokemon _pokemon = new Pokemon(_level, _pokemonSpecies, Stat.GetRandomIV(), Nature.GetRandomNature(), _moves);
        return _pokemon;
    }

}
