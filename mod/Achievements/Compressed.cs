using HarmonyLib;
using UltraAchievements_Lib;

namespace UltraAchievements_Revamped.Achievements;
[HarmonyPatch(typeof(StyleHUD), nameof(StyleHUD.AddPoints))]
[RegisterAchievement("ultraAchievements.compressed")]
public class Compressed
{
    public static void Postfix(string pointID)
    {
        if (pointID == "ultrakill.compressed")
        {
            AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(Compressed)));
        }
    }
}