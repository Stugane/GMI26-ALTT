#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

public class AInventoryWindow : EditorWindow
{
    [MenuItem("Window/Inventory Tools")]
    public static void ShowWindow()
    {
        GetWindow<AInventoryWindow>("Inventory Tools");
    }

    private void OnGUI()
    {
        GUILayout.Label(
            "Inventory Tools",
            EditorStyles.boldLabel
        );

        GUILayout.Space(10);

        GUILayout.Label("Useful inventory debugging tools.");

        if (GUILayout.Button("Print Debug Message"))
        {
            Debug.Log("Inventory tools are working!");
        }
    }
}
#endif