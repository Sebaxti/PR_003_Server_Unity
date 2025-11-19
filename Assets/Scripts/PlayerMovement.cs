using Unity.Netcode;
using UnityEngine;

//public class PlayerMovement : MonoBehaviour
public class PlayerMovement : NetworkBehaviour
{
    [SerializeField]
    float moverSpeed;

    [SerializeField]
    NetworkVariable <int> vida = new NetworkVariable<int>(
        3,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);

        [SerializeField]
    NetworkVariable <int> puntos = new NetworkVariable<int>(
        0,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);

    [SerializeField]
    GameObject prefabProyectil;

    Rigidbody2D rb;
    float moverHorizontal, moverVertical;
    
    Vector2 movimiento;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    void Update() 
    {
        if (!IsOwner) return; //Si no es el duenyo/pcServer return
        moveFuncion();
        disparar();
      
    }

    void moveFuncion()
    {
        //Movimiento Player
        moverHorizontal = Input.GetAxisRaw("Horizontal");
        moverVertical = Input.GetAxisRaw("Vertical");

        movimiento = new Vector2(moverHorizontal, moverVertical).normalized;
        rb.linearVelocity = movimiento * moverSpeed;
    }

    void disparar()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            disparoOnlineServerRpc();
        }
    }

    [ServerRpc]
    void disparoOnlineServerRpc()
    {
        GameObject proyectil= Instantiate(prefabProyectil);
        proyectil.transform.position= transform.position;
        NetworkObject disparo = proyectil.GetComponent<NetworkObject>();
        disparo.Spawn();

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsOwner) return;

        if (collision.collider.CompareTag("moneda"))
        {
            puntos.Value=puntos.Value+1;
            NetworkObject moneda = collision.gameObject.GetComponent<NetworkObject>();
            EliminarMonedaServerRpc(moneda.NetworkObjectId);
        }
    }

    [ServerRpc]
    void EliminarMonedaServerRpc(ulong monedaId)
    {
        NetworkObject moneda=NetworkManager.Singleton.SpawnManager.SpawnedObjects[monedaId];
           moneda.Despawn(); 
    }
}
