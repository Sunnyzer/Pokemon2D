using UnityEngine;

public class GestionUIController : UIController<GestionUI>
{
    public override void UpdateController(float _deltaTime)
    {
        base.UpdateController(_deltaTime);
        if (Input.GetKeyDown(KeyCode.F))
        {
            UIManager.Instance.RemoveQueueSetPreviousUI();
        }
    }
}
