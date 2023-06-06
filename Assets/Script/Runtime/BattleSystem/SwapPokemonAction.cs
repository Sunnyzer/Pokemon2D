using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPokemonAction : TurnAction
{
    Pokemon pokemonToSwap;
    public override int GetPriority(BattleField _battleField)
    {
        return 100;
    }
    public SwapPokemonAction(Pokemon _pokemonToSwap)
    {
        pokemonToSwap = _pokemonToSwap;
    }
    public override void Action(BattleField _battleField)
    {
        _battleField.ChangeFirstPokemon(pokemonToSwap);
    }
}
