using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ZoneDataSO))]
public class ZoneDataEditor : Editor
{
    ZoneDataSO zoneData;
    int index = -1;
    bool test = true;
    Vector2 currentPos;
    List<PokemonEncouterParameter> pokemonEncouterParameters = new List<PokemonEncouterParameter>();
    private void OnEnable()
    {
        zoneData = (ZoneDataSO)target;
    }
    public void ResetRect()
    {
        for (int i = 0; i < 1; i++)
        {
            PokemonsInZoneByRarity _pokemonZone = zoneData.PokemonsInZoneByRarities[i];
            for (int j = 0; j < _pokemonZone.PokemonsEncounter.Length; j++)
            {
                PokemonEncouterParameter _pep = _pokemonZone.PokemonsEncounter[j];
                pokemonEncouterParameters.Add(_pep);
                //if (_pep.rect == null)
                {
                    _pep.rect = new Rect(j * 100, i * 150, 100, 100);
                }
            }
        }
    }
    public void InitRect()
    {
        for (int i = 0; i < 1; i++)
        {
            PokemonsInZoneByRarity _pokemonZone = zoneData.PokemonsInZoneByRarities[i];
            float _sizeButton = EditorGUIUtility.currentViewWidth / _pokemonZone.PokemonsEncounter.Length;
            for (int j = 0; j < _pokemonZone.PokemonsEncounter.Length; j++)
            {
                PokemonEncouterParameter _pep = _pokemonZone.PokemonsEncounter[j];
                pokemonEncouterParameters.Add(_pep);
                if (_pep.rect == null)
                {
                    _pep.rect = new Rect(j * _sizeButton, i * 150, _sizeButton, 100);
                }
            }
        }
    }
    public override void OnInspectorGUI()
    {
        if(test)
        {
            InitRect();
            test = false;
        }
        GUILayoutUtility.GetRect(500, 500);
        if (Event.current.type == EventType.MouseUp)
        {
            index = -1;
            Debug.Log("MouseUp");
        }
        for (int i = 0; i < 1 /*zoneData.PokemonsInZoneByRarities.Count*/; i++)
        {
            PkmBar(zoneData.PokemonsInZoneByRarities[i]);
        }
        if(index != -1)
        {
            Drag();
        }
    }
    public bool Drag()
    {
        bool _action = false;
        //Debug.Log("Drag " + Event.current.delta.x);
        if (Event.current.delta.x > 0)
        {
            if(pokemonEncouterParameters.Count > index + 1)
            {
                MoveToTheRightButton(index);
                _action = true;
            }
            else
            {
                MoveToTheLeftButton(index);
                _action = true;
            }
        }
        if (Event.current.delta.x < 0)
        {
            if (0 <= index - 1)
            {
                MoveToTheLeftButton(index);
                _action = true;
            }
            else
            {
                MoveToTheRightButton(index);
                _action = true;
            }
        }
        if (_action)
            Repaint();
        return _action;
    }
    public void MoveToTheLeftButton(int index)
    {
        Rect _rect1 = pokemonEncouterParameters[index].rect.Value;
        Rect _rect2 = pokemonEncouterParameters[index - 1].rect.Value;
        float _toAdd = Time.fixedDeltaTime * Event.current.delta.x;
        pokemonEncouterParameters[index].rect = new Rect(_rect1.x + _toAdd, _rect1.y, _rect1.width - _toAdd, _rect1.height);
        pokemonEncouterParameters[index - 1].rect = new Rect(_rect2.x, _rect2.y, _rect2.width + _toAdd, _rect2.height);
    }
    public void MoveToTheRightButton(int index)
    {
        Rect _rect1 = pokemonEncouterParameters[index].rect.Value;
        Rect _rect2 = pokemonEncouterParameters[index + 1].rect.Value;
        float _toAdd = Time.fixedDeltaTime * Event.current.delta.x;
        pokemonEncouterParameters[index].rect = new Rect(_rect1.x, _rect1.y, _rect1.width + _toAdd, _rect1.height);
        pokemonEncouterParameters[index + 1].rect = new Rect(_rect2.x + _toAdd, _rect2.y, _rect2.width - _toAdd, _rect2.height);
    }
    public void PkmBar(PokemonsInZoneByRarity _pokemonZone)
    {
        for (int i = 0; i < _pokemonZone.PokemonsEncounter.Length; i++)
        {
            PokemonEncouterParameter _pokemonEncounter = _pokemonZone.PokemonsEncounter[i];
            if (GUI.RepeatButton(_pokemonEncounter.rect.Value, "Pokemon" + i.ToString()))
            {
                if (index != i)
                {
                    Debug.Log("Drag " + i.ToString());
                    index = i;
                    currentPos = Event.current.mousePosition;
                }
            }
            EditorGUI.FloatField(new Rect(_pokemonEncounter.rect.Value.x + _pokemonEncounter.rect.Value.width/2 - 10, 100,20,20), _pokemonEncounter.ChanceToEncounter);
        }
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