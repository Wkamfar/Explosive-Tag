using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CharacterController : MonoBehaviour
{
    public GameObject playerModel;
    public float speed;
    public PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }
    void CheckInput()
    {
        if (view.IsMine)
        {
            bool isForward = Input.GetKey(KeyCode.W);
            bool isLeft = Input.GetKey(KeyCode.A);
            bool isRight = Input.GetKey(KeyCode.D);
            bool isBack = Input.GetKey(KeyCode.S);
            if (isForward) playerModel.GetComponent<Rigidbody>().AddForce(this.transform.forward * speed * 1000 * Time.deltaTime);
            if (isLeft) playerModel.GetComponent<Rigidbody>().AddForce(this.transform.right * -speed * 1000 * Time.deltaTime);
            if (isRight) playerModel.GetComponent<Rigidbody>().AddForce(this.transform.right * speed * 1000 * Time.deltaTime);
            if (isBack) playerModel.GetComponent<Rigidbody>().AddForce(this.transform.forward * -speed * 1000 * Time.deltaTime);
        }
        
    }
}

