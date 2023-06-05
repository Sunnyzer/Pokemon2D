using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleField
{
    Pokemon firstPokemon;
    Pokemon secondPokemon;
    //Meteo meteo;
    //Hazard hazard

    public Pokemon FirstPokemon => firstPokemon;
    public Pokemon SecondPokemon => secondPokemon;

    public void ChangeFirstPokemon(Pokemon _newPokemon)
    {
        firstPokemon.ResetFieldEffect();
        firstPokemon = _newPokemon;
    }
    public void ChangeSecondPokemon(Pokemon _newPokemon)
    {
        secondPokemon.ResetFieldEffect();
        secondPokemon = _newPokemon;
    }
    public void DestroyBattleField()
    {

    }
    /*
    public void SetupMeteo(Meteo meteo)
    {

    }
    public void SetupHazard(Hazard hazard)
    {

    }
     */
    public BattleField(Pokemon _pokemon, Pokemon _pokemon2)
    {
        firstPokemon = _pokemon;
        secondPokemon = _pokemon2;
    }
    public static implicit operator bool(BattleField _this)
    {
        return _this != null;
    }
}