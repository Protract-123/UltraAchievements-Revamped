using UnityEngine;
using UnityEngine.UI;

namespace UltraAchievements_Revamped.UI;

public class AchievementDisplay : MonoBehaviour
{
    public Image icon;
    public GameObject description;
    public GameObject title;

    private void OnEnable()
    {
        icon.sprite = !Plugin.CurrentInfo.isCompleted ? Plugin.QuestionMark : Plugin.CurrentInfo.icon;
        description.GetComponent<Text>().text = Plugin.CurrentInfo.description;
        title.GetComponent<Text>().text = Plugin.CurrentInfo.name;
    }
}