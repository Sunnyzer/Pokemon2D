using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ControllerManager))]
public class ControllerManagerEditor : Editor
{
    ControllerManager controllerManager;
    Controller[] controllers;
    private void OnEnable()
    {
        controllerManager = (ControllerManager)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (Application.isPlaying) return;
        controllers = FindObjectsOfType<Controller>(true);
        SerializedProperty _indexController = serializedObject.FindProperty("indexController");
        _indexController.intValue = EditorGUILayout.Popup(_indexController.intValue, controllers.Select(ui => ui.name).ToArray());
        SetCurrentController(controllerManager.indexController);
        serializedObject.ApplyModifiedProperties();
    }
    public void SetCurrentController(int _index)
    {
        if (_index >= 0 && _index < controllers.Length)
        {
            serializedObject.FindProperty("currentController").objectReferenceValue = controllers[_index];
            return;
        }
        serializedObject.FindProperty("currentController").objectReferenceValue = null;
    }
}
