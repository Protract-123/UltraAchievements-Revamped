using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildBundles : MonoBehaviour
{
    [MenuItem ("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles ()
    {
        BuildPipeline.BuildAssetBundles ("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
}
