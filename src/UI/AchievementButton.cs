using HarmonyLib;
using UnityEngine;

namespace UltraAchievements_Revamped.UI;


public class AchievementButton : MonoBehaviour
{
    public AchievementInfo info;
}

[HarmonyPatch(typeof(ShopButton), "OnPointerClick")]
public static class ShopButtonPatch
{
    public static void Prefix(ShopButton __instance)
    {
        if(__instance.TryGetComponent<AchievementButton>(out var button))
        {
            AchievementManager.currentInfo = button.info;
        }
    }
}