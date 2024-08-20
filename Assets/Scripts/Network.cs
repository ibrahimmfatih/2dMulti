using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Network : MonoBehaviourPunCallbacks
{
    public List<Vector3> blueTeamSpawnPoints; // Mavi takým doðma noktalarý
    public List<Vector3> redTeamSpawnPoints;  // Kýrmýzý takým doðma noktalarý

    private int blueTeamIndex = 0; // Mavi takým için doðma noktasý sýrasý
    private int redTeamIndex = 0;  // Kýrmýzý takým için doðma noktasý sýrasý

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
            // Mavi takým için sýradaki pozisyonu seç
            spawnPos = blueTeamSpawnPoints[blueTeamIndex];
            blueTeamIndex = (blueTeamIndex + 1) % blueTeamSpawnPoints.Count; // Bir sonraki indexe geç
        }
        else if (playerTeam == "PlayerRed")
        {
            // Kýrmýzý takým için sýradaki pozisyonu seç
            spawnPos = redTeamSpawnPoints[redTeamIndex];
            redTeamIndex = (redTeamIndex + 1) % redTeamSpawnPoints.Count; // Bir sonraki indexe geç
        }

        PhotonNetwork.Instantiate(playerTeam, spawnPos, Quaternion.identity);
    }
}
