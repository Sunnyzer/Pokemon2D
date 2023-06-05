using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubUI : MonoBehaviour
{
    public event Action<SubUI> OnActivation = null;
    public event Action<SubUI> OnDeactivate = null;

    public virtual void Activate()
    {
        gameObject.SetActive(true);
        OnActivation?.Invoke(this);
    }
    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
        OnDeactivate?.Invoke(this);
    }
}
