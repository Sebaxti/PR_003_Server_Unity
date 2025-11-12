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

    Rigidbody2D rb;
    float moverHorizontal, moverVertical;
    
    Vector2 movimiento;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    void Update() 
    {
        if (!IsOwner) return; //Si no es el due�o/pcServer return
        moveFuncion();
      
    }

    void moveFuncion()
    {
        //Movimiento Player
        moverHorizontal = Input.GetAxisRaw("Horizontal");
        moverVertical = Input.GetAxisRaw("Vertical");

        movimiento = new Vector2(moverHorizontal, moverVertical).normalized;
        rb.linearVelocity = movimiento * moverSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        vida.Value=vida.Value-1;
    }
}
