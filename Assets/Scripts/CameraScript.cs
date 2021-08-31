using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraScript : MonoBehaviour
{
    private bool foundPlayer;
    public GameObject player;
    public float speed = 1;
    public float xLimit = 30;
    public float yLimit = 20;
    public float zLimit = 38.75f;
    // Start is called before the first frame update
    void Start() // declare what number the player is
    {
        /*yLimit = this.transform.position.y;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foundPlayer = true;
        foreach(GameObject p in players)
        {
            if (PhotonView.Get(p).IsMine)
            {
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
            Vector3 targetPosition = new Vector3(player.transform.position.x, yLimit, player.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * speed);
            if (this.transform.position.x > xLimit)
                this.transform.position = new Vector3(xLimit, this.transform.position.y, this.transform.position.z);
            else if (this.transform.position.x < -xLimit)
                this.transform.position = new Vector3(-xLimit, this.transform.position.y, this.transform.position.z);
            if (this.transform.position.z > zLimit)
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, zLimit);
            else if (this.transform.position.z < -zLimit)
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -zLimit);
        }
    }
    public void SearchForPlayer()
    {
        yLimit = this.transform.position.y;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foundPlayer = true;
        foreach (GameObject p in players)
        {
            if (PhotonView.Get(p).IsMine)
            {
                player = p;
                break;
            }
        }
    }
}
