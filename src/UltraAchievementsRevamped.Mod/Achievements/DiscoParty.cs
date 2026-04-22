using HarmonyLib;
using UltraAchievementsRevamped.Core.Achievements;
using UnityEngine;

namespace UltraAchievementsRevamped.Mod.Achievements;

[HarmonyPatch]
internal class DiscoParty
{
    [HarmonyPatch(typeof(ItemPlaceZone), "CheckItem")]
    [HarmonyPostfix]
    private static void SoapPlacedPatch(ItemPlaceZone __instance)
    {
        if (StatsManager.Instance?.levelNumber != 18) return;
        if (__instance.acceptedItemType != ItemType.Soap) return;

        if (__instance.gameObject.GetComponentInChildren<ItemIdentifier>() == null) return;
        if (__instance.gameObject.GetComponentInChildren<ItemIdentifier>().itemType != ItemType.Soap) return;
        
        foreach (GameObject obj in __instance.activateOnSuccess)
        {
            if (!obj.name.Contains("Rainbow")) continue;
            AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.discoParty");
        }
    }
}