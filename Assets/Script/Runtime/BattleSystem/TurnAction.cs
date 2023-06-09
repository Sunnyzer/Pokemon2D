
using System;

public abstract class TurnAction
{
    public abstract bool IsValidAction(BattleField _battleField);
    public virtual int GetPriority(BattleField _battleField)
    {
        return 0;
    }
    public virtual void Action(BattleField _battleField)
    {

    }
}