using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CharacterController : MonoBehaviour
{
    public GameObject playerModel;
    public float speed;
    public PhotonView view;
    private bool isTagged;
    private float tagTimer;
    public Color taggedColor;
    public Color nonTaggedColor;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        Debug.Log("CharacterController.Start" + view.ViewID);
        if (view.ViewID == 1001)
        {
            isTagged = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        if (isTagged == true)
        {
            this.gameObject.GetComponent<Renderer>().material.color = taggedColor;
        }
        else if (isTagged == false)
        {
            this.gameObject.GetComponent<Renderer>().material.color = nonTaggedColor;
        }
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
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player") && isTagged && col.gameObject.GetComponent<CharacterController>().tagTimer <= 0)
        {
            col.gameObject.GetComponent<CharacterController>().isTagged = true;
            isTagged = false;
            tagTimer = 3;
            Invoke("resetTagTimer", tagTimer);
        }
    }
    private void resetTagTimer()
    {
        tagTimer = 0;
    }
}

