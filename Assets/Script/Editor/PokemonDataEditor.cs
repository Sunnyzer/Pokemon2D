using Newtonsoft.Json;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PokemonDataSO))]
public class PokemonDataEditor : Editor
{
    PokemonDataSO moveDataSO;
    TextAsset file = null;
    int index = 0;
    private void OnEnable()
    {
        moveDataSO = (PokemonDataSO)target;
    }
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        file = (TextAsset)Resources.Load("pokedex");
        if (GUILayout.Button("Generate Pokedex"))
        {
            PokemonData[] allPokemons = JsonConvert.DeserializeObject<PokemonData[]>(file.text);
            moveDataSO.allPokemon = allPokemons;
        }
        index = EditorGUILayout.Popup(index, moveDataSO.allPokemon.Select(p=> p.name.french).ToArray());
        SerializedProperty _pokemonData = serializedObject.FindProperty("allPokemon").GetArrayElementAtIndex(index);
        EditorGUILayout.PropertyField(_pokemonData);
        serializedObject.ApplyModifiedProperties();
    }
}
