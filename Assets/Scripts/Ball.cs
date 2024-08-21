using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviourPunCallbacks
{
    [SerializeField] private int maxScore = 3; // Maximum score to win the game
    [SerializeField] private float force;
    private Rigidbody2D rb;
    [SerializeField] public Score opScore, score;
    [SerializeField] public GameManager gameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (collision.CompareTag("SagKale"))
            {
                score.Scored();
                Debug.Log("scored");
                CheckForWinner();
                gameManager.ResetPositions();


            }
            if (collision.CompareTag("SolKale"))
            {
                opScore.OpScored();
                Debug.Log("opScored");
                CheckForWinner();
                gameManager.ResetPositions();
            }
        }
    }
    public void Shoot(Vector3 direction)
    {
        // Sahiplik zaten 'PlayerMovement' s�n�f�nda topa dokunurken devral�nd��� i�in burada tekrar devralmaya gerek yok.

        rb.velocity = Vector2.zero; // H�z� s�f�rla, b�ylece yeni kuvvet do�ru �ekilde uygulan�r
        rb.AddForce(direction * force);

        // Di�er oyunculara vurma i�lemini bildir
        photonView.RPC("RPC_Shoot", RpcTarget.Others, direction);
    }
    [PunRPC]
    void RPC_Shoot(Vector3 direction)
    {
        rb.velocity = Vector2.zero;  // Kuvvet uygulanmadan �nce h�z� s�f�rla
        rb.AddForce(direction * force);
    }
    private void CheckForWinner()
    {
        if (score.GetScore() >= maxScore)
        {
            gameManager.EndGame("Team 1 Wins!");
        }
        else if (opScore.GetOpScore() >= maxScore)
        {
            gameManager.EndGame("Team 2 Wins!");
        }
    }
}