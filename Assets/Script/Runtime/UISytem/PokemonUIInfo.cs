using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonUIInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText = null;
    [SerializeField] TextMeshProUGUI nameText = null;
    [SerializeField] Slider hpBar = null;
    [SerializeField] Slider expBar = null;
    [SerializeField] Image pokemonSprite = null;
    Pokemon currentPokemon = null;
    public void InitInfo(Pokemon _pokemon)
    {
        currentPokemon = _pokemon;
        levelText.text = currentPokemon.Level.ToString();
        nameText.text = currentPokemon.Name;
        if(expBar)
            pokemonSprite.sprite = currentPokemon.Data.backSprite;
        else
            pokemonSprite.sprite = currentPokemon.Data.completeSprite;
    }
}