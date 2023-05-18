using UnityEngine;

public abstract class Player : Controller
{
    protected override void Start()
    {
        base.Start();
        PlayerManager.Instance.AddPlayer(this);
    }
}
