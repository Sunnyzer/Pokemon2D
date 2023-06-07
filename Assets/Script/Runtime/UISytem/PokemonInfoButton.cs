using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonInfoButton : Button
{
    [SerializeField] TextMeshProUGUI pokemonName;
    [SerializeField] TextMeshProUGUI pokemonLevel;
    [SerializeField] TextMeshProUGUI pokemonHp;
    [SerializeField] Slider pokemonHpBar;
    [SerializeField] Image pokemonSprite;

    public void UpdatePokemon(Pokemon _pokemon)
    {
        pokemonHp.text = _pokemon.Hp + "/" + _pokemon.HpMax;
        pokemonName.text = _pokemon.Name;
        pokemonLevel.text = "N." + _pokemon.Level;
        pokemonHpBar.value = (float)_pokemon.Hp / (float)_pokemon.HpMax;
        pokemonSprite.sprite = _pokemon.Data.completeSprite;
    }
    public void Activate()
    {
        gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
