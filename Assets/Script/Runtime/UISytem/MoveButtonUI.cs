using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveButtonUI : Button
{
    [SerializeField] TextMeshProUGUI moveText;
    [SerializeField] TextMeshProUGUI movePPText;

    [SerializeField] Move move;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        SelectMove();
    }
    public void SelectMove()
    {
        if (!move.CanUse) return;
        move.UseMove();
        movePPText.text = move.PP + "/" + move.PPMax;
        BattleManager.Instance.SelectAction(new AttackAction(BattleManager.Instance.BattleField.FirstPokemon, move));
    }
    public void UpdateMove(Move _move)
    {
        Activate();
        move = _move;
        moveText.text = move.Name;
        movePPText.text = move.PP + "/" + move.PPMax;
    }
    public void Activate()
    {
        gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
        move = null;
    }
}
