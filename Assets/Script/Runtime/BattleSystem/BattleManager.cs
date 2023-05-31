using UnityEngine;
using System.Linq;
using System;
using Unity.VisualScripting;

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

    public void StartBattleWildPokemon(PlayerTrainer _player, WildPokemon _wildPokemon)
    {
        if (_player.GetPokemon() == null) return;
        _player.IsInBattle = true;
        wildPokemon = _wildPokemon;
        playerTrainer = _player;
        battleUI.SetInfoMyPokemon(_player);
        battleUI.SetInfoOpponentPokemon(_wildPokemon.GetPokemon());
        UIManager.Instance.SetCurrentUIDisplay(battleUI);
    }
    private void Update()
    {
        if (!playerTrainer || !playerTrainer.IsReady) return;
        playerTrainer.IsReady = false;
        BattleInfo _playerInfo = new BattleInfo(wildPokemon, playerTrainer);
        BattleInfo _wildPokemonInfo = new BattleInfo(playerTrainer, wildPokemon);
        TurnAction _action = playerTrainer.Turn(_playerInfo);
        TurnAction _action2 = wildPokemon.Turn(_wildPokemonInfo);

        int _priority = _action.GetPriority(_action2);
        int _priority2 = _action2.GetPriority(_action);
        if(_priority > _priority2)
        {
            _action.Turn();
            if(IsBattleFinish(playerTrainer, wildPokemon))
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
        else if(_priority < _priority2)
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
        else
        {
            int _random = UnityEngine.Random.Range(0, 2);
            if(_random == 0)
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
        battleUI.ResetMenu();
    }
    public bool IsBattleFinish(PlayerTrainer _trainer, WildPokemon _wildPokemon)
    {
        return _trainer.GetPokemon().Fainted || _wildPokemon.GetPokemon().Fainted;
    }
    public void BattleFinish()
    {
        if(!playerTrainer.GetPokemon().Fainted)
        {
            playerTrainer.GetPokemon().GainExp(wildPokemon.GetPokemon().XpGive);
        }
        else
        {

        }
        battleUI.ResetMenu();
        StopBattle();
        playerTrainer.IsReady = false;
        playerTrainer = null;
        wildPokemon = null;
    }
    public void StopBattle()
    {
        playerTrainer.IsInBattle = false;
        battleUI.DesinitBattle();
    }

}