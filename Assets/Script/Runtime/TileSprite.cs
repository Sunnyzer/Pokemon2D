using System;
using System.Collections.Generic;
using UnityEngine;

public class CellSettings
{
    [SerializeField] public bool collisionInside = false;
    [SerializeField] public bool collisionBorder = true;
    [SerializeField] public int layer = 3;
}

public enum EDirection
{
    Right,
    Up,
    Left,
    Down,

    RightUp,
    RightDown,
    LeftUp,
    LeftDown,
}
public static class Direction
{
    public static Dictionary<EDirection, Vector2> AllDirection = new Dictionary<EDirection, Vector2>()
    {
        { EDirection.Right , new Vector2(1, 0) },
        { EDirection.Up , new Vector2(0, 1) },
        { EDirection.Left, new Vector2(-1, 0) },
        { EDirection.Down , new Vector2(0, -1) },
        { EDirection.RightUp , new Vector2(1, 0) + new Vector2(0, 1) },
        { EDirection.RightDown , new Vector2(1, 0) + new Vector2(0, -1) },
        { EDirection.LeftUp, new Vector2(-1, 0)+ new Vector2(0, 1) },
        { EDirection.LeftDown , new Vector2(-1, 0)+ new Vector2(0, -1) },
    };
}
public class TileSprite : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Collider2D boxCollider;
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            if (!spriteRenderer)
            {
                if (TryGetComponent<SpriteRenderer>(out spriteRenderer))
                    return spriteRenderer;
                else
                    return spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            }
            return spriteRenderer;
        }
    }
    public Collider2D BoxCollider2
    {
        get
        {
            if (!boxCollider)
            {
                if(TryGetComponent<Collider2D>(out boxCollider))
                    return boxCollider;
                else
                    return boxCollider = gameObject.AddComponent<Collider2D>();
            }
            return boxCollider;
        }
    }
    public bool destroyCollisionAtStart = false;
    #if UNITY_EDITOR
    public SpriteDataSO spriteData;
    public bool right;
    public bool left;
    public bool up;
    public bool down;
    public bool rightUp;
    public bool leftUp;
    public bool rightDown;
    public bool leftDown;

    public int GetSpriteID()
    {
        int r = right ? 1 : 0;
        int u = up ? 1 : 0;
        int l = left ? 1 : 0;
        int d = down ? 1 : 0;
        int ru = rightUp ? 1 : 0;
        int lu = leftUp ? 1 : 0;
        int rd = rightDown ? 1 : 0;
        int ld = leftDown ? 1 : 0;
        int _ID = r + (u << 1) + (l << 2) + (d << 3);
        if (_ID <= 0)
        {
            _ID = 15;
            _ID += ru;
            if (_ID == 15)
            {
                _ID += lu * 2;
                if (_ID == 15)
                {
                    _ID += rd * 3;
                    if (_ID == 15)
                    {
                        _ID += ld * 4;
                        if (_ID == 15)
                        {
                            return 0;
                        }
                    }
                }
            }
            return _ID;
        }
        return _ID;
    }
    public void UpdateCell(SpriteDataSO _grassData, CellSettings _settings)
    {
        int _ID = GetSpriteID();
        if (_settings.collisionBorder)
            DeactiveTrigger();
        else
            ActiveTrigger();
        if (_ID == 0 && !_settings.collisionInside)
            ActiveTrigger();
        if (_ID == 0 && _settings.collisionInside)
            destroyCollisionAtStart = true;
        SpriteRenderer.sortingOrder = _settings.layer;
        SpriteRenderer.sprite = _grassData.GrassSprite[_ID];
    }
    public void ModifCell()
    {
        Vector3 _position = transform.position;
        TileSprite rightGrass = DetectTileSprite(_position + new Vector3(1, 0));
        TileSprite upGrass = DetectTileSprite(_position + new Vector3(0, 1));
        TileSprite leftGrass = DetectTileSprite(_position + new Vector3(-1, 0));
        TileSprite downGrass = DetectTileSprite(_position + new Vector3(0, -1));

        TileSprite rightUpGrass = DetectTileSprite(_position + new Vector3(1, 1));
        TileSprite leftUpGrass = DetectTileSprite(_position + new Vector3(-1, 1));
        TileSprite rightDownGrass = DetectTileSprite(_position + new Vector3(1, -1));
        TileSprite leftDownGrass = DetectTileSprite(_position + new Vector3(-1, -1));
        right = !rightGrass;
        up = !upGrass;
        left = !leftGrass;
        down = !downGrass;

        rightUp = !rightUpGrass;
        leftUp = !leftUpGrass;
        rightDown = !rightDownGrass;
        leftDown = !leftDownGrass;
    }
    public void ModifCellFromDirection(EDirection _direction, bool _addDelete)
    {
        switch (_direction)
        {
            case EDirection.Right:
                left = _addDelete;
                break;
            case EDirection.Up:
                down = _addDelete;
                break;
            case EDirection.Left:
                right = _addDelete;
                break;
            case EDirection.Down:
                up = _addDelete;
                break;
            case EDirection.RightUp:
                leftDown = _addDelete;
                break;
            case EDirection.RightDown:
                leftUp = _addDelete;
                break;
            case EDirection.LeftUp:
                rightDown = _addDelete;
                break;
            case EDirection.LeftDown:
                rightUp = _addDelete;
                break;
            default:
                break;
        }
    }
    public void AddCellFromDirection(EDirection _direction)
    {
        ModifCellFromDirection(_direction, false);
    }
    public void RemoveCellFromDirection(EDirection _direction)
    {
        ModifCellFromDirection(_direction, true);
    }
    public virtual TileSprite DetectTileSprite(Vector3 _position)
    {
        Collider2D _collider = Physics2D.CircleCast(_position, 0.1f, Vector3.forward, 0.1f).collider;
        if (_collider)
            return (TileSprite)_collider.GetComponent(GetType());
        return null;
    }
    #endif
    private void Start()
    {
        if(destroyCollisionAtStart)
            Destroy(BoxCollider2);
    }
    public void SetSprite(Sprite _sprite)
    {
        SpriteRenderer.sprite = _sprite;
    }
    public void DeleteCollider()
    {
        Destroy(BoxCollider2);
    }
    public void ActiveTrigger()
    {
        BoxCollider2.isTrigger = true;
        destroyCollisionAtStart = false;
    }
    public void DeactiveTrigger()
    {
        BoxCollider2.isTrigger = false;
        destroyCollisionAtStart = false;
    }
}
