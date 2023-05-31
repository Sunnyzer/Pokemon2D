using Newtonsoft.Json;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class TestText
{
    [SerializeField] public string[] namePokemon;
}

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
        file = (TextAsset)Resources.Load("pokedex");
        if (GUILayout.Button("Generate Pokedex"))
        {
            PokemonData[] _allPokemons = JsonUtility.FromJson<PokemonData[]>(file.text);
            for (int i = 0; i < _allPokemons.Length; i++)
                serializedObject.FindProperty("allPokemon").GetArrayElementAtIndex(i).managedReferenceValue = _allPokemons[i];
        }
        index = EditorGUILayout.Popup(index, moveDataSO.AllPokemon.Select(p=> p.name.french).ToArray());
        SerializedProperty _pokemonData = serializedObject.FindProperty("allPokemon").GetArrayElementAtIndex(index);
        EditorGUILayout.PropertyField(_pokemonData);
        serializedObject.ApplyModifiedProperties();
        if (GUILayout.Button("Test"))
        {
            FindObjectOfType<MonoBehaviour>().StartCoroutine(GetPokemonData());
        }
    }
    public IEnumerator GetPokemonData()
    {
        file = (TextAsset)Resources.Load("en");
        string _t = "{ allPokemon : [ ";
        TestText _test = JsonUtility.FromJson<TestText>(file.text);
        for (int i = 0; i < 2; i++)
        {
            UnityWebRequest _webRequest = UnityWebRequest.Get("https://pokeapi.co/api/v2/pokemon/" + _test.namePokemon[i].ToLower());
            yield return _webRequest.SendWebRequest();
            if (_webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResult = _webRequest.downloadHandler.text;
                _t += jsonResult + ",";
            }
            else
            {
                Debug.Log("Erreur lors de la requête : " + _webRequest.error);
            }
        }
        _t += "] }";
        TextAsset text = new TextAsset(_t);
        AssetDatabase.CreateAsset(text, "Assets/Resources/MyPokemonFile.asset");
    }
}

public class PokemonJson
{
    /* abilities 
     * base_experience
     * species
     * id
     * moves
     * stats [6] base_stat
     * types
     */
}
