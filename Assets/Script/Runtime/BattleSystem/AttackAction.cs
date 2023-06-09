using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : TurnAction
{
    Pokemon launcher;
    Move move;
    public AttackAction(Pokemon _launcher, Move _move)
    {
        launcher = _launcher;
        move = _move;
    }
    public override int GetPriority(BattleField _battleField)
    {
        Pokemon _target = _battleField.FirstPokemon == launcher ? _battleField.SecondPokemon : _battleField.FirstPokemon;
        int _speedTie = launcher.GetSpeedInCombat() > _target.GetSpeedInCombat() ? 1 : 0;
        return move.Priority + _speedTie;
    }
    public override void Action(BattleField _battleField)
    {
        Pokemon _target = _battleField.FirstPokemon == launcher ? _battleField.SecondPokemon : _battleField.FirstPokemon;
        _target.TakeDamage(launcher, move);
    }

    public override bool IsValidAction(BattleField _battleField)
    {
        return true;
    }
}
