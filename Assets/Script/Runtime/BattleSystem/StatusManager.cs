using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatusSprite
{
    public Status status;
    public Sprite sprite;
}

public class StatusManager : Singleton<StatusManager>
{
    [SerializeField] List<StatusSprite> statusSprites = new List<StatusSprite>();
    
    public Sprite GetSpriteByStatus(Status _status)
    {
        for (int i = 0; i < statusSprites.Count; i++)
        {
            if(statusSprites[i].status == _status)
            {
                return statusSprites[i].sprite;
            }
        }
        return null;
    }
}
