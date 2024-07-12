using System.Collections.Generic;
using System.IO;
using System.Reflection;
using GameConsole;
using UltraAchievements_Lib;
using Debug = UnityEngine.Debug;

namespace UltraAchievements_Revamped.Achievements;

[RegisterAchievement("ultraAchievements.ultracrypt")]
[RegisterCommand]
public class UltracryptReference : ICommand
{
    private readonly string _lines = File.ReadAllText(Path.Combine(Plugin.ModFolder, "CryptASCII.txt"));
    
    public void Execute(Console con, string[] args)
    {
        Debug.Log(_lines);
        AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(UltracryptReference)));
    }

    public string Name => "Ultracrypt Reference";
    public string Description => "Activates Ultracrypt ARG (WIP)";
    public string Command => "uar_ultracrypt";
}