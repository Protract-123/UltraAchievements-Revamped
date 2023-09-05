using UnityEngine;
using UnityEngine.UI;

namespace UltraAchievements_Revamped;

public class AchievementInfo : ScriptableObject
{
    public string Id;
    public string Name;
    public Sprite Icon;
    public string Title;
    [TextArea] public string Description;
    public Sprite BgSprite;
}