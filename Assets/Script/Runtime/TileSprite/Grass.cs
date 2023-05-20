using UnityEngine;

public class Grass : TileSprite
{
    [SerializeField] Zone currentZone = null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPokemon _player = collision.GetComponent<PlayerPokemon>();
        if (!_player) return;
        int _proba = Random.Range(0, 100);
        if (_proba < 1.25 / 187.5f * 100)
        {
            Pokemon _pokemon = currentZone.GetPokemonEncounter(Rarity.VeryRare);
            if (_pokemon != null)
            {
                BattleManager.Instance.StartBattle(_player, _pokemon);
                return;
            }
        }
        if (_proba < 3.33f / 187.5f * 100)
        {
            Pokemon _pokemon = currentZone.GetPokemonEncounter(Rarity.Rare);
            if (_pokemon != null)
            {
                BattleManager.Instance.StartBattle(_player, _pokemon);
                return;
            }
        }
        if (_proba < 6.75f / 187.5f * 100)
        {
            Pokemon _pokemon = currentZone.GetPokemonEncounter(Rarity.MediumRare);
            if (_pokemon != null)
            {
                BattleManager.Instance.StartBattle(_player, _pokemon);
                return;
            }
        }
        if (_proba < 8.5f / 187.5f * 100)
        {
            Pokemon _pokemon = currentZone.GetPokemonEncounter(Rarity.Commun);
            if (_pokemon != null)
            {
                BattleManager.Instance.StartBattle(_player, _pokemon);
                return;
            }
        }
        if(_proba < 10/187.5f * 100)
        {
            Pokemon _pokemon = currentZone.GetPokemonEncounter(Rarity.VeryCommun);
            if (_pokemon != null)
            {
                BattleManager.Instance.StartBattle(_player, _pokemon);
                return;
            }
        }
    }
}
