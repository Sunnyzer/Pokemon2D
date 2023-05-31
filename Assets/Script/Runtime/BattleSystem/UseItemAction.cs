using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemAction : TurnAction
{
    public override BattleInfo BattleInfo { get; set; }

    public override int GetPriority(TurnAction _turnAction)
    {
        return 0;
    }
    //Item item;
    public override void Turn()
    {
        //item.Use(_opponentPokemon);
    }
}
