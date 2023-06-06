using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

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
    [SerializeField] Stat fieldStat;
    [SerializeField] Stat modificationStat;
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
    public int HpMax => Mathf.FloorToInt(((2 * data.stat.HP + ivStat.HP + Mathf.FloorToInt(evStat.HP / 4)) * level) / 100) + level + 10;
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
        evStat = new Stat(0, 0, 0, 0, 0, 0);
        nature.name = _nature;
        moves = _moves;
        currentStat = new Stat(_data.stat);
        fieldStat = new Stat(_data.stat);
        modificationStat = Stat.One;

        OnLevelUp += (p) =>
        {
            List<Move> _moves = MoveToLearn();
            for (int i = 0; i < _moves.Count; i++)
            {
                LearnMove(_moves[i]);
            }
            ApplyStat();
        };
        ApplyStat();
        currentStat.HP = HpMax;
    }
    public float GetSpeedInCombat()
    {
        return currentStat.Speed;
    }
    public void TakeDamage(Pokemon _owner, Move _move)
    {
        int _crit = Random.Range(0, 100) < (1/24 * (_move.CritRate * 2 + 1) * 100) ? 2 : 1;
        if(_crit == 2)
        {
            Debug.Log(" Critique !!! ");
        }
        float _stab = _owner.data.pkmTypes[0] == _move.Type ? 1.5f : 1;
        float effectiveness1 = BattleManager.Instance.TypeTable.GetEffective(_move.Type, data.pkmTypes[0]);
        float effectiveness2 = data.pkmTypes.Length == 2 ? BattleManager.Instance.TypeTable.GetEffective(_move.Type, data.pkmTypes[1]) : 1;
        float _critique = ((2 * _crit * _owner.level) / 5) + 2;
        if (_move.Power == 0)
        {
            Debug.Log(0 + " Damage");
            return;
        }
        float _damage = ((_critique * _move.Power  * _owner.currentStat.Attack/currentStat.Defense)/50 + 2) * _stab * effectiveness1 * effectiveness2 * ((float)Random.Range(217, 256)/255);
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
        currentStat.HP = currentStat.HP > HpMax ? HpMax : currentStat.HP;
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
        modificationStat = Stat.One;
        ApplyStat();
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
    public void ApplyStat()
    {
        NatureBoost _natureBoost = NatureData.natureBoosts[nature.name];
        currentStat.Attack = Mathf.FloorToInt(((2 * data.stat.Attack + ivStat.Attack + Mathf.FloorToInt(evStat.Attack / 4)) * level) / 100 + 5) * (1 + _natureBoost.Stat.Attack / 100); 
        currentStat.Defense = Mathf.FloorToInt(((2 * data.stat.Defense + ivStat.Defense + Mathf.FloorToInt(evStat.Defense / 4)) * level) / 100 + 5) * (1 + _natureBoost.Stat.Defense / 100);
        currentStat.SpAttack = Mathf.FloorToInt(((2 * data.stat.SpAttack + ivStat.SpAttack + Mathf.FloorToInt(evStat.SpAttack / 4)) * level) / 100 + 5) * (1 + _natureBoost.Stat.SpAttack / 100); 
        currentStat.SpDefense = Mathf.FloorToInt(((2 * data.stat.SpDefense + ivStat.SpDefense + Mathf.FloorToInt(evStat.SpDefense / 4)) * level) / 100 + 5) * (1 + _natureBoost.Stat.SpDefense / 100); 
        currentStat.Speed = Mathf.FloorToInt(((2 * data.stat.Speed + ivStat.Speed + Mathf.FloorToInt(evStat.Speed / 4)) * level) / 100 + 5) * (1 + _natureBoost.Stat.Speed / 100);
        fieldStat = currentStat * modificationStat;
    }
    public static implicit operator bool(Pokemon _pokemon)
    {
        return _pokemon != null;
    }
}