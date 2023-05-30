using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpponentPokemonUIInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText = null;
    [SerializeField] TextMeshProUGUI namePkmText = null;
    [SerializeField] Slider hpBar = null;
    [SerializeField] Image pokemonSprite = null;
    Pokemon currentPokemon = null;

    public void InitInfo(Pokemon _pokemon)
    {
        currentPokemon = _pokemon;
        levelText.text = currentPokemon.Level.ToString();
        namePkmText.text = currentPokemon.Name;
        pokemonSprite.sprite = currentPokemon.Data.completeSprite;
        pokemonSprite.rectTransform.sizeDelta = currentPokemon.Data.completeSprite.rect.size * 3.75f;
        hpBar.value = currentPokemon.Hp / currentPokemon.HpMax;
        currentPokemon.OnHpChange += (pokemon) =>
        {
            UpdateHpBar();
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
    public void UpdateHpBar()
    {
        hpBar.value = currentPokemon.Hp / currentPokemon.HpMax;
    }
}
