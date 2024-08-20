using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Network : MonoBehaviourPunCallbacks
{
    public List<Vector3> blueTeamSpawnPoints; // Mavi tak�m do�ma noktalar�
    public List<Vector3> redTeamSpawnPoints;  // K�rm�z� tak�m do�ma noktalar�

    private int blueTeamIndex = 0; // Mavi tak�m i�in do�ma noktas� s�ras�
    private int redTeamIndex = 0;  // K�rm�z� tak�m i�in do�ma noktas� s�ras�

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Sunucuya Giri� Yapt�n");
        JoinOrCreateRoom();
    }

    public void JoinOrCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions
        { MaxPlayers = 10 };
        PhotonNetwork.JoinOrCreateRoom(PlayerPrefs.GetString("room"), roomOptions, typedLobby: default);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Not In The Room");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("In The Room");

        string playerTeam = PlayerPrefs.GetString("player");
        Vector3 spawnPos = Vector3.zero;

        if (playerTeam == "PlayerBlue")
        {
            // Mavi tak�m i�in s�radaki pozisyonu se�
            spawnPos = blueTeamSpawnPoints[blueTeamIndex];
            blueTeamIndex = (blueTeamIndex + 1) % blueTeamSpawnPoints.Count; // Bir sonraki indexe ge�
        }
        else if (playerTeam == "PlayerRed")
        {
            // K�rm�z� tak�m i�in s�radaki pozisyonu se�
            spawnPos = redTeamSpawnPoints[redTeamIndex];
            redTeamIndex = (redTeamIndex + 1) % redTeamSpawnPoints.Count; // Bir sonraki indexe ge�
        }

        PhotonNetwork.Instantiate(playerTeam, spawnPos, Quaternion.identity);
    }
}
