using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamInfoUI : TeamUI
{
    [SerializeField] PokemonStatUI pokemonStatUI;
    [SerializeField] PokemonProfileUI pokemonProfileUI;
    public int CurrentIndexPokemonDisplay { get; set; }

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
        CurrentIndexPokemonDisplay = _index;
        PokemonTeam _pokemonTeam = GetOwnerMainUi<PlayerTrainer>().PokemonTeam;
        owner.ActiveSubUI(pokemonStatUI);
        pokemonStatUI.UpdatePokemon(_pokemonTeam[_index]);
        pokemonProfileUI.UpdatePokemon(_pokemonTeam[_index]);
    }
    public void NextPokemonStat()
    {
        PokemonTeam _pokemonTeam = GetOwnerMainUi<PlayerTrainer>().PokemonTeam;
        if(CurrentIndexPokemonDisplay + 1 >= _pokemonTeam.Lenght)
        {
            CurrentIndexPokemonDisplay = 0;
        }
        else
            CurrentIndexPokemonDisplay++;
        pokemonStatUI.UpdatePokemon(_pokemonTeam[CurrentIndexPokemonDisplay]);
        pokemonProfileUI.UpdatePokemon(_pokemonTeam[CurrentIndexPokemonDisplay]);
    }
    public void PreviousPokemonStat()
    {
        PokemonTeam _pokemonTeam = GetOwnerMainUi<PlayerTrainer>().PokemonTeam;
        if (CurrentIndexPokemonDisplay - 1 < 0)
            CurrentIndexPokemonDisplay = _pokemonTeam.Lenght - 1;
        else
            CurrentIndexPokemonDisplay--;

        pokemonStatUI.UpdatePokemon(_pokemonTeam[CurrentIndexPokemonDisplay]);
        pokemonProfileUI.UpdatePokemon(_pokemonTeam[CurrentIndexPokemonDisplay]);
    }
}
