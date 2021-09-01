using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class MinimapScript : MonoBehaviour
{
    private bool foundPlayer;

    public GameObject terrain;
    public RawImage cameraIndicator;
    public RawImage minimapImage;
    private float floorX;
    private float floorZ;
    private float cameraX;
    private float cameraY;
    private float minimapX;
    private float minimapY;
    public GameObject player;
    public GameObject playerIcon;
    public GameObject playerSightIdentifier;
    private float xLocation;
    private float yLocation;
    private float xLimit;
    private float yLimit;
    public GameObject minimap;
    void Start()
    {
        /*GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
         * foundPlayer = true;
        foreach (GameObject p in players)
        {
            if (PhotonView.Get(p).IsMine)
            {
                player = p;
                break;
            }
        }
        floorX = terrain.GetComponent<Renderer>().bounds.size.x;
        floorZ = terrain.GetComponent<Renderer>().bounds.size.z;
        cameraX = cameraIndicator.GetComponent<RectTransform>().rect.width;
        cameraY = cameraIndicator.GetComponent<RectTransform>().rect.height;
        minimapX = minimap.GetComponent<RectTransform>().rect.width;
        minimapY = minimap.GetComponent<RectTransform>().rect.height;
        xLimit = (minimapX / 2) - (cameraX / 2);
        yLimit = (minimapY / 2) - (cameraY / 2);
        //xLimit = */
    }
    void Update()
    {
        if (foundPlayer)
        {
            //For the player Icon Tracking
            xLocation = (player.transform.position.x / (floorX / 2)) * (minimapX / 2);
            yLocation = (player.transform.position.z / (floorZ / 2)) * (minimapY / 2);
            playerIcon.transform.position = new Vector3(xLocation, yLocation) + minimap.transform.position;
            //For the Camera
            playerSightIdentifier.transform.position = playerIcon.transform.position;
            if (playerSightIdentifier.transform.localPosition.x > xLimit)
                playerSightIdentifier.transform.localPosition = new Vector3(xLimit, playerSightIdentifier.transform.localPosition.y);
            else if (playerSightIdentifier.transform.localPosition.x < -xLimit)
                playerSightIdentifier.transform.localPosition = new Vector3(-xLimit, playerSightIdentifier.transform.localPosition.y);
            if (playerSightIdentifier.transform.localPosition.y > yLimit)
                playerSightIdentifier.transform.localPosition = new Vector3(playerSightIdentifier.transform.localPosition.x, yLimit);
            else if (playerSightIdentifier.transform.localPosition.y < -yLimit)
                playerSightIdentifier.transform.localPosition = new Vector3(playerSightIdentifier.transform.localPosition.x, -yLimit, playerSightIdentifier.transform.position.z);

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
                player = p;
                break;
            }
        }
        floorX = terrain.GetComponent<Renderer>().bounds.size.x;
        floorZ = terrain.GetComponent<Renderer>().bounds.size.z;
        cameraX = cameraIndicator.GetComponent<RectTransform>().rect.width;
        cameraY = cameraIndicator.GetComponent<RectTransform>().rect.height;
        minimapX = minimap.GetComponent<RectTransform>().rect.width;
        minimapY = minimap.GetComponent<RectTransform>().rect.height;
        xLimit = (minimapX / 2) - (cameraX / 2);
        yLimit = (minimapY / 2) - (cameraY / 2);
        //xLimit = 
    }
}
