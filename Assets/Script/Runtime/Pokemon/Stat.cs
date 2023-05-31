using System;
using UnityEngine;

[Serializable]
public struct Names
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
