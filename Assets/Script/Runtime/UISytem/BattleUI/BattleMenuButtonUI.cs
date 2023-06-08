using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMenuButtonUI : SubUI
{
    [SerializeField] Button fightButton;
    [SerializeField] Button pokemonButton;
    [SerializeField] Button bagButton;
    [SerializeField] Button runButton;

    public override void Init(SubUIManagement _owner)
    {
        base.Init(_owner);
        fightButton.onClick.AddListener(() => { owner.ActiveSubUi(1); });
        pokemonButton.onClick.AddListener(() => { owner.ActiveSubUi(2); });
        bagButton.onClick.AddListener(() => { /*_owner.ActiveSubUi(3); */});
        runButton.onClick.AddListener(() => { });
    }
    public override void OnActivate()
    {
        
    }

    public override void OnDeactivate()
    {
        
    }
}
