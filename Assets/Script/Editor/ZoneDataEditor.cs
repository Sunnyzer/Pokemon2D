using JetBrains.Annotations;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ZoneDataSO))]
public class ZoneDataEditor : Editor
{
    ZoneDataSO zoneData;
    bool _test = false;
    private void OnEnable()
    {
        zoneData = (ZoneDataSO)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.ApplyModifiedProperties();
    }
}

[CustomEditor(typeof(PokemonsInZoneByRarity))]
public class PokemonsInZoneByRarityEditor : Editor
{

}

[CustomPropertyDrawer(typeof(PokemonEncouterParameter))]
public class PokemonEncouterParameterEditor : PropertyDrawer
{
    Rect rect;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect _rect1 = new Rect(position.x, position.y, position.width, 20);
        Rect _rect2 = new Rect(position.x, position.y + 20, position.width, 20);
        Rect _rect3 = new Rect(position.x, position.y + 40, position.width, 20);
        EditorGUI.PropertyField(_rect1, property.FindPropertyRelative("pokemon"));
        SerializedProperty _levelMin = property.FindPropertyRelative("levelMinEncounter");
        SerializedProperty _levelMax = property.FindPropertyRelative("levelMaxEncounter");
        _levelMin.intValue = EditorGUI.IntSlider(_rect2, "levelMin", _levelMin.intValue, 1, _levelMax.intValue);
        _levelMax.intValue = EditorGUI.IntSlider(_rect3, "levelMax", _levelMax.intValue, _levelMin.intValue, 100);
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 60;
    }
}