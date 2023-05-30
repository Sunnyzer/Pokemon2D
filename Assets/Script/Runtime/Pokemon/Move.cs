using System;
using UnityEngine;

[Serializable]
public class MoveData
{
    [SerializeField] int accuracy;
    [SerializeField] string ename;
    [SerializeField] int id;
    [SerializeField] int power;
    [SerializeField] int pp;
    [SerializeField] PkmType type;

    public string Name => ename;
    public int Accuracy => accuracy;
    public int Id => id;
    public int Power => power;
    public PkmType Type => type;
    public int PP => pp;
}

[Serializable]
public class Move
{
    [SerializeField] MoveData data;
    int currentPP;
    public string Name => data.Name;
    public int PP => currentPP;
    public int PPMax => data.PP;
    public bool CanUse => currentPP > 0;
    public int Power => data.Power;
    public PkmType Type => data.Type;
    public Move(MoveData data)
    {
        this.data = data;
        currentPP = data.PP;
    }
    public bool UseMove()
    {
        if (!CanUse) return false;
        --currentPP;
        return true;
    }
}

[Serializable]
public class MoveChoice
{
    public int indexMove;
}