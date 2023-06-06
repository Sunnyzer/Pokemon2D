using System;
using UnityEngine;

[Serializable]
public class Names
{
    public string french;
    public string english;
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
    public static Stat One
    {
        get => new Stat(1, 1, 1, 1, 1, 1);
    }
    public Stat(int _hp, int _attack, int _defense, int _spAttack, int _spDefense, int _speed)
    {
        HP = _hp;
        Attack = _attack;
        Defense = _defense;
        SpAttack = _spAttack;
        SpDefense = _spDefense;
        Speed = _speed;
    }
    public Stat(Stat _stat)
    {
        HP = _stat.HP;
        Attack = _stat.Attack;
        Defense = _stat.Defense;
        SpAttack = _stat.SpAttack;
        SpDefense = _stat.SpDefense;
        Speed = _stat.Speed;
    }
    public Stat()
    {
        HP = 0;
        Attack = 0;
        Defense = 0;
        SpAttack = 0;
        SpDefense = 0;
        Speed = 0;
    }
    public static Stat operator *(Stat _first, Stat _second)
    {
        int _hp = _first.HP * _second.HP;
        int _attack = _first.Attack * _second.Attack;
        int _defense = _first.Defense * _second.Defense;
        int _spAttack = _first.SpAttack * _second.SpAttack;
        int _spDefense = _first.SpDefense * _second.SpDefense;
        int _speed = _first.Speed * _second.Speed;
        return new Stat(_hp, _attack, _defense, _spAttack, _spDefense, _speed);
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
