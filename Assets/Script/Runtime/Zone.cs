using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
    VeryCommun,
    Commun,
    MediumRare,
    Rare,
    VeryRare,
}

public class Zone : MonoBehaviour
{
    [SerializeField] Sprite zoneFont;
    [SerializeField] ZoneDataSO zoneData;
    public RarityRate[] RarityRate => zoneData.RarityRate;
    public string ZoneName => name;
    public Sprite ZoneFont => zoneFont;
    public float ChanceToEncounterPokemon => zoneData.ChanceToEncounterPokemon;

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
        for (int i = 0; i < zoneData.PokemonsInZoneByRarities.Count; i++)
            if (zoneData.PokemonsInZoneByRarities[i].Rarity == _rarity)
                return zoneData.PokemonsInZoneByRarities[i];
        return null;
    }
    public Pokemon GetPokemonEncounter(Rarity _rarity)
    {
        PokemonsInZoneByRarity _pokemonInZone = GetPokemonsByRarity(_rarity);
        if (_pokemonInZone == null) return null;
        PokemonEncouterParameter[] _pokemonEncouterParameters = _pokemonInZone.PokemonsEncounter;
        if (_pokemonEncouterParameters.Length <= 0) return null;
        if (_pokemonEncouterParameters.Length == 1) return PokemonManager.Instance.GeneratePokemon(_pokemonEncouterParameters[0].GetLevelBetweenMinMax(), PokemonManager.Instance.GetPokemonData(_pokemonEncouterParameters[0].Pokemon));
        float _proba = Random.Range(0, 1000)/10f;
        float _chance = 0;
        for (int i = 0; i < _pokemonEncouterParameters.Length; i++)
        {
            PokemonEncouterParameter _pokemonEncouterParameter = _pokemonEncouterParameters[i];
            float _chanceToEncounter = _pokemonEncouterParameter.ChanceToEncounter;
            if(_proba >= _chance && _proba <= _chance + _chanceToEncounter)
            {
                PokemonData _data = PokemonManager.Instance.GetPokemonData(_pokemonEncouterParameter.Pokemon);
                return PokemonManager.Instance.GeneratePokemon(_pokemonEncouterParameter.GetLevelBetweenMinMax(), _data);
            }
            _chance += _chanceToEncounter;
        }
        return null;
    }
    public PokemonData GetPokemonEncounter(PokemonEncouterParameter[] _pokemons)
    {
        return null;
    }
    void InitEncounterCell(EncounterCell _cell)
    {
        if (!_cell) return;
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
