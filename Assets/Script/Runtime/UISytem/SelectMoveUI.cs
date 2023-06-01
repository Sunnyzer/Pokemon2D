using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMoveUI : MonoBehaviour
{
    [SerializeField] List<MoveButtonUI> moveButtonUIs = new List<MoveButtonUI>();
    [SerializeField] VerticalLayoutGroup verticalLayout = null;
    public void InitMove(Pokemon _pokemon)
    {
        for (int i = 0; i < _pokemon.Moves.Length; i++)
        {
            Move _move = _pokemon.Moves[i];
            if(_move != null)
            {
                moveButtonUIs[i].Init(_move);
            }
            else
            {
                moveButtonUIs[i].gameObject.SetActive(false);
            }
        }
    }
    public void ActivateUI()
    {
        gameObject.SetActive(true);
    }
    public void DeactivateUI()
    {
        gameObject.SetActive(false);
    }
}
