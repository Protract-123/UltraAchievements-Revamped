using System;
using HarmonyLib;
using UltraAchievements_Lib;
using UnityEngine;

namespace UltraAchievements_Revamped.Achievements;
[HarmonyPatch(typeof(NewMovement), "Update")]
[RegisterAchievement("ultraAchievements.speed125")]
public class Speeding
{
    private static Rigidbody _movement;
    public static void Postfix()
    {
        if (_movement == null)
        {
            _movement = NewMovement.Instance.GetComponent<Rigidbody>();
        }


        if (Math.Abs(_movement.velocity.x) > 125 || Math.Abs(_movement.velocity.z) > 125)
        {
            AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(Speeding)));
        }
    }
}