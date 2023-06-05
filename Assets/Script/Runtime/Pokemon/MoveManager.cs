using System.Collections.Generic;
using UnityEngine;

public class MoveManager : Singleton<MoveManager>
{
    [SerializeField] MoveDataSO moveData;
    [SerializeField] MoveData lutteMove;
    public MoveData GetMoveDataByIndex(int _index)
    {
        return GetMoveDataByIndex(moveData, _index);
    }
    public MoveData GetMoveDataByName(string _name)
    {
        return GetMoveDataByName(moveData, _name);
    }
    public MoveData GetMoveDataByMoveChoice(MoveChoice _moveChoice)
    {
        return GetMoveDataByName(_moveChoice.moveName);
    }
    public MoveData GetMoveDataByMoveByLevel(MoveByLevel _moveByLevel)
    {
        return GetMoveDataByName(_moveByLevel.Name);
    }
    public List<Move> GenerateMoveByLevel(int _level, PokemonData _pokemonData)
    {
        List<Move> _moves = new List<Move>();
        for (int i = _pokemonData.moveChoices.Length - 1; i >= 0; i--)
        {
            if (_pokemonData.moveChoices[i].Level <= _level)
            {
                for (int j = i; j > i - 4; j--)
                {
                    if (j < 0) return _moves;
                    //Debug.Log(_pokemonData.moveChoices[j].Name);
                    //Debug.Log(_pokemonData.moveChoices[j].Level);
                    MoveData _moveData = GetMoveDataByMoveByLevel(_pokemonData.moveChoices[j]);
                    if(_moveData == null)
                    {
                        Debug.Log(_pokemonData.moveChoices[j].Name);
                        return null;
                    }
                    _moves.Add(new Move(_moveData));
                }
                return _moves;
            }
        }
        return _moves;
    }
    public MoveData GetMoveLutte()
    {
        return lutteMove;
    }
    public static MoveData GetMoveDataByName(MoveDataSO _moveDataSO, string _name)
    {
        for (int i = 0; i < _moveDataSO.allMoves.moves.Length; i++)
        {
            if (_moveDataSO.allMoves.moves[i].name == _name)
            {
                return _moveDataSO.allMoves.moves[i];
            }
        }
        return null;
    }
    public static MoveData GetMoveDataByIndex(MoveDataSO _moveDataSO, int _index)
    {
        return _moveDataSO.allMoves.moves[_index];
    }
}
