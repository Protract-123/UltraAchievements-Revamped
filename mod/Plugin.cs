using System.IO;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UltraAchievements_Lib;
using UltraAchievements_Revamped.Achievements;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace UltraAchievements_Revamped;

[BepInPlugin("protract.ultrakill.ultra_achievements_revamped", "UltraAchievements", "1.0.0")]
[BepInDependency("protract.ultrakill.ultra_achievements_lib", BepInDependency.DependencyFlags.HardDependency)]
public class Plugin : BaseUnityPlugin
{
    private static AssetBundle _modBundle;
    private static string ModFolder => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    internal static Sprite QuestionMark;
    private static GameObject _terminalTemplate;

    internal static AchievementInfo CurrentInfo;
    
    private void Awake()
    {
        Harmony harmony = new Harmony("Protract.UltraAchievementsRevamped");
        harmony.PatchAll();
    }

    private void Start()
    {
        _modBundle = AssetBundle.LoadFromFile(Path.Combine(ModFolder, "assets"));
        _terminalTemplate = _modBundle.LoadAsset<GameObject>("Achievements Terminal.prefab");
        AchievementInfo[] allInfos = _modBundle.LoadAllAssets<AchievementInfo>();
        _modBundle.LoadAllAssets();
        
        AchievementManager.RegisterAchievementInfos(allInfos);
        AchievementManager.RegisterAllAchievements(typeof(Plugin).Assembly);
        
        QuestionMark = Addressables.LoadAssetAsync<Sprite>("Assets/Textures/UI/questionMark.png")
            .WaitForCompletion();

        SceneManager.activeSceneChanged += OnSceneChange;
    }

    private void Update()
    {
        OneinMillion.Check();
        Florp.FlorpCheck();
    }

    private void OnSceneChange(Scene current, Scene next)
    {
        AllPRanks.CheckRanks();
        
        FirstRoomPrefab room = FindObjectOfType<FirstRoomPrefab>();
        if (room == null) return;
        
        GameObject firstRoom = room.transform.GetChild(0).gameObject;

        if (firstRoom == null) return;
        GameObject term = Instantiate(_terminalTemplate, firstRoom.transform);
        term.transform.localPosition = new Vector3(10, 2, 32);
        term.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}