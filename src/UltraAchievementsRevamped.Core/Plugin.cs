using BepInEx;
using BepInEx.Logging;

namespace UltraAchievementsRevamped.Core;

[BepInPlugin("", "", "")]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
        
    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin is loaded!");
    }
}