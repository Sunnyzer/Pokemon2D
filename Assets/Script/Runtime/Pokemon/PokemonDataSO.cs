using UnityEngine;

[CreateAssetMenu(fileName = "PokemonDataTable")]
public class PokemonDataSO : ScriptableObject
{
    [SerializeField] PokemonData[] allPokemon;
    public PokemonData[] AllPokemon => allPokemon;
}
