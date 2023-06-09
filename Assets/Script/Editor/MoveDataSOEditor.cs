using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

[CustomEditor(typeof(MoveDataSO))]
public class MoveDataSOEditor : Editor
{
    MoveDataSO moveDataSO;
    TextAsset file = null;
    int index = 0;
    float pourcentage = 0;
    private void OnEnable()
    {
        moveDataSO = (MoveDataSO)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        index = EditorGUILayout.IntField(index);
        EditorGUILayout.Slider(pourcentage * 100, 0, 100);
        if (GUILayout.Button("Generate Move"))
        {
            FindObjectOfType<MonoBehaviour>().StartCoroutine(GenerateMove());
        }
    }
    private IEnumerator GenerateMove()
    {
        if (index == 0) yield break;
        moveDataSO.allMoves.moves = null;
        moveDataSO.allMoves.moves = new MoveData[index];
        pourcentage = 0;
        for (int i = 1; i < index + 1; i++)
        {
            UnityWebRequest _webRequest = UnityWebRequest.Get("https://pokeapi.co/api/v2/move/" + i);
            yield return _webRequest.SendWebRequest();
            pourcentage = (float)i / (float)(index + 1);
            if (_webRequest.result == UnityWebRequest.Result.Success)
            {
                //Debug.Log(i);
                RootMove _move = JsonConvert.DeserializeObject<RootMove>(_webRequest.downloadHandler.text);
                MoveData _moveData = new MoveData();
                if(_move.accuracy != null)
                    _moveData.accuracy = _move.accuracy.Value;
                _moveData.name = _move.name;
                _moveData.id = i;
                if(_move.power != null)
                    _moveData.power = _move.power.Value;
                if(_move.pp != null)
                    _moveData.pp = _move.pp.Value;
                _moveData.priority = _move.priority;
                _moveData.type = Enum.Parse<PkmType>(_move.type.name);
                _moveData.damageType = Enum.Parse<DamageType>(_move.damage_class.name);
                _moveData.critRate = _move.meta.crit_rate;
                _moveData.statChance = _move.meta.stat_chance;
                _moveData.healing = _move.meta.healing;
                _moveData.drain = _move.meta.drain;
                _moveData.flinchRate = _move.meta.flinch_chance;
                if(_move.effect_chance != null)
                    _moveData.effectChance = _move.effect_chance.Value;
                if (_move.stat_changes.Count > 0)
                    _moveData.stat_changes = new Stat(0,0,0,0,0,0);
                
                if (Enum.TryParse(_move.meta.ailment.name, out Status _result))
                    _moveData.effectStatus = _result;
                else
                    Debug.Log(_move.meta.ailment.name);

                if(_move.meta.max_hits != null)
                    _moveData.max_hits = _move.meta.max_hits.Value;
                if(_move.meta.min_hits != null)
                    _moveData.min_hits = _move.meta.min_hits.Value;
                if(_move.meta.max_turns != null)
                    _moveData.max_turns = _move.meta.max_turns.Value;
                if(_move.meta.min_turns != null)
                    _moveData.min_turns = _move.meta.min_turns.Value;
                
                for (int j = 0; j < _move.stat_changes.Count; j++)
                {
                    if (_move.stat_changes[j].stat.name == "attack")
                        _moveData.stat_changes.Attack += _move.stat_changes[j].change;
                    if (_move.stat_changes[j].stat.name == "defense")
                        _moveData.stat_changes.Defense += _move.stat_changes[j].change;
                    if (_move.stat_changes[j].stat.name == "special-attack")
                        _moveData.stat_changes.SpAttack += _move.stat_changes[j].change;
                    if (_move.stat_changes[j].stat.name == "special-defense")
                        _moveData.stat_changes.SpDefense += _move.stat_changes[j].change;
                    if (_move.stat_changes[j].stat.name == "speed")
                        _moveData.stat_changes.Speed += _move.stat_changes[j].change;
                }
                moveDataSO.allMoves.moves[i - 1] = _moveData;
            }
        }
        serializedObject.ApplyModifiedProperties();
        pourcentage = 1;
    }
}
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Ailment
{
    public string name { get; set; }
    public string url { get; set; }
}

public class Category
{
    public string name { get; set; }
    public string url { get; set; }
}

public class ContestCombos
{
    public Normal normal { get; set; }
    public Super super { get; set; }
}

public class ContestEffect
{
    public string url { get; set; }
}

public class ContestType
{
    public string name { get; set; }
    public string url { get; set; }
}

public class DamageClass
{
    public string name { get; set; }
    public string url { get; set; }
}

public class EffectEntry
{
    public string effect { get; set; }
    public Language language { get; set; }
    public string short_effect { get; set; }
}

public class FlavorTextEntry
{
    public string flavor_text { get; set; }
    public Language language { get; set; }
    public VersionGroupJson version_group { get; set; }
}

public class Generation
{
    public string name { get; set; }
    public string url { get; set; }
}

public class Language
{
    public string name { get; set; }
    public string url { get; set; }
}

public class LearnedByPokemon
{
    public string name { get; set; }
    public string url { get; set; }
}

public class Machine
{
    public Machine2 machine { get; set; }
    public VersionGroupJson version_group { get; set; }
}

public class Machine2
{
    public string url { get; set; }
}

public class Meta
{
    public Ailment ailment { get; set; }
    public int ailment_chance { get; set; }
    public Category category { get; set; }
    public int crit_rate { get; set; }
    public int drain { get; set; }
    public int flinch_chance { get; set; }
    public int healing { get; set; }
    public int? max_hits { get; set; }
    public int? max_turns { get; set; }
    public int? min_hits { get; set; }
    public int? min_turns { get; set; }
    public int stat_chance { get; set; }
}

public class Name
{
    public Language language { get; set; }
    public string name { get; set; }
}

public class Normal
{
    //public object use_after { get; set; }
    public List<UseBefore> use_before { get; set; }
}

public class RootMove
{
    public int? accuracy { get; set; }
    public ContestCombos contest_combos { get; set; }
    public ContestEffect contest_effect { get; set; }
    public ContestType contest_type { get; set; }
    public DamageClass damage_class { get; set; }
    public int? effect_chance { get; set; }
    //public List<object> effect_changes { get; set; }
    public List<EffectEntry> effect_entries { get; set; }
    public List<FlavorTextEntry> flavor_text_entries { get; set; }
    public Generation generation { get; set; }
    public int id { get; set; }
    public List<LearnedByPokemon> learned_by_pokemon { get; set; }
    public List<Machine> machines { get; set; }
    public Meta meta { get; set; }
    public string name { get; set; }
    public List<Name> names { get; set; }
    public int? power { get; set; }
    public int? pp { get; set; }
    public int priority { get; set; }
    public List<StatChange> stat_changes { get; set; }
    public SuperContestEffect super_contest_effect { get; set; }
    public Target target { get; set; }
    public TypeJson type { get; set; }
}

public class Stat3
{
    public string name { get; set; }
    public string url { get; set; }
}

public class StatChange
{
    public int change { get; set; }
    public Stat3 stat { get; set; }
}

public class Super
{
    //public object use_after { get; set; }
    //public object use_before { get; set; }
}

public class SuperContestEffect
{
    public string url { get; set; }
}

public class Target
{
    public string name { get; set; }
    public string url { get; set; }
}

public class TypeJson
{
    public string name { get; set; }
    public string url { get; set; }
}

public class UseBefore
{
    public string name { get; set; }
    public string url { get; set; }
}

public class VersionGroupJson
{
    public string name { get; set; }
    public string url { get; set; }
}