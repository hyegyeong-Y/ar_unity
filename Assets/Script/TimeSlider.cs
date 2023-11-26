using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeSlider : MonoBehaviour
{
    Slider timerSlider;
    TextMeshProUGUI timerText;

    public float gameTime = 20.0f;

    Image fillImage;
    public Color32 normalFillColor;
    public Color32 warningFillColor;
    public float warningLimit;  // as a percentage

    public bool stopTimer;

    TextMeshProUGUI gameOverText;

    private ARPlacement arPlacement; // Add reference to ARPlacement script
    private bool verify;

    private void Start()
    {
        arPlacement = FindObjectOfType<ARPlacement>();


        gameObject.GetComponent<Shoot>().enabled = false; // Disable Shoot component initially
    }

    private void Update()
    {
        if (!arPlacement.getObjectIsPlaced()) // Check if object is placed using ARPlacement script
        {
            return; // Exit Update method if object is not placed
        }

        if (!verify)
        {
            gameOverText = GameObject.FindGameObjectWithTag("GameOverText").GetComponent<TextMeshProUGUI>();
            gameOverText.gameObject.SetActive(false);

            timerSlider = GameObject.FindGameObjectWithTag("TimeSlider").GetComponent<Slider>();
            timerText = GameObject.FindGameObjectWithTag("TimeSlider").GetComponentInChildren<TextMeshProUGUI>();

            //fillImage = timerSlider.fillRect.GetComponentInChildren<Image>();
            fillImage = GameObject.FindGameObjectWithTag("TimeSlider").transform.Find("Fill Area").Find("Fill").GetComponent<Image>();

            timerSlider.maxValue = gameTime;
            timerSlider.value = gameTime;

            fillImage.color = normalFillColor;

            stopTimer = true;

            verify = !verify;
        }

        if (stopTimer)
        {
            gameTime -= Time.deltaTime;
            gameTime = Mathf.Max(gameTime, 0f);

            string textTime = "Time left: " + gameTime.ToString("f0") + "s";

            timerText.text = textTime;
            timerSlider.value = gameTime;

            if (timerSlider.value < ((warningLimit / 100) * timerSlider.maxValue))
            {
                fillImage.color = warningFillColor;
            }

            if (gameTime <= 0)  // On Game over
            {
                stopTimer = false;
                timerSlider.gameObject.SetActive(false); // Disable the slider
                gameOverText.gameObject.SetActive(true);

                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Spider");
                foreach (GameObject enemy in enemies)
                {
                    Destroy(enemy); // Destroy all spiders in the scene
                }
            }
        }

        if (gameTime <= 10 && !gameObject.GetComponent<Shoot>().enabled)
        {
            gameObject.GetComponent<Shoot>().enabled = true; // Enable Shoot component when 10 seconds are left
        }
    }
}

