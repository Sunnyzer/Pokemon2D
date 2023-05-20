using System;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : Singleton<ControllerManager>
{
    public event Action<Controller> OnChangeController;
    [SerializeField] List<Controller> controllers = new List<Controller>();
    [SerializeField] Controller currentController = null;
    [SerializeField] Controller mainController = null;
    public Controller CurrentController => currentController;
    #if UNITY_EDITOR
    public int indexController;
    #endif
    private void Update()
    {
        currentController?.UpdateController(Time.deltaTime);
    }
    public void TakeControl(Controller _currentController)
    {
        currentController?.RemoveControl();
        //Debug.Log("LoseControl of" + currentController?.name +"-> TakeControl of " + _currentController?.name);
        currentController = _currentController;
        currentController?.AddControl();
        OnChangeController?.Invoke(currentController);
    }
    public void AddController(Controller _controller)
    {
        controllers.Add(_controller);
    }
    public void RemoveController(Controller _controller)
    {
        controllers.Remove(_controller);
    }
    public void TakeControlOnMainController()
    {
        TakeControl(mainController);
    }
}
