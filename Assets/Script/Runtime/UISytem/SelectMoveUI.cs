using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMoveUI : MonoBehaviour
{
    [SerializeField] List<MoveButtonUI> moveButtonUIs = new List<MoveButtonUI>();
    [SerializeField] VerticalLayoutGroup verticalLayout = null;
    public void InitMove(Pokemon _pokemon)
    {
        for (int i = 0; i < 4; i++)
        {
            if(i < _pokemon.Moves.Length && _pokemon.Moves[i] != null)
            {
                Move _move = _pokemon.Moves[i];
                moveButtonUIs[i].Init(_move);
            }
            else
            {
                Debug.Log("Desactivate Move " + i);
                moveButtonUIs[i].Deactivate();
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
