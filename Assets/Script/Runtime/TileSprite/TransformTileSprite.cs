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
            if (Vector2.Distance(_sceneViewPos, _childPos) > 17)
                continue;
            if (!_child.BoxCollider2 || _child.destroyCollisionAtStart) continue;
            Handles.color = _child.BoxCollider2.isTrigger ? Color.green : Color.red;
            if(_child.BoxCollider2.bounds.size == new Vector3(1,1,0))
                Handles.DrawWireCube(_childPos, Vector3.one * 0.9f);
            else
                Handles.DrawWireCube(_childPos, _child.BoxCollider2.bounds.size);
        }
    }
}
