using UnityEngine;

public class AItemDetector : MonoBehaviour
{
    private AInventory inventory;

    private void Start()
    {
        inventory = FindAnyObjectByType<AInventory>();

        if (inventory == null)
        {
            Debug.LogError("Could not find AInventory!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AItem item = other.GetComponentInParent<AItem>();

        if (item != null && inventory != null)
        {
            inventory.AddNearbyItem(item);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AItem item = other.GetComponentInParent<AItem>();

        if (item != null && inventory != null)
        {
            inventory.RemoveNearbyItem(item);
        }
    }
}