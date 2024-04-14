using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace UltraAchievements_Revamped;

public static class AchievementManager
{
    private static readonly Dictionary<Type, AchievementInfo> TypeToAchInfo = new();
    public static readonly Dictionary<string, AchievementInfo> IdToAchInfo = new();
    
    public static AchievementInfo currentInfo;

    private static string[] allSaveLines
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

    private static string[] allProgLines
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
            GameConsole.Console.Instance.ProcessInput($"Achievement does not exist, please check that the id matches");
            return;
        }

        if (achInfo.isCompleted) return;

        achInfo.isCompleted = true;
        if (IdToAchInfo.TryGetValue(achInfo.Id, out var info))
        {
            info.isCompleted = true;
        }
        
        GameObject achHolderGO = GameObject.Instantiate(achInfo.HolderTemplate);
        achHolderGO.transform.SetParent(CreateOverlay().transform);
        
        AchievementHolder achHolder = achHolderGO.GetComponent<AchievementHolder>();
        
        achHolder.Description.GetComponent<Text>().text = achInfo.Description;
        achHolder.Title.GetComponent<Text>().text = achInfo.Name;
        achHolder.Icon.sprite = achInfo.Icon;
        achHolderGO.AddComponent<AchievementBehaviour>();
        SaveToFile(achInfo);
        
    }

    private static void SaveToFile(AchievementInfo achInfo)
    {
        if (!File.Exists(Plugin.SavePath))
        {
            File.Create(Plugin.SavePath);
            using var sw = new StreamWriter(Plugin.SavePath);
            sw.WriteLine($"{achInfo.Id}");
            sw.Close();
        }
        else if (File.Exists(Plugin.SavePath))
        {
            using var sw = new StreamWriter(Plugin.SavePath, true);
            sw.WriteLine($"{achInfo.Id}");
            sw.Close();
        }
    }

    private static void SaveProgAchievement(AchievementInfo achInfo)
    {
        if (!File.Exists(Plugin.ProgSavePath))
        {
            File.Create(Plugin.ProgSavePath);

            using var sw = new StreamWriter(Plugin.ProgSavePath);
            sw.WriteLine($"{achInfo.Id} - {achInfo.progress}");
            sw.Close();
        }
        else if (File.Exists(Plugin.ProgSavePath))
        {
            List<string> lines = new List<string>(File.ReadAllLines(Plugin.ProgSavePath));
            for (int index = 0; index < lines.Count; index++)
            {
                string line = lines[index];
                if (line.Contains(achInfo.Id))
                {
                    lines[index] = $"{achInfo.Id} - {achInfo.progress}";
                    File.WriteAllLines(Plugin.ProgSavePath, lines);
                    return;
                }
            }
            
            using var sw = new StreamWriter(Plugin.ProgSavePath, true);
            sw.WriteLine($"{achInfo.Id} - {achInfo.progress}");
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
        string achId = ach.GetCustomAttribute<RegisterAchievementAttribute>().id;
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
            foreach (string line in allSaveLines)
            {
                if (info.Id == line)
                {
                    info.isCompleted = true;
                }
            }

            foreach (string line in allProgLines)
            {
                if (line.Contains(info.Id) && info.isProgressive)
                {
                    info.progress = Convert.ToInt32(line.Split('-')[1]);
                }
            }
            IdToAchInfo.Add(info.Id, info);
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