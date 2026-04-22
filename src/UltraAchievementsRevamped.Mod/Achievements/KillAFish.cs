using HarmonyLib;
using UnityEngine;

namespace UltraAchievementsRevamped.Mod.Achievements;

[HarmonyPatch]
internal class KillAFish : MonoBehaviour
{
    [HarmonyPatch(typeof(Breakable), "Break", typeof(float))]
    [HarmonyPostfix]
    private static void FishDeathPatch(Breakable __instance)
    {
        if (__instance.gameObject.TryGetComponent<FishGhost>(out _))
            Core.Achievements.AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.killAFish");
    }
}