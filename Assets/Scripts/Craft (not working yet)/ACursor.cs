using UnityEngine;

public class ACursor : MonoBehaviour
{
    private void Awake()
    {
        transform.position = Input.mousePosition;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}
