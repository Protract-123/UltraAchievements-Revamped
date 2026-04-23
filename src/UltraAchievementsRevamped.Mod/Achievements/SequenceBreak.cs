using System.Linq;
using HarmonyLib;
using UltraAchievementsRevamped.Core.Achievements;

namespace UltraAchievementsRevamped.Mod.Achievements;

[HarmonyPatch]
internal class SequenceBreak
{
    [HarmonyPatch(typeof(StatsManager), "GetFinalRank")]
    [HarmonyPostfix]
    private static void WeaponsCheckPatch()
    {
        switch (StatsManager.Instance?.levelNumber)
        {
            case 1:
            {
                if (!HasWeapon("Revolver"))
                    AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.sequenceBreak");
                break;
            }
            case 3:
            {
                if (!HasWeapon("Shotgun"))
                    AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.sequenceBreak");
                break;
            }
            case 6:
            {
                if (!HasWeapon("Nailgun"))
                    AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.sequenceBreak");
                break;
            }
            case 11:
            {
                if (!HasWeapon("Railcannon"))
                    AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.sequenceBreak");
                break;
            }
            case 22:
            {
                if (!HasWeapon("Rocket Launcher"))
                    AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.sequenceBreak");
                break;
            }
        }
    }

    private static bool HasWeapon(string name) =>
        (GunControl.Instance?.allWeapons ?? []).Any(weapon => weapon.name.Contains(name));
}