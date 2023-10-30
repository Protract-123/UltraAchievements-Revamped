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
        Description.GetComponent<TextMeshPro>().text = AchievementManager.currentInfo.Description;
        Description.GetComponent<TextMeshPro>().text = AchievementManager.currentInfo.Name;
    }
}