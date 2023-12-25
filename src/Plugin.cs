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
    public static GameObject TerminalTemplate;

    private void Awake()
    {
        Harmony harmony = new Harmony("Protract.UltraAchievementsRevamped");
        harmony.PatchAll();
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
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject term = Instantiate(TerminalTemplate, NewMovement.Instance.transform.position + new Vector3(0, 10, 0),
                Quaternion.identity);
            term.transform.rotation = new Quaternion(180, 0, 0, 0);
            
            AllPRanks.CheckRanks();
        }
        OneinMillion.Check();
        Florp.FlorpCheck();
    }

    private void OnSceneChange(Scene current, Scene next)
    {
        AllPRanks.CheckRanks();
    }
}