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
}

[Serializable]
public class Move
{
    [SerializeField] MoveData data;
    int currentPP;
}

[Serializable]
public class MoveChoice
{
    public int indexMove;
}