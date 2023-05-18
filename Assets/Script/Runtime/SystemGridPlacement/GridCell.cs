using UnityEngine;

public class GridCell : Singleton<GridCell>
{
    [SerializeField] Vector2 cellSize = Vector3.one;

    public static Vector2 GetCellPosition2DWithWorldPosition(Vector3 _position)
    {
        float _x = Mathf.FloorToInt(_position.x) + 0.5f;
        float _y = Mathf.FloorToInt(_position.y) + 0.5f;
        return new Vector2(_x, _y);
    }
    public static Vector3 GetCellPositionWithWorldPosition(Vector3 _position)
    {
        float _x = Mathf.FloorToInt(_position.x) + 0.5f;
        float _y = Mathf.FloorToInt(_position.y) + 0.5f;
        return new Vector3(_x, _y, 0);
    }
}
