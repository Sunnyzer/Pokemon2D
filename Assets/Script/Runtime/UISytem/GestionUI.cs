using Unity.VisualScripting;
using UnityEngine;

public class GestionUI : UI
{
    [SerializeField] SubUIManagement subMenuUIManagement;

    public override void OnActivate()
    {

    }

    public override void OnDeactivate()
    {

    }

    protected override void Start()
    {
        base.Start();
        subMenuUIManagement.Init(this);
    }

}
