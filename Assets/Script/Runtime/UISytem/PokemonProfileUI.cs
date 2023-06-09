using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonProfileUI : SubUI
{
    [SerializeField] Transform allType;
    [SerializeField] TextMeshProUGUI pokemonNumero;
    [SerializeField] TextMeshProUGUI pokemonName;
    [SerializeField] TextMeshProUGUI itemName;

    [SerializeField] TextMeshProUGUI nameInfo;
    [SerializeField] TextMeshProUGUI pokemonLevel;
    [SerializeField] Image pokemonSprite;

    public override void OnActivate()
    {

    }

    public override void OnDeactivate()
    {

    }

    public void UpdatePokemon(Pokemon _pokemon)
    {
        Image _type1 = allType.GetChild(0).GetComponent<Image>();
        Image _type2 = allType.GetChild(1).GetComponent<Image>();

        _type1.sprite = BattleManager.Instance.TypeTable.GetSpriteType(_pokemon.Data.pkmTypes[0]);
        if (1 < _pokemon.Data.pkmTypes.Length)
        {
            _type2.gameObject.SetActive(true);
            _type2.sprite = BattleManager.Instance.TypeTable.GetSpriteType(_pokemon.Data.pkmTypes[1]);
        }
        else
            _type2.gameObject.SetActive(false);
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
