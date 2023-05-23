using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    [SerializeField] BattleUI battleUI;
    [SerializeField] int encounterAmount = 0; 
    public void StartBattle(PlayerPokemon _player, Pokemon _pokemon)
    {
        encounterAmount++;
        UIManager.Instance.SetCurrentUIDisplay(battleUI);
        battleUI.InitBattle(_player, _pokemon);
    }
}