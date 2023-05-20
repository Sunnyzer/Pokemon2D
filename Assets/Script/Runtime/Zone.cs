using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum Rarity
{
    VeryCommun,
    Commun,
    MediumRare,
    Rare,
    VeryRare,
}
public class RarityPokemons
{
    Rarity rarity;
    PokemonChoice[] pokmChoices;
    public RarityPokemons(Rarity _rarity, PokemonChoice[] _pokemon)
    {
        rarity = _rarity;
        pokmChoices = _pokemon;
    }
}
public class Zone : MonoBehaviour
{
    [SerializeField] string zoneName = string.Empty;
    [SerializeField] BindingFlags BindingFlags;
    [SerializeField] PokemonChoice[] veryCommun;
    [SerializeField] PokemonChoice[] commun;
    [SerializeField] PokemonChoice[] mediumRare;
    [SerializeField] PokemonChoice[] rare;
    [SerializeField] PokemonChoice[] veryRare;
    Dictionary<Rarity, PokemonChoice[]> pokemonZone = new Dictionary<Rarity, PokemonChoice[]>()
    {
        { Rarity.VeryCommun, null },
        { Rarity.Commun, null },
        { Rarity.MediumRare, null },
        { Rarity.Rare, null},
        { Rarity.VeryRare, null},
    };
    private void Start()
    {
        pokemonZone[Rarity.VeryCommun] = veryCommun;
        pokemonZone[Rarity.Commun] = commun;
        pokemonZone[Rarity.MediumRare] = mediumRare;
        pokemonZone[Rarity.Rare] = rare;
        pokemonZone[Rarity.VeryRare] = veryRare;
    }
    public Pokemon GetPokemonEncounter(Rarity _rarity)
    {
        PokemonChoice[] pokemonChoices = pokemonZone[_rarity];
        if (pokemonChoices.Length <= 0) return null;
        int _index = Random.Range(0, pokemonChoices.Length);
        Pokemon _pokemon = Pokemon.GetRandomPokemon(Random.Range(3, 7), PokemonDataSO.GetPokemon(pokemonChoices[_index]));
        return _pokemon;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerPokemon _playerPokemon = other.GetComponent<PlayerPokemon>();

        if (!_playerPokemon)
        {
            Grass _grass = other.GetComponent<Grass>();
            if (!_grass) return;
            _grass.GetType().GetField("currentZone", BindingFlags).SetValue(_grass, this);
            return;
        }
        Debug.Log("Enter " + zoneName);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerPokemon _playerPokemon = collision.GetComponent<PlayerPokemon>();
        if (!_playerPokemon) return;
        Debug.Log("Exit " + zoneName);
    }
}
