using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SubUIManagement
{
    [SerializeField] List<SubUI> subUIManagements = new List<SubUI>();
    [SerializeField] int indexActivation = 0;
    [SerializeField] SubUI currentSubUI = null;

    public void Init()
    {
        for (int i = 0; i < subUIManagements.Count; i++)
        {
            SubUI _subUI = subUIManagements[i];
            _subUI.OnActivation += OnActiveSubUI;
        }
        OnActiveSubUI(subUIManagements[indexActivation]);

    }
    private void OnActiveSubUI(SubUI _subUI)
    {
        for (int i = 0; i < subUIManagements.Count; i++)
            subUIManagements[i].Deactivate();

        currentSubUI = _subUI;
        currentSubUI.gameObject.SetActive(true);
    }
    public void Reset()
    {
        OnActiveSubUI(subUIManagements[indexActivation]);
    }
}
