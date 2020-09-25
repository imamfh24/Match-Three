using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Instance sebagai global access
    public static GameManager instance;
    int playerScore;
    int playerCombo;
    int pointCombo;
    public Text scoreText;
    public Text finalScoreText;
    public Text comboText;
    public GameObject comboPanel;
    public GameObject gameOverPanel;

    private bool isFinish = false;

    public bool Finish
    {
        get { return isFinish; }
        set { isFinish = value; }
    }

    

    //Singleton
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        /*DontDestroyOnLoad(gameObject);*/
    }

    public void GetScore(int point)
    {
        pointCombo = point;
        playerScore += point;
        scoreText.text = playerScore.ToString();
        finalScoreText.text = "Your Score: " + playerScore.ToString();
    }

    public void ComboTile(bool combo)
    {
        if (combo)
        {
            playerCombo++;
            comboText.text = playerCombo.ToString() + "x Combo";
            comboPanel.SetActive(combo);
        }
        else
        {
            GetScoreCombo(playerCombo);
            playerCombo = 0;
            comboPanel.SetActive(combo);
        }
    }

    private void GetScoreCombo(int playerCombo)
    {
        playerScore = playerScore + (playerCombo * pointCombo);
        scoreText.text = playerScore.ToString();
        finalScoreText.text = "Your Score: " + playerScore.ToString();
    }

    public void GameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
