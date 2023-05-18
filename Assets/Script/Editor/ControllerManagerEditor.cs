using System.Linq;
using UnityEditor;

[CustomEditor(typeof(ControllerManager))]
public class ControllerManagerEditor : Editor
{
    ControllerManager controllerManager;
    private void OnEnable()
    {
        controllerManager = (ControllerManager)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Controller[] _controllers = FindObjectsOfType<Controller>(true);
        int _indexUI = EditorGUILayout.Popup(controllerManager.indexController, _controllers.Select(controller => controller.name).ToArray());
        if (_indexUI != controllerManager.indexController)
        {
            controllerManager.indexController = _indexUI;
            serializedObject.FindProperty("currentController").objectReferenceValue = _controllers[_indexUI];
            //controllerManager.TakeControl(_controllers[_indexUI]);
        }
    }
}
