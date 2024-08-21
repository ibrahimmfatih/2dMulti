using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class MenuMenager : MonoBehaviourPunCallbacks
{
    public TMP_InputField ad;
    public TMP_InputField odaadi;
    public GameObject main, select;

    // Decision k�sm�nda kullan�lan UI elementleri
    public TMP_Text lobbyText;
    public TMP_Text blueTeamText;
    public TMP_Text redTeamText;
    public TMP_Text noTeamText;

    public Network network; // Network script'ine referans (manuel atama)

    private void Start()
    {
        if (network == null)
        {
            network = FindObjectOfType<Network>(); // Yedek olarak
        }

        main.SetActive(true);
        select.SetActive(false);
    }

    public void Enter()
    {
        if (network == null)
        {
            Debug.LogError("Network script is not assigned or found!");
            return;
        }

        string username = ad.text;
        string roomname = odaadi.text;

        // Kullan�c� ad�n� ve oda ad�n� PlayerPrefs'e kaydedin
        PlayerPrefs.SetString("name", username);
        PlayerPrefs.SetString("room", roomname);

        PhotonNetwork.LocalPlayer.NickName = username;

        Debug.Log("Username: " + username);
        Debug.Log("Room Name: " + roomname);

        main.SetActive(false);
        select.SetActive(true);

        // Network script'inin OnConnectedToMaster() metodunu �a��r
        network.ConnectToMaster();
    }

    public void BlueTeam()
    {
        PlayerPrefs.SetString("player", "PlayerBlue");

        // Tak�m bilgisini CustomProperties ile sakla ve g�ncellemesini bekle
        ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable { { "playerTeam", "PlayerBlue" } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);

        Debug.Log("Player Team: PlayerBlue");

        // Oyuncunun tak�m se�imini tamamlad�ktan sonra sahneye ge�i�ini sa�lar
        // Di�er oyuncular i�in sahne senkronizasyonunu zorlamay�n
        PhotonNetwork.LeaveRoom();  // Odadan ��k�yoruz
        UnityEngine.SceneManagement.SceneManager.LoadScene("EgemenScene"); // Oyunun sahnesine ge�i� yap�yoruz
    }

    public void RedTeam()
    {
        PlayerPrefs.SetString("player", "PlayerRed");

        // Tak�m bilgisini CustomProperties ile sakla ve g�ncellemesini bekle
        ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable { { "playerTeam", "PlayerRed" } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);

        Debug.Log("Player Team: PlayerRed");

        // Oyuncunun tak�m se�imini tamamlad�ktan sonra sahneye ge�i�ini sa�lar
        PhotonNetwork.LeaveRoom();  // Odadan ��k�yoruz
        UnityEngine.SceneManagement.SceneManager.LoadScene("EgemenScene"); // Oyunun sahnesine ge�i� yap�yoruz
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player entered room: " + newPlayer.NickName);
        UpdateLobbyUI();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player left room: " + otherPlayer.NickName);
        UpdateLobbyUI();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        UpdateLobbyUI();
    }


    void UpdateLobbyUI()
    {
        if (PhotonNetwork.CurrentRoom == null)
        {
            Debug.LogError("CurrentRoom is null!");
            return;
        }

        // Lobby ad�n� g�ncelle
        lobbyText.text = "Oda Ad�: " + PhotonNetwork.CurrentRoom.Name;

        // Her iki tak�m� ve tak�ms�z oyuncular� temizle
        blueTeamText.text = "";
        redTeamText.text = "";
        noTeamText.text = "";

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            string username = player.NickName;
            Debug.Log("Processing player: " + username);

            if (player.CustomProperties.TryGetValue("playerTeam", out object team))
            {
                Debug.Log("Player " + username + " is in team: " + team);

                if (team.ToString() == "PlayerBlue")
                {
                    blueTeamText.text += username + "\n";
                }
                else if (team.ToString() == "PlayerRed")
                {
                    redTeamText.text += username + "\n";
                }
            }
            else
            {
                Debug.Log("Player " + username + " has not chosen a team.");
                noTeamText.text += username + "\n";
            }
        }
    }
}
