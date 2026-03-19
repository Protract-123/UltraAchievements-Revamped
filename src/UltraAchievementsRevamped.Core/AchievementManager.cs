using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UltraAchievementsRevamped.Core;

public static class AchievementManager
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "achievements.dat");
    private const int SaveFormatVersion = 1;
    private static Dictionary<string, (bool isComplete, int? progress)> _saveDataCache;

    private static readonly Dictionary<string, AchievementInfo> IdToAchInfo = new();
    
    public static void RegisterAchievementInfos(IEnumerable<AchievementInfo> infos)
    {
        foreach (AchievementInfo info in infos)
        {
            if (info == null)
            {
                Plugin.Logger.LogError("Cannot register null achievement info");
                continue;                
            }

            if (IdToAchInfo.ContainsKey(info.Id))
            {
                Plugin.Logger.LogError($"Achievement with id {info.Id} has already been registered");
                continue;
            }
            
            switch (info)
            {
                case ProgressiveAchievementInfo progressiveInfo:
                    RegisterProgressiveInfo(progressiveInfo);
                    break;
                default:
                    RegisterInfo(info);
                    break;
            }
            Plugin.Logger.LogInfo($"Registered achievement with id: {info.Id}");
        }
    }
    
    public static AchievementInfo GetAchievementInfo(string id)
    {
        IdToAchInfo.TryGetValue(id, out AchievementInfo achInfo);
        
        if (achInfo == null) Plugin.Logger.LogError($"Achievement {id} has not been registered");
        
        return achInfo;
    }

    public static void MarkAchievementComplete(string id)
    {
        AchievementInfo achievementInfo = GetAchievementInfo(id);

        if (achievementInfo == null) return; // Already logged by GetAchievementInfo
        
        if (achievementInfo.IsComplete) return;
        
        achievementInfo.IsComplete = true;
        Plugin.AchievementPopUp.CreateInstance(achievementInfo, null);
        Plugin.Logger.LogInfo($"Marked achievement {achievementInfo.Id} as complete");
        
        SaveAchievementProgress();
    }

    public static void MarkAchievementComplete(AchievementInfo achievementInfo)
    {
        if (achievementInfo == null)
        {
            Plugin.Logger.LogError("AchievementInfo is not valid");
            return;
        }
        
        if (achievementInfo.IsComplete) return;
        
        achievementInfo.IsComplete = true;
        Plugin.AchievementPopUp.CreateInstance(achievementInfo, null);
        Plugin.Logger.LogInfo($"Marked achievement {achievementInfo.Id} as complete");
        
        SaveAchievementProgress();
    }

    public static void AddProgressToAchievement(string id, int amount)
    {
        AchievementInfo achievementInfo = GetAchievementInfo(id);

        if (achievementInfo == null) return; // Already logged by GetAchievementInfo

        if (achievementInfo is ProgressiveAchievementInfo progressiveAchievementInfo)
        {
            progressiveAchievementInfo.CurrentProgress += amount;
            if (progressiveAchievementInfo.CurrentProgress >= progressiveAchievementInfo.MaxProgress)
            {
                MarkAchievementComplete(progressiveAchievementInfo);
            }
        }
        else Plugin.Logger.LogError($"Achievement {id} is not a progressive achievement");
        
        SaveAchievementProgress();
    }

    public static void AddProgressToAchievement(AchievementInfo achievementInfo, int amount)
    {
        if (achievementInfo == null)
        {
            Plugin.Logger.LogError("AchievementInfo is not valid");
            return;
        }
        
        if (achievementInfo is ProgressiveAchievementInfo progressiveAchievementInfo)
        {
            progressiveAchievementInfo.CurrentProgress += amount;
            if (progressiveAchievementInfo.CurrentProgress >= progressiveAchievementInfo.MaxProgress)
            {
                MarkAchievementComplete(progressiveAchievementInfo);
            }
        }
        else Plugin.Logger.LogError($"Achievement {achievementInfo.Id} is not a progressive achievement");

        SaveAchievementProgress();
    }

    private static void RegisterInfo(AchievementInfo info)
    {
        _saveDataCache ??= LoadAchievementProgress();
        
        if (_saveDataCache.TryGetValue(info.Id, out (bool isComplete, int? progress) saveData))
        {
            info.IsComplete = saveData.isComplete;
        }
        
        IdToAchInfo.Add(info.Id, info);
    }

    private static void RegisterProgressiveInfo(ProgressiveAchievementInfo info)
    {
        _saveDataCache ??= LoadAchievementProgress();

        if (_saveDataCache.TryGetValue(info.Id, out (bool isComplete, int? progress) saveData))
        {
            info.IsComplete = saveData.isComplete;

            if (saveData.progress.HasValue)
                info.CurrentProgress = saveData.progress.Value;
        }

        IdToAchInfo.Add(info.Id, info);
    }
    
    private static void SaveAchievementProgress()
    {
        using BinaryWriter saveWriter = new(File.OpenWrite(SavePath));
        saveWriter.Write(SaveFormatVersion);
        saveWriter.Write(IdToAchInfo.Count);

        foreach ((string id, AchievementInfo info) in IdToAchInfo)
        {
            saveWriter.Write(id);
            saveWriter.Write(info.IsComplete);
            saveWriter.Write(info is ProgressiveAchievementInfo);

            if (info is ProgressiveAchievementInfo progressive)
                saveWriter.Write(progressive.CurrentProgress);
        }
        
        Plugin.Logger.LogInfo($"Achievements saved at {Time.time}");
    }

    private static Dictionary<string, (bool isComplete, int? progress)> LoadAchievementProgress()
    {
        Dictionary<string, (bool, int?)> achievementProgress = new();
        if (!File.Exists(SavePath)) return achievementProgress;

        try
        {
            using BinaryReader saveReader = new(File.OpenRead(SavePath));
            int formatVersion = saveReader.ReadInt32();
            int count = saveReader.ReadInt32();

            for (int i = 0; i < count; i++)
            {
                string id = saveReader.ReadString();
                bool isComplete = saveReader.ReadBoolean();
                bool hasProgress = saveReader.ReadBoolean();
                int? progress = hasProgress ? saveReader.ReadInt32() : null;

                achievementProgress[id] = (isComplete, progress);
            }
        }
        catch { return achievementProgress; }

        return achievementProgress;
    }
}