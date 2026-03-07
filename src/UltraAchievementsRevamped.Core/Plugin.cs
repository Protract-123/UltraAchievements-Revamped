using System;
using System.Collections;
using BepInEx;
using BepInEx.Logging;
using UnityEngine;

namespace UltraAchievementsRevamped.Core;

[BepInPlugin(PluginInfo.Guid, PluginInfo.Name, PluginInfo.Version)]
public class Plugin : BaseUnityPlugin
{
    private static class PluginInfo {
        public const string Name = "UltraAchievementsRevamped.Core";
        public const string Guid = "protract.ultrakill.ultra_achievements_core";
        public const string Version = "2.0.0";
    }
    
    internal new static ManualLogSource Logger;
        
    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"{PluginInfo.Name} {PluginInfo.Version} has loaded!");
        
        AchievementInfo testInfo =  ScriptableObject.CreateInstance<AchievementInfo>();
        testInfo.id = "test";
        AchievementManager.RegisterAchievementInfos([testInfo]);
    }

    private void Start()
    {
        StartCoroutine(SaveAchievements());
    }

    private static IEnumerator SaveAchievements()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            
            AchievementManager.SaveAchievementProgress();
            Logger.LogInfo($"Achievements saved at {Time.time}");
        }
    }
}