using HarmonyLib;
using UltraAchievementsRevamped.Core.Achievements;

namespace UltraAchievementsRevamped.Mod.Achievements;

[HarmonyPatch]
internal class YouAreAMonster
{
    [HarmonyPatch(typeof(Breakable), "Break", typeof(float))]
    [HarmonyPostfix]
    private static void SandcastleCheck(Breakable __instance)
    {
        if (__instance.name.Contains("Sand Castle"))
            AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.youAreAMonster");
    }
}