using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
public class WaitingForPlayers : MonoBehaviour
{
    private static bool startGame = false;
    public TextMeshProUGUI playersInLobbyDisplay;
    public int minimumPlayerCount;
    public int maximumPlayerCount;
    public float waitingTime;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //[PunRPC]
    void JoinPlayer()
    {
         
    }
}
