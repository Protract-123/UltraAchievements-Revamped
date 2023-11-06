using System;
using HarmonyLib;
using UnityEngine;

namespace UltraAchievements_Revamped.Achievements;
[HarmonyPatch(typeof(NewMovement), "Update")]
[RegisterAchievement("ultraAchievements.speed125")]
public class Speeding
{
    private static Rigidbody movement;
    public static void Postfix()
    {
        if (movement == null)
        {
            movement = NewMovement.Instance.GetComponent<Rigidbody>();
        }


        if (Math.Abs(movement.velocity.x) > 125 || Math.Abs(movement.velocity.z) > 125)
        {
            AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(Speeding)));
        }
    }
}