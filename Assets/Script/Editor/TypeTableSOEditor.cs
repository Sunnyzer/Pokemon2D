using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TypeTableSO))]
public class TypeTableSOEditor : Editor
{
    TypeTableSO typeTableSO;
    TextAsset textAsset;
    private void OnEnable()
    {
        typeTableSO = (TypeTableSO)target;
        textAsset = (TextAsset)Resources.Load("types");
        Init();   
    }
    public void Init()
    {
        Array _array = Enum.GetValues(typeof(PkmType));
        int _count = typeTableSO.typeTable.typeTable.Count;
        for (int i = _count + 1; i < _array.Length - _count; i++)
        {
            typeTableSO.typeTable.typeTable.Add(new PkmTypeIWS((PkmType)i, null, null, null));
        }
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.BeginHorizontal();
        textAsset = (TextAsset)EditorGUILayout.ObjectField(textAsset, typeof(TextAsset));
        if(GUILayout.Button("SetJson"))
        {
            typeTableSO.typeTable = JsonUtility.FromJson<TypeTable>(textAsset.text);
        }
        GUILayout.EndHorizontal();
        if(GUILayout.Button("Clear"))
        {
            typeTableSO.typeTable.typeTable.Clear();
            Init();
        }
    }
}
