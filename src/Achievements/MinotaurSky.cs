using System.Collections.Generic;
using HarmonyLib;

namespace UltraAchievements_Revamped.Achievements;

[RegisterAchievement("ultraAchievements.minotaursky")]
[HarmonyPatch(typeof(Minotaur), "Start")]
public class MinotaurSky
{
    private static List<int> levels = [13];
    [HarmonyPrefix]
    public static void LevelCheck()
    {
        if (levels.Contains(SceneHelper.CurrentLevelNumber))
        {
            AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(MinotaurSky)));
        }
    }
}