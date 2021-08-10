using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionSphereScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(mousePosition);
        this.transform.position = cursorPos;
    }
}
