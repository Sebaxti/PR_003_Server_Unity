using UnityEngine;
using Unity.Netcode;

public class SCR_ProyectilContoller : NetworkBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        gameObject.GetComponent<Rigidbody2D>().linearVelocity=Vector2.up;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
