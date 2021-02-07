using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScoreBehaviour : MonoBehaviour
{
    private Text scoreText;
    private int score;
    void Start()
    {
        scoreText = gameObject.GetComponent<Text>();
        score = 0;
        scoreText.text = "Score: " + score;
    }
    void Update()
    {
        
    }
    public void UpdateScore(int points)
    {
        score = score + points * 100;
        scoreText.text = "Score: " + score;
    }
}
