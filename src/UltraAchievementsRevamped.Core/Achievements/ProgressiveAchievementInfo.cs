using UnityEngine;

namespace UltraAchievementsRevamped.Core.Achievements;

public class ProgressiveAchievementInfo : AchievementInfo
{
    [SerializeField] private int maxProgress;
    public int MaxProgress => maxProgress;
    
    [System.NonSerialized] public int CurrentProgress;
}