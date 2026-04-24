using HarmonyLib;
using UltraAchievementsRevamped.Core.Achievements;

namespace UltraAchievementsRevamped.Mod.Achievements;

[HarmonyPatch]
internal class KillingMachine
{
    [HarmonyPatch(typeof(FinalRank), "LevelChange")]
    [HarmonyPrefix]
    private static void RankTimeCheckPrefix(FinalRank __instance)
    {
        string[] parts = __instance.time.text.Split(':');
        if (parts.Length < 2) return;
        if (!int.TryParse(parts[0], out int minutes) || !float.TryParse(parts[1], out float secs)) return;

        float seconds = minutes * 60 + secs;
        bool perfectRank = __instance.totalRank.text.Contains(">P<");

        if (perfectRank && seconds <= 60)
            AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.killingMachine");
    }
}