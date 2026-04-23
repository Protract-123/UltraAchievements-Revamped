using HarmonyLib;
using UltraAchievementsRevamped.Core.Achievements;

namespace UltraAchievementsRevamped.Mod.Achievements;

[HarmonyPatch]
internal class MemoryWiped
{
    [HarmonyPatch(typeof(SaveSlotMenu), "ConfirmWipe")]
    [HarmonyPostfix]
    private static void DeleteSavePatch() =>
        AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.memoryWiped");
}