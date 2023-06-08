using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class PokemonSwapUI : TeamUI
{
    [SerializeField] Button returnButton;

    public bool ForceSwap => returnButton.interactable == false;
    
    public override void Init(SubUIManagement _owner)
    {
        base.Init(_owner);
        returnButton.onClick.AddListener(owner.ActivePreviousSubUI);
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
        if (ForceSwap)
        {
            if (BattleManager.Instance.BattleField.ChangeFirstPokemon(GetOwnerMainUi<PlayerTrainer>().PokemonTeam[_index]))
                owner.ActivePreviousSubUI();
            else
                Debug.Log("Pokemon Fainted !!!");
        }
        else
        {
            if (!BattleManager.Instance.PlayerTrainer.PokemonTeam[_index].Fainted)
            {
                BattleManager.Instance.SelectAction(new SwapPokemonAction(GetOwnerMainUi<PlayerTrainer>().PokemonTeam[_index]));
                owner.ActivePreviousSubUI();
            }
            else
                Debug.Log("Pokemon Fainted !!!");
        }
    }
}
