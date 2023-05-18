using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    protected virtual void Start()
    {
        ControllerManager.Instance.AddController(this);
    }
    public virtual void AddControl() { }
    public virtual void RemoveControl() { }
    public abstract void UpdateController(float _deltaTime);
}
