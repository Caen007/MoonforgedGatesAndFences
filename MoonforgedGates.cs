using System.Reflection;
using System.IO;
using BepInEx;
using UnityEngine;
using Jotunn.Managers;
using HarmonyLib;
using System.Collections.Generic;
using MoonforgedGatesAndFences;

namespace Moonforged.GatesAndFences
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    public class MoonforgedGates : BaseUnityPlugin
    {
        public const string PluginGUID = "Moonforged.GatesAndFences";
        public const string PluginName = "Moonforged Gates & Fences";
        public const string PluginVersion = "1.0.0";

        private AssetBundle gatesBundle;

        private static readonly List<GameObject> placedObjects = new List<GameObject>();

        public static void TrackAllPrefabsInBundle(AssetBundle bundle)
        {
            foreach (var prefab in bundle.LoadAllAssets<GameObject>())
            {
                if (prefab != null && prefab.GetComponent<PlacementWatcher>() == null)
                {
                    prefab.AddComponent<PlacementWatcher>().RegisterList = placedObjects;
                }
            }
        }

        private void Awake()
        {
            new Harmony("moonforged.gates.scalingdebug").PatchAll();

            string resourcePath = "MoonforgedGatesAndFences.gatesandfences";
            gatesBundle = EmbeddedAssetBundleLoader.LoadBundle(resourcePath);

            if (gatesBundle == null)
            {
                Logger.LogError("Failed to load embedded AssetBundle.");
                return;
            }

            TrackAllPrefabsInBundle(gatesBundle);

            foreach (var category in RelicRegistrar.GetAllCategories())
                PieceManager.Instance.AddPieceCategory(category);

            PrefabManager.OnPrefabsRegistered += () =>
            {
                RelicRegistrar.RegisterAllRelics(gatesBundle);
            };
        }
    }

    public static class EmbeddedAssetBundleLoader
    {
        public static AssetBundle LoadBundle(string resourcePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream == null)
                {
                    Debug.LogError("AssetBundle resource not found: " + resourcePath);
                    return null;
                }
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return AssetBundle.LoadFromMemory(buffer);
            }
        }
    }
}
