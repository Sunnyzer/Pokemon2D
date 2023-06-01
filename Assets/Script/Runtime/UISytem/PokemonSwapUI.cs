using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonSwapUI : MonoBehaviour
{
    [SerializeField] List<PokemonSwapButton> pokemonSwapButtons = new List<PokemonSwapButton>();
    [SerializeField] Button backButton = null;
    public bool ForceSwap => !backButton.interactable;

    public void Activate(bool _withoutReturn = false)
    {
        gameObject.SetActive(true);
        if(_withoutReturn)
            backButton.interactable = false;
        else
            backButton.interactable = true;
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
    public void DisplayUI(bool _withoutReturn = false)
    {
        UpdateUI(BattleManager.Instance.PlayerTrainer.PokemonTeam);
        Activate(_withoutReturn);
    }
    public void Init()
    {
        backButton.onClick.AddListener(Back);
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
    public void UpdateUI(PokemonTeam _team)
    {
        for (int i = 0; i < _team.Lenght; i++)
        {
            PokemonSwapButton pokemonSwapButton = pokemonSwapButtons[i];
            Pokemon _pokemon = _team[i];
            if (_pokemon != null)
                pokemonSwapButton.UpdatePokemon(_pokemon);
        }
    }
    public void Back()
    {
        Deactivate();
    }
}
