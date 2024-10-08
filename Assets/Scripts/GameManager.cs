using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System;

public class GameManager : MonoBehaviourPun
{
    public List<Vector3> team1SpawnPoints; // Set these in the inspector
    public List<Vector3> team2SpawnPoints; // Set these in the inspector

    public GameObject gameOverPanel; // UI Panel to show when the game ends
    public Text winnerText; // Text to show the winner

    public Ball ball;
    private Vector3 ballStartPos;

    void Start()
    {
        ballStartPos = ball.transform.position;
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        Application.targetFrameRate = 60;
    }

    public void ResetPositions()
    {
        // Reset the ball position
        ball.transform.position = ballStartPos;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // Reset positions of all Team1 (PlayerBlue) players
        GameObject[] team1Players = GameObject.FindGameObjectsWithTag("Team1");
        for (int i = 0; i < team1Players.Length; i++)
        {
            int spawnIndex = i % team1SpawnPoints.Count;
            team1Players[i].transform.position = team1SpawnPoints[spawnIndex];
            team1Players[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        // Reset positions of all Team2 (PlayerRed) players
        GameObject[] team2Players = GameObject.FindGameObjectsWithTag("Team2");
        for (int i = 0; i < team2Players.Length; i++)
        {
            int spawnIndex = i % team2SpawnPoints.Count;
            team2Players[i].transform.position = team2SpawnPoints[spawnIndex];
            team2Players[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public void EndGame(string winner)
    {
        ball.gameObject.SetActive(false); // Deactivate the ball
        winnerText.text = winner;
        gameOverPanel.SetActive(true); // Show the game over panel
    }

    // Bu metot, t�m oyuncular�n sahneyi yeniden y�klemesini sa�lar
    [PunRPC]
    public void RPC_RestartGame()
    {
        PhotonNetwork.LoadLevel("EgemenScene");
    }

    public void RestartGame()
    {
        if (photonView != null)
        {
            // RPC fonksiyonunu t�m oyunculara �a��r
            photonView.RPC("RPC_RestartGame", RpcTarget.All);
        }
        else
        {
            Debug.LogError("photonView is null in GameManager.");
        }
    }
    public void QuitToMainMenu()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Menu"); // Replace with your main menu scene name
    }
}