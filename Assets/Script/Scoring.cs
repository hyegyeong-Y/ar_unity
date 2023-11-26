using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoring : MonoBehaviour
{
    private ARPlacement arPlacement;
    TextMeshProUGUI scoreText;
    GameObject scoreBoardUI;
    public static int score;
    private bool verify;

    private void Start()
    {
        arPlacement = FindObjectOfType<ARPlacement>();
        //gameObject.GetComponent<Shoot>().enabled = true;
        //scoreBoardUI = GameObject.FindGameObjectWithTag("ScoreCanvas");
        //scoreText = GameObject.FindGameObjectWithTag("ScoreOnBanner").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (arPlacement.getObjectIsPlaced())
        {
            if (!verify)
            {
                gameObject.GetComponent<Shoot>().enabled = true;
                scoreBoardUI = GameObject.FindGameObjectWithTag("ScoreCanvas");
                scoreText = GameObject.FindGameObjectWithTag("ScoreOnBanner").GetComponent<TextMeshProUGUI>();

                verify = !verify;
            }
            scoreText.text = "Score: " + score.ToString();
        }


    }

}




