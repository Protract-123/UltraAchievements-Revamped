using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace BuildPipeline.Editor.Config
{
    public class PipelineSettings : ScriptableObject
    {
        public const string SettingsFileName = "Settings.asset";
        public static readonly string SettingsPath = Path.Combine("Assets", "Build Pipeline", SettingsFileName);

        public List<ModConfig> Mods = new List<ModConfig>();

        public static PipelineSettings Instance
        {
            get
            {
                PipelineSettings? instance = AssetDatabase.LoadAssetAtPath<PipelineSettings>(SettingsPath);

                if (instance == null)
                {
                    instance = CreateInstance<PipelineSettings>();
                    AssetDatabase.CreateAsset(instance, SettingsPath);
                }

                return instance;
            }
        }
    }
}