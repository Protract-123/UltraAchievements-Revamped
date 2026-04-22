using HarmonyLib;
using UnityEngine;
using UltraAchievementsRevamped.Core.Achievements;

namespace UltraAchievementsRevamped.Mod.Achievements;

[HarmonyPatch]
internal class KillAFish : MonoBehaviour
{
    [HarmonyPatch(typeof(Breakable), "Break", typeof(float))]
    [HarmonyPostfix]
    private static void FishDeathPatch(Breakable __instance)
    {
        if (__instance.gameObject.TryGetComponent<FishGhost>(out _))
            AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.killAFish");
    }
}