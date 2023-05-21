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

[System.Serializable]
public class PokemonEncouterParameter
{
    [SerializeField] PokemonChoice pokemon;
    [SerializeField] int levelMinEncounter = 1;
    [SerializeField] int levelMaxEncounter = 10;
    [SerializeField] bool male = true;
    [SerializeField] float chanceToEncounter = 10;
    public PokemonChoice Pokemon => pokemon;
    public float ChanceToEncounter => chanceToEncounter;
    public int GetLevelBetweenMinMax()
    {
        return Random.Range(levelMinEncounter, levelMaxEncounter + 1);
    }
}

public class PokemonsInZoneByRarity
{
    Rarity rarity = Rarity.VeryCommun;
    PokemonEncouterParameter[] pokemonsEncounter;

    public Rarity Rarity => rarity;
}

public class Zone : MonoBehaviour
{
    [SerializeField] string zoneName = string.Empty;
    [SerializeField] BindingFlags BindingFlags;
    [SerializeField] Sprite zoneFont;
    [SerializeField] List<PokemonsInZoneByRarity> pokemonsByRarity = new List<PokemonsInZoneByRarity>();
    [SerializeField] float chanceToEncounterPokemon = 20;

    public string ZoneName => zoneName;
    public Sprite ZoneFont => zoneFont;
    private void Start()
    {
        ZoneManager.Instance.AddZone(this);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerPokemon _playerPokemon = other.GetComponent<PlayerPokemon>();
        if (!_playerPokemon)
        {
            EncounterCell _encounterCell = other.GetComponent<EncounterCell>();
            InitEncounterCell(_encounterCell);
            return;
        }
        EnterZone();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerPokemon _playerPokemon = collision.GetComponent<PlayerPokemon>();
        if (!_playerPokemon) return;
        ExitZone();
    }
    public PokemonsInZoneByRarity GetPokemonsByRarity(Rarity _rarity)
    {
        for (int i = 0; i < pokemonsByRarity.Count; i++)
            if (pokemonsByRarity[i].Rarity == _rarity)
                return pokemonsByRarity[i];
        return null;
    }
    public Pokemon GetPokemonEncounter(Rarity _rarity)
    {
        //PokemonsInZoneByRarity _pokemonInZone = GetPokemonsByRarity(_rarity);
        //PokemonParam[] _pokemonChoices = _pokemonInZone.po;
        //if (_pokemonChoices.Length <= 0) return null;
        //int _index = Random.Range(0, _pokemonChoices.Length);
        //Pokemon _pokemon = PokemonManager.Instance.GeneratePokemon(_pokemonChoices[_index].GetLevelBetweenMinMax(), PokemonManager.Instance.GetPokemonData(pokemonChoices[_index].Pokemon));
        //return _pokemon;
        return null;
    }
    public PokemonData GetPokemonEncounter(PokemonEncouterParameter[] _pokemons)
    {
        return null;
    }
    void InitEncounterCell(EncounterCell _cell)
    {
        if (_cell)
            _cell.CurrentZone = this;
    }
    void EnterZone()
    {
        ZoneManager.Instance.ChangeZone(this);
    }
    void ExitZone()
    {
        
    }
}
