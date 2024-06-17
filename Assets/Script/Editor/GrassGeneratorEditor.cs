using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GrassGeneratorEditor : EditorWindow
{
    static GrassGeneratorEditor instance;
    TileSprite tileSpritePrefab;
    SceneView sceneView;
    Vector3 cellPosition;
    
    KeyCode keyDelete = KeyCode.C;
    KeyCode keyPaint = KeyCode.F;
    KeyCode keyIncreaseSize = KeyCode.UpArrow;
    KeyCode keyDecreaseSize = KeyCode.DownArrow;
    LayerMask objectToIgnore;
    CellSettings settings = new CellSettings();
    
    int paintSize = 1;
    bool debugCollision = true;
    Dictionary<System.Type, TransformTileSprite> transformList = new Dictionary<System.Type, TransformTileSprite>();

    [MenuItem("PokemonTools/GrassGenerator")]
    public static void CreateWindow()
    {
        if(!instance)
        {
            instance = GetWindow<GrassGeneratorEditor>("2D Designer");
            instance.sceneView = SceneView.lastActiveSceneView;
        }
    }
    public void InitTransformList()
    {
        List<System.Type> _tileSprite = TypeCache.GetTypesDerivedFrom(typeof(TileSprite)).ToList();
        TransformTileSprite[] transformTileSprites = FindObjectsOfType<TransformTileSprite>();
        transformList.Clear();
        for (int i = 0; i < transformTileSprites.Length; i++)
        {
            TransformTileSprite _transformTileSprite = transformTileSprites[i];
            for (int j = 0; j < _tileSprite.Count; j++)
            {
                if (_transformTileSprite.name == _tileSprite[j].Name)
                    transformList.Add(_tileSprite[j], _transformTileSprite);
            }
        }
    }

    void InputToggle(KeyCode _inputKey, Action _callback)
    {
        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == _inputKey)
        {
            Selection.activeGameObject = null;
            _callback?.Invoke();
        }
    }
    void InputToggleMouse(int _inputMouse, Action _callback)
    {
        if (Event.current.isScrollWheel && Event.current.rawType == EventType.ScrollWheel)
        {
            Debug.Log(Event.current.type);
            Selection.activeGameObject = null;
            _callback?.Invoke();
        }
    }
    public void CreateTileSprite(TileSprite _tileSpritePrefab, Vector3 _position)
    {
        TransformTileSprite _transformTileSprite = null;
        if (!transformList.ContainsKey(_tileSpritePrefab.GetType()))
        {
            GameObject _create = new GameObject(_tileSpritePrefab.GetType().Name);
            TransformTileSprite _t = _create.AddComponent<TransformTileSprite>();
            _transformTileSprite = _t;
            transformList.Add(_tileSpritePrefab.GetType(), _t);
        }
        else
            _transformTileSprite = transformList[_tileSpritePrefab.GetType()];

        TileSprite _tileSprite = Instantiate(_tileSpritePrefab, _position, Quaternion.identity, _transformTileSprite.transform);
        _transformTileSprite.Add(_tileSprite);

        UpdateGrassAroundPosition(_position);
        _tileSprite.ModifCell();
        _tileSprite.UpdateCell(tileSpritePrefab.spriteData, settings);
    }
    public void Paint()
    {
        if (!tileSpritePrefab) return;
        for (int i = 0; i < paintSize; i++)
        {
            for (int j = 0; j < paintSize; j++)
            {
                Vector3 _pos = cellPosition + new Vector3(i, j);
                Collider2D _collider = Physics2D.CircleCast(_pos, 0.1f, Vector3.forward, 1f, 1 << objectToIgnore).collider;
                if (_collider)
                {
                    TileSprite _tileSprite = _collider.GetComponent<TileSprite>();
                    if(_tileSprite && tileSpritePrefab.GetType() == _tileSprite.GetType())
                        continue;
                }
                CreateTileSprite(tileSpritePrefab, _pos);
            }
        }
    }
    public void Delete()
    {
        for (int i = 0; i < paintSize; i++)
        {
            for (int j = 0; j < paintSize; j++)
            {
                Vector3 _pos = cellPosition + new Vector3(i, j, 0);
                TileSprite _tileSprite = DetectTileSprite(_pos);
                if (_tileSprite)
                {
                    if (tileSpritePrefab.GetType() != _tileSprite.GetType()) continue;
                    UpdateGrassAroundPosition(_pos, false);
                    transformList[_tileSprite.GetType()].Remove(_tileSprite);
                    DestroyImmediate(_tileSprite.gameObject);
                    continue;
                }
            }
        }
    }
    public void IncreasePainSize()
    {
        paintSize++;
    }
    public void DecreasePainSize()
    {
        paintSize--;
    }
    public Collider2D Detect(Vector2 _position)
    {
        RaycastHit2D _ray = Physics2D.CircleCast(_position, 0.1f, Vector3.forward, 0.1f, 1 << objectToIgnore);
        return _ray.collider;
    }
    public TileSprite DetectTileSprite(Vector2 _position)
    {
        Collider2D _collider = Detect(_position);
        if (_collider)
            return (TileSprite)_collider.GetComponent(tileSpritePrefab.GetType());
        return null;
    }

    public void UpdateGrassAroundPosition(Vector3 _position, bool _fill = true)
    {
        Vector2 _p = _position;
        foreach (var item in Direction.AllDirection)
        {
            TileSprite tileSprite = DetectTileSprite(_p + item.Value);
            tileSprite?.ModifCellFromDirection(item.Key, !_fill);
            tileSprite?.UpdateCell(tileSpritePrefab.spriteData, settings);
        }
    }
    public void SetGridCellPosition()
    {
        Vector2 mousePosition = Event.current.mousePosition + instance.position.position - sceneView.position.position + new Vector2(0, 19 - 45);
        mousePosition.x *= EditorGUIUtility.pixelsPerPoint;
        mousePosition.y = sceneView.camera.pixelHeight - mousePosition.y * EditorGUIUtility.pixelsPerPoint;
        cellPosition = sceneView.camera.ScreenToWorldPoint(mousePosition);
        cellPosition = GridCell.GetCellPositionWithWorldPosition(cellPosition);
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }
    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnGUI()
    {
        if (!instance)
        {
            instance = this;
            instance.sceneView = SceneView.lastActiveSceneView;
        }

        SetGridCellPosition();
        Repaint();

        tileSpritePrefab = (TileSprite)EditorGUILayout.ObjectField("TileSpritePrefab", tileSpritePrefab, typeof(TileSprite), true);
        keyDelete = (KeyCode)EditorGUILayout.EnumPopup("KeyDelete",keyDelete);
        keyPaint = (KeyCode)EditorGUILayout.EnumPopup("KeyPaint", keyPaint);
        keyIncreaseSize = (KeyCode)EditorGUILayout.EnumPopup("KeyIncrease", keyIncreaseSize);
        keyDecreaseSize = (KeyCode)EditorGUILayout.EnumPopup("KeyDecrease", keyDecreaseSize);
        objectToIgnore = EditorGUILayout.LayerField("ObjectToDetect", objectToIgnore);

        settings.collisionBorder = EditorGUILayout.Toggle("Active Border Collision", settings.collisionBorder);
        settings.collisionInside = EditorGUILayout.Toggle("Active Inside Collision", settings.collisionInside);
        settings.layer = EditorGUILayout.IntField("Order In Layer", settings.layer);
        paintSize = EditorGUILayout.IntField("Paint Size", paintSize);
        debugCollision = EditorGUILayout.Toggle("Display Collision", debugCollision);
    }
    private void OnSceneGUI(SceneView sc)
    {
        InitTransformList();

        InputToggle(keyPaint, Paint);
        InputToggle(keyDelete, Delete);
        InputToggle(keyIncreaseSize, IncreasePainSize);
        InputToggle(keyDecreaseSize, DecreasePainSize);

        Handles.DrawWireCube(cellPosition + ((Vector3.one/2)*(paintSize-1)), Vector2.one * paintSize);
        Repaint();
        if (!debugCollision) return;
        foreach (TransformTileSprite item in transformList.Values)
        {
            if(sceneView)
                item.DrawEditorOnly(sceneView);
        }
    }
}