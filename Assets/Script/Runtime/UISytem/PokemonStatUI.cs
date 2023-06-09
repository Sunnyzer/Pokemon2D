using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct StatUI
{
    [SerializeField] public TextMeshProUGUI hpText;
    [SerializeField] public TextMeshProUGUI attackText;
    [SerializeField] public TextMeshProUGUI defenseText;
    [SerializeField] public TextMeshProUGUI spAttackText;
    [SerializeField] public TextMeshProUGUI spDefenseText;
    [SerializeField] public TextMeshProUGUI speedText;
    public void UpdateStat(Pokemon _pokemon)
    {
        hpText.text = _pokemon.Hp.ToString() +  "/" + _pokemon.HpMax.ToString();
        attackText.text = _pokemon.CurrentStat.Attack.ToString();
        defenseText.text = _pokemon.CurrentStat.Defense.ToString();
        spAttackText.text = _pokemon.CurrentStat.SpAttack.ToString();
        spDefenseText.text = _pokemon.CurrentStat.SpDefense.ToString();
        speedText.text = _pokemon.CurrentStat.Speed.ToString();
    }
}

[Serializable]
public struct XpUI
{
    [SerializeField] public TextMeshProUGUI nextLevelText;
    [SerializeField] public TextMeshProUGUI expPointText;
    public void UpdateXP(Pokemon _pokemon)
    {
        nextLevelText.text = _pokemon.XpMax.ToString();
        expPointText.text = _pokemon.Xp.ToString();
    }
}

public class PokemonStatUI : SubUI
{
    [SerializeField] StatUI statUI;
    [SerializeField] XpUI xpUI;
    [SerializeField] TextMeshProUGUI abilityName;
    [SerializeField] TextMeshProUGUI descriptionAbility;
    [SerializeField] TextMeshProUGUI pokemonName;
    [SerializeField] TextMeshProUGUI pokemonLevel;
    [SerializeField] Image pokemonSprite;

    public void UpdatePokemon(Pokemon _pokemon)
    {
        _pokemon.ApplyStat();
        pokemonName.text = _pokemon.Name;
        abilityName.text = "Ability";
        descriptionAbility.text = "Aucun description";
        pokemonLevel.text = "Lv "+ _pokemon.Level.ToString();
        statUI.UpdateStat(_pokemon);
        xpUI.UpdateXP(_pokemon);

        pokemonSprite.sprite = _pokemon.Data.completeSprite;
        pokemonSprite.SetNativeSize();
        RectTransform m_RectTransform = pokemonSprite.GetComponent<RectTransform>();
        m_RectTransform.sizeDelta /= 1.5f;
        pokemonSprite.SetAllDirty();
    }
    public override void OnActivate()
    {

    }

    public override void OnDeactivate()
    {

    }
}
