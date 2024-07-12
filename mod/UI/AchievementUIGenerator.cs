using UltraAchievements_Lib;
using UnityEngine;
using UnityEngine.UI;

namespace UltraAchievements_Revamped.UI;

public class AchievementUIGenerator : MonoBehaviour
{
    public GameObject template;
    //public GameObject listTemplate;

    private void Start()
    {
        foreach (AchievementInfo info in AchievementManager.IdToAchInfo.Values)
        {
            switch (0)
            {
                case 0:
                {
                    GameObject __instance = Instantiate(template, this.transform);
                    ShopButton button = __instance.GetComponentInChildren<ShopButton>();
                    if (!info.isHidden)
                    {
                        __instance.SetActive(true);
                    }

                    __instance.transform.GetChild(1).GetComponent<Image>().sprite = info.icon;
                    button.gameObject.AddComponent<AchievementButton>().info = info;

                    if (!info.isCompleted)
                    {
                        __instance.transform.GetChild(1).GetComponent<Image>().sprite = Plugin.QuestionMark;
                    }
                    else __instance.SetActive(true);

                    break;
                }
/*
                case 1:
                {
                    GameObject __instance = Instantiate(listTemplate, this.transform);
                    ShopButton button = __instance.GetComponentInChildren<ShopButton>();
                    if (!info.isHidden)
                    {
                        __instance.SetActive(true);
                    }

                    __instance.transform.GetChild(1).GetComponent<Image>().sprite = info.icon;
                    button.gameObject.AddComponent<AchievementButton>().info = info;

                    if (!info.isCompleted)
                    {
                        __instance.transform.GetChild(1).GetComponent<Image>().sprite = Plugin.QuestionMark;
                    }
                    else __instance.SetActive(true);
                    
                    break;
                }
*/
            }
        }
    }
}