using System.IO;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace UltraAchievements_Revamped;

[BepInPlugin("protract.ultrakill.ultra_achievements_revamped", "UltraAchievements", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    private static AssetBundle ModBundle;
    private static string ModFolder => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    private void Awake()
    {
        Harmony harmony = new Harmony("Protract.UltraAchievementsRevamped");
        harmony.PatchAll();
    }

    private void Start()
    {
        ModBundle = AssetBundle.LoadFromFile(Path.Combine(ModFolder, "assets"));
        AchievementInfo[] allInfos = ModBundle.LoadAllAssets<AchievementInfo>();
        AchievementManager.RegisterAchievementInfos(allInfos);
        AchievementManager.RegisterAllAchievements(typeof(Plugin).Assembly);
    }
    

}