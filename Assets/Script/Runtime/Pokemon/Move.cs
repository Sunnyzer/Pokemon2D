using System;
using UnityEngine;

[Serializable]
public class MoveData
{
    [SerializeField] public int accuracy = 0;
    [SerializeField] public string ename = "Pound";
    [SerializeField] public int id = 0;
    [SerializeField] public int power = 0;
    [SerializeField] public int pp = 0;
    [SerializeField] public PkmType type;
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