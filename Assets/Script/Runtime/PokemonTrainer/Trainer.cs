using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trainer : MonoBehaviour, BattleFighter
{
    [SerializeField] PokemonTeam team = null;

    public Pokemon[] Pokemons => team.Pokemons;

    public bool IsInBattle { get; set; }
    public bool IsReady { get; set; }

    public Pokemon CurrentPokemonInCombat => team.GetFirstLivingPokemon();


    public Pokemon GetPokemon()
    {
        return team.GetFirstLivingPokemon();
    }

    public TurnAction Turn(BattleInfo _battleInfo)
    {
        AttackAction _action = new AttackAction(CurrentPokemonInCombat, CurrentPokemonInCombat.GetRandomMove());
        _action.BattleInfo = _battleInfo;
        return _action;
    }
}
