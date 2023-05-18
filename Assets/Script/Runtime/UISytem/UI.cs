using System;
using UnityEngine;

public abstract class UI : MonoBehaviour
{
    public event Action OnUIActivate = null;
    public event Action OnUIDeactivate = null;
    public virtual void ActivateUI()
    {
        gameObject.SetActive(true);
        OnUIActivate?.Invoke();
    }
    public virtual void DeactivateUI()
    {
        gameObject.SetActive(false);
        OnUIDeactivate?.Invoke();
    }
}
