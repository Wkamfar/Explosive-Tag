using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhantomChainControllerScript : MonoBehaviour
{
    private bool foundPlayer;
    private GameObject player;
    private int maxChainCount;
    private int currentChainCount;
    public GameObject[] chains;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (foundPlayer)
        {
            chains = GameObject.FindGameObjectsWithTag("Chain");
            if (chains.Length > maxChainCount)
            {
                DestroyLastChain();
            }
        }
    }
    public void SpawnChain(GameObject _chain, Vector3 spawnLocation)
    {
        GameObject currentChain = Instantiate(_chain,  spawnLocation, Quaternion.identity);
        currentChain.GetComponent<PhantomChainScript>().DefinePlayer();
        currentChain.GetComponent<PhantomChainScript>().SetChainNumber(currentChainCount);
        currentChainCount++;
        Debug.Log("PhantomChainControllerScript.SpawnChain: The chain spawned");
    }
    public void DestroyLastChain()
    {
        Destroy(chains[chains.Length - 1]);
        currentChainCount--;
    }
    public void DestroySpecificChain(GameObject _chain)
    {
        Destroy(_chain);
        for (int i = 0; i < chains.Length - 1; i++)
        {
            chains[i].GetComponent<PhantomChainScript>().DecreaseChainNumber();
        }
    }
    public GameObject[] GetChainArray()
    {
        return chains;
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
        maxChainCount = player.GetComponent<CharacterController>().maxChains;
    }
}
