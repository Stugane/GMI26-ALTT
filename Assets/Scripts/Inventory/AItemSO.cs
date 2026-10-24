using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "NewItem")]
public class AItemSO : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int maxStackSize;
    public GameObject itemPrefab;
    public GameObject handItemPrefab;
}
