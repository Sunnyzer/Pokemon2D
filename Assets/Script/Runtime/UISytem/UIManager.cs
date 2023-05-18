using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public event Action<UI> OnUIChange = null;
    [SerializeField] UI currentUI;
    [SerializeField] List<UI> uiQueue = new List<UI>();
    #if UNITY_EDITOR
    public int indexUI = 0;
    #endif
    public void SetCurrentUIDisplay(UI _ui)
    {
        uiQueue.Add(currentUI);
        currentUI?.DeactivateUI();
        currentUI = _ui;
        currentUI.ActivateUI();
        OnUIChange?.Invoke(currentUI);
    }
    public void RemoveQueueSetPreviousUI()
    {
        uiQueue.Remove(currentUI);
        currentUI?.DeactivateUI();
        if (uiQueue.Count > 0)
        {
            currentUI = uiQueue[uiQueue.Count - 1];
            ControllerManager.Instance.TakeControl(null);
            currentUI.ActivateUI();
            if (!ControllerManager.Instance.CurrentController)
                ControllerManager.Instance.TakeControlOnMainController();
            OnUIChange?.Invoke(currentUI);
            return;
        }
        ControllerManager.Instance.TakeControlOnMainController();
    }
}
