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
    [SerializeField] Move move;
    PlayerTrainer playerTrainer;
    public void Init(PlayerTrainer _player, Move _move)
    {
        move = _move;
        playerTrainer = _player;
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
            playerTrainer.SelectAction(new AttackAction(playerTrainer.GetPokemon(), move));
        }
    }
    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
