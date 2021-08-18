using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
public class CharacterController : MonoBehaviour
{
    public GameObject lookAtSphere;
    public GameObject playerModel;
    public float speed;
    private float taggerSpeed;
    private float currentSpeed;
    public PhotonView view;
    private bool isTagged;
    private float tagTimer;
    public Color taggedColor;
    public Color nonTaggedColor;
    public RawImage playerMinimapIcon;
    public GameObject iconCanvas;

    private Vector2 mouseLocation;

    public Transform LookPos { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        //this.view.RPC("GettingTagged", RpcTarget.All, 2001);
        taggerSpeed = speed * 1.5f;
        view = GetComponent<PhotonView>();
        Debug.Log("CharacterController.Start" + view.ViewID);
        if (view.ViewID == 1001)
        {
            isTagged = true;
        }
        if (view.IsMine)
        {
            iconCanvas.gameObject.layer = LayerMask.NameToLayer("MyIcon");
        }
        else
        {
            iconCanvas.gameObject.layer = LayerMask.NameToLayer("OtherIcon");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        if (isTagged == true)
        {
            playerModel.gameObject.GetComponent<Renderer>().material.color = taggedColor;
            currentSpeed = taggerSpeed;
            playerMinimapIcon.color = taggedColor;
        }
        else if (isTagged == false)
        {
            playerModel.gameObject.GetComponent<Renderer>().material.color = nonTaggedColor;
            currentSpeed = speed;
            playerMinimapIcon.color = nonTaggedColor;
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
            if (isForward) this.GetComponent<Rigidbody>().AddForce(this.transform.forward * currentSpeed * 1000 * Time.deltaTime);
            if (isLeft) this.GetComponent<Rigidbody>().AddForce(this.transform.right * -currentSpeed * 1000 * Time.deltaTime);
            if (isRight) this.GetComponent<Rigidbody>().AddForce(this.transform.right * currentSpeed * 1000 * Time.deltaTime);
            if (isBack) this.GetComponent<Rigidbody>().AddForce(this.transform.forward * -currentSpeed * 1000 * Time.deltaTime);
            if (!isForward && !isLeft && !isBack && !isRight) currentSpeed = 0;
            VelocityCheck();
            Direction();
        }
        
    }
    void VelocityCheck()
    {
        Rigidbody rigidbody = this.gameObject.GetComponent<Rigidbody>();
        if (rigidbody.velocity.magnitude > currentSpeed)
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, currentSpeed);
    }
    void Direction()
    {
        Vector3 lookPos = new Vector3(lookAtSphere.transform.position.x, this.transform.position.y, lookAtSphere.transform.position.z);
        playerModel.transform.LookAt(lookPos);
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player") && isTagged && col.gameObject.GetComponent<CharacterController>().tagTimer <= 0)
        {
            this.view.RPC("GettingTagged", RpcTarget.All, col.gameObject.GetComponent<PhotonView>().ViewID);
            //col.gameObject.GetComponent<CharacterController>().isTagged = true;
            isTagged = false;
            tagTimer = 3;
            Invoke("resetTagTimer", tagTimer);
        }
    }
    private void resetTagTimer()
    {
        tagTimer = 0;
    }
    [PunRPC]
    private void GettingTagged(int id)
    {
        Debug.Log("CharacterController.GettingTagged: ViewID is " + view.ViewID);
        if(view.ViewID == id)
        {
            isTagged = true;
        }
    }
    public bool TaggedState()
    {
        return isTagged;
    }
}

