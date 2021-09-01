using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class DirectionSphereScript : MonoBehaviour
{
    public PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        //view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 18;
            Vector3 cursorPos = Camera.main.ScreenToWorldPoint(mousePosition);
            this.transform.position = cursorPos;
        }

    }
}
