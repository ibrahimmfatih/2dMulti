using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Network : MonoBehaviourPunCallbacks
{
    public List<Vector3> blueTeamSpawnPoints; // Mavi takým doðma noktalarý
    public List<Vector3> redTeamSpawnPoints;  // Kýrmýzý takým doðma noktalarý

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = false; // Sahne senkronizasyonunu devre dýþý býrak
        if (PhotonNetwork.InRoom)
        {
            OnJoinedRoom();
        }
    }

    public void ConnectToMaster()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Sunucuya Giriþ Yaptýn");
        JoinOrCreateRoom();
    }

    public void JoinOrCreateRoom()
    {
        string roomName = PlayerPrefs.GetString("room");
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 10 };
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Odaya katýlým baþarýsýz: " + message);
    }

   public override void OnJoinedRoom()
{
    Debug.Log("Odaya Girildi");

    // Aktif sahne adýný kontrol et
    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "EgemenScene")
    {
        // Her oyuncu kendi takýmýný bulur
        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("playerTeam", out object playerTeam))
        {
            Vector3 spawnPos = Vector3.zero;

            if (playerTeam.ToString() == "PlayerBlue")
            {
                // Mavi takým için rastgele bir doðma noktasý seç
                int randomIndex = Random.Range(0, blueTeamSpawnPoints.Count);
                spawnPos = blueTeamSpawnPoints[randomIndex];
            }
            else if (playerTeam.ToString() == "PlayerRed")
            {
                // Kýrmýzý takým için rastgele bir doðma noktasý seç
                int randomIndex = Random.Range(0, redTeamSpawnPoints.Count);
                spawnPos = redTeamSpawnPoints[randomIndex];
            }

            PhotonNetwork.Instantiate(playerTeam.ToString(), spawnPos, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Takým seçilmemiþ. Oyuncu spawn edilmeyecek.");
        }
    }
}
}
