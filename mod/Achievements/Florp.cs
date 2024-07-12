using UltraAchievements_Lib;

namespace UltraAchievements_Revamped.Achievements;

[RegisterAchievement("ultraAchievements.florp")]
public class Florp
{
    public static void FlorpCheck()
    {
        if (NewMovement.Instance != null)
        {
            ItemIdentifier itemIdentifier = NewMovement.Instance.GetComponentInChildren<FistControl>().heldObject;
            if (itemIdentifier != null)
            {
                if (itemIdentifier.name == "Florp")
                {
                    AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(Florp)));
                }
            }
        }
    }
}