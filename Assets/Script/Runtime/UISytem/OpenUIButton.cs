using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenUIButton : MonoBehaviour
{
    [SerializeField] UI ui;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            UIManager.Instance.SetCurrentUIDisplay(ui, UIManager.Instance.CurrentUI.Owner);
        }
        );
    }
}
