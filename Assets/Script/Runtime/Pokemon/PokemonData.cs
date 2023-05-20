using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Names
{
    public string french;
}

[Serializable]
public class Stat
{
    [SerializeField] public int HP;
    [SerializeField] public int Attack;
    [SerializeField] public int Defense;
    [SerializeField] public int SpAttack;
    [SerializeField] public int SpDefense;
    [SerializeField] public int Speed;
    public Stat(int _hp, int _attack, int _defense, int _spAttack, int _spDefense, int _speed)
    {
        HP = _hp;
        Attack = _attack;
        Defense = _defense;
        SpAttack = _spAttack;
        SpDefense = _spDefense;
        Speed = _speed;
    }
    public static Stat GetRandomIV()
    {
        int hp = UnityEngine.Random.Range(0, 32);
        int attack = UnityEngine.Random.Range(0, 32);
        int defense = UnityEngine.Random.Range(0, 32);
        int spAttack = UnityEngine.Random.Range(0, 32);
        int spDefense = UnityEngine.Random.Range(0, 32);
        int speed = UnityEngine.Random.Range(0, 32);
        Stat _stat = new Stat(hp, attack, defense, spAttack, spDefense, speed);
        return _stat;
    }
}

[Serializable]
public class MoveByLevel
{
    [SerializeField] int level;
    [SerializeField] MoveChoice moveChoice;
}

[Serializable]
public class PokemonChoice
{
    [SerializeField] int indexPokemon;
    public static implicit operator int(PokemonChoice _choice)
    {
        return _choice.indexPokemon;
    }
}

[Serializable]
public class PokemonData
{
    [SerializeField] public int id = 0;
    [SerializeField] public Names name;
    [SerializeField] public PkmType[] pkmTypes;
    [SerializeField] public MoveByLevel[] moveChoices;
    [SerializeField] public Stat stat;
    [SerializeField] int courbeExpType;
    [SerializeField] public Sprite completeSprite;
    [SerializeField] public Sprite backSprite;
}

public class Pokemon
{
    PokemonData data;
    Stat ivStat;
    Stat evStat;
    Move[] moves;
    Nature nature = new Nature();
    string name;
    int exp = 0;
    int expMax = 0;
    int level = 1;

    public PokemonData Data => data;
    public Move[] Moves => moves;
    public string Name => name;
    public int Level => level;
    public Pokemon(int _level, PokemonData _data, Stat _ivStat, string _nature)
    {
        name = _data.name.french;
        level = _level;
        data = _data;
        ivStat = _ivStat;
        nature.name = _nature;
    }
    public static Pokemon GetRandomPokemon(int _level, PokemonData _pokemonData)
    {
        return new Pokemon(_level, _pokemonData, Stat.GetRandomIV(), Nature.GetRandomNature());
    }
}