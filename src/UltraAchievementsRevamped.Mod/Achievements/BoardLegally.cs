using HarmonyLib;
using UltraAchievementsRevamped.Core.Achievements;

namespace UltraAchievementsRevamped.Mod.Achievements;

[HarmonyPatch]
internal class BoardLegally
{
    [HarmonyPatch(typeof(FerrymanFake), "BlowCoin")]
    [HarmonyPostfix]
    private static void FerrymanCoinCheck() =>
        AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.boardLegally");
}