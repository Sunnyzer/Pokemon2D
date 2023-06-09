using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PkmType
{
    None,
    normal = 1,
    fire = 2,
    ice = 3,
    water = 4,
    fighting = 5,
    electric = 6,
    grass = 7,
    ground = 8,
    flying = 9,
    poison = 10,
    bug = 11,
    dark = 12,
    psychic = 13,
    dragon = 14,
    rock = 15,
    ghost = 16,
    steel = 17,
    fairy = 18,
}

[Serializable]
public class PkmTypeIWS
{
    public Sprite spriteType;
    public PkmType pkmType;
    public List<PkmType> immunes = new List<PkmType>();
    public List<PkmType> resistances = new List<PkmType>();
    public List<PkmType> strengths = new List<PkmType>();
    public PkmTypeIWS(PkmType _pkmType, List<PkmType> immunes, List<PkmType> resistances, List<PkmType> strenghts)
    {
        this.pkmType = _pkmType;
        this.resistances = resistances;
        this.immunes = immunes;
        this.strengths = strenghts;
    }
    public float GetEffective(PkmType _target)
    {
        if (immunes.Contains(_target))
            return 0;
        if (resistances.Contains(_target))
            return 0.5f;
        if (strengths.Contains(_target))
            return 2f;
        return 1;
    }
}

public static class PkmTypeManager
{
    //public static Dictionary<PkmType, PkmTypeIWS> weakness = new Dictionary<PkmType, PkmTypeIWS>()
    //{
    //    { PkmType.normal, new PkmTypeIWS(new List<PkmType>(){ PkmType.ghost }, new List<PkmType>(){ PkmType.rock, PkmType.steel }, new List<PkmType>(){ }) },
    //    { PkmType.fire, new PkmTypeIWS(new List<PkmType>(){ }, new List<PkmType>(){ PkmType.fire, PkmType.water, PkmType.rock }, new List<PkmType>(){ PkmType.grass,PkmType.ice,PkmType.bug, PkmType.steel }) },
    //    { PkmType.water, new PkmTypeIWS(new List<PkmType>(){ }, new List<PkmType>(){ PkmType.fire, PkmType.water, PkmType.rock }, new List<PkmType>(){ PkmType.grass,PkmType.ice,PkmType.bug, PkmType.steel }) },
    //};
}