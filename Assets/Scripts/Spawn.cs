using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Spawn : MonoBehaviour
{
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start() //make it so that you spawn after you go through this menu delete this later
    {
        //Vector3 position = new Vector3(Random.Range(-48, 48), 1, Random.Range(-48, 48));
        //PhotonNetwork.Instantiate(playerPrefab.name, position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
