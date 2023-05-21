using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : Singleton<ZoneManager>
{
    public event Action<Zone> OnZoneChange = null;
    [SerializeField] List<Zone> zones = new List<Zone>();
    public Zone currentZone = null;
    public List<Zone> Zones => zones;
    public void AddZone(Zone _zone)
    {
        zones.Add(_zone);
    }
    public void RemoveZone(Zone _zone)
    {
        zones.Remove(_zone);
    }
    public void ChangeZone(Zone _zone)
    {
        currentZone = _zone;
        OnZoneChange?.Invoke(currentZone);
    }
}
