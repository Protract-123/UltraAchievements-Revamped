using HarmonyLib;
using UltraAchievements_Lib;
using UnityEngine;

namespace UltraAchievements_Revamped.Achievements;

[RegisterAchievement("ultraAchievements.cerbkill")]
[HarmonyPatch]
public class CerbDoubleKill : MonoBehaviour
{
    private static int _kills = 0;
    private static bool _timerActive = false;
    private static float _timer = 0f;
    
    [HarmonyPatch(typeof(EnemyIdentifier), nameof(EnemyIdentifier.Death), [typeof(bool)])]
    public static void Prefix(EnemyIdentifier __instance)
    {
        if (__instance.gameObject.TryGetComponent<StatueBoss>(out _))
        {
            if (!__instance.dead)
            {
                _kills++;
            }

            if (_kills >= 2 && _timer < 3f)
            {
                AchievementManager.MarkAchievementComplete(
                    AchievementManager.GetAchievementInfo(typeof(CerbDoubleKill)));
            }
            else
            {
                SetTimerActive();
            }
        }
    }

    [HarmonyPatch(typeof(NewMovement), "Start"), HarmonyPostfix]
    public static void AddTimer()
    {
        NewMovement.Instance.gameObject.AddComponent<CerbDoubleKill>();
    }

    private static void SetTimerActive()
    {
        _timerActive = true;
        _timer = 0;
    }

    private void Update()
    {
        if (_timerActive)
        {
            _timer += Time.deltaTime;
        }

        if (_timer > 3f)
        {
            _timerActive = false;
            _timer = 0;
            _kills = 0;
        }
    }
}
