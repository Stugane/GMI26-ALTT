using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TDeath : MonoBehaviour
{
    private BoxCollider triggerdeath;
    

    private void OnTriggerEnter(Collider other)
    {
        Vector3 currentPos = other.transform.position;
        Quaternion currentRot = other.transform.rotation;
        triggerdeath = other.GetComponent<BoxCollider>();
        Destroy(GameObject.FindWithTag("Player"));
        TRespawn.RespawnPlayer(currentPos, currentRot);
    }
}
