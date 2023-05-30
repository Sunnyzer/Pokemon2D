using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MoveDataSO))]
public class MoveDataEditor : Editor
{
    MoveDataSO moveDataSO;
    TextAsset file = null;
    private void OnEnable()
    {
        moveDataSO = (MoveDataSO)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        file = (TextAsset)Resources.Load("moves");
        if (GUILayout.Button("Generate Move"))
        {
            AllMoves allMoves = JsonUtility.FromJson<AllMoves>(file.text);
            moveDataSO.allMoves = allMoves;
        }
    }
}
