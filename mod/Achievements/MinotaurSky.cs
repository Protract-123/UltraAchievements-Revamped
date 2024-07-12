using System.Collections.Generic;
using HarmonyLib;
using UltraAchievements_Lib;

namespace UltraAchievements_Revamped.Achievements;

[RegisterAchievement("ultraAchievements.minotaursky")]
[HarmonyPatch(typeof(Minotaur), "Start")]
public class MinotaurSky
{
    private static List<int> _levels = [13];
    [HarmonyPrefix]
    public static void LevelCheck()
    {
        if (_levels.Contains(SceneHelper.CurrentLevelNumber))
        {
            AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(MinotaurSky)));
        }
    }
}