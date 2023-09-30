using UnityEngine;

namespace UltraAchievements_Revamped;

public class AchievementInfo : ScriptableObject
{
    public string Id;
    public string Name;
    public Sprite Icon;
    [TextArea] public string Description;
    public GameObject HolderTemplate;
    [HideInInspector] public bool isCompleted;
}