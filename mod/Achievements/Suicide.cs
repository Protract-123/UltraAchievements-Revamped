using HarmonyLib;
using UltraAchievements_Lib;
using UnityEngine;

namespace UltraAchievements_Revamped.Achievements;

[RegisterAchievement("ultraAchievements.suicide")]
[HarmonyPatch(typeof(DeathZone), "GotHit")]
public class Suicide
{
    [HarmonyPrefix]
    private static void SuicideCheck(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && NewMovement.Instance.hp == 1)
        {
            AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(Suicide)));
        }
    }
}