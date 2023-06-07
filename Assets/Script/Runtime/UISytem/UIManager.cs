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
    private void ChangeUI(UI _ui, MonoBehaviour _owner = null)
    {
        currentUI?.DeactivateUI();
        currentUI = _ui;
        if (currentUI == null)
            ControllerManager.Instance.TakeControlOnMainController();
        currentUI?.ActivateUI(_owner);
        OnUIChange?.Invoke(currentUI);
    }
    public bool SetCurrentUIDisplay(UI _ui, MonoBehaviour _owner = null)
    {
        if (_ui == currentUI) return false;
        if(currentUI)
            uiQueue.Add(currentUI);
        ChangeUI(_ui, _owner);
        return true;
    }
    public void ClearUIQueue()
    {
        if (uiQueue.Count > 0)
        {
            ChangeUI(uiQueue[0]);
            uiQueue.Clear();
            return;
        }
        ChangeUI(null);
    }
    public bool RemoveQueueSetPreviousUI()
    {
        if (uiQueue.Count <= 0)
        {
            ChangeUI(null);
            return false;
        }

        currentUI?.DeactivateUI();
        if (uiQueue.Count > 0)
        {
            MonoBehaviour _owner = currentUI.Owner;
            currentUI = uiQueue[uiQueue.Count - 1];
            uiQueue.RemoveAt(uiQueue.Count - 1);
            ControllerManager.Instance.TakeControl(null);
            currentUI.ActivateUI(_owner);
            if (!ControllerManager.Instance.CurrentController)
                ControllerManager.Instance.TakeControlOnMainController();
            OnUIChange?.Invoke(currentUI);
            return true;
        }
        ControllerManager.Instance.TakeControlOnMainController();
        return true;
    }
}
