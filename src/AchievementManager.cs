using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UltraAchievements_Revamped;

public static class AchievementManager
{
    private static readonly Dictionary<Type, AchievementInfo> TypeToAchInfo = new();
    private static Dictionary<string, AchievementInfo> IdToAchInfo = new();
    

    public static void MarkAchievementComplete(AchievementInfo achInfo)
    {
        if (achInfo.isCompleted) return;

        achInfo.isCompleted = true;
        GameObject achHolderGO = GameObject.Instantiate(achInfo.HolderTemplate);
        achHolderGO.transform.SetParent(CreateOverlay().transform);
        
        AchievementHolder achHolder = achHolderGO.GetComponent<AchievementHolder>();
        
        achHolder.Description.GetComponent<Text>().text = achInfo.Description;
        achHolder.Title.GetComponent<Text>().text = achInfo.Name;
        achHolder.Icon.sprite = achInfo.Icon;
        achHolderGO.AddComponent<AchievementBehaviour>();
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
        blank.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        blank.GetComponent<CanvasScaler>().matchWidthOrHeight = 0f;
        blank.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920,1080);
        GameObject.DontDestroyOnLoad(blank);
        return blank;
    }
}