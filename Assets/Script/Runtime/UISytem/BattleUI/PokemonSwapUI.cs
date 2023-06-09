using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonSwapUI : TeamUI
{
    [SerializeField] Button returnButton;

    public bool ForceSwap => returnButton.interactable == false;
    
    public override void Init(SubUIManagement _owner)
    {
        base.Init(_owner);
        returnButton.onClick.AddListener(owner.ActivePreviousSubUIDisplay);
    }
    public override void OnActivate()
    {
        PlayerTrainer _ownerTrainer = GetOwnerMainUi<PlayerTrainer>();
        UpdateUI(_ownerTrainer.PokemonTeam);
    }

    public override void OnDeactivate()
    {

    }
    public void UpdateUI(PokemonTeam _team)
    {
        returnButton.interactable = true;
        for (int i = 0; i < _team.Lenght; i++)
        {
            PokemonInfoButton _pokemonInfoButton = pokemonInfoButtons[i];
            Pokemon _pokemon = _team[i];
            if (_pokemon != null)
                _pokemonInfoButton.UpdatePokemon(_pokemon);
        }
    }
    
    public void ActiveForceSwap()
    {
        DeactivateReturn();
    }
    public void DeactivateReturn()
    {
        returnButton.interactable = false;
    }

    public override void InitButton(int _index, PokemonInfoButton _pokemonSwapButton)
    {
        _pokemonSwapButton.onClick.AddListener(() => Swap(_index));
    }

    //TODO doit etre changer n a rien n a faire ici
    public void Swap(int _index)
    {
        PokemonTeam _pokemonTeam = GetOwnerMainUi<PlayerTrainer>().PokemonTeam;
        if (ForceSwap)
        {
            if (BattleManager.Instance.BattleField.ChangeFirstPokemon(_pokemonTeam[_index]))
                owner.Reset();
            else
                Debug.Log("Pokemon Already In Field or Fainted !!!");
        }
        else
        {
            
            if(BattleManager.Instance.SelectAction(new SwapPokemonAction(_pokemonTeam[_index])))
                owner.Reset();
            else
                Debug.Log("Pokemon Already In Field or Fainted !!!");
        }
    }
}
