using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trainer : MonoBehaviour, BattleFighter
{
    public event Action<Pokemon> OnSwapPokemon;
    [SerializeField] PokemonTeam team = null;

    public Pokemon[] Pokemons => team.Pokemons;

    public bool IsInBattle { get; set; }
    public bool IsReady { get; set; }

    public Pokemon CurrentPokemonInCombat => null;


    public Pokemon GetFirstSlotPokemon()
    {
        return null;
    }

    public bool Swap(int _index)
    {
        return false;
    }

    public TurnAction Turn(BattleInfo _battleInfo)
    {
        return null;
    }
}
