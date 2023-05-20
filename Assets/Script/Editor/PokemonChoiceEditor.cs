using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(PokemonChoice))]
public class PokemonChoiceEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        base.OnGUI(position, property, label);
        PokemonDataSO _pokemonDataSO = (PokemonDataSO)Resources.Load("PokemonData");
        SerializedProperty _indexPokemon = property.FindPropertyRelative("indexPokemon");
        _indexPokemon.intValue = EditorGUI.Popup(position, _indexPokemon.intValue, _pokemonDataSO.allPokemon.Select(pokemon => pokemon.name.french).ToArray());
    }
}
