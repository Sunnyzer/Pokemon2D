using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance;
    public static T Instance => instance;
    protected virtual void Awake()
    {
        if(instance)
        {
            Destroy(instance);
            return;
        }
        instance = this as T;
    }
}
