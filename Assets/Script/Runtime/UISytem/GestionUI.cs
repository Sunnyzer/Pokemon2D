using UnityEngine;

public class GestionUI : UI
{
    [SerializeField] SubUIManagement subMenuUIManagement;
    private void Start()
    {
        subMenuUIManagement.Init();
    }

}
