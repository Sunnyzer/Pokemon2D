using UnityEngine;

public class Grass : TileSprite
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int _proba = Random.Range(0, 100);
        if (_proba < 1.25 / 187.5f * 100)
        {
            BattleManager.Instance.StartBattle();
            return;
        }
        if (_proba < 3.33f / 187.5f * 100)
        {
            BattleManager.Instance.StartBattle();
            return;
        }
        if (_proba < 6.75f / 187.5f * 100)
        {
            BattleManager.Instance.StartBattle();
            return;
        }
        if (_proba < 8.5f / 187.5f * 100)
        {
            BattleManager.Instance.StartBattle();
            return;
        }
        if(_proba < 10/187.5f * 100)
        {
            BattleManager.Instance.StartBattle();
            return;
        }
    }
}
