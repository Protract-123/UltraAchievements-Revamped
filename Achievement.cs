namespace UltraAchievements_Revamped;

public abstract class Achievement
{
    private AchievementInfo achInfo
    {
        get
        {
            AchievementManager.TypeToAchInfo.TryGetValue(GetType(), out var info);
            return info;
        }
    }

    public void CompleteAchievement()
    {
        AchievementManager.MarkAchievementComplete(achInfo);
    }
    
}