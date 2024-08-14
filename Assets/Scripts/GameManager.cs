using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    private Vector3 ballStartPos;
    [SerializeField] private float resetDelay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        ballStartPos = ball.transform.position;
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
    public IEnumerator ResetPositionsWithDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(resetDelay);

        // Reset the ball and players to their starting positions
        ResetPositions();
    }
}
