using System;
using System.IO;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UltraAchievementsRevamped.Mod.Assets;

[HarmonyPatch]
public static class AssetManager
{
    // ReSharper disable once MemberCanBePrivate.Global     DO NOT MAKE THIS PRIVATE this took me 6 hours to debug
    public static string AssetPath => Path.Combine(ModFolder, "Assets");
    private static string ModFolder => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new Exception("Somehow the assembly doesn't have a location. I don't think this is possible.");
    private static string CatalogPath => Path.Combine(AssetPath, "catalog_mod.json");

    public static void LoadCatalog()
    {
        Addressables.LoadContentCatalogAsync(CatalogPath, true).WaitForCompletion();
    }
}