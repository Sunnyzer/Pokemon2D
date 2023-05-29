using System;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class RarityRate
{
    [SerializeField] Rarity rarity = Rarity.Commun;
    [SerializeField, Range(0, 100)] float rate = 0;
    public Rarity Rarity => rarity;
    public float Rate => rate;
    public RarityRate(Rarity _rarity, float _rate)
    {
        rarity = _rarity;
        rate = _rate;
    }
}

public class EncounterCell : TileSprite
{

    public event Action<EncounterCell> OnEnterCell = null;
    [SerializeField] Zone cellZone = null;
    public Zone CellZone
    {
        get => cellZone;
        set => cellZone = value;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPokemon _player = collision.GetComponent<PlayerPokemon>();
        EnterCell(_player);
    }
    public void EnterCell(PlayerPokemon _playerPokemon)
    {
        if (!_playerPokemon) return;
        OnEnterCell?.Invoke(this);
        float _proba = Random.Range(0, 1000)/10f;
        if (_proba > cellZone.ChanceToEncounterPokemon) return;
        Pokemon _pokemon = null;
        float _chance = 0;
        float _ratio = Random.Range(0, 1000)/10f;
        RarityRate _rateMax = new RarityRate(Rarity.VeryCommun, 0);
        foreach (var item in cellZone.RarityRate)
        {
            float _chanceToEncounter = item.Rate;
            if (_rateMax.Rate < _chanceToEncounter)
                _rateMax = item;
            if (_ratio >= _chance && _ratio < _chance + _chanceToEncounter)
            {
                _pokemon = cellZone.GetPokemonEncounter(item.Rarity);
                break;
            }
            _chance += _chanceToEncounter;
        }
        if (_pokemon == null)
            _pokemon = cellZone.GetPokemonEncounter(_rateMax.Rarity);
        BattleManager.Instance.StartBattle(_playerPokemon, _pokemon);
    }    
}
