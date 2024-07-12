using HarmonyLib;
using UltraAchievements_Lib;

namespace UltraAchievements_Revamped.Achievements;
[HarmonyPatch(typeof(SaveSlotMenu), "ConfirmWipe")]
[RegisterAchievement("ultraAchievements.deletesave")]
public class DeleteSave
{
    public static void Prefix()
    {
        AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(DeleteSave)));
    }
}