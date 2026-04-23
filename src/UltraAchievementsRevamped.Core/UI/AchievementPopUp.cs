using System.Collections;
using TMPro;
using UltraAchievementsRevamped.Core.Achievements;
using UnityEngine;
using UnityEngine.UI;

namespace UltraAchievementsRevamped.Core.UI;

public class AchievementPopUp : MonoBehaviour
{
#pragma warning disable CS0649
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image achievementIcon;

    [SerializeField] private AudioClip achievementSound;
    [SerializeField] private HudOpenEffect hudOpenEffect;
#pragma warning restore CS0649

    private const float DestroyDelay = 10.0f;
    private const float HUDReverseSpeed = 30f;
    private const float HUDReverseTime = 1f;

    private void Start()
    {
        NewMovement.Instance?.GetComponent<AudioSource>()?.PlayOneShot(achievementSound);
        StartCoroutine(WaitAndDestroy());
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(DestroyDelay);

        hudOpenEffect.Reverse(HUDReverseSpeed);
        yield return new WaitForSeconds(HUDReverseTime);
        Destroy(gameObject);
    }

    internal static void CreateInstance(AchievementInfo achievementInfo, Transform parent)
    {
        AchievementPopUp instance = Instantiate(Assets.AchievementPopUpPrefab, parent);

        instance.titleText.text = achievementInfo.DisplayName;
        instance.descriptionText.text = achievementInfo.Description;
        instance.achievementIcon.sprite = achievementInfo.Icon;
    }
}