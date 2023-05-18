using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] List<Player> players = new List<Player>();
    public void AddPlayer(Player _player)
    {
        players.Add(_player);
    }
    public void RemovePlayer(Player _player)
    {
        players.Remove(_player);
    }
}
