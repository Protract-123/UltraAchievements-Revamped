using HarmonyLib;
using UltraAchievements_Lib;

namespace UltraAchievements_Revamped.Achievements;

[HarmonyPatch(typeof(Breakable), nameof(Breakable.Break))]
[RegisterAchievement("ultraAchievements.killfish")]
public class KillFish
{
    public static void Prefix(Breakable __instance)
    {
        if (__instance.gameObject.TryGetComponent<FishGhost>(out _))
        {
            AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(KillFish)));
        }
    
    }
}