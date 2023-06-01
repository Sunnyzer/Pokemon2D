using UnityEngine;

public class BattleUI : UI
{
    [SerializeField] MyPokemonUIInfo myPokemon;
    [SerializeField] OpponentPokemonUIInfo opponentPokemon;
    [SerializeField] SelectMoveUI selectMoveUI;
    [SerializeField] PokemonSwapUI pokemonSwapUI;
    [SerializeField] Transform menuButton;

    public PokemonSwapUI PokemonSwapUI => pokemonSwapUI;

    private void Start()
    {
        pokemonSwapUI.Init();
        pokemonSwapUI.Deactivate();
    }
    public void UpdatePokemonInfo(Pokemon _pokemon)
    {
        myPokemon.InitInfo(_pokemon);
        selectMoveUI.InitMove(_pokemon);
    }
    public void SetInfoOpponentPokemon(Pokemon _pokemon)
    {
        opponentPokemon.InitInfo(_pokemon);
    }
    public override void DeactivateUI()
    {
        base.DeactivateUI();
        ResetMenu();
    }

    public void ResetMenu()
    {
        selectMoveUI.DeactivateUI();
        menuButton.gameObject.SetActive(true);
    }

    public void FightButton()
    {
        selectMoveUI.ActivateUI();
        menuButton.gameObject.SetActive(false);
    }
    public void PokemonButton()
    {
        pokemonSwapUI.DisplayUI();
    }
    public void BagButton()
    {
        Debug.Log("Bag");
    }
    public void RunButton()
    {
        BattleManager.Instance.StopBattle();
    }
}
