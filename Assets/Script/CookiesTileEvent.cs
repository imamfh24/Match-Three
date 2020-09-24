using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookiesTileEvent : TileEvent
{
    private int matchCount;
    private int requiredAmount;

    public CookiesTileEvent(int amount) { requiredAmount = amount; }

    public override bool AchievementCompleted()
    {
        if (matchCount == requiredAmount) { return true; }
        else { return false; }
    }

    public override void OnMatch()
    {
        matchCount++;
    }
}
