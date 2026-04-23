using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UltraAchievementsRevamped.Core.Achievements;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UltraAchievementsRevamped.Mod;

internal static class Assets
{
    // ReSharper disable once MemberCanBePrivate.Global
    // This can't be private since it breaks addressable bundles if it is
    public static string AssetPath => Path.Combine(ModFolder, "Assets");
    private static string ModFolder => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    private static string CatalogPath => Path.Combine(AssetPath, "catalog_mod.json");

    internal static void LoadAssets()
    {
        Addressables.LoadContentCatalogAsync(CatalogPath, true).WaitForCompletion();

        List<AchievementInfo> achievementInfos = [];
        Addressables.LoadAssetsAsync<AchievementInfo>("UltraAchievementsMod", (asset) =>
        {
            Debug.Log($"Loaded Achievement: {asset.Id}");
            achievementInfos.Add(asset);
        }).WaitForCompletion();

        AchievementManager.RegisterAchievementInfos(achievementInfos);
    }
}