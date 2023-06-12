using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

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
    [SerializeField] TypeTableSO typeTable;
    [SerializeField] BattleUI battleUI;
    PlayerTrainer playerTrainer;
    WildPokemon wildPokemon;
    BattleField battleField;

    public TypeTableSO TypeTable => typeTable;
    public PlayerTrainer PlayerTrainer => playerTrainer;
    public BattleField BattleField => battleField;
    
    List<TurnAction> turnActions = new List<TurnAction>();
    public void StartBattleWildPokemon(PlayerTrainer _player, WildPokemon _wildPokemon)
    {
        Pokemon _firstPokemon = _player.GetFirstPokemonNotFainted();
        if (_firstPokemon == null || _firstPokemon.Fainted) return;
        playerTrainer = _player;
        wildPokemon = _wildPokemon;
        battleField = new BattleField(_firstPokemon, _wildPokemon.CurrentPokemonInCombat);
        battleUI.StartBattle(battleField);
        UIManager.Instance.SetCurrentUIDisplay(battleUI, _player);
    }
    private void Update()
    {
        if (turnActions.Count == 0) return;
        turnActions.Add(wildPokemon.CalculAction(BattleField));
        turnActions = turnActions.OrderByDescending(t => t.GetPriority(BattleField)).ToList();
        for (int i = 0; i < turnActions.Count; i++)
        {
            turnActions[i].Action(BattleField);
            if(battleField.FirstPokemon.Fainted)
            {
                if(!playerTrainer.PokemonTeam.HavePokemonLeft())
                {
                    RunBattle();
                    return;
                }
                Debug.Log(battleField.FirstPokemon.Name + " is Fainted");
                turnActions.Clear();
                battleUI.DisplaySwapPokemon();
                battleUI.PokemonSwapUI.ActiveForceSwap();
                battleField.SecondPokemon.ApplyEffectTurn();
                return;
            }
            if(battleField.SecondPokemon.Fainted)
            {
                battleField.FirstPokemon.GainExp(40);
                RunBattle();
                return;
            }
        }
        battleField.FirstPokemon.ApplyEffectTurn();
        battleField.SecondPokemon.ApplyEffectTurn();
        if (battleField.FirstPokemon.Fainted)
        {
            if (!playerTrainer.PokemonTeam.HavePokemonLeft())
            {
                RunBattle();
                return;
            }
            Debug.Log(battleField.FirstPokemon.Name + " is Fainted");
            turnActions.Clear();
            battleUI.DisplaySwapPokemon();
            battleUI.PokemonSwapUI.ActiveForceSwap();
            battleField.SecondPokemon.ApplyEffectTurn();
            return;
        }
        if (battleField.SecondPokemon.Fainted)
        {
            battleField.FirstPokemon.GainExp(40);
            RunBattle();
            return;
        }
        turnActions.Clear();
    }
    public bool SelectAction(TurnAction _action)
    {
        if (_action.IsValidAction(battleField))
        {
            turnActions.Add(_action);
            return true;
        }
        return false;
    }
    public void RunBattle()
    {
        //Debug.Log("Run Battle");
        battleField.DestroyBattleField();
        battleField = null;
        turnActions.Clear();
        UIManager.Instance.RemoveQueueSetPreviousUI();
    }
}