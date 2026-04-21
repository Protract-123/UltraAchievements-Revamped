using UnityEngine;

namespace UltraAchievementsRevamped.Core.Achievements;

public class AchievementInfo : ScriptableObject
{
#pragma warning disable CS0649
    [SerializeField] private string id;
    [SerializeField] private string sourceMod;
    [SerializeField] private Sprite icon;
    [SerializeField] private string displayName;
    [SerializeField, TextArea] private string description;
    [SerializeField] private bool isHidden;
#pragma warning restore CS0649

    public string Id => id;
    public string SourceMod => sourceMod;
    public Sprite Icon => icon;
    public string DisplayName => displayName;
    public string Description => description;
    public bool IsHidden => isHidden;

    [System.NonSerialized] public bool IsComplete;
}