using HarmonyLib;
using UltraAchievements_Lib;
using UnityEngine;

namespace UltraAchievements_Revamped.Achievements;

[HarmonyPatch(typeof(ItemPlaceZone), "CheckItem")]
[RegisterAchievement("ultraAchievements.rotatingrainbow")]
public class RotatingRainbow
{
    public static void Postfix(ItemPlaceZone __instance)
    {
        foreach (GameObject obj in __instance.activateOnSuccess)
        {
            if (obj.name.Contains("Rainbow") 
                && __instance.gameObject.GetComponentInChildren<ItemIdentifier>().itemType == ItemType.Soap 
                && StatsManager.Instance.levelNumber == 18)
            {
                AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(RotatingRainbow)));
            }
        }
    }
}