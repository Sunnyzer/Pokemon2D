using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyPokemonUIInfo : PokemonInfoUI
{
    [SerializeField] TextMeshProUGUI hpText = null;
    [SerializeField] Slider expBar = null;
    private void Update()
    {
        UpdatePokemon(battleField.FirstPokemon);
    }
    public override void UpdatePokemon(Pokemon _pokemon)
    {
        base.UpdatePokemon(_pokemon);
        UpdateXpBar(_pokemon);
    }
    public void UpdateXpBar(Pokemon _pokemon)
    {
        expBar.value = (float)_pokemon.Xp / (float)_pokemon.XpMax;
    }
    public override void UpdateHp(Pokemon _pokemon)
    {
        base.UpdateHp(_pokemon);
        hpText.text = _pokemon.Hp + "/" + _pokemon.HpMax;
    }
    public override void UpdateSprite(Pokemon _pokemon)
    {
        pokemonSprite.sprite = _pokemon.Data.backSprite;
        pokemonSprite.SetNativeSize();
    }
}