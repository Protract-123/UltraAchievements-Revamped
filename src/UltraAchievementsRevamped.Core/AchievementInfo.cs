using UnityEngine;

namespace UltraAchievementsRevamped.Core;

[CreateAssetMenu(fileName = "NewAchievementInfo", menuName = "UltraAchievements/Achievement Info")]
public class AchievementInfo : ScriptableObject
{
    public string id;
    public string sourceMod;
    public Sprite icon;
    public string displayName;
    [TextArea] public string description;
    public bool isHidden;
    
    [HideInInspector] public bool isComplete;
}