using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonMenuUIController : UIController<PokemonMenuUI>
{
    public override void UpdateController(float _deltaTime)
    {
        base.UpdateController(_deltaTime);
        if (Input.GetKeyDown(KeyCode.F))
            UIManager.Instance.RemoveQueueSetPreviousUI(true);

        if (Input.GetKeyDown(KeyCode.D))
            ui.NextSubUI();

        if (Input.GetKeyDown(KeyCode.A))
            ui.PreviousSubUI();

        if (Input.GetKeyDown(KeyCode.W))
            ui.SelectPreviousPokemon();

        if (Input.GetKeyDown(KeyCode.S))
            ui.SelectNextPokemon();
    }
}
