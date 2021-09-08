using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCastingOrbScript : MonoBehaviour
{
    private GameObject player;
    private Transform orbDestination;
    private float orbSpeed;
    Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, orbSpeed * Time.deltaTime); // use the other time function later this is more for a demo and testing purposes
        //Debug.Log("PortalCastingOrbScript.Update: orb's x is " + this.transform.position.x);
        //Debug.Log("PortalCastingOrbScript.Update: orb's z is " + this.transform.position.z);
        //Debug.Log("PortalCastingOrbScript.Update: orb destination's x is " + targetPos.x);
        //Debug.Log("PortalCastingOrbScript.Update: orb destination's z is " + targetPos.z);
        if (new Vector3(Mathf.Round(this.transform.position.x), this.transform.position.y, Mathf.Round(this.transform.position.z)) == targetPos)
        {
            //Debug.Log("PortalCastingOrbScript.Update: the orb has reached it's destination");
            this.transform.position = targetPos;
            player.GetComponent<CharacterController>().SpawnPortal(this.transform);
            Destroy(this.gameObject);
        }
    }

    public void DefineOrb(GameObject _player, Transform _orbDestination, float _orbSpeed)
    {
        player = _player;
        orbDestination = _orbDestination;
        orbSpeed = _orbSpeed;
        targetPos = new Vector3(Mathf.Round(orbDestination.position.x), this.transform.position.y, Mathf.Round(orbDestination.position.z));
    }
    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("PortalCastingOrbScript.OnTriggerEnter: has hit an object");
        this.transform.position = targetPos;
        player.GetComponent<CharacterController>().SpawnPortal(this.transform);
        Destroy(this.gameObject);
    }
}
