using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MinimapController : MonoBehaviour
{
    private bool foundPlayer;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        /*GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
         foundPlayer = true;
        foreach(GameObject p in players)
        {
            if(PhotonView.Get(p).IsMine)
            {
                //Debug.Log("player was found");
                player = p;
                break;
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (foundPlayer)
        {
            //Debug.Log(player);
            //Debug.Log(player.GetComponent<CharacterController>());
            bool taggedState = player.GetComponent<CharacterController>().TaggedState();
            int layer3 = 1 << 3;
            int layer6 = 1 << 6;
            int layer7 = 1 << 7;
            int layer10 = 1 << 10;
            if (taggedState)
            {
                this.GetComponent<Camera>().cullingMask = (layer3 | layer6 | layer7 | layer10);
            }
            else if (!taggedState)
            {
                this.GetComponent<Camera>().cullingMask = (layer3 | layer6 | layer10);
            }

        }
    }
    public void SearchForPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foundPlayer = true;
        foreach (GameObject p in players)
        {
            if (PhotonView.Get(p).IsMine)
            {
                //Debug.Log("player was found");
                player = p;
                break;
            }
        }
    }
}
