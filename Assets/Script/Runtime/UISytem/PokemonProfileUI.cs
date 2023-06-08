using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonProfileUI : MonoBehaviour
{
    [SerializeField] Transform allType;
    [SerializeField] TextMeshProUGUI pokemonNumero;
    [SerializeField] TextMeshProUGUI pokemonName;
    [SerializeField] TextMeshProUGUI itemName;

    [SerializeField] TextMeshProUGUI nameInfo;
    [SerializeField] TextMeshProUGUI pokemonLevel;
    [SerializeField] Image pokemonSprite;

    public void UpdatePokemon(Pokemon _pokemon)
    {
        //allType
        _pokemon.ApplyStat();
        pokemonNumero.text = _pokemon.Data.id.ToString();

        pokemonName.text = _pokemon.Name;
        nameInfo.text = _pokemon.Name;
        pokemonLevel.text = "Lv " + _pokemon.Level.ToString();
        itemName.text = "Item";

        pokemonSprite.sprite = _pokemon.Data.completeSprite;
        pokemonSprite.SetNativeSize();
        RectTransform m_RectTransform = pokemonSprite.GetComponent<RectTransform>();
        m_RectTransform.sizeDelta /= 1.5f;
        pokemonSprite.SetAllDirty();
    }
}
