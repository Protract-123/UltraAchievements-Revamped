using System.IO;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UltraAchievements_Revamped.Achievements;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace UltraAchievements_Revamped;

[BepInPlugin("protract.ultrakill.ultra_achievements_revamped", "UltraAchievements", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    private static AssetBundle ModBundle;
    private static string ModFolder => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    internal static Sprite questionMark;
    private static GameObject TerminalTemplate;
    
    public static readonly string SavePath = Path.Combine(Application.persistentDataPath, "achList.txt");
    public static readonly string ProgSavePath = Path.Combine(Application.persistentDataPath, "achProgress.txt");
    
    private void Awake()
    {
        Harmony harmony = new Harmony("Protract.UltraAchievementsRevamped");
        harmony.PatchAll();

        if (!File.Exists(SavePath))
        {
            File.Create(SavePath);
        }
        if (!File.Exists(ProgSavePath))
        {
            File.Create(ProgSavePath);
        }
    }

    private void Start()
    {
        ModBundle = AssetBundle.LoadFromFile(Path.Combine(ModFolder, "assets"));
        TerminalTemplate = ModBundle.LoadAsset<GameObject>("Achievements Terminal.prefab");
        AchievementInfo[] allInfos = ModBundle.LoadAllAssets<AchievementInfo>();
        ModBundle.LoadAllAssets();
        
        AchievementManager.RegisterAchievementInfos(allInfos);
        AchievementManager.RegisterAllAchievements(typeof(Plugin).Assembly);
        
        questionMark = Addressables.LoadAssetAsync<Sprite>("Assets/Textures/UI/questionMark.png")
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
        GameObject term = Instantiate(TerminalTemplate, firstRoom.transform);
        term.transform.localPosition = new Vector3(10, 2, 32);
        term.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void OnDestroy()
    {
        AchievementManager.SaveAllProgAchievements();
    }
}