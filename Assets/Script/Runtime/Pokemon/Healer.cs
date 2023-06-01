using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : InteractObject
{
    public void HealPokemon(PlayerTrainer _playerTrainer)
    {
        string _pokemonHeal = "";
        for (int i = 0; i < _playerTrainer.PokemonTeam.Lenght; i++)
        {
            _pokemonHeal += _playerTrainer.PokemonTeam[i]?.Name + ",";
            _playerTrainer.PokemonTeam[i]?.RecoverAll();
        }
        Debug.Log("Heal Pokemon " + _pokemonHeal);
    }

    public override void Interact(PlayerTrainer _player)
    {
        HealPokemon(_player);
    }
}
