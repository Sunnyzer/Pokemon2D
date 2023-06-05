using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] List<Move> moves;
    [SerializeField] Nature nature = new Nature();
    [SerializeField] int level = 1;
    //Status currentStatus;

    string name;
    int xp = 0;
    int xpMax = 100;
    bool fainted = false;

    public PokemonData Data => data;
    public List<Move> Moves => moves;
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

    public Pokemon(int _level, PokemonData _data, Stat _ivStat, string _nature, List<Move> _moves)
    {
        name = _data.name.english;
        level = _level;
        data = _data;
        ivStat = _ivStat;
        nature.name = _nature;
        moves = _moves;
        currentStat = new Stat(_data.stat);
        OnLevelUp += (p) =>
        {
            List<Move> _moves = MoveToLearn();
            for (int i = 0; i < _moves.Count; i++)
            {
                LearnMove(_moves[i]);
            }
        };
    }
    public float GetSpeedInCombat()
    {
        return currentStat.Speed;
    }
    public void TakeDamage(Pokemon _owner, Move _move)
    {
        int _crit = UnityEngine.Random.Range(0, 10) == 0 ? 2 : 1;
        float _stab = _owner.data.pkmTypes[0] == _move.Type ? 1.5f : 1;
        int effectiveness1 = 1;
        int effectiveness2 = 1;
        float _critique = ((2 * _crit * _owner.level) / 5) + 2;
        if (_move.Power == null)
        {
            Debug.Log(0 + " Damage");
            return;
        }
        float _damage = ((_critique * _move.Power.Value  * _owner.currentStat.Attack/currentStat.Defense)/50 + 2) * _stab * effectiveness1 * effectiveness2 * 1;
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
        for (int i = 0; i < moves.Count; i++)
        {
            moves[i]?.RecoverPP();
        }
        Heal(HpMax);
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
        if(moves.Count == 0) return null;
        int _moves = UnityEngine.Random.Range(0, moves.Count);
        return moves[_moves];
    }
    public void ResetFieldEffect()
    {

    }
    public bool LearnMove(Move _move)
    {
        if(_move == null) return false;
        if(moves.Count < 4)
        {
            moves.Add(_move);
            Debug.Log(name + " Learn " + _move.Name);
        }
        else
        {
            Debug.Log(name + "Can Learn " + _move.Name + "But already learn 4 moves");
        }
        return true;
    }
    public List<Move> MoveToLearn()
    {
        List<Move> _moves = new List<Move>();
        for (int i = 0; i < data.moveChoices.Length; i++)
        {
            if(data.moveChoices[i].Level == level)
            {
                _moves.Add(new Move(MoveManager.Instance.GetMoveDataByMoveByLevel(data.moveChoices[i])));
            }
            if(level < data.moveChoices[i].Level)
                break;
        }
        return _moves;
    }
    public static implicit operator bool(Pokemon _pokemon)
    {
        return _pokemon != null;
    }
}