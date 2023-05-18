using System;
using UnityEditor;
using UnityEngine;

public class PrefabGridPlacer : EditorWindow
{
    GridPrefab prefab;
    KeyCode keyDelete = KeyCode.C;
    KeyCode keyPaint = KeyCode.F;
    Vector3 cellPosition;
    SceneView sceneView;
    TransformGrid grid = null;
    static PrefabGridPlacer instance;

    [MenuItem("PokemonTools/GridPlacer")]
    public static void CreateWindow()
    {
        if (!instance)
        {
            instance = GetWindow<PrefabGridPlacer>("GridPlacer");
            instance.Init();
        }
    }
    private void Init()
    {
        sceneView = SceneView.lastActiveSceneView;
        grid = FindObjectOfType<TransformGrid>();
        if(!grid)
        {
            grid = new GameObject("TransformGrid").AddComponent<TransformGrid>();
        }
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
            Init();
        }

        SetGridCellPosition();
        Repaint();

        prefab = (GridPrefab)EditorGUILayout.ObjectField("prefab", prefab, typeof(GridPrefab), true);
        keyPaint = (KeyCode)EditorGUILayout.EnumPopup("keyPaint", keyPaint);
        keyDelete = (KeyCode)EditorGUILayout.EnumPopup("keyDelete", keyDelete);
    }
    void InputToggle(KeyCode _inputKey, Action _callback)
    {
        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == _inputKey)
            _callback?.Invoke();
    }
    public void Paint()
    {
        if (!prefab) return;
        if (Physics2D.CircleCast(cellPosition, 0.1f, Vector3.forward)) return;
        GridPrefab _gridPrefab = Instantiate(prefab, cellPosition, Quaternion.identity, grid.transform);
    }
    public void Delete()
    {
        RaycastHit2D _raycast = Physics2D.CircleCast(cellPosition, 0.1f, Vector3.forward);
        if (!_raycast || !_raycast.collider.GetComponent<GridPrefab>()) return;
        DestroyImmediate(_raycast.collider.gameObject);
    }
    public void SetGridCellPosition()
    {
        Vector2 mousePosition = Event.current.mousePosition + instance.position.position - sceneView.position.position + new Vector2(0, 19 - 45);
        mousePosition.x *= EditorGUIUtility.pixelsPerPoint;
        mousePosition.y = sceneView.camera.pixelHeight - mousePosition.y * EditorGUIUtility.pixelsPerPoint;
        cellPosition = sceneView.camera.ScreenToWorldPoint(mousePosition);
        cellPosition = GridCell.GetCellPositionWithWorldPosition(cellPosition);
    }
    private void OnSceneGUI(SceneView sc)
    {
        InputToggle(keyPaint, Paint);
        InputToggle(keyDelete, Delete);
    }
}
