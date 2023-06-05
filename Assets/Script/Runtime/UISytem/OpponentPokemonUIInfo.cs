using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpponentPokemonUIInfo : PokemonInfoUI
{
    private void Update()
    {
        UpdatePokemon(battleField.SecondPokemon);
    }
    public override void UpdateSprite(Pokemon _pokemon)
    {
        pokemonSprite.sprite = _pokemon.Data.completeSprite;
        pokemonSprite.SetNativeSize();
    }
}
