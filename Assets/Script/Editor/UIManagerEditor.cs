using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIManager))]
public class UIManagerEditor : Editor
{
    UIManager uiManager;
    UI[] uis;
    private void OnEnable()
    {
        uiManager = (UIManager)target;
        uis = FindObjectsOfType<UI>(true);
        SetCurrentUI(uiManager.indexUI - 1);
        serializedObject.ApplyModifiedProperties();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (Application.isPlaying) return;
        uis = FindObjectsOfType<UI>(true);
        List<string> names = uis.Select(ui => ui.name).ToList();
        names.Insert(0, "Null");
        SerializedProperty _indexUI = serializedObject.FindProperty("indexUI");
        _indexUI.intValue = EditorGUILayout.Popup(_indexUI.intValue, names.ToArray());
        SetCurrentUI(uiManager.indexUI - 1);
        serializedObject.ApplyModifiedProperties();
    }
    public void SetCurrentUI(int _index)
    {
        if (_index >= 0 && _index < uis.Length)
        {
            serializedObject.FindProperty("currentUI").objectReferenceValue = uis[_index];
            return;
        }
        serializedObject.FindProperty("currentUI").objectReferenceValue = null;
    }
}
