using HarmonyLib;
using UltraAchievements_Lib;

namespace UltraAchievements_Revamped.Achievements;

[HarmonyPatch(typeof(StyleHUD), nameof(StyleHUD.AddPoints))]
[RegisterAchievement("ultraAchievements.lightningbottle")]
public class LightningBottle
{
    public static void Postfix(string pointID)
    {
        if (pointID == "ultrakill.lightningbolt")
        {
            AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(LightningBottle)));
        }
    }
}