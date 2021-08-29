using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookScript : MonoBehaviour
{
    CharacterController characterController;
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
        if (col.CompareTag("Wall"))
        {
            Debug.Log("GrapplingHookScript.OnTriggerEnter: the hook collided");
            characterController.Grapple(this.gameObject);
            
        }
    }
}
