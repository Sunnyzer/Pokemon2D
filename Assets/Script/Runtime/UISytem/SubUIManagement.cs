using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SubUIManagement
{
    public const string SubUISName = "SubUIS";
    protected List<SubUI> subUIManagements = new List<SubUI>();
    [SerializeField] protected int indexActivation = 0;
    [SerializeField] protected bool activeOnStart = true;
    [SerializeField] protected SubUI currentSubUI = null;
    [SerializeField] protected Transform parentSubUI;
    protected SubUI previousSubUI = null;
    protected UI owner = null;
    public UI Owner => owner;
    public SubUI CurrentSubUIDisplay => currentSubUI;
    public SubUI PreviousSubUIDisplay => previousSubUI;
    
    public void Init(UI _owner)
    {
        owner = _owner;
        int _childCount = _owner.transform.childCount;
        for (int i = 0; i < _childCount; i++)
        {
            Transform _child = _owner.transform.GetChild(i);
            if(_child.name.Contains(SubUISName))
            {
                parentSubUI = _child;
                break;
            }
        }
        if(!parentSubUI)
        {
            //Debug.LogError("Parent not find");
            return;
        }
        _childCount = parentSubUI.childCount;
        for (int i = 0; i < _childCount; i++)
        {
            Transform _child = parentSubUI.transform.GetChild(i);
            SubUI _subUI = _child.GetComponent<SubUI>();
            if(!_subUI)
            {
                Debug.LogWarning("Child" + _child.name + " doesn t have SubUI class couldn 't add to list");
                continue;
            }
            _subUI.Init(this);
            subUIManagements.Add(_subUI);
        }

        //for (int i = 0; i < subUIManagements.Count; i++)
        //{
        //    SubUI _subUI = subUIManagements[i];
        //    _subUI.OnActivation += OnActiveSubUI;
        //}
        if(activeOnStart)
            OnActiveSubUI(subUIManagements[indexActivation]);
    }
    private void OnActiveSubUI(SubUI _subUI)
    {
        for (int i = 0; i < subUIManagements.Count; i++)
            subUIManagements[i].DeactivateUI();

        previousSubUI = currentSubUI;
        currentSubUI = _subUI;
        currentSubUI.ActivateUI();
    }
    public void ActiveSubUi(int _index)
    {
        OnActiveSubUI(subUIManagements[_index]);
    }
    public void ActiveSubUi(SubUI _subUi)
    {
        OnActiveSubUI(_subUi);
    }
    public void ActivePreviousSubUI()
    {
        OnActiveSubUI(previousSubUI);
    }
    public void Reset()
    {
        OnActiveSubUI(subUIManagements[indexActivation]);
    }
}
