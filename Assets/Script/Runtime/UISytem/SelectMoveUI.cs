using UnityEngine;
using UnityEngine.UI;

public class SelectMoveUI : MonoBehaviour
{
    [SerializeField] VerticalLayoutGroup verticalLayout = null;
    [SerializeField] MoveButtonUI moveButton = null;

    public void InitMove(PlayerTrainer _player)
    {
        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);

        Pokemon _pokemon = _player.GetPokemon();
        for (int i = 0; i < _pokemon.Moves.Length; i++)
        {
            MoveButtonUI _moveButton = Instantiate(moveButton, verticalLayout.transform);
            _moveButton.Init(_player, _pokemon.Moves[i]);
        }
    }
    public void ActivateUI()
    {
        gameObject.SetActive(true);
    }
    public void DeactivateUI()
    {
        gameObject.SetActive(false);
    }
}
