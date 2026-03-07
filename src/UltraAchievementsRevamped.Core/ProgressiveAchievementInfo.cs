using UnityEngine;

namespace UltraAchievementsRevamped.Core;

[CreateAssetMenu(fileName = "NewProgressiveAchievementInfo", menuName = "UltraAchievements/Progressive Achievement Info")]
public class ProgressiveAchievementInfo : AchievementInfo
{
    public int maxProgress;
    [HideInInspector] public int currentProgress;
}