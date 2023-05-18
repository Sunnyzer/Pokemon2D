using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIManager))]
public class UIManagerEditor : Editor
{
    UIManager uiManager;
    private void OnEnable()
    {
        uiManager = (UIManager)target;
        UI[] _uis = FindObjectsOfType<UI>(true);
        uiManager.SetCurrentUIDisplay(_uis[uiManager.indexUI]);
    }

    public override void OnInspectorGUI()
    {
        UI[] _uis = FindObjectsOfType<UI>(true);
        int _indexUI = EditorGUILayout.Popup(uiManager.indexUI, _uis.Select(ui => ui.name).ToArray());
        if(_indexUI != uiManager.indexUI)
        {
            uiManager.indexUI = _indexUI;
            serializedObject.FindProperty("currentUI").objectReferenceValue = _uis[_indexUI];
            //uiManager.SetCurrentUIDisplay(_uis[_indexUI]);
        }
    }
}
