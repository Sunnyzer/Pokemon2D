using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

//[CustomPropertyDrawer(typeof(MoveData))]
public class MoveDataEditor : PropertyDrawer
{
    //int size = 0;
    //bool open = false;
    //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //{
    //    //size = 20;
    //    base.OnGUI(position, property, label);
    //    //SerializedProperty _accuracy = property.FindPropertyRelative("accuracy");
    //    //SerializedProperty _name = property.FindPropertyRelative("name");
    //    //SerializedProperty _id = property.FindPropertyRelative("id");
    //    //SerializedProperty _power = property.FindPropertyRelative("power");
    //    //SerializedProperty _pp = property.FindPropertyRelative("pp");
    //    //SerializedProperty _type = property.FindPropertyRelative("type");
    //    //SerializedProperty _damageType = property.FindPropertyRelative("damageType");
    //    //SerializedProperty _critRate = property.FindPropertyRelative("critRate");
    //    //SerializedProperty _statChance = property.FindPropertyRelative("statChance");
    //    //SerializedProperty _healing = property.FindPropertyRelative("healing");
    //    //SerializedProperty _drain = property.FindPropertyRelative("drain");
    //    //SerializedProperty _flinchRate = property.FindPropertyRelative("flinchRate");
    //    //SerializedProperty _effectChance = property.FindPropertyRelative("effectChance");
    //    //SerializedProperty _stat_changes = property.FindPropertyRelative("stat_changes");
    //    //Rect _rect = new Rect(position.position + new Vector2(0, 0), new Vector2(position.width, 20));
    //    //open = EditorGUI.Foldout(_rect, open, _name.stringValue);
    //    //if (open)
    //    //{
    //    //    AddProperty(position, _accuracy);
    //    //    AddProperty(position, _name);
    //    //    AddProperty(position, _id);
    //    //    AddProperty(position, _power);
    //    //    AddProperty(position, _pp);
    //    //    AddProperty(position, _type);
    //    //    AddProperty(position, _damageType);
    //    //    AddProperty(position, _critRate);
    //    //    AddProperty(position, _statChance);
    //    //    AddProperty(position, _healing);
    //    //    AddProperty(position, _drain);
    //    //    AddProperty(position, _flinchRate);
    //    //    AddProperty(position, _effectChance);
    //    //    AddProperty(position, _stat_changes);
    //    //}
    //    //EditorGUI.EndFoldoutHeaderGroup();
    //}
    //private void AddProperty(Rect _position, SerializedProperty _property)
    //{
    //    if (_property != null)
    //    {
    //        EditorGUI.PropertyField(new Rect(_position.position + new Vector2(0, size), new Vector2( _position.width, 20)), _property);
    //        size += 20;
    //    }
    //}
    //public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    //{
    //    return base.GetPropertyHeight(property, label);
    //}
}