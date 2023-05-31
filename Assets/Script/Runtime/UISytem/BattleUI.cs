using UnityEngine;

public class BattleUI : UI
{
    PlayerTrainer player = null;
    Pokemon pokemon = null;
    [SerializeField] MyPokemonUIInfo myPokemon;
    [SerializeField] OpponentPokemonUIInfo opponentPokemon;
    [SerializeField] SelectMoveUI selectMoveUI;
    [SerializeField] Transform menuButton; 
     
    public void SetInfoMyPokemon(PlayerTrainer _trainer)
    {
        myPokemon.InitInfo(_trainer.GetPokemon());
        selectMoveUI.InitMove(_trainer);
    }
    public void SetInfoOpponentPokemon(Pokemon _pokemon)
    {
        opponentPokemon.InitInfo(_pokemon);
    }
    public void DesinitBattle()
    {
        myPokemon.Desinit();
        opponentPokemon.Desinit();
        UIManager.Instance.RemoveQueueSetPreviousUI();
    }

    public void ResetMenu()
    {
        selectMoveUI.DeactivateUI();
        menuButton.gameObject.SetActive(true);
    }

    public void RunButton()
    {
        DesinitBattle();
    }
    public void FightButton()
    {
        Debug.Log("Fight");
        selectMoveUI.ActivateUI();
        menuButton.gameObject.SetActive(false);
    }
    public void BagButton()
    {
        Debug.Log("Bag");
    }
    public void PokemonButton()
    {
        Debug.Log("Pokemon");
    }
}
