using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Network : MonoBehaviourPunCallbacks
{
   
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        print("Sunucuya Giriþ Yaptýn");
        JoinOrCreateRoom();
    }
   public void JoinOrCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions
        { MaxPlayers = 10};
        PhotonNetwork.JoinOrCreateRoom(PlayerPrefs.GetString("room"), roomOptions, typedLobby: default);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Not In The Room");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("In The Room");
    
    Vector3 pos = new Vector3(0, 0, 0);
    PhotonNetwork.Instantiate("Player" , pos, Quaternion.identity);
    }
}
