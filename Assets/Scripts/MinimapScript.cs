using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    public GameObject player;
    public GameObject playerIcon;
    public GameObject playerSightIdentifier;
    private float xLocation;
    private float yLocation;
    public float xLimit;
    public float yLimit;
    public Vector2 centerOfMinimap;
    void Update()
    {
        //For the player Icon Tracking
        xLocation = player.transform.position.x;
        yLocation = player.transform.position.z;
        playerIcon.transform.position = new Vector2(xLocation, yLocation) + centerOfMinimap;
        //For the Camera
        playerSightIdentifier.transform.position = playerIcon.transform.position;
        if (playerSightIdentifier.transform.localPosition.x > xLimit)
            playerSightIdentifier.transform.localPosition =  new Vector3(xLimit, playerSightIdentifier.transform.localPosition.y);
        else if (playerSightIdentifier.transform.localPosition.x < -xLimit)
            playerSightIdentifier.transform.localPosition =  new Vector3(-xLimit, playerSightIdentifier.transform.localPosition.y);
        if (playerSightIdentifier.transform.localPosition.y > yLimit)
            playerSightIdentifier.transform.localPosition =  new Vector3(playerSightIdentifier.transform.localPosition.x, yLimit);
        else if (playerSightIdentifier.transform.localPosition.y < -yLimit)
            playerSightIdentifier.transform.localPosition =  new Vector3(playerSightIdentifier.transform.localPosition.x, -yLimit);
       
    }
}
