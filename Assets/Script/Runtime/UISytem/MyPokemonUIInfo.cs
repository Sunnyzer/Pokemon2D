using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyPokemonUIInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText = null;
    [SerializeField] TextMeshProUGUI namePkmText = null;
    [SerializeField] TextMeshProUGUI hpText = null;
    [SerializeField] Slider hpBar = null;
    [SerializeField] Slider expBar = null;
    [SerializeField] Image pokemonSprite = null;

    Pokemon currentPokemon = null;
    public void InitInfo(Pokemon _pokemon)
    {
        if(currentPokemon != null)
            currentPokemon.OnHpChange -= OnHpChange;
        currentPokemon = _pokemon;
        UpdateLevel(currentPokemon.Level);
        UpdateNamePokemon(currentPokemon.Name);
        pokemonSprite.sprite = currentPokemon.Data.backSprite;
        pokemonSprite.SetNativeSize();
        //pokemonSprite.rectTransform.sizeDelta = currentPokemon.Data.backSprite.rect.size * 3;
        UpdateXpBar(currentPokemon);
        UpdateHpBar(currentPokemon);
        UpdateHpText(currentPokemon.Hp, currentPokemon.HpMax);
        _pokemon.OnHpChange += OnHpChange;
        //currentPokemon.OnXpChange += UpdateXpBar;
    }
    public void OnHpChange(Pokemon _pokemon)
    {
        UpdateHpBar(_pokemon);
        UpdateHpText(_pokemon.Hp, _pokemon.HpMax);
    }
    public void UpdateLevel(int _newLevel)
    {
        levelText.text = _newLevel.ToString();
    }
    public void UpdateNamePokemon(string _name)
    {
        namePkmText.text = _name;
    }
    public void UpdateXpBar(Pokemon _pokemon)
    {
        expBar.value = (float)_pokemon.Xp / (float)_pokemon.XpMax;
    }
    public void UpdateHpBar(Pokemon _pokemon)
    {
        hpBar.value = (float)_pokemon.Hp / (float)_pokemon.HpMax;
    }
    public void UpdateHpText(int _hp, int _hpMax)
    {
        hpText.text = _hp + "/" + _hpMax;
    }
}