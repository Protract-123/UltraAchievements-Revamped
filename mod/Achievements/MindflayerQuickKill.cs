using HarmonyLib;
using UltraAchievements_Lib;
using UnityEngine;

namespace UltraAchievements_Revamped.Achievements;

[RegisterAchievement("ultraAchievements.mindflayerkill")]
[HarmonyPatch]
public class MindflayerQuickKill
{
    [HarmonyPatch(typeof(Mindflayer), nameof(Mindflayer.Death)), HarmonyPrefix]
    public static void Prefix(Mindflayer __instance)
    {
        if (__instance.gameObject.GetComponent<MindflayerTimer>().timeSinceSpawn < 3.5f)
        {
            AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(MindflayerQuickKill)));
        }
    }

    [HarmonyPatch(typeof(Mindflayer), "Start"), HarmonyPostfix]
    public static void Postfix(Mindflayer __instance)
    {
        __instance.gameObject.AddComponent<MindflayerTimer>();
    }
}


public class MindflayerTimer : MonoBehaviour
{
    public float timeSinceSpawn = 0f;

    private void Update()
    {
        timeSinceSpawn += Time.deltaTime;
    }
}