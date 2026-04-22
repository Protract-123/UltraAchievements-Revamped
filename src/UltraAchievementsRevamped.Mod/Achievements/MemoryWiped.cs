using HarmonyLib;

namespace UltraAchievementsRevamped.Mod.Achievements;

[HarmonyPatch]
internal class MemoryWiped
{
    [HarmonyPatch(typeof(SaveSlotMenu), "ConfirmWipe")]
    [HarmonyPostfix]
    private static void DeleteSavePatch() =>
        Core.Achievements.AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.memoryWiped");
    
}