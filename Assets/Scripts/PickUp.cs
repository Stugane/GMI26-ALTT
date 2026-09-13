using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PickUp : MonoBehaviour
{
    private SphereCollider triggerArea;

    private void Awake()
    {
        triggerArea = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickupable")
        {
            Debug.Log("You can pick up this item.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey("e") == true && other.gameObject.tag == "Pickupable")
        {
            Destroy(other.gameObject);
            Debug.Log("You picked up this item.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Pickupable")
        {
            Debug.Log("You are too far to pick up this item.");
        }
    }
}
