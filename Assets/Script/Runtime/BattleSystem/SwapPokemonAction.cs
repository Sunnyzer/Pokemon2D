using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPokemonAction : TurnAction
{
    int index = 0;
    public override BattleInfo BattleInfo { get; set; }

    public SwapPokemonAction(int _indexToSwap)
    {
        index = _indexToSwap; 
    }
    public override int GetPriority(TurnAction _turnAction)
    {
        return 100;
    }

    public override void Turn()
    {
        BattleInfo.myFighter.Swap(index);
    }
}
