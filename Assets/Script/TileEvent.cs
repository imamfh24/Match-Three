﻿public abstract class TileEvent
{
    // Abstract class untuk base event dari tile

    // Apa yang terjadi jika tile match
    public abstract void OnMatch();

    // Check jika persyaratan event telah terpenuhi
    public abstract bool AchievementCompleted();
}
