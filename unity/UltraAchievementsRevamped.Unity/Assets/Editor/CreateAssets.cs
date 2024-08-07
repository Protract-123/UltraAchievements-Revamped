﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UltraAchievements_Lib;

public class CreateAssets : MonoBehaviour
{
    [MenuItem("Assets/Create/UltraAchievements/Custom Achievement")]
    public static void CreateCustomEnemyInfo() 
    {
       AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<AchievementInfo>(), "Assets/Mod Assets/AchievementInfo/Custom Achievement.asset");
        Debug.Log("New Achievement Info Created in Assets/AchievementInfo");
    }
}
