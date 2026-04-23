using System.Collections;
using HarmonyLib;
using UnityEngine;
using UltraAchievementsRevamped.Core.Achievements;

namespace UltraAchievementsRevamped.Mod.Achievements;

[HarmonyPatch]
internal class SandboxAddiction : MonoBehaviour
{
    [HarmonyPatch(typeof(ShopZone), "Start")]
    [HarmonyPostfix]
    private static void TerminalSpawnPatch(ShopZone __instance)
    {
        if (__instance.name != "Sandbox Shop") return;
        __instance.gameObject.AddComponent<SandboxAddiction>();
    }

    private void Start() => StartCoroutine(SandboxTimeCheck());

    private static IEnumerator SandboxTimeCheck()
    {
        WaitForSeconds wait = new(60f);
        while (true)
        {
            float sandboxHours = SteamController.Instance?.GetSandboxStats().hoursSpend ?? 0f;
            if (sandboxHours > 5)
            {
                AchievementManager.MarkAchievementComplete("ultraAchievementsRevamped.sandboxAddiction");
                yield break;
            }

            yield return wait;
        }
    }
}