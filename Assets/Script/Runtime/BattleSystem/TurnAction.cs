
using System;

public abstract class TurnAction
{
    public virtual int GetPriority(BattleField _battleField)
    {
        return 0;
    }
    public virtual void Action(BattleField _battleField)
    {

    }
}