using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ZoneDataSO))]
public class ZoneDataEditor : Editor
{
    ZoneDataSO zoneData;
    bool _test = false;
    float value;
    float value2;
    float value3;
    Gradient gradient = new Gradient();
    private void OnEnable()
    {
        zoneData = (ZoneDataSO)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SerializedProperty _pokemonsZone = serializedObject.FindProperty("pokemonsInZoneByRarities");
        for (int i = 0; i < _pokemonsZone.arraySize; i++)
        {
            SerializedProperty _sp = _pokemonsZone.GetArrayElementAtIndex(i);
            _sp.FindPropertyRelative("rarity");
            _sp.FindPropertyRelative("rarityRate");
        }

        //value = EditorGUILayout.Knob(new Vector2(200, 200), value, 0, 100, "%" , Color.red, Color.white, true, GUILayout.ExpandWidth(true));
        //EditorGUILayout.MinMaxSlider(ref value2, ref value3, 0, 100);
        //GUILayout.Space(5);
        GUISkin gUIStyle = (GUISkin)Resources.Load("guiskin");
        value = GUILayout.VerticalSlider(value, 0, 100, gUIStyle.GetStyle("verticalslider"), gUIStyle.GetStyle("verticalslider"));
        //value2 = GUILayout.VerticalSlider(value2, 0, 100);
        serializedObject.ApplyModifiedProperties();
    }
}

//[CustomPropertyDrawer(typeof(PokemonsInZoneByRarity))]
public class PokemonsInZoneByRarityEditor : PropertyDrawer
{
    int currentHeight = 0;
    int currentMax = 0;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        currentHeight = 0;
        SerializedProperty _rate = property.FindPropertyRelative("rarityRate");
        SerializedProperty _tab = property.FindPropertyRelative("pokemonsEncounter");
        SerializedProperty _labelRarity = property.FindPropertyRelative("rarity");
        EditorGUI.LabelField(new Rect(position.x, position.y + currentHeight, position.width, 20), ((Rarity)_labelRarity.enumValueIndex).ToString());
        currentHeight += 20;
        if (GUI.Button(new Rect(position.x, position.y + currentHeight, position.width / 2, 20), "Add"))
        {
            _tab.InsertArrayElementAtIndex(0);
        }
        if(GUI.Button(new Rect(position.x + position.width / 2, position.y + currentHeight, position.width / 2, 20), "Remove"))
        {
            _tab.DeleteArrayElementAtIndex(0);
        }
        currentHeight += 20;
        for (int i = 0; i < _tab.arraySize; i++)
        {
            EditorGUI.PropertyField(new Rect(position.x + position.width/_tab.arraySize * i, position.y + currentHeight, position.width / _tab.arraySize, 0), _tab.GetArrayElementAtIndex(i));
        }
        //currentHeight += _tab.arraySize > 0 ? 60 : 0;
        currentHeight += 60;
        EditorGUI.PropertyField(new Rect(position.x, position.y + currentHeight, position.width, 20), _rate);
        currentMax = currentHeight;
        currentHeight = 0;
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 150;
    }
}

//[CustomPropertyDrawer(typeof(PokemonEncouterParameter))]
public class PokemonEncouterParameterEditor : PropertyDrawer
{
    Rect rect;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect _rect1 = new Rect(position.x, position.y, position.width, 20);
        Rect _rect2 = new Rect(position.x, position.y + 20, position.width/2, 20);
        Rect _rect3 = new Rect(position.x + position.width / 2, position.y + 20, position.width/2, 20);
        Rect _rect4 = new Rect(position.x, position.y + 40, position.width / 2, 20);
        Rect _rect5 = new Rect(position.x + position.width / 2, position.y + 40, position.width / 2, 20);
        Rect _rect6 = new Rect(position.x, position.y + 60, position.width / 2, 20);
        Rect _rect7 = new Rect(position.x + position.width / 2, position.y + 60, position.width / 2, 20);
        
        EditorGUI.PropertyField(_rect1, property.FindPropertyRelative("pokemon"));
        SerializedProperty _levelMin = property.FindPropertyRelative("levelMinEncounter");
        SerializedProperty _levelMax = property.FindPropertyRelative("levelMaxEncounter");
        SerializedProperty _chanceToEncounter = property.FindPropertyRelative("chanceToEncounter");
        EditorGUI.LabelField(_rect2, "levelMin");
        _levelMin.intValue = EditorGUI.IntSlider(_rect3, _levelMin.intValue, 0, _levelMax.intValue);
        EditorGUI.LabelField(_rect4, "levelMax");
        _levelMax.intValue = EditorGUI.IntSlider(_rect5, _levelMax.intValue, _levelMin.intValue, 100);
        //EditorGUI.LabelField(_rect6, "chanceToEncounter");
        //_chanceToEncounter.floatValue = EditorGUI.Slider(_rect7, _chanceToEncounter.floatValue, 0, 100);
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 80;
    }
}