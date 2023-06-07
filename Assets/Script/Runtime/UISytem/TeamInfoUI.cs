using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamInfoUI : TeamUI
{
    [SerializeField] PokemonStatUI pokemonStatUI;
    [SerializeField] PokemonProfileUI pokemonProfileUI;
    [SerializeField] PokemonCellUI pokemonCellUI;

    public override void Activate()
    {

    }

    public override void Deactivate()
    {

    }

    public override void InitButton(int _index, PokemonInfoButton _pokemonInfoButton)
    {
        _pokemonInfoButton.onClick.AddListener(() => {  });
    }
}
