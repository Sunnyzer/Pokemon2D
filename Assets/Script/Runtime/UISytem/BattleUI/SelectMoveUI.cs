using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMoveUI : SubUI
{
    [SerializeField] List<MoveButtonUI> moveButtonUIs = new List<MoveButtonUI>();
    [SerializeField] Button returnButton = null;
    public override void Init(SubUIManagement _owner)
    {
        base.Init(_owner);
        returnButton.onClick.AddListener(owner.ActivePreviousSubUI);
    }
    public override void OnActivate()
    {
        UpdateUI(BattleManager.Instance.BattleField.FirstPokemon);
    }

    public override void OnDeactivate()
    {
        
    }

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
}
