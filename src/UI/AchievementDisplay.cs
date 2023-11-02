using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UltraAchievements_Revamped.UI;

public class AchievementDisplay : MonoBehaviour
{
    public Image Icon;
    public GameObject Description;
    public GameObject Title;

    private void OnEnable()
    {
        Icon.sprite = AchievementManager.currentInfo.Icon;
        Description.GetComponent<Text>().text = AchievementManager.currentInfo.Description;
        Title.GetComponent<Text>().text = AchievementManager.currentInfo.Name;
    }
}