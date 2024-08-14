using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
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
        if (collision.CompareTag("SagKale"))
        {
            score.Scored();
            Debug.Log("scored");
            gameManager.ResetPositionsWithDelay();


        }
        if (collision.CompareTag("SolKale"))
        {
            opScore.OpScored();
            Debug.Log("opScored");
            gameManager.ResetPositionsWithDelay();
        }
    }

    public void Shoot(Vector3 direction)
    {
        rb.AddForce(direction * force);

    }
}