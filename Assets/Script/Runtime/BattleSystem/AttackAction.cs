using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : TurnAction
{
    Move moveSelected = null;
    Pokemon assailantPokemon = null;
    public AttackAction(Pokemon _assailantPokemon, Move _moveSelect)
    {
        assailantPokemon = _assailantPokemon;
        moveSelected = _moveSelect;
    }

    public override BattleInfo BattleInfo { get; set; }

    public override int GetPriority(TurnAction _turnAction)
    {
        return moveSelected.Priority;
    }

    public override void Turn()
    {
        BattleInfo.opponentFighter.GetPokemon().TakeDamage(assailantPokemon, moveSelected);
    }
}
