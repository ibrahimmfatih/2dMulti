using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    int score, opScore;
    Text scoreText, opScoreText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();

        opScore = 0;
        opScoreText = GetComponent<Text>();
        opScoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Scored()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void OpScored()
    {
        opScore++;
        opScoreText.text = opScore.ToString();
    }

    public int GetScore()
    {
        return score;
    }

    public int GetOpScore()
    {
        return opScore;
    }
}