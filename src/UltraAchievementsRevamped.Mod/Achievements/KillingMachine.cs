using HarmonyLib;
using UltraAchievementsRevamped.Core.Achievements;

namespace UltraAchievementsRevamped.Mod.Achievements;

[HarmonyPatch]
internal class KillingMachine
{
    [HarmonyPatch(typeof(FinalRank), "LevelChange")]
    [HarmonyPrefix]
    private static void RankTimeCheckPostfix(FinalRank __instance)
    {
        string[] parts = __instance.time.text.Split(':');
        float seconds = int.Parse(parts[0]) * 60 + float.Parse(parts[1]);
        bool perfectRank = __instance.totalRank.text.Contains(">P<");
        
        if (perfectRank && seconds <= 60)
            AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.killingMachine");
    }
}