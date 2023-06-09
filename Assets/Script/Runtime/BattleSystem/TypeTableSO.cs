using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TypeTable
{
    [SerializeField] public List<PkmTypeIWS> typeTable = new List<PkmTypeIWS>();
    public float GetEffective(PkmType _type, PkmType _targetType)
    {
        return typeTable[(int)_type].GetEffective(_targetType);
    }
}

[CreateAssetMenu(fileName = "TypeTableSO")]
public class TypeTableSO : ScriptableObject
{
    [SerializeField] public TypeTable typeTable;
    public Sprite GetSpriteType(PkmType _pkmType)
    {
        return typeTable.typeTable[(int)_pkmType].spriteType;
    }
    public float GetEffective(PkmType _type, PkmType _targetType)
    {
        return typeTable.GetEffective(_type, _targetType);
    }
}
