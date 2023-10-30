using HarmonyLib;

namespace UltraAchievements_Revamped.UI;


public class AchievementButton : ShopButton
{
    public AchievementInfo info;
}

[HarmonyPatch(typeof(AchievementButton), "OnPointerClick")]
public static class AchievementButtonPatch
{
    public static void Prefix(AchievementButton __instance)
    {
        AchievementManager.currentInfo = __instance.info;
    }
}