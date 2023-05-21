using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterCell : TileSprite
{
    [SerializeField] Zone currentZone = null;
    public Zone CurrentZone
    {
        get => currentZone;
        set => currentZone = value;
    }
    public static Dictionary<Rarity, float> rariryRatio = new Dictionary<Rarity, float>()
    {
        { Rarity.VeryRare, 1.25f/187.5f },
        { Rarity.Rare, 3.33f/187.5f },
        { Rarity.MediumRare, 6.75f/187.5f },
        { Rarity.Commun, 8.5f/187.5f },
        { Rarity.VeryCommun, 10f/187.5f },
    };
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPokemon _player = collision.GetComponent<PlayerPokemon>();
        if (!_player) return;
        float _proba = Random.Range(0f, 100f);
        if (_proba < 157.67f / 187.5f * 100) return;
        Pokemon _pokemon = null;
        foreach (var item in rariryRatio)
        {
            _pokemon = EncounterSuccess(Random.Range(0f, 29.83f / 187.5f * 100), item.Key);
            if (_pokemon != null) break;
        }
        if (_pokemon == null) return;
        BattleManager.Instance.StartBattle(_player, _pokemon);
    }

    public Pokemon EncounterSuccess(float _proba, Rarity _rarity)
    {
        if (_proba < rariryRatio[_rarity] * 100)
        {
            Pokemon _pokemon = currentZone.GetPokemonEncounter(_rarity);
            return _pokemon;
        }
        return null;
    }
}
