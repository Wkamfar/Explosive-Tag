using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class IceScript : MonoBehaviour
{
    public float iceDespawnTime;
    public GameObject player;
    // Start is called before the first frame update
    void Start() //make it so that if you get a certain distance away from the ice or if a certain amout of time passes the ice will expire, I will have to syncronize it soon, so leave a note to make a server wide synced invoke function
    {
        Invoke("Despawn", iceDespawnTime);
    }

    private void Despawn()
    {
        //player.GetComponent<CharacterController>().SkaterPassiveDespawnIce(this.gameObject);
        player.GetComponent<CharacterController>().view.RPC("NetworkDespawn", RpcTarget.All);
    }
    public void DefinePlayer(GameObject _player)
    {
        player = _player;
    }
     

    [PunRPC]
    public void NetworkDespawn()
    {
        player.GetComponent<CharacterController>().SkaterPassiveDespawnIce(this.gameObject);
    }
}
