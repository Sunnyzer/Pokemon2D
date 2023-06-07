using System.Collections.Generic;
using UnityEngine;

public abstract class TeamInfoUI : SubUI
{
    [SerializeField] protected List<PokemonInfoButton> pokemonInfoButtons = new List<PokemonInfoButton>();
    public override void Init(SubUIManagement _owner)
    {
        base.Init(_owner);
        UpdateUITeam();
    }
    public void UpdateUITeam()
    {
        PlayerTrainer _playerTrainer = GetOwnerMainUi<PlayerTrainer>(); 
        PokemonTeam _team = _playerTrainer.PokemonTeam;
        int _lenghtTeam = _team.Lenght; 
        for (int i = 0; i < _lenghtTeam; i++)
        {
            PokemonInfoButton _pokemonInfoButton = pokemonInfoButtons[i];
            Pokemon _pokemon = _team[i];
            if (_pokemon != null)
                InitButton(i, _pokemonInfoButton);
            else
                _pokemonInfoButton.Deactivate();
        }
    }
    public abstract void InitButton(int _index, PokemonInfoButton _pokemonSwapButton);
}