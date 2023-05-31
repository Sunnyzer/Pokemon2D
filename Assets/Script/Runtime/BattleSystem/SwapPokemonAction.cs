using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPokemonAction : TurnAction
{
    public override BattleInfo BattleInfo { get; set; }

    public override int GetPriority(TurnAction _turnAction)
    {
        return 100;
    }

    public override void Turn()
    {

    }
}
