using UnityEngine;

namespace UltraAchievementsRevamped.Core.Achievements;

public class ProgressiveAchievementInfo : AchievementInfo
{
#pragma warning disable CS0649
    [SerializeField] private int maxProgress;
#pragma warning restore CS0649

    public int MaxProgress => maxProgress;

    [System.NonSerialized] public int CurrentProgress;
}