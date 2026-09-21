using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TDeath : MonoBehaviour
{
    private BoxCollider triggerdeath;
    

    private void OnTriggerEnter(Collider other)
    {
        triggerdeath = other.GetComponent<BoxCollider>();
        Destroy(GameObject.FindWithTag("Player"));
    }
}
