using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public float speed = 1;
    public float xLimit = 30;
    public float yLimit = 20;
    public float zLimit = 38.75f;
    // Start is called before the first frame update
    void Start() // declare what number the player is
    {
        yLimit = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x, yLimit, player.transform.position.z);
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition , Time.deltaTime * speed);
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
