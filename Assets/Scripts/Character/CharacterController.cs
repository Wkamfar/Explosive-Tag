using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
public class CharacterController : MonoBehaviour
{ 
    //Game Manager info
    public GameObject gameManager;
    //New Timers
    public TimeManager deathTime;
    public TimeManager flickerTime;
    private const float FLICKER_RATE = 1000;
    private const int MAX_TIME_OFFSET = 15;
    //New Colors
    public ColorManager colorManager;
    //Alt Timers
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
    private bool isAddict;
    private bool isGrappler;
    private bool isPhoenix;
    private bool isDasher;
    private bool isShouter;
    private bool isEngineer;
    private bool isPhantom;
    private bool isSkater;
    private bool isArchitect;

    //For the raycast
    public Transform rayCastShootPoint;
    public GameObject grapplingHook;
    public LayerMask rayCastLayerMask;
    //Grappler info
    public float grapplingSpeed;
    public float grapplingHookShootForce;
    public float maxGrappleDistance;
    //Dasher info
    public float maxDashDistance;
    //Engineer info //send all the information throught the EngineerPortalManagerScript
    private GameObject engineerPortalManager;
    public GameObject portal;
    public GameObject portalOrb;
    public GameObject portalGun;
    public float portalOrbSpeed;
    public int maxPortalUse = 1;
    public Color portal1Color;
    public Color portal2Color;
    //Phantom info
    public GameObject shellBody;
    public GameObject hostBody;
    public float ghostFollowTime;
    public float ghostFormDuration;
    public float maxDistance;
    //skater info
    //for the passive
    private GameObject iceSpawningController;
    private bool inIce;
    public GameObject ice;
    public float spawnIceRate;
    private bool spawnedIce;
    private bool idolSpawnedIce;
    //for the rocket
    private bool directionLocked;
    private bool isRocketBoosting;
    public float rocketDuration;
    public float rocketCooldown;
    public float rocketBoostAmount;
    public float rocketMaxSpeed;
    
    public Transform LookPos { get; private set; }
    //item Crate drops and info
    public GameObject[] items;
    private bool hasItem;
    // Start is called before the first frame update
    void Start()
    {
        deathTime = new TimeManager();
        flickerTime = new TimeManager();
        taggedColorTime = 1 * 1000;
        flashingColorTime = 0.3f * 1000;

        colorManager = new ColorManager();

        //Find all of the ability managers here
        iceSpawningController = GameObject.Find("SkaterPassiveSpawnCap");
        engineerPortalManager = GameObject.Find("EngineerPortalManager");
        engineerPortalManager.GetComponent<EngineerPortalManagerScript>().DefinePlayer();
        gameManager = GameObject.Find("Game Manager");
        playerMod = this.gameObject.transform.Find("PlayerModel");
        //this.view.RPC("GettingTagged", RpcTarget.All, 2001);
        taggerSpeed = speed * 1.1f;
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
        DecidedClass(gameManager.GetComponent<GameManagerScript>().GetConfirmedNumber());
    }
    void CreateTagTimer()
    {// Fix this later
        deathTime.ResetEndTime(minTimeToDeath);
        deathTime.AddRandomOffsetToEndTime(MAX_TIME_OFFSET);
        startTagTimer = currentTagTime; //Update to everyone 
        this.view.RPC("UpdateTagTimer", RpcTarget.All, deathTime.GetEndTime());
        colorManager.StartTimer(deathTime.GetStartTime(), deathTime.GetEndTime());
    }
    [PunRPC]
    public void UpdateTagTimer(float endTime)
    {
        deathTime.UpdateEndTime(endTime);
    }
    [PunRPC]
    void KillPlayer()
    {//Make a spectator mode for this instead
        gameManager.GetComponent<GameManagerScript>().DeathCanvas();
        Destroy(this.gameObject);
        //Turn this back on later
    }
    // Update is called once per frame
    void Update()
    {//Fix this later
        if(deathTime.IsTimeUp() && isTagged)
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
        colorManager.Update();
        if (colorManager.IsFlashing())
        {
            playerModel.gameObject.GetComponent<Renderer>().material.color = flashingColor;
        }
        else
        {
            playerModel.gameObject.GetComponent<Renderer>().material.color = taggedColor;
        }
    }
    private void resetTagTimer()
    {
        tagTimer = 0;
    }
    [PunRPC]
    private void GettingTagged(int id, float endTime, float startTagTime)
    {
        if (isTagged)
        {
            tagTimer = 1;
            Invoke("resetTagTimer", tagTimer);
            isTagged = false;
            colorManager.EndFlashing();
        }
        Debug.Log("CharacterController.GettingTagged: ViewID is " + id);
        Debug.Log("CharacterControler.GettingTagged:" + view.ViewID);
        if(view.ViewID == id)
        {
            deathTime.UpdateEndTime(endTime);
            deathTime.UpdateStartTime(startTagTime);
            isTagged = true;
            colorManager.StartTimer(deathTime.GetStartTime(), deathTime.GetEndTime());
            Debug.Log("CharacterController.GettingTaggged: We made it to this if statement." + isTagged);
        }
    }
    public  void YouAreIt(float timeToDeath, float _startTagTime)
    {//Fix this later
        //this.view.RPC("UpdateTagTimer", RpcTarget.All, timeToDeath + tagTimeAdd);
        this.view.RPC("GettingTagged", RpcTarget.All, view.ViewID, timeToDeath + tagTimeAdd, _startTagTime);
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
            if (!isForward && !isLeft && !isBack && !isRight && !inIce)  currentSpeed = 0;  //disable this if you want sliding for the skater class // make it so that the skater might leave behind a trail of slipper ice, and if you come in contact with it you slide
            Debug.Log("CharacterController.CheckInput: CurrentSpeed is: " + currentSpeed);
            VelocityCheck();
            
            if (!directionLocked) { Direction(); }
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
            col.gameObject.GetComponent<CharacterController>().YouAreIt(deathTime.GetEndTime(), deathTime.GetStartTime());
            //col.gameObject.GetComponent<CharacterController>().isTagged = true;
            //isTagged = false;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("ItemCrate"))
        {
            int randNum = Random.Range(0, items.Length);
            gameManager.GetComponent<GameManagerScript>().ItemCrate(randNum);
            ItemCrate(randNum);
            Destroy(col.gameObject);
        }
        if (col.CompareTag("Ice")) {inIce = true; }
    }
    private void OnTriggerExit(Collider col)
    {
        Debug.Log("CharacterContoller.OnTriggerExit: inIce = " + inIce);
        if (col.CompareTag("Ice")) {inIce = false;  }
    }
    private void DecidedClass(int classNumber)
    {
        if (classNumber == 0) { isAddict = true; }
        else if(classNumber == 1) { isGrappler = true; }
        else if (classNumber == 2) { isPhoenix = true; }
        else if (classNumber == 3) { isDasher = true; }
        else if (classNumber == 4) { isShouter = true; }
        else if (classNumber == 5) { isEngineer = true; }
        else if (classNumber == 6) { isPhantom = true; }
        else if (classNumber == 7) { isSkater = true; }
        else if (classNumber == 8) { isArchitect = true; }
    }
    private void ClassAbilities()
    {
        //Passive abilities
        if (isAddict) { AddictPassive(); }
        //Grappler
        else if (isGrappler) { GrapplerPassive(); }
        //Phoenix
        else if (isPhoenix) { PhoenixPassive(); }
        //Dasher
        else if (isDasher) { DasherPassive(); }
        //Shouter
        else if (isShouter) { ShouterPassive(); }
        //Engineer
        else if (isEngineer) { EngineerPassive(); }
        //Phantom
        else if (isPhantom) { PhantomPassive(); }
        //Skater
        else if (isSkater) { SkaterPassive(); }
        //Architect
        else if (isArchitect) { ArchitectPassive(); }
        //Active abilities
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //Raycast information that can be used in several situations
            rayCastLayerMask = LayerMask.GetMask("Wall");
            RaycastHit hit;
            Vector3 targetPoint;
            Vector3 ray = new Vector3(rayCastShootPoint.position.x, rayCastShootPoint.transform.position.y, rayCastShootPoint.transform.position.z);
            //Addict
            if (isAddict)
            {

            }
            //Grappler
            else if (isGrappler)
            {
                if (Physics.Raycast(ray, rayCastShootPoint.forward, out hit, maxGrappleDistance, rayCastLayerMask))
                    targetPoint = hit.point;
                else //add here an animation that shoots out a grapple and then it fails, use this animation for any failed grapples
                    return;
                //start adding force until you are at the wall
                //Create the grapple hook here
                GameObject currentHook = Instantiate(grapplingHook, rayCastShootPoint.transform.position, Quaternion.identity);
                currentHook.GetComponent<Rigidbody>().AddForce(rayCastShootPoint.forward * grapplingHookShootForce, ForceMode.Impulse);
            }
            //Phoenix
            else if (isPhoenix)
            {

            }
            //Dasher
            else if (isDasher) // rework this class so that it doesn't teleport you into the wall, maybe make a different shoot point for the grappling then the Dash ability, or make it so that the sphere isn't used entirely, and maybe use the visor, but that would be difficult
            {
                if (Physics.Raycast(ray, rayCastShootPoint.forward, out hit, maxDashDistance, rayCastLayerMask))
                    targetPoint = hit.point;
                else
                    targetPoint = new Vector3(lookAtSphere.transform.position.x, lookAtSphere.transform.position.y, lookAtSphere.transform.position.z);
                this.gameObject.transform.position = targetPoint;
            }
            //Shouter
            else if (isShouter)
            {

            }
            //Make the portal de stablize so that the more you use them the closer they get to collasping // make particle effects for the different stages and the portal's collaspe
            //Engineer //make a portal orb with the engineer manager script, also make it so that if you press the button while the other portals are active they will close 
            else if (isEngineer) // this will only spawn portal orbs and EngineerPortalManager will controls the other stuff // ahve the portal take the direction you were walking in when you walked in and boost you out in that direction
            {
                engineerPortalManager.GetComponent<EngineerPortalManagerScript>().SpawnPortalOrb();
            }
            //Phantom
            else if (isPhantom) // activate the phantom form only when the ability gets activated
            {

            }
            //Skater // use the fuel gauge to display how long the rocket is going to last for. 
            else if (isSkater) //you can't cancel it by pressing the ability button again, but it will deactivate after hitting a wall, but you will be stunned for a split second
            {
                directionLocked = true;
                isRocketBoosting = true; //Make the cooldown and everything for this ability when you make the timers
            }
            //Architect
            else if (isArchitect)
            {

            }

        }


    }
    public void Grapple(GameObject currentHook)
    {

    }

    private void AddictPassive()
    {

    }
    private void GrapplerPassive()
    {

    }
    private void PhoenixPassive()
    {

    }
    private void DasherPassive()
    {

    }
    private void ShouterPassive()
    {

    }
    public GameObject GetPortalManager()
    {
        return engineerPortalManager;
    }
    private void EngineerPassive()
    {

    }
    //Maybe the ability is when this ghost form comes out instead of it always being out, and then you teleport to where the body is after the ability times out
    private void PhantomPassive() //The phantom's hitbox follows the mouse, and is chained to a body that moves slowly with wasd, and can also be teleported (if not in a wall to the ghastly hitbox)
    {
        //THis is for the invisiblity, comment this out now, and fix it up later
        /*GameObject visor = GameObject.Find("Visor");
        if(this.GetComponent<Rigidbody>().velocity.magnitude == 0)
        {
            playerModel.GetComponent<MeshRenderer>().enabled = false;
            visor.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            playerModel.GetComponent<MeshRenderer>().enabled = true;
            visor.GetComponent<MeshRenderer>().enabled = true;
        }*/
        //hostBody.SetActive(true);
        float distance;
        float a = lookAtSphere.transform.position.x - shellBody.transform.position.x;
        float b = lookAtSphere.transform.position.z - shellBody.transform.position.z;
        distance = Mathf.Sqrt((a * a + b * b));
        distance = Mathf.Round(distance * 100) / 100;
        if (distance < maxDistance)
        {
            //hostBody.transform.position = Vector3.Lerp(hostBody.transform.position, new Vector3(lookAtSphere.transform.position.x, hostBody.transform.position.y, lookAtSphere.transform.position.z), ghostFollowTime * Time.deltaTime);
            hostBody.transform.position = new Vector3(lookAtSphere.transform.position.x, hostBody.transform.position.y, lookAtSphere.transform.position.z);
        }
        else // make a formula to figure out the angle and split it into 4 quadrants so that you can figure out the total angle
        {
            float c = Mathf.Atan(b/a) / Mathf.PI * 180;
            if (a >= 0 && b >= 0) { } //You are in quadrant 1 and nothing happens
            else if (a < 0 && b > 0) { c += 180; } //You are in quadrant 2 and you have to move the angle
            else if (a <= 0 && b <= 0) { c += 180; } //You are in quadrant 3
            else if (a > 0 && b < 0) { c += 360; } //You are in quadrant 4
            float targetX = Mathf.Cos(c / 180 * Mathf.PI) * maxDistance;
            float targetZ = Mathf.Sin(c / 180 * Mathf.PI) * maxDistance;
            Debug.Log("Character Controller.PhantomPassive: The angle is equal to: " + c);
            Debug.Log("TargetX is: " + targetX + " TargetZ is: " + targetZ);
            hostBody.transform.position = new Vector3(shellBody.transform.position.x + targetX, hostBody.transform.position.y, shellBody.transform.position.z + targetZ);
        }
    }
    private void SkaterPassive() // there are two options, you do it based on movement, only spawn under you when the last despawns when you aren't moving, or only spawn when you have moved a certain distance // the first option is simplier, so I will do the second
    {
        if (this.GetComponent<Rigidbody>().velocity.magnitude == 0)
        {
            if (!spawnedIce)
            {
                SkaterPassive(0);
                spawnIceRate = ice.GetComponent<IceScript>().iceDespawnTime;
            }
            spawnedIce = true;
            idolSpawnedIce = true;
            Invoke("ResetSpawnedIce", spawnIceRate);

        }
        else if (this.GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            if(idolSpawnedIce == true)
            {
                idolSpawnedIce = false;
                spawnedIce = false;
            }
            if (!spawnedIce)
            {
                SkaterPassive(0);
                if (this.GetComponent<Rigidbody>().velocity.magnitude > 10) { spawnIceRate = (ice.GetComponent<IceScript>().iceDespawnTime / this.GetComponent<Rigidbody>().velocity.magnitude); }

                else { spawnIceRate = (ice.GetComponent<IceScript>().iceDespawnTime / 10); }
            }
            spawnedIce = true;
            Invoke("ResetSpawnedIce", spawnIceRate);
        }

    }
    public void SkaterPassive(int i)
    {
        GameObject playerFeet = GameObject.Find("PlayerFeet");
        GameObject currentIce = iceSpawningController.GetComponent<IceSpawningController>().SpawnIce(playerFeet.transform);
        currentIce.GetComponent<IceScript>().DefinePlayer(this.gameObject);
        iceSpawningController.GetComponent<IceSpawningController>().DefinePlayer(this.gameObject);
    }
    public void SkaterPassiveDespawnIce(GameObject currentIce)
    {
        iceSpawningController.GetComponent<IceSpawningController>().DespawnIce(currentIce);
    }
    private void ResetSpawnedIce()
    {
        spawnedIce = false;
    }
    private void ArchitectPassive()
    {

    }
    //For the Item Crate
    private void ItemCrate(int itemNumber) //make the stun gun shoot out a stun bullet that will follow the mouse until it gets to its exact location and explodes to stun
    {
        if (!hasItem)
        {
            items[itemNumber].SetActive(true);
            hasItem = true;
        }
        else
        {

        }
    }

}

