using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    GameObject player;
    GameObject portalManager;
    int portalNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            TeleportPlayer(col.gameObject, portalNumber);
        }
    }
    public void GetInfo(GameObject _player, int _portalNumber)
    {
        player = _player;
        portalManager = player.GetComponent<CharacterController>().GetPortalManager();
        portalNumber = _portalNumber;
    }
    private void TeleportPlayer(GameObject _player, int _portalNumber)
    {
        portalManager.GetComponent<EngineerPortalManagerScript>().TeleportPlayer(_player, _portalNumber);
    }
}
