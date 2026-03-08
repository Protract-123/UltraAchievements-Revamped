using UnityEngine;

namespace UltraAchievementsRevamped.Core;

public class AchievementInfo : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private string sourceMod;
    [SerializeField] private Sprite icon;
    [SerializeField] private string displayName;
    [SerializeField, TextArea] private string description;
    [SerializeField] private bool isHidden;

    public string Id => id;
    public string SourceMod => sourceMod;
    public Sprite Icon => icon;
    public string DisplayName => displayName;
    public string Description => description;
    public bool IsHidden => isHidden;
    
    [System.NonSerialized] public bool IsComplete;
}