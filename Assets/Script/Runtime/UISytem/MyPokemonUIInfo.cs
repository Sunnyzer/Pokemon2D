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
        currentPokemon = _pokemon;
        UpdateLevel(currentPokemon.Level);
        UpdateNamePokemon(currentPokemon.Name);
        pokemonSprite.sprite = currentPokemon.Data.backSprite;
        pokemonSprite.rectTransform.sizeDelta = currentPokemon.Data.backSprite.rect.size * 3;
        UpdateXpBar();
        UpdateHpBar();
        UpdateHpText(currentPokemon.Hp, currentPokemon.HpMax);
        currentPokemon.OnHpChange += (pokemon) =>
        {
            UpdateHpBar();
            UpdateHpText(pokemon.Hp, pokemon.HpMax);
        };
    }
    public void Desinit()
    {

    }
    public void UpdateLevel(int _newLevel)
    {
        levelText.text = _newLevel.ToString();
    }
    public void UpdateNamePokemon(string _name)
    {
        namePkmText.text = _name;
    }
    public void UpdateXpBar()
    {
        expBar.value = currentPokemon.Xp / currentPokemon.XpMax;
    }
    public void UpdateHpBar()
    {
        hpBar.value = currentPokemon.Hp / currentPokemon.HpMax;
    }
    public void UpdateHpText(int _hp, int _hpMax)
    {
        hpText.text = _hp + "/" + _hpMax;
    }
}