using HarmonyLib;
using UltraAchievementsRevamped.Core.Achievements;

namespace UltraAchievementsRevamped.Mod.Achievements;

[HarmonyPatch]
internal class V2IntroKill
{
    [HarmonyPatch(typeof(V2), "Die")]
    [HarmonyPrefix]
    private static void V2DeathPatch(V2 __instance)
    {
        if (__instance.inIntro)
            AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.v2IntroKill");
    }
}