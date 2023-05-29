using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    [SerializeField] BattleUI battleUI;
    public void StartBattle(PlayerPokemon _player, Pokemon _pokemon)
    {
        _player.IsInBattle = true;
        UIManager.Instance.SetCurrentUIDisplay(battleUI);
        battleUI.InitBattle(_player, _pokemon);
    }
}