using UnityEngine;

namespace UltraAchievements_Revamped.Achievements;

[RegisterAchievement("ultraAchievements.oneinmillion")]
public class OneinMillion {
    public static void Check()
    {
        int num = Random.Range(0, 60000000);
        if (num == 1)
        {
            AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(OneinMillion)));
        }
    }
}