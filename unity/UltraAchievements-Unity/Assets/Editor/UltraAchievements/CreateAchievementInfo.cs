using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UltraAchievementsRevamped.Core;

public class CreateAchievementInfo : MonoBehaviour
{
    [MenuItem("Assets/Create/UltraAchievements/Custom Achievement")]
    public static void CreateCustomAchievement() 
    {
       AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<AchievementInfo>(), "Assets/Custom Achievement.asset");
        Debug.Log("New Achievement Info Created in Assets/AchievementInfo");
    }

    [MenuItem("Assets/Create/UltraAchievements/Custom Progressive Achievement")]
    public static void CreateCustomProgressiveAchievement() 
    {
       AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<ProgressiveAchievementInfo>(), "Assets/Custom Progressive Achievement.asset");
        Debug.Log("New Achievement Info Created in Assets/AchievementInfo");
    }
}
