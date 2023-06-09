using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    public int Lenght => subUIManagements.Count;

    public void Init(UI _owner)
    {
        owner = _owner;
        int _childCount = _owner.transform.childCount;
        for (int i = 0; i < _childCount; i++)
        {
            Transform _child = _owner.transform.GetChild(i);
            if (_child.name.Contains(SubUISName))
            {
                parentSubUI = _child;
                break;
            }
        }
        if (!parentSubUI)
        {
            //Debug.LogError("Parent not find");
            return;
        }
        _childCount = parentSubUI.childCount;
        for (int i = 0; i < _childCount; i++)
        {
            Transform _child = parentSubUI.transform.GetChild(i);
            SubUI _subUI = _child.GetComponent<SubUI>();
            if (!_subUI)
            {
                Debug.LogWarning("Child" + _child.name + " doesn t have SubUI class couldn 't add to list");
                continue;
            }
            _subUI.Init(this);
            subUIManagements.Add(_subUI);
        }

        if (activeOnStart)
            OnActiveSubUI(subUIManagements[indexActivation]);
    }
    public bool ActiveNextSubUI()
    {
        int _index = GetCurrentIndex();
        if (_index == -1) return false;
        int _nextIndex = _index + 1;
        if (_nextIndex >= subUIManagements.Count)
            _nextIndex = 0;
        if (_nextIndex >= subUIManagements.Count) return false;
        ActiveSubUI(subUIManagements[_nextIndex]);
        return true;
    }
    public int GetCurrentIndex()
    {
        for (int i = 0; i < subUIManagements.Count; i++)
            if (subUIManagements[i] == currentSubUI)
                return i;
        return -1;
    }
    private void OnActiveSubUI(SubUI _subUI)
    {
        for (int i = 0; i < subUIManagements.Count; i++)
            subUIManagements[i].DeactivateUI();

        previousSubUI = currentSubUI;
        currentSubUI = _subUI;
        currentSubUI.ActivateUI();
    }
    public void ActiveSubUI(int _index)
    {
        OnActiveSubUI(subUIManagements[_index]);
    }
    public void ActiveSubUI(SubUI _subUi)
    {
        OnActiveSubUI(_subUi);
    }
    public void ActivePreviousSubUIDisplay()
    {
        OnActiveSubUI(previousSubUI);
    }
    public bool ActivePreviousSubUI()
    {
        int _index = GetCurrentIndex();
        if (_index == -1) return false;
        int _previousIndex = _index - 1;
        if (_previousIndex < 0)
            _previousIndex = 0;
        if (_previousIndex >= subUIManagements.Count) return false;
        ActiveSubUI(subUIManagements[_previousIndex]);
        return true;
    }
    public void DeactivateAllSubUI()
    {
        currentSubUI = null;
        for (int i = 0; i < subUIManagements.Count; i++)
            subUIManagements[i].DeactivateUI();
    }
    public void Reset()
    {
        OnActiveSubUI(subUIManagements[indexActivation]);
    }
}
