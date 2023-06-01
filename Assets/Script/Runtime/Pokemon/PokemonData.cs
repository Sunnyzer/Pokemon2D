using System;
using UnityEngine;

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
    public Action<Pokemon> OnHpChange = null;
    PokemonData data;
    [SerializeField] Stat currentStat;
    [SerializeField] Stat ivStat;
    [SerializeField] Stat evStat;
    [SerializeField] Move[] moves;
    [SerializeField] Nature nature = new Nature();
    [SerializeField] int level = 1;
    //Status currentStatus;
    string name;
    int xp = 0;
    int xpMax = 100;
    bool fainted = false;

    public PokemonData Data => data;
    public Move[] Moves => moves;
    public string Name => name;
    public int Level => level;
    public bool Fainted => fainted;
    public int Hp => currentStat.HP;
    public int HpMax => data.stat.HP;
    public int Xp => xp;
    public int XpMax => xpMax;
    public int XpGive => 40;
    public Stat CurrentStat => currentStat;
    //public Status CurrentStatus => currentStatus;

    public Pokemon(int _level, PokemonData _data, Stat _ivStat, string _nature, Move[] _moves)
    {
        name = _data.name.french;
        level = _level;
        data = _data;
        ivStat = _ivStat;
        nature.name = _nature;
        moves = _moves;
        currentStat = new Stat(_data.stat);
    }
    public float GetSpeedInCombat()
    {
        return currentStat.Speed;
    }
    public void TakeDamage(Pokemon _owner, Move _move)
    {
        int _crit = UnityEngine.Random.Range(0,10) == 0 ? 2 : 1;
        float _stab = _owner.data.pkmTypes[0] == _move.Type ? 1.5f : 1;
        int effectiveness1 = 1;
        int effectiveness2 = 1;
        float _damage = (((((2 * _crit * _owner.level)/5) + 2) * _move.Power * _owner.currentStat.Attack/currentStat.Defense)/50 + 2) * _stab * effectiveness1 * effectiveness2 * 1;
        TakeDamage((int)_damage);
    }
    public void TakeDamage(int _damage)
    {
        if (_damage <= 0 || fainted)
        {
            Debug.Log("D :" + _damage);
            return;
        }
        currentStat.HP -= _damage;
        if (currentStat.HP <= 0)
        {
            currentStat.HP = 0;
            fainted = true;
            OnFainted?.Invoke(this);
        }
        OnHpChange?.Invoke(this);
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
        currentStat.HP = currentStat.HP > data.stat.HP ? data.stat.HP : currentStat.HP;
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
            xpMax = 100;//TODO
            GainExp(_xpLeft);
        }
    }
    public Move GetRandomMove()
    {
        if(moves.Length == 0) return null;
        int _moves = UnityEngine.Random.Range(0, moves.Length);
        return moves[_moves];
    }
}