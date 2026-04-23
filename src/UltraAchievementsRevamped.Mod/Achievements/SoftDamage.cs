using HarmonyLib;
using UltraAchievementsRevamped.Core.Achievements;
using UnityEngine;

namespace UltraAchievementsRevamped.Mod.Achievements;

[HarmonyPatch]
internal class SoftDamage
{
    private static bool _timerStarted;
    private static float _timePassed;

    [HarmonyPatch(typeof(NewMovement), "Update")]
    [HarmonyPostfix]
    public static void Postfix(NewMovement __instance)
    {
        if (_timerStarted)
        {
            _timePassed += Time.deltaTime;

            if (_timePassed < 6f && __instance.hp >= 99)
                AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.softDamage");
            else if (_timePassed > 6f)
                _timerStarted = false;
        }
        else if (__instance.antiHp >= 98)
            _timerStarted = true;
    }
}