using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AllMoves
{
    [SerializeField] public MoveData[] moves;
}

[CreateAssetMenu(fileName = "MovesDataTable")]
public class MoveDataSO : ScriptableObject
{
    [SerializeField] public AllMoves allMoves;
}
