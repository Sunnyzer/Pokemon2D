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
    
    public void Init(Move _move)
    {
        UpdateMove(_move);
        button.onClick.RemoveListener(OnClick);
        button.onClick.AddListener(OnClick);
    }
    public void UpdateMove(Move _move)
    {
        move = _move;
        moveText.text = move.Name;
        movePPText.text = move.PP + "/" + move.PPMax;
    }
    public void OnClick()
    {
        if(move.CanUse)
        {
            move.UseMove();
            movePPText.text = move.PP + "/" + move.PPMax;
            BattleManager.Instance.PlayerTrainer.SelectAction(new AttackAction(BattleManager.Instance.PlayerTrainer.CurrentPokemonInCombat, move));
        }
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
        move = null;
    }
    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
