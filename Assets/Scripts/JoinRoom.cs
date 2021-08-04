using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class JoinRoom : MonoBehaviourPunCallbacks
{
    public string roomName = "William";
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void CreateRoom() 
    {
        PhotonNetwork.CreateRoom(roomName);
    }
    public void JoinTheRoom() 
    {
        PhotonNetwork.JoinRoom(roomName);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
