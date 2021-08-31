using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
public class CharacterController : MonoBehaviour
{
    public float minTimeToDeath;
    public float tagTimeAdd;
    private float currentTagTime;
    public float taggedColorTime;
    public float flashingColorTime;
    public float flashingStartTime;
    private float MIN_FLASHING_TIME = .25f * 1000;
    public float flashingWindow = 15 * 1000;
    private float startTagTimer;

    public GameObject lookAtSphere;
    public GameObject playerModel;
    public float speed;
    private float taggerSpeed;
    private float currentSpeed;
    public PhotonView view;
    private bool isTagged;
    private float tagTimer;
    public Color taggedColor;
    public Color flashingColor;
    public Color nonTaggedColor;
    public RawImage playerMinimapIcon;
    public GameObject iconCanvas;

    private Vector2 mouseLocation;

    private Transform playerMod;
    private Quaternion syncRot;

    //Class Booleans
    private bool isDasher;
    private bool isGrappler;
    private bool isShouter;
    private bool isPhantom;
    private bool isArchitect;
    private bool isSkater;
    private bool isEngineer;
    private bool isPhoenix;
    private bool isAddict;

    //For the raycast
    public Transform rayCastShootPoint;
    public GameObject grapplingHook;
    //Dasher info
    public float maxDashDistance;
    //Grappler info
    public float grapplingHookShootForce;
    public Transform LookPos { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        playerMod = this.gameObject.transform.Find("PlayerModel");
        //this.view.RPC("GettingTagged", RpcTarget.All, 2001);
        taggerSpeed = speed * 1.1f;
        taggedColorTime = 1 * 1000;
        flashingColorTime = 0.3f * 1000;
        view = GetComponent<PhotonView>();
        Debug.Log("CharacterController.Start" + playerMod);
        if (view.ViewID == 1001)
        {//Change Later When Tagger is Chosen Randomly
            CreateTagTimer();
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
    void CreateTagTimer()
    {// Fix this later
        float randDeathNum = Random.Range(0, 15);
        currentTagTime = PhotonNetwork.ServerTimestamp;
        minTimeToDeath += currentTagTime;
        minTimeToDeath += randDeathNum * 1000;
        startTagTimer = currentTagTime; //Update to everyone 
        this.view.RPC("UpdateTagTimer", RpcTarget.All, minTimeToDeath);
    }
    [PunRPC]
    public void UpdateTagTimer(float timeToDeath)
    {
        minTimeToDeath = timeToDeath;
    }
    [PunRPC]
    void KillPlayer()
    {//Make a spectator mode for this instead
        Destroy(this.gameObject);
        //Turn this back on later
    }
    // Update is called once per frame
    void Update()
    {//Fix this later
        currentTagTime = PhotonNetwork.ServerTimestamp;
        if(currentTagTime >= minTimeToDeath && isTagged)
        {
            this.view.RPC("KillPlayer", RpcTarget.All);
        }
        CheckInput();
        ClassAbilities();
        Debug.Log("CharacterController.Update: Tag Status:" + view.ViewID + isTagged);
        if (isTagged == true)
        {
            currentSpeed = taggerSpeed;
            playerMinimapIcon.color = taggedColor;
            FlashingColors();
        }
        else if (isTagged == false)
        {//If tagged is false reset FlashingColors timers?
            playerModel.gameObject.GetComponent<Renderer>().material.color = nonTaggedColor;
            currentSpeed = speed;
            playerMinimapIcon.color = nonTaggedColor;
        }
    }
    void FlashingColors()
    {
        float timeDifference = PhotonNetwork.ServerTimestamp - flashingStartTime;
        if (timeDifference < taggedColorTime)
        {
            playerModel.gameObject.GetComponent<Renderer>().material.color = taggedColor;
        }
        else if (timeDifference < (flashingColorTime + taggedColorTime))
        {
            playerModel.gameObject.GetComponent<Renderer>().material.color = flashingColor;
        }
        else 
        {
            Debug.Log("CharacterController.FlashingColors: Timer got reset");
            flashingStartTime = PhotonNetwork.ServerTimestamp;
            
        } 
        float percentageTimeLeft = (currentTagTime - minTimeToDeath) / (startTagTimer - minTimeToDeath);
        taggedColorTime = taggedColorTime < MIN_FLASHING_TIME ? MIN_FLASHING_TIME : 1 * 1000 * percentageTimeLeft;
        flashingColorTime = flashingColorTime < MIN_FLASHING_TIME ? MIN_FLASHING_TIME : .3f * 1000 * percentageTimeLeft;

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
            tagTimer = 1;
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
    public  void YouAreIt(float timeToDeath)
    {//Fix this later
        this.view.RPC("UpdateTagTimer", RpcTarget.All, timeToDeath + tagTimeAdd);
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
    private void OnCollisionEnter(Collision col)
    {
        if (!view.IsMine) return;
        if (col.gameObject.CompareTag("Player") && isTagged && col.gameObject.GetComponent<CharacterController>().tagTimer <= 0)
        {
            this.view.RPC("GettingTagged", RpcTarget.All, col.gameObject.GetComponent<PhotonView>().ViewID);
            this.transform.position = new Vector3(Random.Range(-48, 48), 1, Random.Range(-48, 48));
            col.gameObject.GetComponent<CharacterController>().YouAreIt(minTimeToDeath);
            //col.gameObject.GetComponent<CharacterController>().isTagged = true;
            //isTagged = false;
        }
    }
    private void ClassAbilities()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //Dasher
            RaycastHit hit;
            Vector3 targetPoint;
            Vector3 ray = new Vector3(rayCastShootPoint.position.x, rayCastShootPoint.transform.position.y, rayCastShootPoint.transform.position.z);
            if (Physics.Raycast(ray, rayCastShootPoint.forward, out hit, maxDashDistance))
                targetPoint = hit.point;
            else
                targetPoint = new Vector3(lookAtSphere.transform.position.x, lookAtSphere.transform.position.y, lookAtSphere.transform.position.z);
            this.gameObject.transform.position = targetPoint;
            //Grappler

            //Addict

            //Shouter

            //Phoenix

            //Phantom

            //Skater

            //Architect

            //Engineer
        }


    }
    public void Grapple(GameObject currentHook)
    {
        this.gameObject.transform.position = currentHook.transform.position;
        Debug.Log("CharacterController.Grapple: Grapple Complete");
    }
}

