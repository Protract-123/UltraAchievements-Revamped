using System;

namespace UltraAchievements_Revamped;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class RegisterAchievementAttribute : Attribute
{
    public string id { get;}
    public RegisterAchievementAttribute(string id)
    {
        this.id = id;
    }
}