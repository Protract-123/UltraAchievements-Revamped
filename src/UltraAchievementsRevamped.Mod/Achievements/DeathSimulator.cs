using HarmonyLib;
using UltraAchievementsRevamped.Core.Achievements;

namespace UltraAchievementsRevamped.Mod.Achievements;

[HarmonyPatch]
internal class DeathSimulator
{
    [HarmonyPatch(typeof(NewMovement), "Respawn")]
    [HarmonyPrefix]
    private static void DeathPatch() =>
        AchievementManager.AddProgressToAchievement("ultraAchievementsRevamped.deathSimulator", 1);
}