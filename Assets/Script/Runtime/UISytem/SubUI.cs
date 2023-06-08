using System;
using UnityEngine;

public abstract class SubUI : MonoBehaviour
{
    public event Action<SubUI> OnActivation = null;
    public event Action<SubUI> OnDeactivation = null;
    protected SubUIManagement owner = null;
    public SubUIManagement Owner => owner;

    public T GetOwnerMainUi<T>() where T : MonoBehaviour
    {
        return owner.Owner.Owner.GetComponent<T>();
    }
    public bool TryGetOwnerMainUI<T>(out T _owner) where T : MonoBehaviour
    {
        _owner = null;
        if (owner.Owner.Owner == null) return false;
        _owner = GetOwnerMainUi<T>();
        return true;
    }
    public virtual void Init(SubUIManagement _owner)
    {
        owner = _owner;
    }

    public void ActivateUI()
    {
        gameObject.SetActive(true);
        OnActivate();
        OnActivation?.Invoke(this);
    }
    public void DeactivateUI()
    {
        gameObject.SetActive(false);
        OnDeactivate();
        OnDeactivation?.Invoke(this);
    }

    public abstract void OnActivate();
    public abstract void OnDeactivate();
}
