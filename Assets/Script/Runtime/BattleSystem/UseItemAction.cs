using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemAction : TurnAction
{
    public override bool IsValidAction(BattleField _battleField)
    {
        return true;
    }
}
