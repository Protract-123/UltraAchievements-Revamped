using System;

namespace UltraAchievements_Lib;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class RegisterAchievementAttribute : Attribute
{
    public string ID { get;}
    public RegisterAchievementAttribute(string id)
    {
        this.ID = id;
    }
}