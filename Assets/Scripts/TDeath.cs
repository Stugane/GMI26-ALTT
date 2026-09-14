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

    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
