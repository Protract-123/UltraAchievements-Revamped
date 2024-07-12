using UnityEngine;

namespace UltraAchievements_Lib;

public class AchievementInfo : ScriptableObject
{
    public string id;
    public string achName;
    public Sprite icon;
    [TextArea] public string description;
    public GameObject holderTemplate;
    public bool isHidden;
    public bool isProgressive;
    public int maxProgress;
    
    [HideInInspector] public bool isCompleted;
    [HideInInspector] public int progress;
}