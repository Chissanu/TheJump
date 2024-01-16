using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class game : MonoBehaviour
{
    public int coinsAmount;
    public GameObject coinPrefab;
    public Text time;
    public Text scoreText;
    public Text finalScore;
    public GameObject gameOver;
    public int score;
    public float timeRemaining = 20;
    public bool gameState;
    public bool playSound = false;
    // Start is called before the first frame update
    public static game instance;
    void Start()
    {
        for (int i = 0; i <= coinsAmount; i++)
        {
            Instantiate(coinPrefab, new Vector3(Random.Range(-3f,3f),Random.Range(0,3f),0f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        string output = "";
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            float min = Mathf.FloorToInt(timeRemaining / 60);
            float sec = Mathf.FloorToInt(timeRemaining % 60);
            output = "Time left: " + string.Format("{0:00}:{1:00}", min, sec);
            time.text = output;
        } else
        {
            if (playSound == false)
            {
                this.GetComponent<AudioSource>().Play();
            }
            GameOver();
        }
    }

    public void AddScore()
    {
        score += 1;
        scoreText.text = "Score:" + score.ToString();
    }

    public void GameOver()
    {
        playSound = true;
        gameState = false;
        gameOver.SetActive(true);
        finalScore.text = "Score: " + score.ToString();
        scoreText.text = "";
        timeRemaining = 0;
        time.text = "";
    }

    public void RestartGame()
    {
        gameState = true;
        gameOver.SetActive(false);
        score = 0;
        timeRemaining = 20;
    }
}
