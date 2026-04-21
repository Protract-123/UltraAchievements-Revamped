using System.Collections.Generic;
using HarmonyLib;
using TMPro;
using UltraAchievementsRevamped.Core.Achievements;
using UnityEngine;
using UnityEngine.UI;

namespace UltraAchievementsRevamped.Core.UI;

[HarmonyPatch]
public class AchievementPanel : MonoBehaviour
{
    public GameObject modButtonTemplate;
    public GameObject modPanelTemplate;
    public GameObject achievementTemplate;

    public ShopButton backButton;

    public int itemsPerPage;
    private readonly List<GameObject> modButtons = [];
    private readonly List<Button> pageButtons = [];
    private readonly List<GameObject> modPanels = [];

    public void Start()
    {
        Transform pageButtonParent = transform.Find("Left Panel/Buttons/Page Buttons");
        foreach (Transform child in pageButtonParent)
        {
            Button btn = child.GetComponent<Button>();
            if (btn != null) pageButtons.Add(btn);
        }

        for (int i = 0; i < pageButtons.Count; i++)
        {
            int pageIndex = i;
            pageButtons[i].onClick.AddListener(() => GoToPage(pageIndex));
        }

        foreach ((string modName, List<AchievementInfo> achievements) in AchievementManager.ModNameToAchInfo)
            CreateModButton(modName, achievements);


        GoToPage(0);
    }

    private void CreateModButton(string modName, List<AchievementInfo> achievements)
    {
        Transform parent = transform.Find("Left Panel/Buttons/Mod Buttons");
        GameObject modButton = Instantiate(modButtonTemplate, parent);
        modButton.GetComponentInChildren<TMP_Text>().text = modName;

        int panelIndex = modButtons.Count;
        modButton.GetComponent<Button>().onClick.AddListener(() => SelectModPanel(panelIndex));

        CreateModPanel(modName, achievements);
        modButtons.Add(modButton);
    }

    private void CreateModPanel(string modName, List<AchievementInfo> achievements)
    {
        GameObject modPanel = Instantiate(modPanelTemplate, modPanelTemplate.transform.parent);
        modPanel.transform.Find("Title").GetComponent<TMP_Text>().text = modName;
        modPanel.SetActive(false);

        Transform achievementContent = modPanel.transform.Find("Achievement List/Viewport/Content");

        foreach (AchievementInfo achievement in achievements)
        {
            GameObject entry = Instantiate(achievementTemplate, achievementContent);
            entry.SetActive(true);
            entry.transform.Find("Name").GetComponent<TMP_Text>().text = achievement.DisplayName;
            entry.transform.Find("Description").GetComponent<TMP_Text>().text = achievement.Description;
            entry.transform.Find("Icon").GetComponent<Image>().sprite = achievement.Icon;
        }

        modPanels.Add(modPanel);
    }

    private void SelectModPanel(int index)
    {
        for (int i = 0; i < modPanels.Count; i++)
            modPanels[i].SetActive(i == index);
    }

    private void GoToPage(int pageIndex)
    {
        int start = pageIndex * itemsPerPage;
        int end = start + itemsPerPage;

        for (int i = 0; i < modButtons.Count; i++)
        {
            modButtons[i].SetActive(i >= start && i < end);
        }

        int totalPages = Mathf.CeilToInt((float)modButtons.Count / itemsPerPage);

        for (int i = 0; i < pageButtons.Count; i++)
        {
            if (i >= totalPages)
            {
                pageButtons[i].interactable = false;
                pageButtons[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            }
            else if (i == pageIndex)
            {
                pageButtons[i].interactable = true;
                pageButtons[i].GetComponent<Image>().color = new Color(255, 0, 0, 255);
            }
            else
            {
                pageButtons[i].interactable = true;
                pageButtons[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            }
        }
    }

    [HarmonyPatch(typeof(ShopZone), "Start")]
    [HarmonyPostfix]
    private static void AchievementPanelPatch(ShopZone __instance)
    {
        GameObject mainPanel = __instance.transform.Find("Canvas/Background/Main Panel").gameObject;
        AchievementPanel achievementPanel = Instantiate(Assets.AchievementPanel, mainPanel.transform);

        GameObject terminalIcon = __instance.transform.Find("Canvas/Background/Icon").gameObject;
        terminalIcon.AddComponent<Button>().interactable = true;
        terminalIcon.GetComponent<Image>().raycastTarget = true;

        ShopButton achievementPanelButton = terminalIcon.AddComponent<ShopButton>();
        achievementPanelButton.toActivate = [achievementPanel.gameObject];
        achievementPanelButton.toDeactivate = [];
        achievementPanelButton.clickSound = achievementPanel.backButton.clickSound;
    }
}