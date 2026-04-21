using System.Collections;
using UltraAchievementsRevamped.Core.Achievements;
using UnityEngine;
using UnityEngine.UI;

namespace UltraAchievementsRevamped.Core.UI;

public class AchievementPopUp : MonoBehaviour
{
#pragma warning disable CS0649
    [SerializeField] private TMPro.TMP_Text titleText;
    [SerializeField] private TMPro.TMP_Text descriptionText;
    [SerializeField] private Image achievementIcon;

    [SerializeField] private AudioClip achievementSound;
    [SerializeField] private HudOpenEffect hudOpenEffect;
#pragma warning restore CS0649

    private const float DestroyDelay = 10.0f;

    private void Start()
    {
        NewMovement.Instance?.GetComponent<AudioSource>().PlayOneShot(achievementSound);
        StartCoroutine(WaitAndDestroy());
    }


    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(DestroyDelay);

        hudOpenEffect.Reverse(30f);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    internal void CreateInstance(AchievementInfo achievementInfo, Transform parent)
    {
        AchievementPopUp instance = Instantiate(gameObject, parent).GetComponent<AchievementPopUp>();

        instance.titleText.text = achievementInfo.DisplayName;
        instance.descriptionText.text = achievementInfo.Description;
        instance.achievementIcon.sprite = achievementInfo.Icon;
    }
}