using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMoveUI : SubUI
{
    [SerializeField] List<MoveButtonUI> moveButtonUIs = new List<MoveButtonUI>();
    [SerializeField] VerticalLayoutGroup verticalLayout = null;
    public void UpdateUI(Pokemon _pokemon)
    {
        for (int i = 0; i < 4; i++)
        {
            if(i < _pokemon.Moves.Count && _pokemon.Moves[i] != null)
            {
                Move _move = _pokemon.Moves[i];
                moveButtonUIs[i].UpdateMove(_move);
            }
            else
            {
                moveButtonUIs[i].Deactivate();
            }
        }
    }
    public override void Activate()
    {
        base.Activate();
        UpdateUI(BattleManager.Instance.BattleField.FirstPokemon);
    }
}
