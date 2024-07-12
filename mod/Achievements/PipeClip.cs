using HarmonyLib;
using UltraAchievements_Lib;

namespace UltraAchievements_Revamped.Achievements;

[HarmonyPatch(typeof(HudMessage), "PlayMessage")]
[RegisterAchievement("ultraAchievements.pipeclip")]
public class PipeClip
{
    public static void Postfix(HudMessage __instance)
    {
        if (__instance.message.Contains("PIPE CLIP"))
        {
            AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(PipeClip)));
        }
    }
}