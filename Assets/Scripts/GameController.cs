using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Text scoreText;
    int score;

    public void Start()
    {
        score = -1;
        IncreaseScore();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

}

