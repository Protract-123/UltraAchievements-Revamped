using HarmonyLib;
using UltraAchievementsRevamped.Core.Achievements;

namespace UltraAchievementsRevamped.Mod.Achievements;

[HarmonyPatch]
internal class CultOfJakito
{
    [HarmonyPatch(typeof(HudMessage), "PlayMessage")]
    [HarmonyPostfix]
    private static void WorldDestructionCheckPatch(HudMessage __instance)
    {
        if (__instance.message.Contains("THANK YOU. NOW I SHALL LAY WASTE TO THIS WORLD."))
            AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.cultOfJakito");
    }
}