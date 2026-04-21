using System.IO;
using System.Reflection;
using UltraAchievementsRevamped.Core.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UltraAchievementsRevamped.Core;

internal static class Assets
{
    // ReSharper disable once MemberCanBePrivate.Global
    // This can't be private since it breaks addressable bundles if it is
    public static string AssetPath => Path.Combine(ModFolder, "Assets");
    private static string ModFolder => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    private static string CatalogPath => Path.Combine(AssetPath, "catalog_core.json");

    internal static AchievementPopUp AchievementPopUpPrefab;
    internal static AchievementPanel AchievementPanelPrefab;

    internal static void LoadAssets()
    {
        Addressables.LoadContentCatalogAsync(CatalogPath, true).WaitForCompletion();

        AchievementPopUpPrefab = Addressables
            .LoadAssetAsync<GameObject>("Assets/UltraAchievementsCore/Achievement Overlay.prefab").WaitForCompletion()
            .GetComponent<AchievementPopUp>();

        AchievementPanelPrefab = Addressables
            .LoadAssetAsync<GameObject>("Assets/UltraAchievementsCore/Achievement Panel.prefab").WaitForCompletion()
            .GetComponent<AchievementPanel>();
    }
}