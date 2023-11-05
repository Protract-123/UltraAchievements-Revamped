using HarmonyLib;

namespace UltraAchievements_Revamped.Achievements;
[HarmonyPatch(typeof(StyleHUD), nameof(StyleHUD.AddPoints))]
[RegisterAchievement("ultraAchievements.catapulted")]
public class Catapulted
{
    public static void Postfix(string pointID)
    {
        if (pointID == "ultrakill.catapulted")
        {
            AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(Catapulted)));
        }
    }
}