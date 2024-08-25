using HarmonyLib;
using UltraAchievements_Lib;

namespace UltraAchievements_Revamped.Achievements;

[RegisterAchievement("ultraAchievements.doubleit")]
[HarmonyPatch]
public class DoubleIt
{
    private static int _counter = 0;
    
    [HarmonyPatch(typeof(DualWield), "Start")]
    public static void OnPowerUpGain()
    {
        _counter++;
        if (_counter >= 4)
        {
            AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(DoubleIt)));
        }
    }

    [HarmonyPatch(typeof(DualWield), nameof(DualWield.EndPowerUp))]
    public static void OnPowerUpEnd()
    {
        _counter--;
    }
}