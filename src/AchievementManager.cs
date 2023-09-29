using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mono.CompilerServices.SymbolWriter;
using UnityEngine;

namespace UltraAchievements_Revamped;

public static class AchievementManager
{
    public static readonly Dictionary<Type, AchievementInfo> TypeToAchInfo = new();
    public static Dictionary<string, AchievementInfo> IdToAchInfo = new();

    public static void MarkAchievementComplete(AchievementInfo achInfo)
    {
        Debug.Log(achInfo.Name);
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
}