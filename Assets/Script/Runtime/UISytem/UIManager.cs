using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public event Action<UI> OnUIChange = null;
    [SerializeField] UI currentUI;
    [SerializeField] List<UI> uiQueue = new List<UI>();
    public UI CurrentUI => currentUI;
    #if UNITY_EDITOR
    public int indexUI = 0;
    #endif
    private void ChangeUI(UI _ui)
    {
        currentUI?.DeactivateUI();
        currentUI = _ui;
        currentUI?.ActivateUI();
        OnUIChange?.Invoke(currentUI);
    }
    public bool SetCurrentUIDisplay(UI _ui)
    {
        if (_ui == currentUI) return false;
        if(currentUI)
            uiQueue.Add(currentUI);
        ChangeUI(_ui);
        return true;
    }
    public void ClearUIQueue()
    {
        ChangeUI(uiQueue[0]);
        uiQueue.Clear();
    }
    public bool RemoveQueueSetPreviousUI()
    {
        if (uiQueue.Count <= 0) return false;
        currentUI?.DeactivateUI();
        if (uiQueue.Count > 0)
        {
            currentUI = uiQueue[uiQueue.Count - 1];
            uiQueue.RemoveAt(uiQueue.Count - 1);
            ControllerManager.Instance.TakeControl(null);
            currentUI.ActivateUI();
            if (!ControllerManager.Instance.CurrentController)
                ControllerManager.Instance.TakeControlOnMainController();
            OnUIChange?.Invoke(currentUI);
            return true;
        }
        ControllerManager.Instance.TakeControlOnMainController();
        return true;
    }
}
