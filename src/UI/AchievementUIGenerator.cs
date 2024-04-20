using UnityEngine;
using UnityEngine.UI;

namespace UltraAchievements_Revamped.UI;

public class AchievementUIGenerator : MonoBehaviour
{
    public GameObject Template;
    public GameObject ListTemplate;
    public int UIMode = 0;

    private void Start()
    {
        foreach (AchievementInfo info in AchievementManager.IdToAchInfo.Values)
        {
            switch (UIMode)
            {
                case 0:
                {
                    GameObject instance = Instantiate(Template, this.transform);
                    ShopButton button = instance.GetComponentInChildren<ShopButton>();
                    if (!info.isHidden)
                    {
                        instance.SetActive(true);
                    }

                    instance.transform.GetChild(1).GetComponent<Image>().sprite = info.Icon;
                    button.gameObject.AddComponent<AchievementButton>().info = info;

                    if (!info.isCompleted)
                    {
                        instance.transform.GetChild(1).GetComponent<Image>().sprite = Plugin.questionMark;
                    }
                    else instance.SetActive(true);

                    break;
                }
                case 1:
                {
                    GameObject instance = Instantiate(ListTemplate, this.transform);
                    ShopButton button = instance.GetComponentInChildren<ShopButton>();
                    if (!info.isHidden)
                    {
                        instance.SetActive(true);
                    }

                    instance.transform.GetChild(1).GetComponent<Image>().sprite = info.Icon;
                    button.gameObject.AddComponent<AchievementButton>().info = info;

                    if (!info.isCompleted)
                    {
                        instance.transform.GetChild(1).GetComponent<Image>().sprite = Plugin.questionMark;
                    }
                    else instance.SetActive(true);
                    
                    break;
                }
            }
        }
    }
}