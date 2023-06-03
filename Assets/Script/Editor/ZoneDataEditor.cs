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
    List<PokemonBar> pokemonBars = new List<PokemonBar>();

    private void OnEnable()
    {
        zoneData = (ZoneDataSO)target;
        Init();
    }
    public void Init()
    {
        pokemonBars.Clear();
        for (int i = 0; i < zoneData.PokemonsInZoneByRarities.Count; i++)
        {
            PokemonBar _pokemonBar = new PokemonBar();
            PokemonsInZoneByRarity _pokemonInZoneRarity = zoneData.PokemonsInZoneByRarities[i];
            _pokemonBar.Init(ref _pokemonInZoneRarity);
            pokemonBars.Add(_pokemonBar);
        }
    }
    public override void OnInspectorGUI()
    {
        //GUILayoutUtility.GetRect(EditorGUIUtility.currentViewWidth, 200);
        base.OnInspectorGUI();
        //if(Event.current.clickCount == 2)
        //{
        //    RoundBarSlider();
        //}
        //for (int i = 0; i < pokemonBars.Count; i++)
        //{
        //    PokemonsInZoneByRarity _pokemonBar = zoneData.PokemonsInZoneByRarities[i];
        //    if (pokemonBars[i].Draw(ref _pokemonBar))
        //        Repaint();
        //}
    }
    public void RoundBarSlider()
    {
        for (int i = 0; i < pokemonBars.Count; i++)
        {
            pokemonBars[i].RoundSliders();
        }
    }
}

public class PokemonBar
{
    public List<PokemonSlider> pokemonsSlider = new List<PokemonSlider>();
    Vector2 currentPos;
    int index = -1;
    
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);
    public void Init(ref PokemonsInZoneByRarity pokemonZone)
    {
        pokemonsSlider.Clear();
        for (int i = 0; i < pokemonZone.PokemonsEncounter.Count; i++)
        {
            PokemonEncouterParameter _pokemonEncounter = pokemonZone.PokemonsEncounter[i];
            PokemonSlider _pokemonSlider = new PokemonSlider(ref _pokemonEncounter);
            PokemonSlider _previous = i > 0 ? pokemonsSlider[i - 1] : null;
            _pokemonSlider.leftSlider =  _previous;
            pokemonsSlider.Add(_pokemonSlider);
            if(_previous)
                _previous.rightSlider = _pokemonSlider;
        }
    }
    public float SliderWidth(float _chance)
    {
        return EditorGUIUtility.currentViewWidth * _chance / 100;
    }
    public void RoundSliders()
    {
        for (int j = 0; j < pokemonsSlider.Count; j++)
        {
            pokemonsSlider[j].RoundSlider();
        }
    }
    public void AddPokemon(ref PokemonsInZoneByRarity pokemonZone)
    {
        PokemonEncouterParameter _p = new PokemonEncouterParameter();
        _p.chanceToEncounter = pokemonZone.pokemonsEncounter[pokemonsSlider.Count - 1].chanceToEncounter / 2;
        pokemonZone.pokemonsEncounter[pokemonsSlider.Count - 1].chanceToEncounter /= 2;
        pokemonZone.pokemonsEncounter.Add(_p);
    }
    public void RemovePokemon(ref PokemonsInZoneByRarity pokemonZone)
    {
        pokemonZone.pokemonsEncounter[pokemonZone.pokemonsEncounter.Count - 1].chanceToEncounter += pokemonZone.pokemonsEncounter[pokemonZone.pokemonsEncounter.Count - 1].chanceToEncounter;
        pokemonZone.pokemonsEncounter.RemoveAt(pokemonsSlider.Count - 1);
    }
    public bool Draw(ref PokemonsInZoneByRarity pokemonZone)
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
            AddPokemon(ref pokemonZone);
        }
        if (GUI.Button(new Rect(50, 100, 50, 25), new GUIContent("-", "Remove Pokemon")))
        {
            RemovePokemon(ref pokemonZone);
        }
        if (Event.current.type == EventType.MouseUp)
        {
            index = -1;
            Debug.Log("MouseUp");
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
    public PokemonBar()
    {
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
    public void RoundSlider()
    {
        pokemonEncounter.chanceToEncounter = (float)Math.Round(pokemonEncounter.chanceToEncounter, 2);
    }
    public bool DrawButton(float _x, Action _callback = null)
    {
        float _width = EditorGUIUtility.currentViewWidth * pokemonEncounter.chanceToEncounter / 100;
        rect = new Rect(_x, 0, _width - 1, 100);
        GUI.Box(rect, "Pokemon " + Math.Round(pokemonEncounter.chanceToEncounter, 2) + "%");
        if (rect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
        {
            _callback.Invoke();
            return true;
        }
        return false;
    }
    public void SetValue(float _value)
    {
        float _chance = pokemonEncounter.chanceToEncounter;
        //float _min = leftSlider ? leftSlider.pokemonEncounter.chanceToEncounter + _chance : 0;
        float _max = rightSlider ? rightSlider.pokemonEncounter.chanceToEncounter + _chance : 100;
        pokemonEncounter.chanceToEncounter = _value;
        pokemonEncounter.chanceToEncounter = _value < 0 ? 0 : _value > _max ? _max : _value;
    }
    public void AddValue(float _direction)
    {
        if(rightSlider)
            SetValue(pokemonEncounter.chanceToEncounter + _direction);
        else
            SetValue(pokemonEncounter.chanceToEncounter - _direction);
    }
    public static implicit operator bool(PokemonSlider _pokemonSlider)
    {
        return _pokemonSlider != null;
    }
}