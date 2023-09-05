using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace UltraAchievements_Revamped;

public static class AchievementManager
{
    public static Dictionary<Type, AchievementInfo> TypeToAchInfo = new Dictionary<Type, AchievementInfo>();
    public static Dictionary<string, AchievementInfo> IdToAchInfo = new Dictionary<string, AchievementInfo>();

    public static void MarkAchievementComplete(AchievementInfo achInfo)
    {
        GameObject achHolder = new GameObject();
    }
    

    public static void RegisterAchievement<T>(T ach) where T : Achievement
    {
        string achId = typeof(T).GetCustomAttribute<RegisterAchievementAttribute>().id;
        if (IdToAchInfo.TryGetValue(achId, out var achInf))
        {
            TypeToAchInfo.Add(typeof(T), achInf);
        }
    }
    
    public static void RegisterAllAchievements(Assembly asm)
    {
        IEnumerable<Type> achievements = asm.GetTypes().Where(IsPossibleAchievement);
        foreach (Type ach in achievements)
        {
            RegisterAchievement<>(ach);
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
}