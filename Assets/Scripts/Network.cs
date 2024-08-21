using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Network : MonoBehaviourPunCallbacks
{
    public List<Vector3> blueTeamSpawnPoints; // Mavi tak�m do�ma noktalar�
    public List<Vector3> redTeamSpawnPoints;  // K�rm�z� tak�m do�ma noktalar�

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = false; // Sahne senkronizasyonunu devre d��� b�rak
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
        Debug.Log("Sunucuya Giri� Yapt�n");
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
        Debug.LogError("Odaya kat�l�m ba�ar�s�z: " + message);
    }

   public override void OnJoinedRoom()
{
    Debug.Log("Odaya Girildi");

    // Aktif sahne ad�n� kontrol et
    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "EgemenScene")
    {
        // Her oyuncu kendi tak�m�n� bulur
        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("playerTeam", out object playerTeam))
        {
            Vector3 spawnPos = Vector3.zero;

            if (playerTeam.ToString() == "PlayerBlue")
            {
                // Mavi tak�m i�in rastgele bir do�ma noktas� se�
                int randomIndex = Random.Range(0, blueTeamSpawnPoints.Count);
                spawnPos = blueTeamSpawnPoints[randomIndex];
            }
            else if (playerTeam.ToString() == "PlayerRed")
            {
                // K�rm�z� tak�m i�in rastgele bir do�ma noktas� se�
                int randomIndex = Random.Range(0, redTeamSpawnPoints.Count);
                spawnPos = redTeamSpawnPoints[randomIndex];
            }

            PhotonNetwork.Instantiate(playerTeam.ToString(), spawnPos, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Tak�m se�ilmemi�. Oyuncu spawn edilmeyecek.");
        }
    }
}
}
