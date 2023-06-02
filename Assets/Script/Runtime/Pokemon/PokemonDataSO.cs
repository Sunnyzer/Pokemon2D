using System;
using UnityEngine;

[Serializable]
public class AllPokemon
{
    [SerializeField] public PokemonData[] allPokemon;
}

[CreateAssetMenu(fileName = "PokemonDataTable")]
public class PokemonDataSO : ScriptableObject
{
    [SerializeField] public AllPokemon allPokemonData;
    public PokemonData[] AllPokemon => allPokemonData.allPokemon;
}
