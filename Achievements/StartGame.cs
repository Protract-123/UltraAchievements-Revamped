using HarmonyLib;

namespace UltraAchievements_Revamped.Achievements;

[HarmonyPatch(typeof(NewMovement), "Start")]
public class StartGame : Achievement
{
    
}