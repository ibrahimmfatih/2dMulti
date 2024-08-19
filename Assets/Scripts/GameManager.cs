using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public GameObject gameOverPanel; // UI Panel to show when the game ends
    public Text winnerText; // Text to show the winner

    public Ball ball;
    private Vector3 ballStartPos;
    // Start is called before the first frame update
    void Start()
    {
        ballStartPos = ball.transform.position;

        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = 60;
    }

    public void ResetPositions()
    {
        // Reset the ball and players to their starting positions
        ball.transform.position = ballStartPos;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        
    }
    public void EndGame(string winner)
    {
        ball.gameObject.SetActive(false); // Deactivate the ball
        winnerText.text = winner;
        gameOverPanel.SetActive(true); // Show the game over panel
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Replace with your main menu scene name
    }
}
