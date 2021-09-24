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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (foundPlayer) //use lerp to get the chains in the right position
        {
            phantomBody = player.GetComponent<CharacterController>().hostBody;
            chains = phantomChainManager.GetComponent<PhantomChainControllerScript>().GetChainArray();
            float x = this.transform.position.x;
            float y = this.transform.position.z;
            if (chainNumber == 0)
            {
                float x1 = phantomBody.transform.position.x;
                float y1 = phantomBody.transform.position.z;
                float a = x1 - x;
                float b = y1 - y;
                float distance = Mathf.Sqrt((a * a + b * b));
                if (distance > maxChainDistance)
                {
                    //this.transform.position = Vector3.Lerp(new Vector3(x, this.transform.position.y, y), new Vector3(x1, this.transform.position.y, y1), chainSpeed * Time.deltaTime);
                    this.transform.position = phantomBody.transform.position;
                }
                Debug.Log("PhantomChainScript.Update: this is triggering 2");
            }
            else if (chainNumber > 0)
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
            if (chainNumber == chains.Length - 1)
            {
                float x1 = player.transform.position.x;
                float y1 = player.transform.position.z;
                float a = x1 - x;
                float b = y1 - y;
                float distance = Mathf.Sqrt((a * a + b * b));
                if (distance > maxChainDistance)
                {
                    //this.transform.position = Vector3.Lerp(new Vector3(x, this.transform.position.y, y), new Vector3(x1, this.transform.position.y, y1), chainSpeed * Time.deltaTime);
                    this.transform.position = player.transform.position;
                }
            }
            else if (chainNumber < chains.Length - 1 && chainNumber > 0)
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

        }
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
        //maxChainDistance = player.GetComponent<CharacterController>().maxDistance / (player.GetComponent<CharacterController>().maxChains * 2);
        maxChainDistance = 1;
    }
    }
