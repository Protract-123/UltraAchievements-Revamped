using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BuildPipeline.Editor.Config
{
    public class ConfigWindow : EditorWindow
    {
        private Vector2 _scrollPosition;
        private List<bool> _modFoldouts = new List<bool>();
        private List<bool> _groupFoldouts = new List<bool>();

        [MenuItem("Addressable Build Pipeline/Config")]
        public static void ShowWindow() => GetWindow<ConfigWindow>("WBP Config");

        private void OnGUI()
        {
            PipelineSettings instance = PipelineSettings.Instance;
            List<ModConfig> mods = instance.Mods;

            // Sync foldout lists
            while (_modFoldouts.Count < mods.Count) _modFoldouts.Add(true);
            while (_groupFoldouts.Count < mods.Count) _groupFoldouts.Add(false);

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            EditorGUILayout.LabelField("Mod Configurations", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            int removeIndex = -1;

            for (int i = 0; i < mods.Count; i++)
            {
                ModConfig mod = mods[i];

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                EditorGUILayout.BeginHorizontal();
                _modFoldouts[i] = EditorGUILayout.Foldout(_modFoldouts[i], string.IsNullOrEmpty(mod.Name) ? "Unnamed Mod" : mod.Name, true);
                if (GUILayout.Button("Remove", GUILayout.Width(60)))
                {
                    removeIndex = i;
                }
                EditorGUILayout.EndHorizontal();

                if (_modFoldouts[i])
                {
                    EditorGUI.indentLevel++;

                    mod.Name = EditorGUILayout.TextField(
                        new GUIContent("Name", "Display name for this mod."),
                        mod.Name);
                    mod.AssetPathLocation = EditorGUILayout.TextField(
                        new GUIContent("Asset Path Location", "Where the mod stores its asset bundles at runtime. Should point to a static getter in your mod, e.g. {TemplateMod.AssetManager.AssetPath}."),
                        mod.AssetPathLocation);
                    mod.MonoscriptBundleNaming = EditorGUILayout.TextField(
                        new GUIContent("Monoscript Bundle", "Unique name for this mod's monoscript bundle. Output file would be {MonoscriptBundle}_monoscripts.bundle"),
                        mod.MonoscriptBundleNaming);
                    mod.CatalogPostfix = EditorGUILayout.TextField(
                        new GUIContent("Catalog Postfix", "Unique postfix for the addressable catalog file. Output file would be catalog_{CatalogPostfix}.json"),
                        mod.CatalogPostfix);
                    mod.BuildPath = EditorGUILayout.TextField(
                        new GUIContent("Build Path", "Output directory for the built asset bundles. Each mod should have its own folder."),
                        mod.BuildPath);

                    EditorGUILayout.Space(4);

                    // Groups
                    _groupFoldouts[i] = EditorGUILayout.Foldout(_groupFoldouts[i], $"Addressable Groups ({mod.GroupNames.Length})", true);
                    if (_groupFoldouts[i])
                    {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.HelpBox("Names of addressable asset groups that belong to this mod. Must match the group names in the Addressable Groups window exactly.", MessageType.Info);

                        int removeGroupIndex = -1;
                        for (int g = 0; g < mod.GroupNames.Length; g++)
                        {
                            EditorGUILayout.BeginHorizontal();
                            mod.GroupNames[g] = EditorGUILayout.TextField(mod.GroupNames[g]);
                            if (GUILayout.Button("-", GUILayout.Width(25)))
                            {
                                removeGroupIndex = g;
                            }
                            EditorGUILayout.EndHorizontal();
                        }

                        if (removeGroupIndex >= 0)
                        {
                            List<string> groups = new List<string>(mod.GroupNames);
                            groups.RemoveAt(removeGroupIndex);
                            mod.GroupNames = groups.ToArray();
                        }

                        if (GUILayout.Button("Add Group"))
                        {
                            List<string> groups = new List<string>(mod.GroupNames);
                            groups.Add("");
                            mod.GroupNames = groups.ToArray();
                        }

                        EditorGUI.indentLevel--;
                    }

                    EditorGUILayout.Space(4);

                    // Copy settings
                    mod.DoCopy = EditorGUILayout.Toggle(
                        new GUIContent("Copy Files After Build", "Automatically copy the built bundles to another directory after a successful build."),
                        mod.DoCopy);
                    if (mod.DoCopy)
                    {
                        mod.CopyPath = EditorGUILayout.TextField(
                            new GUIContent("Copy To Path", "Destination directory where built bundles will be copied to."),
                            mod.CopyPath);
                    }

                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(2);
            }

            if (removeIndex >= 0)
            {
                mods.RemoveAt(removeIndex);
                _modFoldouts.RemoveAt(removeIndex);
                _groupFoldouts.RemoveAt(removeIndex);
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Add Mod"))
            {
                mods.Add(new ModConfig());
            }

            EditorGUILayout.EndScrollView();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(instance);
            }
        }
    }
}
