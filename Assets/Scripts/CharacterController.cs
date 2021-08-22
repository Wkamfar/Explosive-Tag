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

    private Transform playerMod;
    private Quaternion syncRot;

    public Transform LookPos { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        playerMod = this.gameObject.transform.Find("PlayerModel");
        //this.view.RPC("GettingTagged", RpcTarget.All, 2001);
        taggerSpeed = speed * 1.5f;
        view = GetComponent<PhotonView>();
        Debug.Log("CharacterController.Start" + playerMod);
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
        Debug.Log("CharacterController.Update: Tag Status:" + view.ViewID + isTagged);
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
            this.view.RPC("UpdatePlayerModelRotation", RpcTarget.All, playerMod.rotation);
            //RotationCheck();
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
    /*void RotationCheck()
    {
        Vector3 rotation = new Vector3(playerMod.rotation.x, playerMod.rotation.y, playerMod.rotation.z);
        if (view.IsMine)
        {
            this.view.RPC("UpdatePlayerModelRotation", RpcTarget.All, rotation);
            //public void UpdatePlayerModelRotation(Vector3 rotation)
        }
        else
        {
            playerMod.rotation = Quaternion.Lerp(playerMod.rotation, syncRot, 0.1f);
        }
    }*/
    private void OnCollisionEnter(Collision col)
    {
        if (!view.IsMine) return;
        if (col.gameObject.CompareTag("Player") && isTagged && col.gameObject.GetComponent<CharacterController>().tagTimer <= 0)
        {
            this.view.RPC("GettingTagged", RpcTarget.All, col.gameObject.GetComponent<PhotonView>().ViewID);
            col.gameObject.GetComponent<CharacterController>().YouAreIt();
            //col.gameObject.GetComponent<CharacterController>().isTagged = true;
            //isTagged = false;
        }
    }
    private void resetTagTimer()
    {
        tagTimer = 0;
    }
    [PunRPC]
    private void GettingTagged(int id)
    {
        if (isTagged)
        {
            tagTimer = 3;
            Invoke("resetTagTimer", tagTimer);
            isTagged = false;
        }
        Debug.Log("CharacterController.GettingTagged: ViewID is " + id);
        Debug.Log("CharacterControler.GettingTagged:" + view.ViewID);
        if(view.ViewID == id)
        {
            isTagged = true;
            Debug.Log("CharacterController.GettingTaggged: We made it to this if statement." + isTagged);
        }
    }
    public  void YouAreIt()
    {
        this.view.RPC("GettingTagged", RpcTarget.All, view.ViewID);
    }
    public bool TaggedState()
    {
        return isTagged;
    }
    [PunRPC]
    public void UpdatePlayerModelRotation(Quaternion rotation)
    {
        //Debug.Log("CharacterController.UpdatePlayerModelRotation");
        Transform playerModel = this.gameObject.transform.Find("PlayerModel");
        //Quaternion rot = new Quaternion(rotation.x, rotation.y, rotation.z, 1);
        playerModel.rotation = Quaternion.Lerp(playerModel.rotation, rotation, 0.1f);
        //playerModel.rotation = rot;
        //syncRot = rot;
    }
}

