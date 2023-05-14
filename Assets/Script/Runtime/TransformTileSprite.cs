using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TransformTileSprite : MonoBehaviour
{
    [SerializeField] List<TileSprite> tileSprites = new List<TileSprite>();
    public List<TileSprite> TileSprites => tileSprites;
    public void Add(TileSprite _tileSprite)
    {
        tileSprites.Add(_tileSprite);
    }
    public void Remove(TileSprite _tileSprite)
    {
        tileSprites.Remove(_tileSprite);
    }
    public void DrawEditorOnly(SceneView _sv)
    {
        int _count = tileSprites.Count;
        Vector3 _sceneViewPos = _sv.camera.transform.position;
        for (int i = 0; i < _count; i++)
        {
            TileSprite _child = tileSprites[i];
            if (!_child) continue;
            Vector3 _childPos = _child.transform.position;
            if (Vector2.Distance(_sceneViewPos, _childPos) > 15)
                continue;
            Handles.color = _child.BoxCollider.isTrigger ? Color.green : Color.red;
            Handles.DrawWireCube(_childPos, Vector3.one * 0.9f);
        }
    }
}
