using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyTileEvent : TileEvent
{
    private int matchCount;
    private int requiredAmount;

    public CandyTileEvent(int amount) { requiredAmount = amount; }

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
