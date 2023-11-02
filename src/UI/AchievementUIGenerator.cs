using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltraAchievements_Revamped.UI;

public class AchievementUIGenerator : MonoBehaviour
{
    public GameObject Template;

    private void Start()
    {
        foreach (AchievementInfo info in AchievementManager.IdToAchInfo.Values)
        {
            GameObject instance = Instantiate(Template, this.transform);
            ShopButton button = instance.GetComponentInChildren<ShopButton>();
            instance.SetActive(true);
            if (!info.isCompleted)
            {
                instance.transform.GetChild(1).GetComponent<Image>().sprite = Plugin.questionMark;
                button.failure = true;
                
                break;
            }
            instance.transform.GetChild(1).GetComponent<Image>().sprite = info.Icon;
            button.gameObject.AddComponent<AchievementButton>().info = info;
        }
    }
}