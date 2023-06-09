using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PokemonMenuUI : UI
{
    [SerializeField] TeamInfoUI teamInfoUI;

    public void SelectNextPokemon()
    {
        teamInfoUI.NextPokemonStat();
    }
    public void SelectPreviousPokemon()
    {
        teamInfoUI.PreviousPokemonStat();
    }
        
    public override void OnActivate()
    {
        teamInfoUI.Init(subUIManagement);
        teamInfoUI.ActivateUI();
    }

    public override void OnDeactivate()
    {
        subUIManagement.DeactivateAllSubUI();
    }
}