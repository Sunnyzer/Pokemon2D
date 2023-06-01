using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PokemonSwapButton))]
public class PokemonSwapButtonEditor : Editor
{
    PokemonSwapButton pokemonSwapButton;
    private void OnEnable()
    {
        pokemonSwapButton = (PokemonSwapButton)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.ApplyModifiedProperties();
    }
}
