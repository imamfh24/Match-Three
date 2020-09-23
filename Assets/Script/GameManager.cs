using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Instance sebagai global access
    public static GameManager instance;
    int playerScore;
    public Text scoreText;

    //Singleton
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        } else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void GetScore(int point)
    {
        playerScore += point;
        scoreText.text = playerScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
