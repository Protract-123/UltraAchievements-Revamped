using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UltraAchievementsRevamped.Core.Achievements;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

namespace UltraAchievementsRevamped.Core;

[BepInPlugin(PluginInfo.Guid, PluginInfo.Name, PluginInfo.Version)]
public class Plugin : BaseUnityPlugin
{
    public static class PluginInfo
    {
        public const string Name = "UltraAchievementsRevamped.Core";
        public const string Guid = "protract.ultrakill.ultra_achievements_core";
        public const string Version = "2.0.0";
    }

    internal new static ManualLogSource Logger;

    private void Awake()
    {
        Logger = base.Logger;
        Logger.LogInfo($"{PluginInfo.Name} {PluginInfo.Version} has loaded!");

        Harmony harmony = new(PluginInfo.Guid);
        harmony.PatchAll();

        Assets.LoadAssets();

        AchievementInfo testAchievement = Addressables
            .LoadAssetAsync<AchievementInfo>("Assets/UltraAchievementsCore/Custom Achievement.asset")
            .WaitForCompletion();

        AchievementInfo testAchievement2 = AchievementInfo.Create<AchievementInfo>("core.test2",
            "UltraAchievements.Core", testAchievement.Icon, "Code Achievement",
            "An achievement created purely in code, absolutely no Unity required!", false);

        AchievementManager.RegisterAchievementInfos([testAchievement, testAchievement2]);
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.kKey.wasPressedThisFrame)
        {
            AchievementManager.MarkAchievementComplete("core.test");
        }
    }
}