using System.Collections.Generic;
using HarmonyLib;
using UltraAchievements_Lib;
using UnityEngine;

namespace UltraAchievements_Revamped.Achievements;

[HarmonyPatch(typeof(StatsManager), "GetFinalRank")]
[RegisterAchievement("ultraAchievements.sequencebreak")]
public class SequenceBreak
{
    //private static readonly List<int> LevelNumbers = [1, 3, 6, 11, 22];
    
    public static void Postfix()
    {
        switch (StatsManager.Instance.levelNumber)
        {
            case 1:
            {
                if (!HasWeapon("Revolver"))
                {
                    AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(SequenceBreak)));
                }
                break;
            }
            case 3:
            {
                if (!HasWeapon("Shotgun"))
                {
                    AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(SequenceBreak)));
                }
                break;
            }
            case 6:
            {
                if (!HasWeapon("Nailgun"))
                {
                    AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(SequenceBreak)));
                }
                break;
            }
            case 11:
            {
                if (!HasWeapon("Railcannon"))
                {
                    AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(SequenceBreak)));
                }
                break;
            }
            case 22:
            {
                if (!HasWeapon("Rocket Launcher"))
                {
                    AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(SequenceBreak)));
                }
                break;
            }
        }
    }

    private static bool HasWeapon(string name)
    {
        foreach (GameObject weapon in GunControl.Instance.allWeapons)
        {
            if (weapon.name.Contains(name))
            {
                return true;
            }
        }
        
        return false;
    }
}