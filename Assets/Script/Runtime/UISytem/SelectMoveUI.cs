using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMoveUI : MonoBehaviour
{
    [SerializeField] VerticalLayoutGroup verticalLayout = null;
    [SerializeField] MoveButtonUI moveButton = null;

    public void InitMove(Pokemon _pokemon)
    {
        for (int i = 0; i < _pokemon.Moves.Length; i++)
        {
            MoveButtonUI _moveButton = Instantiate(moveButton, verticalLayout.transform);
            _moveButton.Init(_pokemon.Moves[i]);
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
