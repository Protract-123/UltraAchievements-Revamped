﻿using UltraAchievements_Lib;
using UnityEngine;

namespace UltraAchievements_Revamped.Achievements;

[RegisterAchievement("ultraAchievements.oneinmillion")]
public class OneinMillion {
    public static void Check()
    {
        int num = Random.Range(0, 1000000);
        if (num == 1)
        {
            AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(OneinMillion)));
        }
    }
}