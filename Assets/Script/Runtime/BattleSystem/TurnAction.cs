
public abstract class TurnAction
{
    public abstract BattleInfo BattleInfo { get; set; }
    public abstract void Turn();
    public abstract int GetPriority(TurnAction _turnAction);
}