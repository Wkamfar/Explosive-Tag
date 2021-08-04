using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Spawn : MonoBehaviour
{
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = new Vector3(Random.Range(0, 5), Random.Range(1, 5), Random.Range(0, 5));
        PhotonNetwork.Instantiate(playerPrefab.name, position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
