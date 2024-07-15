using System;
using HarmonyLib;
using UltraAchievements_Lib;
using UnityEngine;
using UnityEngine.Serialization;

namespace UltraAchievements_Revamped.Achievements;

[RegisterAchievement("ultraAchievements.sandbox")]
[HarmonyPatch(typeof(NewMovement), "Start")]
public class SandboxAddict : MonoBehaviour
{
    private static AchievementInfo Info => AchievementManager.GetAchievementInfo(typeof(SandboxAddict));
    private static float _timeSpent = -2f;

    private void Update()
    {
        if (_timeSpent <= -1)
        {
            _timeSpent = Info.progress;
        }
        //Debug.Log(SceneHelper.CurrentScene);
        if (SceneHelper.CurrentScene == "uk_construct")
        {
            _timeSpent += Time.deltaTime;
            Info.progress = (int)_timeSpent;
        }

        if (_timeSpent > Info.maxProgress)
        {
            AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(SandboxAddict)));
        }
    }

    [HarmonyPostfix]
    private static void StartPatch()
    {
        NewMovement.Instance.gameObject.AddComponent<SandboxAddict>();
    }
}