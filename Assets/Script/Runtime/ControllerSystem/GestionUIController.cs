using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionUIController : UIController<GestionUI>
{
    [SerializeField] UI ui;
    public override void UpdateController(float _deltaTime)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            UIManager.Instance.RemoveQueueSetPreviousUI();
        }
        if (Input.GetKeyDown(KeyCode.Space))
            UIManager.Instance.SetCurrentUIDisplay(ui);
    }
}
