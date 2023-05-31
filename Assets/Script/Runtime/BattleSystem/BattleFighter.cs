using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BattleFighter
{
    public Pokemon[] Pokemons { get; }
    public Pokemon CurrentPokemonInCombat { get; }
    public bool IsInBattle { get; set; }
    public bool IsReady { get; set; }
    public abstract Pokemon GetPokemon();
    public abstract TurnAction Turn(BattleInfo _battleInfo);
}
