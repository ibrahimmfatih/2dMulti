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

    public TMP_Text lobbyText;
    public TMP_Text blueTeamText;
    public TMP_Text redTeamText;
    public TMP_Text noTeamText;

    public Network network;

    private void Start()
    {
        if (network == null)
        {
            network = FindObjectOfType<Network>(); // Yedek olarak
        }

        // PhotonView atamasýný kaldýrýyoruz
        // photonView zaten MonoBehaviourPun tarafýndan saðlanýyor ve otomatik olarak atanýyor
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

        PlayerPrefs.SetString("name", username);
        PlayerPrefs.SetString("room", roomname);

        PhotonNetwork.LocalPlayer.NickName = username;

        main.SetActive(false);
        select.SetActive(true);

        network.ConnectToMaster();
    }

    public void BlueTeam()
    {
        SetTeam("PlayerBlue");
    }

    public void RedTeam()
    {
        SetTeam("PlayerRed");
    }

    private void SetTeam(string teamName)
    {
        PlayerPrefs.SetString("player", teamName);
        ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable { { "playerTeam", teamName } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);

        Debug.Log($"Player Team: {teamName}");

        // Takým deðiþikliðini tüm oyunculara bildir
        photonView.RPC("OnTeamChanged", RpcTarget.All, PhotonNetwork.LocalPlayer.UserId, teamName);

        PhotonNetwork.LeaveRoom();
        UnityEngine.SceneManagement.SceneManager.LoadScene("EgemenScene");
    }

    [PunRPC]
    void OnTeamChanged(string playerId, string newTeam)
    {
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player.UserId == playerId)
            {
                player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "playerTeam", newTeam } });
                break;
            }
        }

        UpdateLobbyUI();
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

        // UI öðelerinin atanmýþ olup olmadýðýný kontrol edin
        if (lobbyText != null)
        {
            lobbyText.text = "Oda Adý: " + PhotonNetwork.CurrentRoom.Name;
        }

        if (blueTeamText != null && redTeamText != null && noTeamText != null)
        {
            blueTeamText.text = "";
            redTeamText.text = "";
            noTeamText.text = "";

            foreach (Player player in PhotonNetwork.PlayerList)
            {
                string username = player.NickName;

                if (player.CustomProperties.TryGetValue("playerTeam", out object team))
                {
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
                    noTeamText.text += username + "\n";
                }
            }
        }
        else
        {
            Debug.Log("UI elements are not required in this scene.");
        }
    }
}