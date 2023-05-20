using UnityEngine;

[CreateAssetMenu(fileName = "PokemonDataTable")]
public class PokemonDataSO : ScriptableObject
{
    public static PokemonData GetPokemon(int _index)
    {
        PokemonDataSO _pokemonDataSO = (PokemonDataSO)Resources.Load("PokemonData");
        //Debug.Log(_index + " " + _pokemonDataSO.allPokemon.Length);
        return _pokemonDataSO.allPokemon[_index];
    }
    [SerializeField] public PokemonData[] allPokemon;
}
