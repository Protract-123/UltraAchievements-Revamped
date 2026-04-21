using UnityEngine;

namespace UltraAchievementsRevamped.Core.Achievements;

public class ProgressiveAchievementInfo : AchievementInfo
{
#pragma warning disable CS0649
    [SerializeField] private int maxProgress;
#pragma warning restore CS0649

    public int MaxProgress => maxProgress;

    [System.NonSerialized] public int CurrentProgress;

    public static ProgressiveAchievementInfo Create(string id, string sourceMod, Sprite icon, string displayName,
        string description, bool isHidden, int maxProgress)
    {
        ProgressiveAchievementInfo info =
            Create<ProgressiveAchievementInfo>(id, sourceMod, icon, displayName, description, isHidden);
        info.maxProgress = maxProgress;

        return info;
    }
}