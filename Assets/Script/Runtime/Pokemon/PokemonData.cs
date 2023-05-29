using System;
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
    public int Level => level;
    public MoveChoice MoveChoice => moveChoice;
}

[Serializable]
public class PokemonChoice
{
    [SerializeField] int indexPokemon;
    public int IndexPokemon => indexPokemon;
    public PokemonChoice(int _index)
    {
        indexPokemon = _index;
    }
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

[Serializable]
public class Pokemon
{
    public Action<Pokemon> OnFainted = null;
    public Action<Pokemon> OnMaxHeal = null;
    public Action<Pokemon> OnLevelUp = null;
    PokemonData data;
    [SerializeField] Stat currentStat;
    [SerializeField] Stat stat;
    [SerializeField] Stat ivStat;
    [SerializeField] Stat evStat;
    [SerializeField] Move[] moves;
    [SerializeField] Nature nature = new Nature();
    [SerializeField] int level = 1;
    //Status currentStatus;
    string name;
    int xp = 0;
    int xpMax = 0;
    bool fainted = false;

    public PokemonData Data => data;
    public Move[] Moves => moves;
    public string Name => name;
    public int Level => level;
    public bool Fainted => fainted;
    //public Status CurrentStatus => currentStatus;

    public Pokemon(int _level, PokemonData _data, Stat _ivStat, string _nature, Move[] _moves)
    {
        name = _data.name.french;
        level = _level;
        data = _data;
        ivStat = _ivStat;
        nature.name = _nature;
        moves = _moves;
        fainted = false;
    }

    public void TakeDamage(int _damage)
    {
        if (_damage <= 0 || fainted) return;
        currentStat.HP -= _damage;
        if(currentStat.HP <= 0)
        {
            currentStat.HP = 0;
            fainted = true;
            OnFainted?.Invoke(this);
        }
    }
    public void Revive(int _heal)
    {
        fainted = false;
        Heal(_heal <= 0 ? 1 : _heal);
    }
    public void RecoverAll()
    {
        //currentStatus = Status.None;
        fainted = false;
        Heal(99999999);
    }
    public void HealStatus()
    {
        //currentStatus = Status.None;
    }
    public void Heal(int _heal)
    {
        if (_heal <= 0 || fainted) return;
        currentStat.HP += _heal;
        currentStat.HP = currentStat.HP > stat.HP ? stat.HP : currentStat.HP;
    }
    public void GainExp(int _xpEarn)
    {
        xp += _xpEarn;
        if(xp >= xpMax && level < 100)
        {
            ++level;
            OnLevelUp?.Invoke(this);
            int _xpLeft = xp - xpMax;
            xp = 0;
            xpMax = 10;//TODO
            GainExp(_xpLeft);
        }
    }
}