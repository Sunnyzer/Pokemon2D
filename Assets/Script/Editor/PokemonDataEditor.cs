using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
//using Object = UnityEngine.Object;


[CustomEditor(typeof(PokemonDataSO))]
public class PokemonDataEditor : Editor
{
    PokemonDataSO pokemonDataSO;
    int index = 0;
    Sprite sprite = null;
    public string filePath => Application.dataPath + "/Graph/Sprite/AllPokemon/";
    List<Root> allPokemonRoot = new List<Root>();
    private void OnEnable()
    {
        pokemonDataSO = (PokemonDataSO)target;
    }
    public override void OnInspectorGUI()
    {
        TextAsset file = (TextAsset)Resources.Load("pokedex");
        if (GUILayout.Button("Generate Pokedex"))
        {
            pokemonDataSO.allPokemonData = JsonUtility.FromJson<AllPokemon>(file.text);
        }
        index = EditorGUILayout.Popup(index, pokemonDataSO.AllPokemon.Select(p=> p.name.french).ToArray());
        index = EditorGUILayout.IntField(index);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("allPokemonData"));
        if (GUILayout.Button("Test"))
        {
            FindObjectOfType<MonoBehaviour>().StartCoroutine(GetPokemonData());
        }
        
        if (0 >= allPokemonRoot.Count) return;
        EditorGUILayout.LabelField(allPokemonRoot[0].name);
        EditorGUILayout.LabelField(allPokemonRoot[0].sprites.front_default);
        EditorGUILayout.LabelField(allPokemonRoot[0].sprites.back_default);
    }
    public IEnumerator GetPokemonData()
    {
        pokemonDataSO.allPokemonData.allPokemon = null;
        pokemonDataSO.allPokemonData.allPokemon = new PokemonData[1010];
        for (int i = 1; i < 1010; i++)
        {
            Debug.Log(i);
            UnityWebRequest _webRequest = UnityWebRequest.Get("https://pokeapi.co/api/v2/pokemon/" + i);
            yield return _webRequest.SendWebRequest();
            if (_webRequest.result == UnityWebRequest.Result.Success)
            {
                Root _pokemon = JsonConvert.DeserializeObject<Root>(_webRequest.downloadHandler.text);
                Directory.CreateDirectory(filePath + _pokemon);
                string _urlBack = _pokemon.sprites.back_default;
                string _urlFront = _pokemon.sprites.front_default;
                allPokemonRoot.Add(_pokemon);
                //yield return LoadPokemonImage(_urlBack, _pokemon.name, "Back");
                //yield return LoadPokemonImage(_urlFront, _pokemon.name, "Front");
                pokemonDataSO.allPokemonData.allPokemon[i - 1] = new PokemonData();
                pokemonDataSO.allPokemonData.allPokemon[i - 1].id = _pokemon.id;
                pokemonDataSO.allPokemonData.allPokemon[i - 1].name.french = _pokemon.name;
                pokemonDataSO.allPokemonData.allPokemon[i - 1].pkmTypes = new PkmType[_pokemon.types.Count];
                for (int j = 0; j < _pokemon.types.Count; j++)
                {
                    if(Enum.TryParse(_pokemon.types[j].type.name, out PkmType _result))
                    {
                        pokemonDataSO.allPokemonData.allPokemon[i - 1].pkmTypes[j] = _result;
                    }
                }
                
                pokemonDataSO.allPokemonData.allPokemon[i - 1].moveChoices = new MoveByLevel[_pokemon.moves.Count];
                for (int j = 0; j < _pokemon.moves.Count; j++)
                {
                    pokemonDataSO.allPokemonData.allPokemon[i - 1].moveChoices[j] = new MoveByLevel(_pokemon.moves[j].version_group_details[0].level_learned_at, _pokemon.moves[j].move.name);
                }
                pokemonDataSO.allPokemonData.allPokemon[i - 1].stat = new Stat(_pokemon.stats[0].base_stat, _pokemon.stats[1].base_stat, _pokemon.stats[2].base_stat, _pokemon.stats[3].base_stat, _pokemon.stats[4].base_stat, _pokemon.stats[5].base_stat);
                yield return LoadSprite(_urlBack);
                pokemonDataSO.allPokemonData.allPokemon[i - 1].backSprite = sprite;
                yield return LoadSprite(_urlFront);
                pokemonDataSO.allPokemonData.allPokemon[i - 1].completeSprite = sprite;
            }
            else
            {
                Debug.Log("Erreur lors de la requête : " + _webRequest.error + " id :" + i);
            }
        }
        serializedObject.ApplyModifiedProperties();
        Debug.Log("Finish SpritePokemon");
    }
    public IEnumerator LoadSprite(string _url)
    {
        UnityWebRequest _www = UnityWebRequestTexture.GetTexture(_url);
        sprite = null;
        yield return _www.SendWebRequest();
        if (_www.result == UnityWebRequest.Result.Success)
        {
            Texture2D loadedTexture = DownloadHandlerTexture.GetContent(_www);
            sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
        }
    }
    public IEnumerator LoadPokemonImage(string _url, string _pokemon, string suffix = "")
    {
        UnityWebRequest _www = UnityWebRequestTexture.GetTexture(_url);
        yield return _www.SendWebRequest();
        if (_www.result == UnityWebRequest.Result.Success)
        {
            Texture2D loadedTexture = DownloadHandlerTexture.GetContent(_www);
            Sprite sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
            WriteImageOnDisk(sprite, _pokemon, suffix);
        }
    }
    private void WriteImageOnDisk(Sprite _sprite, string _imageName, string suffix)
    {
        byte[] textureBytes = _sprite.texture.EncodeToPNG();
        File.WriteAllBytes(filePath + _imageName + "/"+ _imageName + suffix + ".png", textureBytes);
        //Debug.Log("File Written On Disk!");
    }
}

public class Ability
{
    public Ability2 ability { get; set; }
    public bool is_hidden { get; set; }
    public int slot { get; set; }
}

public class Ability2
{
    public string name { get; set; }
    public string url { get; set; }
}

public class Animated
{
    public string back_default { get; set; }
    public string back_female { get; set; }
    public string back_shiny { get; set; }
    public string back_shiny_female { get; set; }
    public string front_default { get; set; }
    public string front_female { get; set; }
    public string front_shiny { get; set; }
    public string front_shiny_female { get; set; }
}

public class BlackWhite
{
    public Animated animated { get; set; }
    public string back_default { get; set; }
    public string back_female { get; set; }
    public string back_shiny { get; set; }
    public string back_shiny_female { get; set; }
    public string front_default { get; set; }
    public string front_female { get; set; }
    public string front_shiny { get; set; }
    public string front_shiny_female { get; set; }
}

public class Crystal
{
    public string back_default { get; set; }
    public string back_shiny { get; set; }
    public string back_shiny_transparent { get; set; }
    public string back_transparent { get; set; }
    public string front_default { get; set; }
    public string front_shiny { get; set; }
    public string front_shiny_transparent { get; set; }
    public string front_transparent { get; set; }
}

public class DiamondPearl
{
    public string back_default { get; set; }
    public string back_female { get; set; }
    public string back_shiny { get; set; }
    public string back_shiny_female { get; set; }
    public string front_default { get; set; }
    public string front_female { get; set; }
    public string front_shiny { get; set; }
    public string front_shiny_female { get; set; }
}

public class DreamWorld
{
    public string front_default { get; set; }
    public object front_female { get; set; }
}

public class Emerald
{
    public string front_default { get; set; }
    public string front_shiny { get; set; }
}

public class FireredLeafgreen
{
    public string back_default { get; set; }
    public string back_shiny { get; set; }
    public string front_default { get; set; }
    public string front_shiny { get; set; }
}

public class Form
{
    public string name { get; set; }
    public string url { get; set; }
}

public class GameIndex
{
    public int game_index { get; set; }
    public Version version { get; set; }
}

public class GenerationI
{
    [JsonProperty("red-blue")]
    public RedBlue redblue { get; set; }
    public Yellow yellow { get; set; }
}

public class GenerationIi
{
    public Crystal crystal { get; set; }
    public Gold gold { get; set; }
    public Silver silver { get; set; }
}

public class GenerationIii
{
    public Emerald emerald { get; set; }

    [JsonProperty("firered-leafgreen")]
    public FireredLeafgreen fireredleafgreen { get; set; }

    [JsonProperty("ruby-sapphire")]
    public RubySapphire rubysapphire { get; set; }
}

public class GenerationIv
{
    [JsonProperty("diamond-pearl")]
    public DiamondPearl diamondpearl { get; set; }

    [JsonProperty("heartgold-soulsilver")]
    public HeartgoldSoulsilver heartgoldsoulsilver { get; set; }
    public Platinum platinum { get; set; }
}

public class GenerationV
{
    [JsonProperty("black-white")]
    public BlackWhite blackwhite { get; set; }
}

public class GenerationVi
{
    [JsonProperty("omegaruby-alphasapphire")]
    public OmegarubyAlphasapphire omegarubyalphasapphire { get; set; }

    [JsonProperty("x-y")]
    public XY xy { get; set; }
}

public class GenerationVii
{
    public Icons icons { get; set; }

    [JsonProperty("ultra-sun-ultra-moon")]
    public UltraSunUltraMoon ultrasunultramoon { get; set; }
}

public class GenerationViii
{
    public Icons icons { get; set; }
}

public class Gold
{
    public string back_default { get; set; }
    public string back_shiny { get; set; }
    public string front_default { get; set; }
    public string front_shiny { get; set; }
    public string front_transparent { get; set; }
}

public class HeartgoldSoulsilver
{
    public string back_default { get; set; }
    public string back_female { get; set; }
    public string back_shiny { get; set; }
    public string back_shiny_female { get; set; }
    public string front_default { get; set; }
    public string front_female { get; set; }
    public string front_shiny { get; set; }
    public string front_shiny_female { get; set; }
}

public class HeldItem
{
    public Item item { get; set; }
    public List<VersionDetail> version_details { get; set; }
}

public class Home
{
    public string front_default { get; set; }
    public string front_female { get; set; }
    public string front_shiny { get; set; }
    public string front_shiny_female { get; set; }
}

public class Icons
{
    public string front_default { get; set; }
    public object front_female { get; set; }
}

public class Item
{
    public string name { get; set; }
    public string url { get; set; }
}

public class Move
{
    public Move2 move { get; set; }
    public List<VersionGroupDetail> version_group_details { get; set; }
}

public class Move2
{
    public string name { get; set; }
    public string url { get; set; }
}

public class MoveLearnMethod
{
    public string name { get; set; }
    public string url { get; set; }
}

public class OfficialArtwork
{
    public string front_default { get; set; }
    public string front_shiny { get; set; }
}

public class OmegarubyAlphasapphire
{
    public string front_default { get; set; }
    public string front_female { get; set; }
    public string front_shiny { get; set; }
    public string front_shiny_female { get; set; }
}

public class Other
{
    public DreamWorld dream_world { get; set; }
    public Home home { get; set; }

    [JsonProperty("official-artwork")]
    public OfficialArtwork officialartwork { get; set; }
}

public class Platinum
{
    public string back_default { get; set; }
    public string back_female { get; set; }
    public string back_shiny { get; set; }
    public string back_shiny_female { get; set; }
    public string front_default { get; set; }
    public string front_female { get; set; }
    public string front_shiny { get; set; }
    public string front_shiny_female { get; set; }
}

public class RedBlue
{
    public string back_default { get; set; }
    public string back_gray { get; set; }
    public string back_transparent { get; set; }
    public string front_default { get; set; }
    public string front_gray { get; set; }
    public string front_transparent { get; set; }
}

public class Root
{
    public List<Ability> abilities { get; set; }
    public int? base_experience { get; set; }
    public List<Form> forms { get; set; }
    public List<GameIndex> game_indices { get; set; }
    public int height { get; set; }
    public List<HeldItem> held_items { get; set; }
    public int id { get; set; }
    public bool is_default { get; set; }
    public string location_area_encounters { get; set; }
    public List<Move> moves { get; set; }
    public string name { get; set; }
    public int order { get; set; }
    public List<object> past_types { get; set; }
    public Species species { get; set; }
    public Sprites sprites { get; set; }
    public List<StatJson> stats { get; set; }
    public List<Type> types { get; set; }
    public int weight { get; set; }
}

public class RubySapphire
{
    public string back_default { get; set; }
    public string back_shiny { get; set; }
    public string front_default { get; set; }
    public string front_shiny { get; set; }
}

public class Silver
{
    public string back_default { get; set; }
    public string back_shiny { get; set; }
    public string front_default { get; set; }
    public string front_shiny { get; set; }
    public string front_transparent { get; set; }
}

public class Species
{
    public string name { get; set; }
    public string url { get; set; }
}

public class Sprites
{
    public string back_default { get; set; }
    public string back_female { get; set; }
    public string back_shiny { get; set; }
    public string back_shiny_female { get; set; }
    public string front_default { get; set; }
    public string front_female { get; set; }
    public string front_shiny { get; set; }
    public string front_shiny_female { get; set; }
    public Other other { get; set; }
    public Versions versions { get; set; }
}

public class StatJson
{
    public int base_stat { get; set; }
    public int effort { get; set; }
    public StatJson stat { get; set; }
}

public class Stat2
{
    public string name { get; set; }
    public string url { get; set; }
}

public class Type
{
    public int slot { get; set; }
    public Type2 type { get; set; }
}

public class Type2
{
    public string name { get; set; }
    public string url { get; set; }
}

public class UltraSunUltraMoon
{
    public string front_default { get; set; }
    public string front_female { get; set; }
    public string front_shiny { get; set; }
    public string front_shiny_female { get; set; }
}

public class Version
{
    public string name { get; set; }
    public string url { get; set; }
}

public class VersionDetail
{
    public int rarity { get; set; }
    public Version version { get; set; }
}

public class VersionGroup
{
    public string name { get; set; }
    public string url { get; set; }
}

public class VersionGroupDetail
{
    public int level_learned_at { get; set; }
    public MoveLearnMethod move_learn_method { get; set; }
    public VersionGroup version_group { get; set; }
}

public class Versions
{
    [JsonProperty("generation-i")]
    public GenerationI generationi { get; set; }

    [JsonProperty("generation-ii")]
    public GenerationIi generationii { get; set; }

    [JsonProperty("generation-iii")]
    public GenerationIii generationiii { get; set; }

    [JsonProperty("generation-iv")]
    public GenerationIv generationiv { get; set; }

    [JsonProperty("generation-v")]
    public GenerationV generationv { get; set; }

    [JsonProperty("generation-vi")]
    public GenerationVi generationvi { get; set; }

    [JsonProperty("generation-vii")]
    public GenerationVii generationvii { get; set; }

    [JsonProperty("generation-viii")]
    public GenerationViii generationviii { get; set; }
}

public class XY
{
    public string front_default { get; set; }
    public string front_female { get; set; }
    public string front_shiny { get; set; }
    public string front_shiny_female { get; set; }
}

public class Yellow
{
    public string back_default { get; set; }
    public string back_gray { get; set; }
    public string back_transparent { get; set; }
    public string front_default { get; set; }
    public string front_gray { get; set; }
    public string front_transparent { get; set; }
}