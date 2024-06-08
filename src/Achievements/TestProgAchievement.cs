using HarmonyLib;
using UnityEngine;

namespace UltraAchievements_Revamped.Achievements;

/*
[RegisterAchievement("ultraAchievements.testprog")]
[HarmonyPatch]
public class TestProgAchievement
{
    [HarmonyPatch(typeof(NewMovement), "Update"), HarmonyPostfix]
    public static void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AchievementInfo info = AchievementManager.GetAchievementInfo(typeof(TestProgAchievement));
            info.progress += 1;
            if (info.progress >= info.MaxProgress)
            {
                AchievementManager.MarkAchievementComplete(info);
            }
        }
    }
}
*/