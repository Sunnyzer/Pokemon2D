using UnityEngine;

public class BattleUI : UI
{
    [SerializeField] SelectMoveUI selectMoveUI;
    
    [SerializeField] PokemonSwapUI pokemonSwapUI;

    [SerializeField] PokemonInfoUI playerPokemonInfoUI;
    [SerializeField] PokemonInfoUI opponentPokemonInfoUI;
    
    [SerializeField] SubUIManagement subUIManagement;

    public PokemonSwapUI PokemonSwapUI => pokemonSwapUI;

    private void Start()
    {
        subUIManagement.Init();
    }
    public void StartBattle(BattleField _battleField)
    {
        SetField(_battleField);
    }
    public override void DeactivateUI()
    {
        base.DeactivateUI();
        StopBattle();
    }
    private void StopBattle()
    {
        subUIManagement.Reset();
        SetField(null);
    }
    private void SetField(BattleField _battleField)
    {
        playerPokemonInfoUI.SetBattleField(_battleField);
        opponentPokemonInfoUI.SetBattleField(_battleField);
    }
}
