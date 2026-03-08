using System;
using System.Collections;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using TMPro;
using UltraAchievementsRevamped.Core.Assets;
using UltraAchievementsRevamped.Core.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

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
    internal static AchievementPopUp AchievementPopUp;
    
    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"{PluginInfo.Name} {PluginInfo.Version} has loaded!");
        
        AssetManager.LoadCatalog();
        AchievementPopUp = Addressables.LoadAssetAsync<GameObject>("Assets/UltraAchievements.Core/Achievment Overlay.prefab").WaitForCompletion().GetComponent<AchievementPopUp>();
        AchievementInfo testAchievement = Addressables.LoadAssetAsync<AchievementInfo>("Assets/UltraAchievementsCore/Custom Achievement.asset").WaitForCompletion();
        AchievementManager.RegisterAchievementInfos([testAchievement]);
    }

    private void Start()
    {
        StartCoroutine(SaveAchievements());
    }
    
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.kKey.wasPressedThisFrame)
        {
            AchievementManager.MarkAchievementComplete("core.test");
        }
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