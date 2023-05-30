using UnityEngine;

public class BattleUI : UI
{
    PlayerPokemon player = null;
    Pokemon pokemon = null;
    [SerializeField] MyPokemonUIInfo myPokemon;
    [SerializeField] OpponentPokemonUIInfo opponentPokemon;
    [SerializeField] SelectMoveUI selectMoveUI;
    [SerializeField] Transform menuButton; 
     
    public void InitBattle(PlayerPokemon _player, Pokemon _pokemon)
    {
        player = _player;
        pokemon = _pokemon;
        myPokemon.InitInfo(player.PokemonTeam[0]);
        opponentPokemon.InitInfo(pokemon);
        selectMoveUI.InitMove(player.PokemonTeam[0]);
    }
    public void RunButton()
    {
        player.IsInBattle = false;
        myPokemon.Desinit();
        opponentPokemon.Desinit();
        UIManager.Instance.RemoveQueueSetPreviousUI();
    }
    public void FightButton()
    {
        Debug.Log("Fight");
        selectMoveUI.ActivateUI();
        menuButton.gameObject.SetActive(false);
    }
    public void ResetMenu()
    {
        selectMoveUI.DeactivateUI();
        menuButton.gameObject.SetActive(true);
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
