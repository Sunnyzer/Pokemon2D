using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamInfoUI : TeamUI
{
    [SerializeField] PokemonStatUI pokemonStatUI;
    [SerializeField] PokemonProfileUI pokemonProfileUI;

    public override void OnActivate()
    {
        base.OnActivate();
    }

    public override void OnDeactivate()
    {

    }

    public override void InitButton(int _index, PokemonInfoButton _pokemonInfoButton)
    {
        _pokemonInfoButton.onClick.AddListener(() => { ActivateStat(_index); });
    }
    public void ActivateStat(int _index)
    {
        PokemonTeam _pokemonTeam = GetOwnerMainUi<PlayerTrainer>().PokemonTeam;
        owner.ActiveSubUi(pokemonStatUI);
        pokemonStatUI.SetPokemonDisplay(_pokemonTeam[_index]);
    }
}
