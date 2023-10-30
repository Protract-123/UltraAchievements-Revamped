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
            if (!info.isCompleted)
            {
                instance.transform.GetChild(1).GetComponent<Image>().sprite = Plugin.questionMark;
                instance.AddComponent<AchievementButton>().deactivated = true;
                Destroy(Template);
                return;
            }
            
            instance.transform.GetChild(1).GetComponent<Image>().sprite = info.Icon;
            instance.AddComponent<AchievementButton>().info = info;
            Destroy(Template);
        }
    }
}