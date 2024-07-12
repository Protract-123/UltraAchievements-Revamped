using HarmonyLib;
using UltraAchievements_Lib;
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
            Plugin.CurrentInfo = button.info;
        }
    }
}