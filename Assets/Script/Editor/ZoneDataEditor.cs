using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.Linq;
using UnityEngine.UI;

[CustomEditor(typeof(ZoneDataSO))]
public class ZoneDataEditor : Editor
{
    ZoneDataSO zoneData;
    int index = -1;
    bool test = true;
    Vector2 currentPos;
    List<PokemonEncouterParameter> pokemonEncouterParameters = new List<PokemonEncouterParameter>();
    List<PokemonSlider> pokemonSliders = new List<PokemonSlider>();
    
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);
    
    private void OnEnable()
    {
        zoneData = (ZoneDataSO)target;
        zoneData.PokemonsInZoneByRarities.Clear();
        PokemonsInZoneByRarity _p = new PokemonsInZoneByRarity();
        for (int i = 0; i < 3; i++)
        {
            PokemonEncouterParameter _pEncounter = new PokemonEncouterParameter();
            _p.pokemonsEncounter.Add(_pEncounter);
            _p.pokemonsEncounter[i].chanceToEncounter = 100 * 1f/3f;
            PokemonSlider _left = null;
            if(pokemonSliders.Count - 1 >= 0)
                _left = pokemonSliders[pokemonSliders.Count - 1];
            PokemonSlider _slider = CreateSlider(ref _pEncounter, null, _left);
            pokemonSliders.Add(_slider);
            if(_left != null)
                _left.rightSlider = _slider;
        }
        zoneData.PokemonsInZoneByRarities.Add(_p);
    }
    public void InitRect()
    {
        for (int i = 0; i < 1; i++)
        {
            PokemonsInZoneByRarity _pokemonZone = zoneData.PokemonsInZoneByRarities[i];
            float _sizeButton = EditorGUIUtility.currentViewWidth / _pokemonZone.PokemonsEncounter.Count;
            for (int j = 0; j < _pokemonZone.PokemonsEncounter.Count; j++)
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
        //base.OnInspectorGUI();
        if(test)
        {
            InitRect();
            test = false;
        }
        GUILayoutUtility.GetRect(EditorGUIUtility.currentViewWidth, 500);
        for (int i = 0; i < 1 /*zoneData.PokemonsInZoneByRarities.Count*/; i++)
        {
            PkmBar(zoneData.PokemonsInZoneByRarities[i]);
        }
        if (Event.current.type == EventType.MouseUp || Event.current.type == EventType.MouseLeaveWindow)
        {
            index = -1;
            Debug.Log("MouseUp");
        }
        if(index != -1)
        {
            
            Vector2 _currentPos = GUIUtility.GUIToScreenPoint(currentPos);
            SetCursorPos((int)_currentPos.x, (int)_currentPos.y);//Call this when you want to set the mouse position
        }
    }
    public PokemonSlider CreateSlider(ref PokemonEncouterParameter _pokemonEncounter, PokemonSlider _right, PokemonSlider _left)
    {
        PokemonSlider pokemonSlider = new PokemonSlider(ref _pokemonEncounter);
        pokemonSlider.rightSlider = _right;
        pokemonSlider.leftSlider = _left;
        return pokemonSlider;
    }
    public void AddPokemon(PokemonsInZoneByRarity _pokemonZone)
    {
        PokemonEncouterParameter _p = new PokemonEncouterParameter();
        PokemonEncouterParameter _lastP = _pokemonZone.pokemonsEncounter[_pokemonZone.pokemonsEncounter.Count - 1];
        _p.chanceToEncounter = _lastP.chanceToEncounter/2;
        _lastP.chanceToEncounter /= 2;
        _pokemonZone.pokemonsEncounter.Add(_p);
        _p.rect = new Rect(_lastP.rect.Value.x + _lastP.rect.Value.width/2, 0, SliderWidth(_p.chanceToEncounter), 100);
        pokemonSliders.Add(new PokemonSlider(ref _p));
    }
    public float SliderWidth(float _chance)
    {
        return EditorGUIUtility.currentViewWidth * _chance / 100;
    }
}

public class PokemonBar
{
    PokemonsInZoneByRarity pokemonZone;
    List<PokemonSlider> pokemonsSlider;
    Vector2 currentPos;
    int index = 0;
    
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);

    public float SliderWidth(float _chance)
    {
        return EditorGUIUtility.currentViewWidth * _chance / 100;
    }
    public void RoundSliders()
    {
        for (int j = 0; j < pokemonZone.PokemonsEncounter.Count; j++)
        {
            PokemonEncouterParameter _pokemonEncounterP = pokemonZone.PokemonsEncounter[j];
            _pokemonEncounterP.chanceToEncounter = Mathf.RoundToInt(_pokemonEncounterP.chanceToEncounter);
        }
    }
    public void AddPokemon()
    {

    }
    public void RemovePokemon()
    {

    }
    public bool Draw()
    {
        float _x = 0;
        bool _drag = false;
        if (index != -1)
        {
            _drag = Drag();
            Vector2 _currentPos = GUIUtility.GUIToScreenPoint(currentPos);
            SetCursorPos((int)_currentPos.x, (int)_currentPos.y);
        }
        for (int i = 0; i < pokemonsSlider.Count; i++)
        {
            PokemonSlider _pokemonS = pokemonsSlider[i];
            float _width = SliderWidth(_pokemonS.pokemonEncounter.ChanceToEncounter);
            _pokemonS.DrawButton(_x, () => Grab(i));
            _x += _width;
        }
        if (GUI.Button(new Rect(0, 100, 50, 25), new GUIContent("+", "Add Pokemon")))
        {
            AddPokemon();
        }
        if (GUI.Button(new Rect(50, 100, 50, 25), new GUIContent("-", "Remove Pokemon")))
        {
            RemovePokemon();
        }
        return _drag;
    }
    public void Grab(int _index)
    {
        if (index != _index)
        {
            index = _index;
            currentPos = Event.current.mousePosition;
        }
    }
    public bool Drag()
    {
        bool _action = false;
        int _directionX = Mathf.RoundToInt(Event.current.mousePosition.x - currentPos.x);
        if (_directionX != 0)
        {
            float _value = _directionX * 0.1f;
            pokemonsSlider[index].AddValue(_value);
            _action = true;
        }
        return _action;
    }
    public PokemonBar(PokemonsInZoneByRarity _pokemonZone)
    {
        pokemonZone = _pokemonZone;
    }
}

public class PokemonSlider
{
    public PokemonEncouterParameter pokemonEncounter;
    public Rect rect;
    public PokemonSlider rightSlider = null;
    public PokemonSlider leftSlider = null;
    public PokemonSlider(ref PokemonEncouterParameter pokemonEncouterParam)
    {
        pokemonEncounter = pokemonEncouterParam;
    }
    public bool DrawButton(float _x, Action _callback = null)
    {
        float _width = EditorGUIUtility.currentViewWidth * pokemonEncounter.chanceToEncounter / 100;
        rect = new Rect(_x, pokemonEncounter.rect.Value.y, _width, pokemonEncounter.rect.Value.height);
        GUI.Box(rect, "Pokemon " + pokemonEncounter.chanceToEncounter + "%");
        if (rect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
        {
            _callback.Invoke();
            return true;
        }
        return false;
    }
    public void AddValue(float _direction)
    {
        if(leftSlider && !rightSlider)
        {
            pokemonEncounter.chanceToEncounter -= _direction;
            leftSlider.pokemonEncounter.chanceToEncounter += _direction;
        }
        else if(rightSlider && !leftSlider)
        {
            pokemonEncounter.chanceToEncounter += _direction;
            rightSlider.pokemonEncounter.chanceToEncounter -= _direction;
        }
        else
        {
            pokemonEncounter.chanceToEncounter += Mathf.Abs(_direction);
            if(_direction >= 0)
                rightSlider.pokemonEncounter.chanceToEncounter -= _direction;
            else
                leftSlider.pokemonEncounter.chanceToEncounter += _direction;
        }
    }
    public static implicit operator bool(PokemonSlider _pokemonSlider)
    {
        return _pokemonSlider != null;
    }
}