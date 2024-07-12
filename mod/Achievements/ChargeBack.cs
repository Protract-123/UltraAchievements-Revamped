using HarmonyLib;
using UltraAchievements_Lib;

namespace UltraAchievements_Revamped.Achievements;
[HarmonyPatch(typeof(StyleHUD), nameof(StyleHUD.AddPoints))]
[RegisterAchievement("ultraAchievements.chargeback")]
public class ChargeBack
{
    public static void Postfix(string pointID)
    {
        if (pointID == "ultrakill.chargeback")
        {
            AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(ChargeBack)));
        }
    }
}