using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GrassData")]
public class SpriteDataSO : ScriptableObject
{
    public Sprite grassTile;
    
    public Sprite grassTileRight;
    public Sprite grassTileUp;
    public Sprite grassTileLeft;
    public Sprite grassTileDown;
           
    public Sprite grassTileRightDown;
    public Sprite grassTileRightUp;
    public Sprite grassTileLeftDown;
    public Sprite grassTileLeftUp;
           
    public Sprite grassTileRightLeft;
    public Sprite grassTileRightLeftDown;
    public Sprite grassTileRightLeftUp;
           
    public Sprite grassTileDownUp;
    public Sprite grassTileRightDownUp;
    public Sprite grassTileLeftDownUp;
           
    public Sprite grassTileLeftRightDownRight;

    public Sprite grassSpriteDRightUp;
    public Sprite grassSpriteDLeftUp;
    public Sprite grassSpriteDRightDown;
    public Sprite grassSpriteDLeftDown;


    [HideInInspector] List<Sprite> sprites = new List<Sprite>();
    public List<Sprite> GrassSprite
    {
        get
        {
            if (sprites == null)
                sprites = new List<Sprite>();
            sprites.Clear();
            sprites.Add(grassTile);//0000

            sprites.Add(grassTileRight);//0001
                
            sprites.Add(grassTileUp);//0010
            sprites.Add(grassTileRightUp);//0011
            sprites.Add(grassTileLeft);//0100

            sprites.Add(grassTileRightLeft);//0101
            sprites.Add(grassTileLeftUp);//0110
            sprites.Add(grassTileRightLeftUp);//0111

            sprites.Add(grassTileDown);//1000
            sprites.Add(grassTileRightDown);//1001

            sprites.Add(grassTileDownUp);//1010
            sprites.Add(grassTileRightDownUp);//1011
            sprites.Add(grassTileLeftDown);//1100

            sprites.Add(grassTileRightLeftDown);//1101
            sprites.Add(grassTileLeftDownUp);//1110
            sprites.Add(grassTileLeftRightDownRight);//1111

            sprites.Add(grassSpriteDRightUp);//1 0000
            sprites.Add(grassSpriteDLeftUp);//1 0001
            sprites.Add(grassSpriteDRightDown);//1 0002
            sprites.Add(grassSpriteDLeftDown);//1 0003
            return sprites;
        }
    }

}
