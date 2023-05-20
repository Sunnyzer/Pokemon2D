using UnityEngine;

public class BattleUI : UI
{
    PlayerPokemon player = null;
    Pokemon pokemon = null;
    [SerializeField] PokemonUIInfo myPokemon;
    [SerializeField] PokemonUIInfo cPokemon;

    public void InitBattle(PlayerPokemon _player, Pokemon _pokemon)
    {
        player = _player;
        pokemon = _pokemon;
        myPokemon.InitInfo(player.PokemonTeam[0]);
        cPokemon.InitInfo(pokemon);
    }
    public void RunButton()
    {
        UIManager.Instance.RemoveQueueSetPreviousUI();
    }
    public void FightButton()
    {
        
    }
    public void BagButton()
    {

    }
    public void PokemonButton()
    {
    }
}
