using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonSwapUI : SubUI
{
    [SerializeField] List<PokemonSwapButton> pokemonSwapButtons = new List<PokemonSwapButton>();
    [SerializeField] Button returnButton;
    [SerializeField] BattleMenuButtonUI menuButton = null;
    public BattleMenuButtonUI MenuButton => menuButton;
    
    private void Start()
    {
        Init();
    }
    public override void Activate()
    {
        base.Activate();
        UpdateUI(BattleManager.Instance.PlayerTrainer.PokemonTeam);
    }
    public void Init()
    {
        PokemonTeam _team = BattleManager.Instance.PlayerTrainer.PokemonTeam;
        for (int i = 0; i < _team.Lenght; i++)
        {
            PokemonSwapButton pokemonSwapButton = pokemonSwapButtons[i];
            Pokemon _pokemon = _team[i];
            if (_pokemon != null)
                pokemonSwapButton.Init(this, i);
            else
                pokemonSwapButton.Deactivate();
        }
    }
    public void DeactivateReturn()
    {
        returnButton.interactable = false;
    }
    public void UpdateUI(PokemonTeam _team)
    {
        returnButton.interactable = true;
        for (int i = 0; i < _team.Lenght; i++)
        {
            PokemonSwapButton pokemonSwapButton = pokemonSwapButtons[i];
            Pokemon _pokemon = _team[i];
            if (_pokemon != null)
                pokemonSwapButton.UpdatePokemon(_pokemon);
        }
    }
}
