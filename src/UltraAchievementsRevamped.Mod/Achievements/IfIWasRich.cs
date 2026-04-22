using HarmonyLib;
using UltraAchievementsRevamped.Core.Achievements;

namespace UltraAchievementsRevamped.Mod.Achievements;

[HarmonyPatch]
internal class IfIWasRich
{
    [HarmonyPatch(typeof(MoneyText), "DivideMoney")]
    [HarmonyPrefix]
    private static void MoneyCheck(int dosh)
    {
        Plugin.Logger.LogInfo($"Money: {dosh}");
        if (dosh > 1_000_000_000)
            AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.ifIWasRich");
    }
}