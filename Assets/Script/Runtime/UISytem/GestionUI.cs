using Unity.VisualScripting;
using UnityEngine;

public class GestionUI : UI
{
    [SerializeField] SubUIManagement subMenuUIManagement;

    public override void Activate()
    {

    }

    public override void Deactivate()
    {

    }

    protected override void Start()
    {
        base.Start();
        subMenuUIManagement.Init(this);
    }

}
