using System.IO;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace UltraAchievements_Lib;

[BepInPlugin("protract.ultrakill.ultra_achievements_lib", "UltraAchievementsLib", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    public static readonly string SavePath = Path.Combine(Application.persistentDataPath, "achList.txt");
    public static readonly string ProgSavePath = Path.Combine(Application.persistentDataPath, "achProgress.txt");
    
    private void Awake()
    {
        Harmony harmony = new Harmony("Protract.UltraAchievements_Lib");
        harmony.PatchAll();

        if (!File.Exists(SavePath))
        {
            File.Create(SavePath);
        }
        if (!File.Exists(ProgSavePath))
        {
            File.Create(ProgSavePath);
        }
    }
    
    private void OnDestroy()
    {
        AchievementManager.SaveAllProgAchievements();
    }
}