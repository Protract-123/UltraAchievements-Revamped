using HarmonyLib;
using UltraAchievementsRevamped.Core.Achievements;

namespace UltraAchievementsRevamped.Mod.Achievements;

[HarmonyPatch]
internal class PipeClip
{
    [HarmonyPatch(typeof(HudMessage), "PlayMessage")]
    [HarmonyPostfix]
    private static void PipeClipCheckPatch(HudMessage __instance)
    {
        if (__instance.message.Contains("PIPE CLIP"))
            AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.pipeClip");
    }
}