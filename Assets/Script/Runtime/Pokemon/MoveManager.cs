using System.Collections.Generic;
using UnityEngine;

public class MoveManager : Singleton<MoveManager>
{
    [SerializeField] MoveDataSO moveData;

    public MoveData GetMoveDataByIndex(int _index)
    {
        return moveData.allMoves.moves[_index];
    }
    public MoveData GetMoveDataByMoveChoice(MoveChoice _moveChoice)
    {
        return GetMoveDataByIndex(_moveChoice.indexMove);   
    }
    public MoveData GetMoveDataByMoveByLevel(MoveByLevel _moveByLevel)
    {
        return GetMoveDataByMoveChoice(_moveByLevel.MoveChoice);
    }
    public Move[] GenerateMoveByLevel(int _level, PokemonData _pokemonData)
    {
        List<Move> _moves = new List<Move>();
        for (int i = _pokemonData.moveChoices.Length - 1; i >= 0; i--)
        {
            if (_pokemonData.moveChoices[i].Level <= _level)
            {
                for (int j = i; j > i - 4; j--)
                {
                    MoveData _moveData = GetMoveDataByMoveByLevel(_pokemonData.moveChoices[j]);
                    _moves.Add(new Move(_moveData));
                }
                return _moves.ToArray();
            }
        }
        return _moves.ToArray();
    }
}
