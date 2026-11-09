#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

public class AInventoryMenu
{
    [MenuItem("Tools/Inventory/Debug Inventory")]
    public static void DebugInventory()
    {
        Debug.Log("Inventory debugging started!");
    }
}
#endif