using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSystem : Observer
{
    public Image achivementBanner;
    public Text achievementText;

    //Event
    TileEvent cakeEvent, candyEvent, cookiesEvent;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();

        //Buat event
        cookiesEvent = new CookiesTileEvent(3);
        cakeEvent = new CakeTileEvent(10);
        candyEvent = new CandyTileEvent(5);
        
        foreach(var point in FindObjectsOfType<PointOfInterest>())
        {
            point.RegisterObserver(this);
        }
    }

    public override void OnNotify(string value)
    {
        string key;

        //Seleksi event yang terjadi, dan panggil class event nya
        if(value.Equals("Cookies event"))
        {
            cookiesEvent.OnMatch();
            if (cookiesEvent.AchievementCompleted())
            {
                key = "Match first cookies";
                NotifyAchievement(key, value);
            }
        }

        if(value.Equals("Cake event"))
        {
            cakeEvent.OnMatch();
            if (cakeEvent.AchievementCompleted())
            {
                key = "Match 10 cake";
                NotifyAchievement(key, value);
            }
        }

        if(value.Equals("Candy event"))
        {
            candyEvent.OnMatch();
            if (candyEvent.AchievementCompleted())
            {
                key = "Match 5 Candy";
                NotifyAchievement(key, value);
            }
        }
    }

    private void NotifyAchievement(string key, string value)
    {
        //check jika achievement sudah diperoleh
        if (PlayerPrefs.GetInt(value) == 1)
            return;

        PlayerPrefs.SetInt(value, 1);
        achievementText.text = key + " Unlocked !";

        //pop up notifikasi
        StartCoroutine(ShowAchievementBanner());
    }

    void ActiveAchievementBanner(bool active)
    {
        achivementBanner.gameObject.SetActive(active);
    }

    IEnumerator ShowAchievementBanner()
    {
        ActiveAchievementBanner(true);
        yield return new WaitForSeconds(2f);
        ActiveAchievementBanner(false);
    }
}
