using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PokemonEncouterParameter
{
    [SerializeField] PokemonChoice pokemon;
    [SerializeField] int levelMinEncounter = 1;
    [SerializeField] int levelMaxEncounter = 10;
    [SerializeField] bool male = true;
    [SerializeField] public float chanceToEncounter = 10;
    public Rect? rect = null;
    public PokemonChoice Pokemon => pokemon;
    public float ChanceToEncounter => chanceToEncounter;
    public int GetLevelBetweenMinMax()
    {
        return Random.Range(levelMinEncounter, levelMaxEncounter + 1);
    }
}
[System.Serializable]
public class PokemonsInZoneByRarity
{
    [SerializeField] Rarity rarity = Rarity.VeryCommun;
    [SerializeField] public List<PokemonEncouterParameter> pokemonsEncounter = new List<PokemonEncouterParameter>();
    [SerializeField] float rarityRate = 20;

    public List<PokemonEncouterParameter> PokemonsEncounter => pokemonsEncounter;
    public float RarityRate => rarityRate;
    public Rarity Rarity => rarity;
}

[CreateAssetMenu(fileName = "ZoneData")]
public class ZoneDataSO : ScriptableObject
{
    [SerializeField] float chanceToEncounterPokemon = 20;
    [SerializeField] List<PokemonsInZoneByRarity> pokemonsInZoneByRarities = new List<PokemonsInZoneByRarity>();
    public float ChanceToEncounterPokemon => chanceToEncounterPokemon;
    public RarityRate[] RarityRate => pokemonsInZoneByRarities.Select(pokemonZone => new RarityRate(pokemonZone.Rarity, pokemonZone.RarityRate)).ToArray();
    public List<PokemonsInZoneByRarity> PokemonsInZoneByRarities => pokemonsInZoneByRarities;
}
