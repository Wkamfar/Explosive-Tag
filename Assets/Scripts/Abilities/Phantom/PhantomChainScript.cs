using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhantomChainScript : MonoBehaviour
{
    private bool foundPlayer;
    private GameObject player;
    private GameObject phantomBody;
    private float chainSpeed;
    private GameObject phantomChainManager;
    private GameObject[] chains;
    private int chainNumber;
    private float maxChainDistance;
    private float maxChains;
    private float recallSpeed;
    private bool phantomActive = true;
    private int lowestMaxChainNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (foundPlayer) //use lerp to get the chains in the right position
        {
            Debug.Log("PhantomChainScript: The chain number is: " + chainNumber);
            phantomBody = player.GetComponent<CharacterController>().hostBody;
            chains = phantomChainManager.GetComponent<PhantomChainControllerScript>().GetChainArray();
            Vector3 firstChainPos = phantomChainManager.GetComponent<PhantomChainControllerScript>().firstChainPos;
            Vector3 lastChainPos = phantomChainManager.GetComponent<PhantomChainControllerScript>().lastChainPos;
            float x = this.transform.position.x;
            float y = this.transform.position.z;
            if (maxChains > lowestMaxChainNumber + 2)
            {
                if (chainNumber == 0 && phantomActive)
                {
                    /*float x1 = phantomBody.transform.position.x;
                    float y1 = phantomBody.transform.position.z;
                    float a = x1 - x;
                    float b = y1 - y;
                    float distance = Mathf.Sqrt((a * a + b * b));
                    if (distance > maxChainDistance)
                    {
                        this.transform.position = Vector3.Lerp(new Vector3(x, this.transform.position.y, y), new Vector3(x1, this.transform.position.y, y1), chainSpeed * Time.deltaTime);
                        //this.transform.position = phantomBody.transform.position;
                    }
                    //Debug.Log("PhantomChainScript.Update: this is triggering 2");*/
                    this.transform.position = firstChainPos;
                }
                else if (chainNumber > 0 && chainNumber < maxChains - 1)
                {
                    float x1 = chains[chainNumber - 1].transform.position.x;
                    float y1 = chains[chainNumber - 1].transform.position.z;
                    float a = x1 - x;
                    float b = y1 - y;
                    float distance = Mathf.Sqrt((a * a + b * b));
                    if (distance > maxChainDistance)
                    {
                        this.transform.position = Vector3.Lerp(new Vector3(x, this.transform.position.y, y), new Vector3(x1, this.transform.position.y, y1), chainSpeed * Time.deltaTime);
                    }
                    //Debug.Log("PhantomChainScript.Update: this is triggering 3");
                }
                if (chainNumber == maxChains - 1 && phantomActive)
                {
                    /*float x1 = player.transform.position.x;
                    float y1 = player.transform.position.z;
                    float a = x1 - x;
                    float b = y1 - y;
                    float distance = Mathf.Sqrt((a * a + b * b));
                    if (distance > maxChainDistance)
                    {
                        this.transform.position = Vector3.Lerp(new Vector3(x, this.transform.position.y, y), new Vector3(x1, this.transform.position.y, y1), chainSpeed * Time.deltaTime);
                        //this.transform.position = player.transform.position;
                    }*/
                    this.transform.position = lastChainPos;
                }
                else if (chainNumber == maxChains - 1 && !phantomActive)
                {
                    float x1 = player.transform.position.x;
                    float y1 = player.transform.position.z;
                    float a = x1 - x;
                    float b = y1 - y;
                    float distance = Mathf.Sqrt((a * a + b * b));
                    if (distance > maxChainDistance)
                    {
                        this.transform.position = Vector3.Lerp(new Vector3(x, this.transform.position.y, y), new Vector3(x1, this.transform.position.y, y1), chainSpeed * Time.deltaTime);
                        
                        //this.transform.position = player.transform.position;
                    }
                }
                else if (chainNumber < maxChains - 1 && chainNumber > 0 && chainNumber < chains.Length - 1)
                {
                    float x1 = chains[chainNumber + 1].transform.position.x;
                    float y1 = chains[chainNumber + 1].transform.position.z;
                    float a = x1 - x;
                    float b = y1 - y;
                    float distance = Mathf.Sqrt((a * a + b * b));
                    if (distance > maxChainDistance)
                    {
                        this.transform.position = Vector3.Lerp(new Vector3(x, this.transform.position.y, y), new Vector3(x1, this.transform.position.y, y1), chainSpeed * Time.deltaTime);
                    }
                }

                if (!phantomActive)
                {
                    float x1 = player.transform.position.x;
                    float y1 = player.transform.position.z;
                    float a = x1 - x;
                    float b = y1 - y;
                    float distance = Mathf.Sqrt((a * a + b * b));
                    if (Mathf.Round(distance) <= maxChainDistance)
                    {
                        phantomChainManager.GetComponent<PhantomChainControllerScript>().LowerMaxChainAll();
                        phantomChainManager.GetComponent<PhantomChainControllerScript>().DestroySpecificChain(this.gameObject);
                    }

                }
                if (!phantomActive && chainNumber == 0)
                {
                    float x1 = chains[chainNumber + 1].transform.position.x;
                    float y1 = chains[chainNumber + 1].transform.position.z;
                    float x2 = phantomBody.transform.position.x;
                    float y2 = phantomBody.transform.position.z;
                    float a = x1 - x;
                    float b = y1 - y;
                    float a1 = x2 - x;
                    float b1 = y2 - y;
                    float distance = Mathf.Sqrt((a * a + b * b));
                    float phantomDistance = Mathf.Sqrt((a1 * a1 + b1 * b1));
                    if (distance > maxChainDistance)
                    {
                        this.transform.position = Vector3.Lerp(new Vector3(x, this.transform.position.y, y), new Vector3(x1, this.transform.position.y, y1), chainSpeed * Time.deltaTime);
                    }
                    phantomBody.transform.position = this.transform.position;
                    this.gameObject.GetComponent<Renderer>().enabled = false;
                }
            }


            if (maxChains == lowestMaxChainNumber + 2)
            {
                float x1 = player.transform.position.x;
                float y1 = player.transform.position.z;
                float x2 = phantomBody.transform.position.x;
                float y2 = phantomBody.transform.position.z;
                float a = x1 - x;
                float b = y1 - y;
                float a1 = x2 - x;
                float b1 = y2 - y;
                float playerDistance = Mathf.Sqrt((a * a + b * b));
                float phantomDistance = Mathf.Sqrt((a1 * a1 + b1 * b1));
                if (playerDistance > maxChainDistance)
                {
                    this.transform.position = Vector3.Lerp(new Vector3(x, this.transform.position.y, y), new Vector3(x1, this.transform.position.y, y1), chainSpeed * Time.deltaTime);
                }
                if (phantomDistance > maxChainDistance)
                {
                    player.GetComponent<CharacterController>().hostBody.transform.position = Vector3.Lerp(new Vector3(x, this.transform.position.y, y), new Vector3(x2, this.transform.position.y, y2), chainSpeed * Time.deltaTime);
                }
                if (Mathf.Round(playerDistance) <= maxChainDistance)
                {
                    player.GetComponent<CharacterController>().hostBody.SetActive(false);
                    player.GetComponent<CharacterController>().isRecallingPhantom = false;
                    phantomChainManager.GetComponent<PhantomChainControllerScript>().EmptyChainArray();
                    phantomChainManager.GetComponent<PhantomChainControllerScript>().DestroySpecificChain(this.gameObject);
                }
            }



        }
    }
    public void DeactivateChains()
    {
        phantomActive = false;
        chainSpeed = recallSpeed;
    }
    public void LowerMaxChainLocal(int _lowestMaxChainNumber)
    {
        maxChains--;
        lowestMaxChainNumber = _lowestMaxChainNumber;
        Debug.Log("PhantomChain max chain Number is: " + maxChains);
    }
    public void SetChainNumber(int _chainNumber)
    {
        chainNumber = _chainNumber;
    }
    public void IncreaseChainNumber()
    {
        chainNumber++;
    }
    public void DecreaseChainNumber()
    {
        chainNumber--;
    }
    public void DefinePlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foundPlayer = true;
        foreach (GameObject p in players)
        {
            if (PhotonView.Get(p).IsMine)
            {
                player = p;
                break;
            }
        }
        
        chainSpeed = player.GetComponent<CharacterController>().chainSpeed;
        phantomChainManager = player.GetComponent<CharacterController>().GetPhantomChainManager();
        //maxChainDistance = player.GetComponent<CharacterController>().maxDistance / (player.GetComponent<CharacterController>().maxChains);
        maxChainDistance = 1;
        maxChains = player.GetComponent<CharacterController>().maxChains;
        recallSpeed = player.GetComponent<CharacterController>().phantomRecallSpeed;
    }
    }
