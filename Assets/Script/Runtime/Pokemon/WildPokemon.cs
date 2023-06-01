using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildPokemon : BattleFighter
{
    public event Action<Pokemon> OnSwapPokemon;

    Pokemon pokemon = null;
    public Pokemon[] Pokemons { get; }
    public bool IsInBattle { get; set; }
    public bool IsReady { get; set; }

    public Pokemon CurrentPokemonInCombat => pokemon;

    public Pokemon GetFirstSlotPokemon()
    {
        return pokemon;
    }

    public TurnAction Turn(BattleInfo _battleInfo)
    {
        Move _move = pokemon.GetRandomMove();
        if (_move == null)
            _move = new Move(MoveManager.Instance.GetMoveLutte());
        AttackAction _action = new AttackAction(pokemon, _move);
        _action.BattleInfo = _battleInfo;
        return _action;
    }

    public bool Swap(int _index)
    {
        return false;
    }

    public WildPokemon(Pokemon _pokemon)
    {
        pokemon = _pokemon;
    }
}
