using UnityEngine;

public class BattleUI : UI
{
    [SerializeField] SelectMoveUI selectMoveUI;
    
    [SerializeField] PokemonSwapUI pokemonSwapUI;

    [SerializeField] PokemonInfoUI playerPokemonInfoUI;
    [SerializeField] PokemonInfoUI opponentPokemonInfoUI;

    public PokemonSwapUI PokemonSwapUI => pokemonSwapUI;

    public void StartBattle(BattleField _battleField)
    {
        SetField(_battleField);
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
    public void DisplaySwapPokemon()
    {
        subUIManagement.ActiveSubUI(pokemonSwapUI);
    }
    public override void OnActivate()
    {
        
    }

    public override void OnDeactivate()
    {
        StopBattle();
    }
}
