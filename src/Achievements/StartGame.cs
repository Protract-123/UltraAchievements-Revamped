using HarmonyLib;

namespace UltraAchievements_Revamped.Achievements;

[HarmonyPatch(typeof(NewMovement), "Start")]
[RegisterAchievement("ultraAchievements.startgame")]
public class StartGame
{
    [HarmonyPostfix]
    private static void StartPatch()
    {
        AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(StartGame)));
    }
}