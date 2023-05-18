using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIController<T> : Controller where T : UI
{
    T ui = null;
    public void Awake()
    {
        ui = GetComponent<T>();
        ui.OnUIActivate += () => ControllerManager.Instance.TakeControl(this);
        ui.OnUIDeactivate += () => Debug.Log("Deactivate " + name);
    }
    
    public override abstract void UpdateController(float _deltaTime);
}
