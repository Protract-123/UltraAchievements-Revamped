using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace UltraAchievements_Lib;

public static class AchievementManager
{
    private static readonly Dictionary<Type, AchievementInfo> TypeToAchInfo = new();
    public static readonly Dictionary<string, AchievementInfo> IdToAchInfo = new();
    
    public static AchievementInfo CurrentInfo;

    private static string[] AllSaveLines
    {
        get
        {
            if (File.Exists(Plugin.SavePath))
            {
                return File.ReadAllLines(Plugin.SavePath);
            }

            File.Create(Plugin.SavePath);
            return Array.Empty<string>();
        }
    }

    private static string[] AllProgLines
    {
        get
        {
            if (File.Exists(Plugin.ProgSavePath))
            {
                return File.ReadAllLines(Plugin.ProgSavePath);
            }

            File.Create(Plugin.ProgSavePath);
            return Array.Empty<string>();
        }
    }


    public static void MarkAchievementComplete(AchievementInfo achInfo)
    {
        if (achInfo == null)
        {
            GameConsole.Console.Instance.ProcessInput("Achievement does not exist, please check that the id matches");
            return;
        }
        GameConsole.Console.Instance.ProcessInput($"Achievement {achInfo.achName} logged");

        if (achInfo.isCompleted) return;

        achInfo.isCompleted = true;
        if (IdToAchInfo.TryGetValue(achInfo.id, out var info))
        {
            info.isCompleted = true;
        }

        if (achInfo.holderTemplate != null)
        {
            GameObject achHolderGo = GameObject.Instantiate(achInfo.holderTemplate, CreateOverlay().transform, true);

            AchievementHolder achHolder = achHolderGo.GetComponent<AchievementHolder>();

            achHolder.description.GetComponent<Text>().text = achInfo.description;
            achHolder.title.GetComponent<Text>().text = achInfo.achName;
            achHolder.icon.sprite = achInfo.icon;
            achHolderGo.AddComponent<AchievementBehaviour>();
        }

        SaveToFile(achInfo);
        
    }

    private static void SaveToFile(AchievementInfo achInfo)
    {
        if (!File.Exists(Plugin.SavePath))
        {
            File.Create(Plugin.SavePath);
            using var sw = new StreamWriter(Plugin.SavePath);
            sw.WriteLine($"{achInfo.id}");
            sw.Close();
        }
        else if (File.Exists(Plugin.SavePath))
        {
            using var sw = new StreamWriter(Plugin.SavePath, true);
            sw.WriteLine($"{achInfo.id}");
            sw.Close();
        }
    }

    private static void SaveProgAchievement(AchievementInfo achInfo)
    {
        if (!File.Exists(Plugin.ProgSavePath))
        {
            File.Create(Plugin.ProgSavePath);

            using var sw = new StreamWriter(Plugin.ProgSavePath);
            sw.WriteLine($"{achInfo.id} - {achInfo.progress}");
            sw.Close();
        }
        else if (File.Exists(Plugin.ProgSavePath))
        {
            List<string> lines = new List<string>(File.ReadAllLines(Plugin.ProgSavePath));
            for (int index = 0; index < lines.Count; index++)
            {
                string line = lines[index];
                if (line.Contains(achInfo.id))
                {
                    lines[index] = $"{achInfo.id} - {achInfo.progress}";
                    File.WriteAllLines(Plugin.ProgSavePath, lines);
                    return;
                }
            }
            
            using var sw = new StreamWriter(Plugin.ProgSavePath, true);
            sw.WriteLine($"{achInfo.id} - {achInfo.progress}");
            sw.Close();
        }
    }

    public static void SaveAllProgAchievements()
    {
        foreach (AchievementInfo achInfo in TypeToAchInfo.Values)
        {
            if (achInfo.isProgressive)
            {
                SaveProgAchievement(achInfo);
            }
        }
    }

    public static void RegisterAchievement(Type ach)
    {
        string achId = ach.GetCustomAttribute<RegisterAchievementAttribute>().ID;
        if (IdToAchInfo.TryGetValue(achId, out var achInf))
        {
            TypeToAchInfo.Add(ach, achInf);
        }
    }
    
    public static void RegisterAllAchievements(Assembly asm)
    {
        IEnumerable<Type> achievements = asm.GetTypes().Where(IsPossibleAchievement);
        foreach (Type ach in achievements)
        {
            RegisterAchievement(ach);
        }
    }

    public static void RegisterAchievementInfos(IEnumerable<AchievementInfo> infos)
    {
        foreach (AchievementInfo info in infos)
        {
            foreach (string line in AllSaveLines)
            {
                if (info.id == line)
                {
                    info.isCompleted = true;
                }
            }

            foreach (string line in AllProgLines)
            {
                if (line.Contains(info.id) && info.isProgressive)
                {
                    info.progress = Convert.ToInt32(line.Split('-')[1]);
                }
            }
            IdToAchInfo.Add(info.id, info);
        }
    }
    
    private static bool IsPossibleAchievement(Type type) {
        if(type.IsInterface) {
            return false;
        }

        if(type.IsAbstract) {
            return false;
        }
        
        return type.GetCustomAttribute<RegisterAchievementAttribute>() != null;
    }
    
    public static AchievementInfo GetAchievementInfo(Type type)
    {
        TypeToAchInfo.TryGetValue(type, out var achInfo);
        return achInfo;
    }

    public static AchievementInfo GetAchievementInfo(string id)
    {
        IdToAchInfo.TryGetValue(id, out var achInfo);
        return achInfo;
    }
    
    
    private static GameObject CreateOverlay()
    {
        GameObject blank = new GameObject();
        blank.name = "Achievement Overlay";
        blank.AddComponent<Canvas>();
        blank.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        blank.GetComponent<Canvas>().sortingOrder = 1000;
        blank.AddComponent<CanvasScaler>();
        blank.AddComponent<GraphicRaycaster>();
        blank.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        blank.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        blank.GetComponent<CanvasScaler>().matchWidthOrHeight = 0f;
        blank.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920,1080);
        GameObject.DontDestroyOnLoad(blank);
        return blank;
    }
}