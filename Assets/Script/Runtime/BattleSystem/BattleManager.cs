using UnityEngine;
using System;

[Serializable]
public class BattleInfo
{
    [SerializeField] public BattleFighter opponentFighter;
    [SerializeField] public BattleFighter myFighter;
    public BattleInfo(BattleFighter opponentFighter, BattleFighter myFighter)
    {
        this.opponentFighter = opponentFighter;
        this.myFighter = myFighter;
    }
}

public class BattleManager : Singleton<BattleManager>
{
    [SerializeField] BattleUI battleUI;
    PlayerTrainer playerTrainer;
    WildPokemon wildPokemon;
    public PlayerTrainer PlayerTrainer => playerTrainer;

    private void Update()
    {
        if (!playerTrainer || !playerTrainer.IsReady) return;
        BattleTurn();
    }
    public void StartBattleWildPokemon(PlayerTrainer _player, WildPokemon _wildPokemon)
    {
        if (_player.GetFirstSlotPokemon() == null) return;
        _player.IsInBattle = true;
        wildPokemon = _wildPokemon;
        playerTrainer = _player;

        playerTrainer.OnSwapPokemon -= battleUI.UpdatePokemonInfo;
        playerTrainer.OnSwapPokemon += battleUI.UpdatePokemonInfo;

        battleUI.UpdatePokemonInfo(_player.CurrentPokemonInCombat);
        battleUI.SetInfoOpponentPokemon(_wildPokemon.CurrentPokemonInCombat);
        UIManager.Instance.SetCurrentUIDisplay(battleUI);
    }
    public void BattleTurn()
    {
        playerTrainer.IsReady = false;
        BattleInfo _playerInfo = new BattleInfo(wildPokemon, playerTrainer);
        BattleInfo _wildPokemonInfo = new BattleInfo(playerTrainer, wildPokemon);
        TurnAction _action = playerTrainer.Turn(_playerInfo);
        TurnAction _action2 = wildPokemon.Turn(_wildPokemonInfo);

        int _priority = _action.GetPriority(_action2);
        int _priority2 = _action2.GetPriority(_action);
        if (_priority > _priority2)
        {
            Debug.Log(playerTrainer.CurrentPokemonInCombat.Name + " > " + wildPokemon.CurrentPokemonInCombat.Name);
            _action.Turn();
            if (IsBattleFinish(playerTrainer, wildPokemon))
            {
                BattleFinish();
                return;
            }
            _action2.Turn();
            if (IsBattleFinish(playerTrainer, wildPokemon))
            {
                BattleFinish();
                return;
            }
        }
        else if (_priority < _priority2)
        {
            Debug.Log(playerTrainer.CurrentPokemonInCombat.Name + " < " + wildPokemon.CurrentPokemonInCombat.Name);
            _action2.Turn();
            if (IsBattleFinish(playerTrainer, wildPokemon))
            {
                BattleFinish();
                return;
            }
            _action.Turn();
            if (IsBattleFinish(playerTrainer, wildPokemon))
            {
                BattleFinish();
                return;
            }
        }
        else
        {
            Debug.Log(playerTrainer.CurrentPokemonInCombat.Name + " = " + wildPokemon.CurrentPokemonInCombat.Name);
            int _random = UnityEngine.Random.Range(0, 2);
            if (_random == 0)
            {
                _action.Turn();
                if (IsBattleFinish(playerTrainer, wildPokemon))
                {
                    BattleFinish();
                    return;
                }
                _action2.Turn();
                if (IsBattleFinish(playerTrainer, wildPokemon))
                {
                    BattleFinish();
                    return;
                }
            }
            else
            {
                _action2.Turn();
                if (IsBattleFinish(playerTrainer, wildPokemon))
                {
                    BattleFinish();
                    return;
                }
                _action.Turn();
                if (IsBattleFinish(playerTrainer, wildPokemon))
                {
                    BattleFinish();
                    return;
                }
            }
        }
        if (playerTrainer.CurrentPokemonInCombat.Fainted)
            battleUI.PokemonSwapUI.DisplayUI(true);
        battleUI.ResetMenu();
    }
    public bool IsBattleFinish(PlayerTrainer _trainer, WildPokemon _wildPokemon)
    {
        return !_trainer.HavePokemonLeft || _wildPokemon.GetFirstSlotPokemon().Fainted;
    }
    void BattleFinish()
    {
        if(!playerTrainer.GetFirstSlotPokemon().Fainted)
        {
            playerTrainer.CurrentPokemonInCombat.GainExp(wildPokemon.CurrentPokemonInCombat.XpGive);
        }
        else
        {

        }
        StopBattle();
    }
    public void StopBattle()
    {
        playerTrainer.IsReady = false;
        playerTrainer.IsInBattle = false;
        playerTrainer.Swap(0);
        playerTrainer = null;
        wildPokemon = null;
        UIManager.Instance.RemoveQueueSetPreviousUI();
    }
}