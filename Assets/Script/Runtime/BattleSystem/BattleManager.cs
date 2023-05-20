using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    [SerializeField] UI battleUI;
    public void StartBattle()
    {
        UIManager.Instance.SetCurrentUIDisplay(battleUI);
    }
}
