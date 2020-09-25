using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text timerText;
    public float timeCountdownInSecond = 90f;

    private float timer;
    void Start()
    {
        SetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1f)
        {
            timer = 0;
            if(timeCountdownInSecond > 0)
            {
                timeCountdownInSecond--;
                if (timeCountdownInSecond == 10) timerText.color = Color.red;
                SetTimer();
            }
            else
            {
                timer = 0;
                GameManager.instance.GameOverPanel();
                GameManager.instance.Finish = true;
                Debug.Log("Time is up");
            }
        }
    }

    private void SetTimer()
    {
        string minutes = Mathf.Floor(timeCountdownInSecond / 60).ToString();
        string seconds = Mathf.Floor(timeCountdownInSecond % 60).ToString();
        timerText.text = minutes + " : " + seconds;
    }
}
