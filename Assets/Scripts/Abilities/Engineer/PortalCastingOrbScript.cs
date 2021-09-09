using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PortalCastingOrbScript : MonoBehaviour
{
    private GameObject portalManager;
    private Vector3 targetPos;
    private float orbSpeed;
    private GameObject portalOrb;

    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, orbSpeed * Time.deltaTime);
        if(new Vector3(Mathf.Round(this.transform.position.x), targetPos.y, Mathf.Round(this.transform.position.z)) == targetPos)
        {
            this.transform.position = new Vector3(targetPos.x, .625f, targetPos.z);
            SpawnPortal();
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (!col.CompareTag("Player"))
        {
            SpawnPortal();
        }
        
    }
    public void GetTarget(Vector3 _targetPos, GameObject player)
    {
        targetPos = _targetPos;
        orbSpeed = player.GetComponent<CharacterController>().portalOrbSpeed;
        portalManager = player.GetComponent<CharacterController>().GetPortalManager();
    }
    private void SpawnPortal()
    {
        portalManager.GetComponent<EngineerPortalManagerScript>().SpawnPortal(this.transform);
        portalManager.GetComponent<EngineerPortalManagerScript>().DestroyPortalOrb(this.gameObject);

    }
}
