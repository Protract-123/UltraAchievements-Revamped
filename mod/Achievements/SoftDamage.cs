using HarmonyLib;
using UltraAchievements_Lib;
using UnityEngine;

namespace UltraAchievements_Revamped.Achievements;

[HarmonyPatch(typeof(NewMovement), "Update")]
[RegisterAchievement("ultraAchievements.softdamage")]
public class SoftDamage
{
    private static bool _timerStarted = false;
    private static float _timePassed = 0f;
    
    public static void Postfix(NewMovement __instance)
    {
        if (_timerStarted)
        {
            _timePassed += Time.deltaTime;


            if (_timePassed < 6f && __instance.hp >= 99)
            {
                AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(SoftDamage)));
            }
            else if (_timePassed > 6f)
            {
                _timePassed = 0f;
                _timerStarted = false;
            }
        }
        
        if (__instance.antiHp >= 98)
        {
            _timerStarted = true;
        }
    }
}