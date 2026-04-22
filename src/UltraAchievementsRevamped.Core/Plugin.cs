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
    }
}