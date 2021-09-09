using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EngineerPortalManagerScript : MonoBehaviour
{ // Make some restrictions on portal spawning later so that it doesn't cause too many bugs.
    public GameObject[] Portals;
    private Color[] portalColors;
    private GameObject[] portalOrbs;
    private int portalCount;
    private int portalOrbCount;
    private const int MAX_PORTALS = 2;
    private int maxTeleports;
    private int teleportCount;
    private bool canSpawnOrb;
    private bool portalsLinked;
    private GameObject player;
    private bool foundPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Portals = new GameObject[MAX_PORTALS];
        portalColors = new Color[MAX_PORTALS];
    }

    // Update is called once per frame
    void Update()
    {
        if (foundPlayer)
        {
            canSpawnOrb = (portalCount + portalOrbCount) < MAX_PORTALS ? true : false;
            if (teleportCount == maxTeleports) { DestroyAllPortals(); }
            portalsLinked = portalCount == MAX_PORTALS ? true : false;
        }
    }

    public void SpawnPortalOrb()
    {
        if (canSpawnOrb)
        {
            GameObject portalOrb = player.GetComponent<CharacterController>().portalOrb;
            Transform spawnPos = player.GetComponent<CharacterController>().portalGun.transform;
            GameObject lookAtSphere = player.GetComponent<CharacterController>().lookAtSphere;
            Vector3 targetPos = new Vector3(Mathf.Round(lookAtSphere.transform.position.x), portalOrb.transform.position.y, Mathf.Round(lookAtSphere.transform.position.z));
            GameObject currentPortalOrb = Instantiate(portalOrb, spawnPos.position, Quaternion.identity);
            currentPortalOrb.GetComponent<PortalCastingOrbScript>().GetTarget(targetPos, player);
            currentPortalOrb.GetComponent<Renderer>().material.color = portalColors[portalCount + portalOrbCount];
            portalOrbCount++;
        }
        else if(portalCount == MAX_PORTALS) { DestroyAllPortals(); }

    }
    public void DestroyPortalOrb(GameObject _portalOrb)
    {
        Destroy(_portalOrb);
        portalOrbCount--;
    }
    public void SpawnPortal(Transform spawnPos)
    {
        GameObject portal = player.GetComponent<CharacterController>().portal;
        GameObject currentPortal = Instantiate(portal, spawnPos.position, Quaternion.identity);
        Portals[portalCount] = currentPortal;
        currentPortal.GetComponent<Renderer>().material.color = portalColors[portalCount];
        currentPortal.GetComponent<PortalScript>().GetInfo(player, portalCount);
        portalCount++;
    }
    public void DestroyAllPortals()
    {
        for(int i = 0; i < MAX_PORTALS; i++) { Destroy(Portals[i]); }
        portalCount = 0;
        teleportCount = 0;
    }
    public void DefinePlayer()
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
        portalColors[0] = player.GetComponent<CharacterController>().portal1Color;
        portalColors[1] = player.GetComponent<CharacterController>().portal2Color;

        maxTeleports = player.GetComponent<CharacterController>().maxPortalUse;
    }
    public void TeleportPlayer(GameObject _player, int _portalNumber)
    {
        if (portalsLinked )
        {
            int teleportToNum = _portalNumber == 0 ? 1 : 0;
            _player.transform.position = new Vector3(Portals[teleportToNum].transform.position.x, _player.transform.position.y, Portals[teleportToNum].transform.position.z);
            Rigidbody rigidbody = _player.GetComponent<Rigidbody>();
            if (rigidbody.velocity.x > 0) { _player.transform.position = new Vector3(_player.transform.position.x + 1.25f, _player.transform.position.y, _player.transform.position.z); }
            else if (rigidbody.velocity.x < 0) { _player.transform.position = new Vector3(_player.transform.position.x - 1.25f, _player.transform.position.y, _player.transform.position.z); }
            if (rigidbody.velocity.z > 0) { _player.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z + 1.25f); }
            else if (rigidbody.velocity.z < 0) { _player.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z - 1.25f); }
            Debug.Log("EngineerPortalManagerScript.TeleportPlayer: the player's velocity is: " + _player.GetComponent<Rigidbody>().velocity);
            teleportCount++;
        }
    }
}
