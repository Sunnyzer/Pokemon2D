using UnityEngine;

public abstract class UIController<T> : Controller where T : UI
{
    protected T ui = null;
    public void Awake()
    {
        ui = GetComponent<T>();
        ui.OnUIActivate += () => ControllerManager.Instance.TakeControl(this);
    }
    
    public override void UpdateController(float _deltaTime)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            UIManager.Instance.ClearUIQueue();
    }
}
