using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class PokemonInfoUI : MonoBehaviour
{
    protected BattleField battleField;
    [SerializeField] protected TextMeshProUGUI levelText = null;
    [SerializeField] protected TextMeshProUGUI namePkmText = null;
    [SerializeField] protected Slider hpBar = null;
    [SerializeField] protected Image pokemonSprite = null;
    [SerializeField] protected Image status = null;

    public void SetBattleField(BattleField _battleField)
    {
        battleField = _battleField;
    }
    public virtual void UpdatePokemon(Pokemon _pokemon)
    {
        UpdateHp(_pokemon);
        UpdateLevel(_pokemon);
        UpdateName(_pokemon);
        UpdateHp(_pokemon);
        UpdateStatus(_pokemon);
        UpdateSprite(_pokemon);
    }
    public virtual void UpdateLevel(Pokemon _pokemon)
    {
        levelText.text = "Lv " + _pokemon.Level;
    }
    public virtual void UpdateName(Pokemon _pokemon)
    {
        namePkmText.text = _pokemon.Name;
    }
    public virtual void UpdateHp(Pokemon _pokemon)
    {
        hpBar.value = (float)_pokemon.Hp/_pokemon.HpMax;
    }
    public void UpdateStatus(Pokemon _pokemon)
    {
        status.sprite = StatusManager.Instance.GetSpriteByStatus(_pokemon.CurrentStatus);
        status.gameObject.SetActive(status.sprite);
    }
    public abstract void UpdateSprite(Pokemon _pokemon);
}
