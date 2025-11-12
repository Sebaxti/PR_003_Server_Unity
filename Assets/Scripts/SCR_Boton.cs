using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class SCR_Boton : MonoBehaviour
{

public void conectarComoCliente()
{
   NetworkManager.Singleton.StartClient();
}

public void conectarComoHost()
{
    NetworkManager.Singleton.StartHost();

}

public void desconectar()
{
   NetworkManager.Singleton.Shutdown();

}
}
