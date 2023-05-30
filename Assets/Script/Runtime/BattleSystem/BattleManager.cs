using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    [SerializeField] BattleUI battleUI;
    [SerializeField] Pokemon pokemon;
    [SerializeField] PlayerPokemon playerPokemon;

    public void StartBattle(PlayerPokemon _player, Pokemon _pokemon)
    {
        _player.IsInBattle = true;
        pokemon = _pokemon;
        playerPokemon = _player;
        UIManager.Instance.SetCurrentUIDisplay(battleUI);
        battleUI.InitBattle(_player, _pokemon);
    }
    public void FinishTurn(Move _moveUse)
    {
        if(pokemon.CurrentStat.Speed < playerPokemon.PokemonTeam[0].CurrentStat.Speed)
        {
            Debug.Log("Damage =>"+ pokemon.Name + " with " + playerPokemon.PokemonTeam[0].Name);
            pokemon.TakeDamage(playerPokemon.PokemonTeam[0], _moveUse);
            if (pokemon.Fainted)
            {
                Debug.Log("Finish Turn");
                playerPokemon.IsInBattle = false;
                UIManager.Instance.RemoveQueueSetPreviousUI();
                return;
            }
            int _count = Random.Range(0, pokemon.Moves.Length);
            if(pokemon.Moves.Length != 0)
                playerPokemon.PokemonTeam[0].TakeDamage(pokemon, pokemon.Moves[_count]);
            if (playerPokemon.PokemonTeam[0].Fainted)
            {
                Debug.Log("Finish Turn");
                playerPokemon.IsInBattle = false;
                UIManager.Instance.RemoveQueueSetPreviousUI();
                return;
            }
        }
        else if (pokemon.CurrentStat.Speed > playerPokemon.PokemonTeam[0].CurrentStat.Speed)
        {
            Debug.Log("Damage =>"+ playerPokemon.PokemonTeam[0].Name + " with " + pokemon.Name);
            int _count = Random.Range(0, pokemon.Moves.Length);
            if (pokemon.Moves.Length != 0)
                playerPokemon.PokemonTeam[0].TakeDamage(pokemon, pokemon.Moves[_count]);
            if (playerPokemon.PokemonTeam[0].Fainted)
            {
                Debug.Log("Finish Turn");
                playerPokemon.IsInBattle = false;
                UIManager.Instance.RemoveQueueSetPreviousUI();
                return;
            }
            pokemon.TakeDamage(playerPokemon.PokemonTeam[0], _moveUse);
            if (pokemon.Fainted)
            {
                Debug.Log("Finish Turn");
                playerPokemon.IsInBattle = false;
                UIManager.Instance.RemoveQueueSetPreviousUI();
                return;
            }
        }
        else
        {
            Debug.Log("Damage =>" + pokemon.Name + " with " + playerPokemon.PokemonTeam[0].Name);
            pokemon.TakeDamage(playerPokemon.PokemonTeam[0], _moveUse);
            if (pokemon.Fainted)
            {
                Debug.Log("Finish Turn");
                playerPokemon.IsInBattle = false;
                UIManager.Instance.RemoveQueueSetPreviousUI();
                return;
            }
            int _count = Random.Range(0, pokemon.Moves.Length);
            if (pokemon.Moves.Length != 0)
                playerPokemon.PokemonTeam[0].TakeDamage(pokemon, pokemon.Moves[_count]);
            if (playerPokemon.PokemonTeam[0].Fainted)
            {
                Debug.Log("Finish Turn");
                playerPokemon.IsInBattle = false;
                UIManager.Instance.RemoveQueueSetPreviousUI();
                return;
            }
        }
        battleUI.ResetMenu();
    }
}