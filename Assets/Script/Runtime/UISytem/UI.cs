using System;
using UnityEngine;

public abstract class UI : MonoBehaviour
{
    public event Action OnUIActivate = null;
    public event Action OnUIDeactivate = null;
    [SerializeField] protected SubUIManagement subUIManagement = new SubUIManagement();
    protected MonoBehaviour owner = null;
    //system de selection with selectableObject.Interact List<>
    public MonoBehaviour Owner => owner;
    public SubUI CurrentSubUIDisplay => subUIManagement.CurrentSubUIDisplay;

    protected virtual void Awake()
    {
        subUIManagement.Init(this);
    }
    /// <summary>
    /// automaticly call in UIManager never call by yourself use SetCurrentUIDisplay in UIManager.Instance instead
    /// </summary>
    /// <param name="_owner"></param>
    public void ActivateUI(MonoBehaviour _owner = null)
    {
        owner = _owner;
        gameObject.SetActive(true);
        OnActivate();
        OnUIActivate?.Invoke();
    }
    /// <summary>
    /// automaticly call in UIManager never call by yourself
    /// </summary>
    public void DeactivateUI()
    {
        gameObject.SetActive(false);
        OnDeactivate();
        OnUIDeactivate?.Invoke();
    }

    public void ClearSubUI()
    {
        subUIManagement.DeactivateAllSubUI();
    }
    public void NextSubUI()
    {
        subUIManagement.ActiveNextSubUI();
    }
    public void PreviousSubUI()
    {
        subUIManagement.ActivePreviousSubUI();
    }
    
    public abstract void OnActivate();
    public abstract void OnDeactivate();
}
