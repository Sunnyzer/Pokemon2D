using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PokemonSwapButton : Button
{
    [SerializeField] TextMeshProUGUI pokemonName;
    [SerializeField] TextMeshProUGUI pokemonLevel;
    [SerializeField] TextMeshProUGUI pokemonHp;
    [SerializeField] Slider hpBar;
    [SerializeField] Image pokemonSprite;

    PokemonSwapUI owner;
    int index = 0;

    public void Init(PokemonSwapUI _owner, int _index)
    {
        owner = _owner;
        index = _index;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        Debug.Log(index);
        if(owner.ForceSwap)
        {
            Debug.Log("Force Swap");
            if(BattleManager.Instance.PlayerTrainer.Swap(index))
            {
                owner.Back();
            }
        }
        else
        {
            Debug.Log("Swap action");
            BattleManager.Instance.PlayerTrainer.SelectAction(new SwapPokemonAction(index));
            owner.Back();
        }
    }
    public void UpdatePokemon(Pokemon _pokemon)
    {
        Activate();
        pokemonHp.text = _pokemon.Hp + "/" + _pokemon.Data.stat.HP;
        pokemonName.text = _pokemon.Name;
        pokemonLevel.text = "N." + _pokemon.Level;
        hpBar.value = (float)_pokemon.Hp /(float)_pokemon.HpMax;
        pokemonSprite.sprite = _pokemon.Data.completeSprite;
    }
}
