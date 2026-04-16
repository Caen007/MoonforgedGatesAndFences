using System.Collections;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using Jotunn.Managers;
using UnityEngine;

namespace Moonforged.GatesAndFences
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    public class MoonforgedGates : BaseUnityPlugin
    {
        public const string PluginGUID = "Moonforged.GatesAndFences";
        public const string PluginName = "Moonforged Gates & Fences";
        public const string PluginVersion = "1.0.9";

        private AssetBundle gatesBundle;
        private Harmony harmony;

        public static ConfigEntry<string> PlayerPreferredCategory;

        private void Awake()
        {
            harmony = new Harmony("moonforged.gates.scalingdebug");
            harmony.PatchAll();

            string resourcePath = GetPlatformBundleResourcePath();
            gatesBundle = EmbeddedAssetBundleLoader.LoadBundle(resourcePath);

            if (gatesBundle == null)
            {
                Logger.LogError("Failed to load embedded AssetBundle: " + resourcePath);
                return;
            }

            Logger.LogInfo("Loaded embedded AssetBundle: " + resourcePath);

            PlayerPreferredCategory = Config.Bind("General",
                "CustomHammerTab",
                "MoonforgedGatesAndFences",
                "Set the hammer tab where this mod's pieces should appear (e.g., Building, Furniture, MoonforgedGatesAndFences)"
            );

            foreach (string category in RelicRegistrar.GetAllCategories())
            {
                PieceManager.Instance.AddPieceCategory(category);
            }

            PrefabManager.OnPrefabsRegistered += OnPrefabsRegistered;
        }

        private static string GetPlatformBundleResourcePath()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.OSXEditor:
                    return "MoonforgedGatesAndFences.gatesandfences_mac";

                default:
                    return "MoonforgedGatesAndFences.gatesandfences_windows";
            }
        }

        private void OnDestroy()
        {
            PrefabManager.OnPrefabsRegistered -= OnPrefabsRegistered;
        }

        private void OnPrefabsRegistered()
        {
            StartCoroutine(DelayedRegister(gatesBundle));
        }

        private IEnumerator DelayedRegister(AssetBundle bundle)
        {
            while (ZNetScene.instance == null)
            {
                yield return null;
            }

            RelicRegistrar.RegisterAllRelics(bundle);
        }
    }

    public static class EmbeddedAssetBundleLoader
    {
        public static AssetBundle LoadBundle(string resourcePath)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream == null)
                {
                    Debug.LogError("AssetBundle resource not found: " + resourcePath);
                    return null;
                }

                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return AssetBundle.LoadFromMemory(memoryStream.ToArray());
                }
            }
        }
    }
}