using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;
    private int score = 0;
    private int lives = 3;

    void Start()
    {
        UpdateScore(0);
        UpdateLives(0);
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int change)
    {
        lives += change;
        livesText.text = "Lives: " + lives;
    }
}

