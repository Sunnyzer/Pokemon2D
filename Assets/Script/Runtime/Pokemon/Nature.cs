using System;
using System.Collections.Generic;
using System.Linq;

public class NatureBoost
{
    Stat natureBoost;
    public NatureBoost(Stat _natureBoost) 
    {
        natureBoost = _natureBoost;
    }
}

[Serializable]
public class Nature
{
    public string name;
    public static string GetRandomNature()
    {
        int _index = UnityEngine.Random.Range(0, NatureData.natureBoosts.Keys.Count);
        return NatureData.natureBoosts.Keys.ToArray()[_index];
    }
}

public static class NatureData
{
    public static Dictionary<string, NatureBoost> natureBoosts = new Dictionary<string, NatureBoost>()
    {
        { "Assure",new NatureBoost(new Stat(0, -10, 10, 0, 0, 0)) },
        { "Bizarre",new NatureBoost(new Stat(0, 0, 0, 0, 0, 0)) },
        { "Brave",new NatureBoost(new Stat(0, 10, 0, 0, 0, -10)) },
        { "Calme",new NatureBoost(new Stat(0, -10, 0, 0, 10, 0)) },
        { "Discret",new NatureBoost(new Stat(0, 0, 0, 10, 0, -10)) },
        { "Docile",new NatureBoost(new Stat(0, 0, 0, 0, 0, 0)) },
        { "Doux",new NatureBoost(new Stat(0, -10, 10, 0, 0, 0)) },
        { "Foufou",new NatureBoost(new Stat(0, 0, 0, 10, -10, 0)) },
        { "Gentil",new NatureBoost(new Stat(0, 0, -10, 0, 10, 0)) },
        { "Hardi",new NatureBoost(new Stat(0, 0, 0, 0, 0, 0)) },
        { "Jovial",new NatureBoost(new Stat(0, 0, 0, 10, 0, -10)) },
        { "Lache",new NatureBoost(new Stat(0, 0, 10, 10, 0, 10)) },
        { "Malin",new NatureBoost(new Stat(0, 0, 10, -10, 0, 0)) },
        { "Malpoli",new NatureBoost(new Stat(0, 0, 0, 0, 10, -10)) },
        { "Mauvais",new NatureBoost(new Stat(0, 10, 0, 0, -10, 0)) },
        { "Modeste",new NatureBoost(new Stat(0, -10, 0, 10, 0, 0)) },
        { "Naif",new NatureBoost(new Stat(0, 0, 0, 0, -10, 10)) },
        { "Presse",new NatureBoost(new Stat(0, 0, -10, 0, 0, 10)) },
        { "Prudent",new NatureBoost(new Stat(0, 0, 0, -10, 10, 0)) },
        { "Pudique",new NatureBoost(new Stat(0, 0, 0, 0, 0, 0)) },
        { "Relax",new NatureBoost(new Stat(0, 0, 10, 0, 0, -10)) },
        { "Rigide",new NatureBoost(new Stat(0, 10, 0, -10, 0, 0)) },
        { "Serieux",new NatureBoost(new Stat(0, 0, 0, 0, 0, 0)) },
        { "Solo",new NatureBoost(new Stat(0, 10, -10, 0, 0, 0)) },
        { "Timide",new NatureBoost(new Stat(0, -10, 0, 0, 0, 10)) },
    };
    public static NatureBoost GetNatureBoost(string _name)
    {
        if(natureBoosts.ContainsKey(_name))
            return natureBoosts[_name];
        return null;
    }
    
}
