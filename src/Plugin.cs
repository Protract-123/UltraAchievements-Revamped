using System;
using System.IO;
using System.Reflection;
using BepInEx;
using UnityEngine;
using Logger = BepInEx.Logging.Logger;

namespace UltraAchievements_Revamped;

[BepInPlugin("protract.ultrakill.ultra_achievements_revamped", "UltraAchievements", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    private static AssetBundle ModBundle;
    public static string ModFolder => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        

    private void Start()
    {
        ModBundle = AssetBundle.LoadFromFile(Path.Combine(ModFolder, "assets"));
        AchievementInfo[] allInfos = ModBundle.LoadAllAssets<AchievementInfo>();
        AchievementManager.RegisterAchievementInfos(allInfos);
        AchievementManager.RegisterAllAchievements(typeof(Plugin).Assembly);
    }
}