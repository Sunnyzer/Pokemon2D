using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomPropertyDrawer(typeof(MoveChoice))]
public class MoveChoiceEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        base.OnGUI(position, property, label);
        MoveDataSO _moveDataSO = (MoveDataSO)Resources.Load("MoveData");
        SerializedProperty _indexMove = property.FindPropertyRelative("indexMove");
        SerializedProperty _nameMove = property.FindPropertyRelative("moveName");
        _indexMove.intValue = EditorGUI.Popup(position, _indexMove.intValue, _moveDataSO.allMoves.moves.Select(move => move.Name).ToArray());
        _nameMove.stringValue = MoveManager.GetMoveDataByIndex(_moveDataSO, _indexMove.intValue).name;
    }
}
