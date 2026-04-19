using BepInEx;
using BepInEx.Logging;

namespace UltraAchievementsRevamped.Mod;

[BepInPlugin(PluginInfo.Guid, PluginInfo.Name, PluginInfo.Version)]
[BepInDependency(Core.Plugin.PluginInfo.Guid, Core.Plugin.PluginInfo.Version)]
public class Plugin : BaseUnityPlugin
{
    private static class PluginInfo {
        public const string Name = "UltraAchievementsRevamped.Mod";
        public const string Guid = "protract.ultrakill.ultra_achievements_revamped";
        public const string Version = "2.0.0";
    }
    
    internal new static ManualLogSource Logger;
        
    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"{PluginInfo.Name} {PluginInfo.Version} has loaded!");
    }
}