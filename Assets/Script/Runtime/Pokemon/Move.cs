using System;
using UnityEngine;

public enum DamageType
{
    physical,
    special,
    status,
    unique,
}

[Serializable]
public class MoveData
{
    [SerializeField] public int accuracy;
    [SerializeField] public string name;
    [SerializeField] public int id;
    [SerializeField] public int power;
    [SerializeField] public int pp;
    [SerializeField] public int priority;
    [SerializeField] public PkmType type;
    [SerializeField] public DamageType damageType;
    [SerializeField] public float critRate;
    [SerializeField] public float statChance;
    [SerializeField] public int healing;
    [SerializeField] public int drain;
    [SerializeField] public float flinchRate;
    [SerializeField] public float effectChance;
    [SerializeField] public Stat stat_changes;

    public string Name => name;
    public int? Accuracy => accuracy;
    public int Id => id;
    public int? Power => power;
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
    public int? Power => data.Power;
    public PkmType Type => data.Type;
    public int Priority => 0;

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
    public void AddPP(int _ppToAdd)
    {
        if (_ppToAdd < 0) return;
        currentPP += _ppToAdd;
        currentPP = currentPP > PPMax ? PPMax : currentPP;
    }
    public void RecoverPP()
    {
        currentPP = PPMax;
    }
}

[Serializable]
public class MoveChoice
{
    public int indexMove;
    public string moveName;
}

[Serializable]
public class MoveByLevel
{
    [SerializeField] int level;
    [SerializeField] string name;
    public string Name => name;
    public MoveByLevel(int _level, string _name)
    {
        level = _level;
        name = _name;
    }
    public int Level => level;
}