using System;
using UnityEngine;

public abstract class UI : MonoBehaviour
{
    public event Action OnUIActivate = null;
    public event Action OnUIDeactivate = null;
    protected SubUIManagement subUIManagement = new SubUIManagement();
    protected MonoBehaviour owner = null;
    //system de selection with selectableObject.Interact List<>
    public MonoBehaviour Owner => owner;
    public SubUI CurrentSubUIDisplay => subUIManagement.CurrentSubUIDisplay;

    protected virtual void Start()
    {
        subUIManagement.Init(this);
    }
    //automaticly call in UIManager never call by yourself use Activate() instead
    public void ActivateUI(MonoBehaviour _owner = null)
    {
        owner = _owner;
        gameObject.SetActive(true);
        Activate();
        OnUIActivate?.Invoke();
    }
    //automaticly call in UIManager never call by yourself use Deactivate() instead
    public void DeactivateUI()
    {
        gameObject.SetActive(false);
        Deactivate();
        OnUIDeactivate?.Invoke();
    }

    public abstract void Activate();
    public abstract void Deactivate();
}
