using UnityEngine;

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
    [SerializeField] Zone currentZone = null;
    public Zone CurrentZone
    {
        get => currentZone;
        set => currentZone = value;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPokemon _player = collision.GetComponent<PlayerPokemon>();
        if (!_player) return;
        EnterCell(_player);
    }
    public bool EnterCell(PlayerPokemon _playerPokemon)
    {
        float _proba = Random.Range(0, 1000)/10f;
        if (_proba > currentZone.ChanceToEncounterPokemon) return false;
        Pokemon _pokemon = null;
        float _chance = 0;
        float _ratio = Random.Range(0, 1000)/10f;
        RarityRate _rateMax = new RarityRate(Rarity.VeryCommun, 0);
        foreach (var item in currentZone.RarityRate)
        {
            float _chanceToEncounter = item.Rate;
            if (_rateMax.Rate < _chanceToEncounter)
                _rateMax = item;
            if (_ratio >= _chance && _ratio < _chance + _chanceToEncounter)
            {
                _pokemon = currentZone.GetPokemonEncounter(item.Rarity);
                break;
            }
            _chance += _chanceToEncounter;
        }
        if (_pokemon == null)
            _pokemon = currentZone.GetPokemonEncounter(_rateMax.Rarity);
        BattleManager.Instance.StartBattle(_playerPokemon, _pokemon);
        return true;
    }    
}
