using System.IO;
using UnityEngine;
using UnityEditor;

public class Pipeline : MonoBehaviour
{
    private static string outputPath = "../Plugin/Assets";
    private static BuildTarget buildTarget = BuildTarget.StandaloneWindows;

    [MenuItem("AssetBunldes/Create AssetBunldes With Specific targets")]
    public static bool BuildAssetBundles()
    {
        Directory.CreateDirectory(outputPath);

        var manifest = BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.StrictMode, buildTarget);
        return manifest != null;
    }
}
