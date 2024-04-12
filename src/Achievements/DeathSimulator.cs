using HarmonyLib;

namespace UltraAchievements_Revamped.Achievements;

[RegisterAchievement("ultraAchievements.death")]
[HarmonyPatch(typeof(NewMovement), nameof(NewMovement.Respawn))]
public class DeathSimulator
{
    public static int Deaths = 0;
    [HarmonyPostfix]
    public static void DeathPatch()
    {
        Deaths++;
        if (Deaths >= 100)
        {
            AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(DeathSimulator)));
        }
    }
}
