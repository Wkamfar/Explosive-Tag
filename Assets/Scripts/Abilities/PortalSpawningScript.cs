using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawningScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() // copy all of the variables from characterController that relate to the spanwing of a portal
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnPortal(GameObject _portal, Transform spawnLocation) //run all of the portal item spawning through this script instead of running it through charactercontroler because that lightens the load
    {
        spawnLocation.position = new Vector3(spawnLocation.position.x, .05f, spawnLocation.position.z);
        GameObject currentPortal = Instantiate(_portal, spawnLocation.position, Quaternion.identity);
    } 
}
