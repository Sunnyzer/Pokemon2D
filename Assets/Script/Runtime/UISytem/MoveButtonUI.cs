using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveButtonUI : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI moveText;
    [SerializeField] TextMeshProUGUI movePPText;
    Move move;
    public void Init(Move _move)
    {
        move = _move;
        moveText.text = move.Name;
        movePPText.text = move.PP + "/" + move.PPMax;
        button.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        if(move.CanUse)
        {
            Debug.Log(move.Name);
            move.UseMove();
            movePPText.text = move.PP + "/" + move.PPMax;
            BattleManager.Instance.FinishTurn(move);
        }
    }
}
