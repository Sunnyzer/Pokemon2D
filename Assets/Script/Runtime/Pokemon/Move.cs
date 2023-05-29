using System;
using UnityEngine;

[Serializable]
public class MoveData
{
    [SerializeField] int accuracy = 100;
    [SerializeField] public string ename = "Pound";
    [SerializeField] int id = 0;
    [SerializeField] int power = 40;
    [SerializeField] int pp = 35;
    [SerializeField] public PkmType type;
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