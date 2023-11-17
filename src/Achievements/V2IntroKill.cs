using HarmonyLib;

namespace UltraAchievements_Revamped.Achievements;

[HarmonyPatch(typeof(V2), nameof(V2.Die))]
[RegisterAchievement("ultraAchievements.v2kill")]
public class V2IntroKill
{
    public static void Prefix(V2 __instance)
    {
        if (__instance.inIntro)
        {
            AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(V2IntroKill)));
        }
    }
}