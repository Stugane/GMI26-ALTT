#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AInventory))]
public class AInventoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AInventory inventory = (AInventory)target;

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField(
            "Inventory Controller",
            EditorStyles.boldLabel
        );

        EditorGUILayout.HelpBox(
            "Controls the player's inventory and hotbar.",
            MessageType.Info
        );

        DrawDefaultInspector();

        EditorGUILayout.Space(10);

        if (GUILayout.Button("Debug Inventory"))
        {
            Debug.Log("Inventory selected: " + inventory.name);
        }
    }
}

#endif