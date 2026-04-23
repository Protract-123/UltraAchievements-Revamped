using HarmonyLib;
using UltraAchievementsRevamped.Core.Achievements;

namespace UltraAchievementsRevamped.Mod.Achievements;

[HarmonyPatch]
internal class DoubleIt
{
    private static int _dualWieldCounter;

    [HarmonyPatch(typeof(DualWield), "Start")]
    [HarmonyPostfix]
    private static void DualWieldGainPatch()
    {
        _dualWieldCounter++;
        if (_dualWieldCounter >= 3)
            AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.doubleIt");
    }

    [HarmonyPatch(typeof(DualWield), "EndPowerUp")]
    [HarmonyPostfix]
    private static void DualWieldEndPatch() => _dualWieldCounter--;
}