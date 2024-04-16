using HarmonyLib;
using UnityEngine;

namespace UltraAchievements_Revamped.Achievements;

[RegisterAchievement("ultraAchievements.cerbkill")]
[HarmonyPatch]
public class CerbDoubleKill : MonoBehaviour
{
    private static int kills = 0;
    private static bool timerActive = false;
    private static float timer = 0f;
    
    [HarmonyPatch(typeof(EnemyIdentifier), nameof(EnemyIdentifier.Death), [typeof(bool)])]
    public static void Prefix(EnemyIdentifier __instance)
    {
        if (__instance.gameObject.TryGetComponent<StatueBoss>(out _))
        {
            if (!__instance.dead)
            {
                kills++;
            }

            if (kills >= 2 && timer < 3f)
            {
                AchievementManager.MarkAchievementComplete(
                    AchievementManager.GetAchievementInfo(typeof(CerbDoubleKill)));
            }
            else
            {
                setTimerActive();
            }
        }
    }

    [HarmonyPatch(typeof(NewMovement), "Start"), HarmonyPostfix]
    public static void AddTimer()
    {
        NewMovement.Instance.gameObject.AddComponent<CerbDoubleKill>();
    }

    private static void setTimerActive()
    {
        timerActive = true;
        timer = 0;
    }

    private void Update()
    {
        if (timerActive)
        {
            timer += Time.deltaTime;
        }

        if (timer > 3f)
        {
            timerActive = false;
            timer = 0;
            kills = 0;
        }
    }
}
