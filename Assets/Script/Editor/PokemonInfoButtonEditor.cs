using UnityEditor;

[CustomEditor(typeof(PokemonInfoButton))]
public class PokemonInfoButtonEditor : Editor
{
    PokemonInfoButton pokemonSwapButton;
    private void OnEnable()
    {
        pokemonSwapButton = (PokemonInfoButton)target;
    }
}
