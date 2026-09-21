using Unity.Cinemachine;
using UnityEngine;

public class TRespawn : MonoBehaviour
{
    [SerializeField] public GameObject PlayerCharacter;
    [SerializeField] public GameObject DeathObjectPrefab;
    private static TRespawn instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        SpawnNewPlayerInstance();
    }

    // Update is called once per frame
    public static void RespawnPlayer(Vector3 deathPosition, Quaternion deathRotation)
    {
        if (instance == null) return;
        if (instance.DeathObjectPrefab != null)
        {
            Instantiate(instance.DeathObjectPrefab, deathPosition, deathRotation);
            Debug.Log("Death object left behind.");
        }
        instance.SpawnNewPlayerInstance();
    }
         private void SpawnNewPlayerInstance()
    {
        if (PlayerCharacter == null) return;

        GameObject newPlayer = Instantiate(PlayerCharacter, transform.position, transform.rotation);

        newPlayer.tag = "Player"; //Adds the newly spawned clone the "Player" tag
        Transform cameraTargetAnchor = newPlayer.transform.Find("PlayerCameraRoot");

            cameraTargetAnchor.gameObject.tag = "CinemachineTarget";

        var vCam = Object.FindAnyObjectByType<CinemachineCamera>();
        if (vCam != null)
        {
            vCam.Follow = cameraTargetAnchor;
            vCam.LookAt = cameraTargetAnchor;
        }
        var starterInput = newPlayer.GetComponent<StarterAssets.StarterAssetsInputs>();
        if (starterInput != null)
        {
            starterInput.cursorLocked = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Debug.Log("Player respawned.");
    }
}
