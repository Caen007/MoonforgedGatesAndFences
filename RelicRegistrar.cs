using System.Collections.Generic;
using System.Linq;
using Jotunn.Entities;
using Jotunn.Configs;
using UnityEngine;
using Jotunn.Managers;

namespace Moonforged.GatesAndFences
{
    public class RelicRegistration
    {
        public string PrefabName;
        public string DisplayName;
        public RequirementConfig[] Requirements;
        public string Description;
        public string Category;
        public int Comfort;
        public string CraftingStation;

        public RelicRegistration(
            string prefab, string display, RequirementConfig[] reqs, string desc, string cat,
            int comfort = 0, string craftingStation = "Workbench")
        {
            PrefabName = prefab;
            DisplayName = display;
            Requirements = reqs;
            Description = desc;
            Category = cat;
            Comfort = comfort;
            CraftingStation = craftingStation;
        }
    }

    public static class RelicRegistrar
    {
        private static bool wasAlreadyRegistered = false;

        public static readonly List<RelicRegistration> AllRegistrations = new List<RelicRegistration>
        {
            new RelicRegistration("v1_SilverGate", "Silver Gate", new[] {
                new RequirementConfig("Silver", 5), new RequirementConfig("Crystal", 2)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_DvergrForgedGate", "Dvergr Forged Gate", new[] {
                new RequirementConfig("BlackMetal", 5), new RequirementConfig("Bronze", 1)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_DvergrForgedGate3m", "Dvergr Forged Gate 3m Tall", new[] {
                new RequirementConfig("BlackMetal", 4), new RequirementConfig("Bronze", 1)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_DvergrForgedGate4m", "Dvergr Forged Gate 4m Tall", new[] {
                new RequirementConfig("BlackMetal", 5), new RequirementConfig("Bronze", 1)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_EmberlightGate", "Moonforged Emberlight Gate", new[] {
                new RequirementConfig("BlackMetal", 5), new RequirementConfig("Bronze", 1), new RequirementConfig("Stone",5), new RequirementConfig("Resin", 2)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_ValkyriesGate", "Valkyrie's Gate", new[] {
                new RequirementConfig("Iron", 3), new RequirementConfig("Bronze", 1), new RequirementConfig("Silver", 2)
            }, "A side gate to Valhalla.", "building", 0, "Forge"),

            new RelicRegistration("v1_NeoWall", "Valkyrie's New Wall", new[] {
                new RequirementConfig("BlackMetal", 2), new RequirementConfig("Tar", 3),new RequirementConfig("Bronze", 1)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_SilverFence", "Silver Fence", new[] {
                new RequirementConfig("Silver", 2), new RequirementConfig("Crystal", 2)
            }, "Silver Fence of the Howling Cavern.", "building", 0, "Forge"),

            new RelicRegistration("v1_IroncrestWardenFenceBK", "Ironcrest Warden Fence Black", new[] {
                new RequirementConfig("BlackMetal", 2)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_IroncrestWardenFenceSilver", "Ironcrest Warden Fence Silver", new[] {
                new RequirementConfig("Silver", 2)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_Stonewatch_Palisade", "Stonewatch Palisade", new[] {
                new RequirementConfig("BlackMetal", 2), new RequirementConfig("Stone", 4)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_GildedRingwatchFence", "Gilded Ringwatch Fence", new[] {
                new RequirementConfig("Iron", 2), new RequirementConfig("Bronze", 1)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_chain_fence", "Golden Chain Fence", new[] {
                new RequirementConfig("BlackMetal", 1), new RequirementConfig("Bronze", 1)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_VikingArchGate", "Viking Arch Gate", new[] {
                new RequirementConfig("Stone", 5), new RequirementConfig("Wood", 5), new RequirementConfig("Iron", 2)
            }, "", "building", 0, "piece_stonecutter"),

            new RelicRegistration("v1_CurvedWoodFence", "Curved Wood Fence", new[] {
                new RequirementConfig("Wood", 2), new RequirementConfig("FineWood", 2)
            }, "", "building", 0, "Workbench"),

            new RelicRegistration("v1_BlackFence", "Black Fence", new[] {
                new RequirementConfig("Iron", 2), new RequirementConfig("Bronze", 1)
            }, "", "building", 0, "BlackForge"),

            new RelicRegistration("v1_Wrought_iron_fence_1", "Wrought Iron Fence I", new[] {
                new RequirementConfig("Iron", 2), new RequirementConfig("Bronze", 1)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_StoneBlackFence", "Stone Black Fence", new[] {
                new RequirementConfig("Iron", 2), new RequirementConfig("Bronze", 1), new RequirementConfig("Stone", 1)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_VikingGate", "Viking Gate", new[] {
                new RequirementConfig("Coal", 4), new RequirementConfig("Wood", 10)
            }, "Old Viking Gate.", "building", 0, "Workbench"),

            new RelicRegistration("v1_GardenGate", "Moonforged Garden Gate", new[] {
                new RequirementConfig("Coal", 2), new RequirementConfig("Wood", 2)
            }, "", "building", 0, "Workbench"),

            new RelicRegistration("v1_GardenFence", "Moonforged Garden Fence", new[] {
                new RequirementConfig("Coal", 2), new RequirementConfig("Wood", 2)
            }, "", "building", 0, "Workbench"),

           new RelicRegistration("v1_Element03", "Moonforged Ivy Gate", new[] {
                new RequirementConfig("FineWood", 5), new RequirementConfig("Wood", 10)
            }, "", "building", 0, "Workbench"),

            new RelicRegistration("v1_Element04", "Moonforged Twinleaf Gate", new[] {
                new RequirementConfig("FineWood", 5), new RequirementConfig("Wood", 10)
            }, "", "building", 0, "Workbench"),

            new RelicRegistration("v1_Element0", "Moonforged Castle Gate", new[] {
                new RequirementConfig("Iron", 5), new RequirementConfig("Wood", 20),new RequirementConfig("Tar", 10)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_Element01", "Moonforged Archgate", new[] {
                new RequirementConfig("Stone", 10), new RequirementConfig("Wood", 10),new RequirementConfig("Iron", 2)
            }, "", "building", 0, "piece_stonecutter"),

            new RelicRegistration("v1_MoonforgedIronboundGate", "Moonforged Ironbound Gate", new[] {
                new RequirementConfig("Iron", 10), new RequirementConfig("Wood", 10),new RequirementConfig("Coal", 2)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_BlackFenceStonePillar", "Stone Black Fence Pillar", new[] {
                new RequirementConfig("Iron", 1), new RequirementConfig("Bronze", 1), new RequirementConfig("Stone", 2),new RequirementConfig("Resin", 1)
            }, "", "building", 0, "piece_stonecutter"),

            new RelicRegistration("v1_BlackFenceStonePillar3m", "Stone Black Fence Pillar 3m", new[] {
                new RequirementConfig("Iron", 1), new RequirementConfig("Bronze", 1), new RequirementConfig("Stone", 2),new RequirementConfig("Resin", 1)
            }, "", "building", 0, "piece_stonecutter"),

            new RelicRegistration("M_WolfGate", "Moonforged Fenrir Gate", new[] {
                new RequirementConfig("Iron", 20),new RequirementConfig("Stone", 40),new RequirementConfig("GreydwarfEye", 10)
            }, "Twin wolves guard the threshold, forged beneath the moon’s cold fire. Their silent howls echo through the ages", "building", 0, "Forge")
        };

        public static IEnumerable<string> GetAllCategories()
        {
            return AllRegistrations.Select(r => CategoryToTab(r.Category)).Distinct();
        }

        private static string CategoryToTab(string category)
        {
            var lower = category.ToLower();
            switch (lower)
            {
                case "building":
                    return MoonforgedGates.PlayerPreferredCategory.Value; // ← Inject the config here
                default:
                    return category;
            }
        }

        public static void RegisterAllRelics(AssetBundle bundle)
        {
            if (wasAlreadyRegistered == false)
            {
                foreach (var reg in AllRegistrations)
                {
                    RegisterRelic(bundle, reg);
                }
                wasAlreadyRegistered = true;
            }
        }

        private static void RegisterRelic(AssetBundle bundle, RelicRegistration reg)
        {
            if (bundle == null) return;
            GameObject prefab = bundle.LoadAsset<GameObject>(reg.PrefabName);
            if (prefab == null) return;

            prefab.name = reg.PrefabName;

            var znv = prefab.GetComponent<ZNetView>();
            if (znv == null) znv = prefab.AddComponent<ZNetView>();
            znv.m_persistent = true;
            znv.m_syncInitialScale = true;

            if (!prefab.GetComponent<ZSyncTransform>())
                prefab.AddComponent<ZSyncTransform>();

            Piece piece = prefab.GetComponent<Piece>();
            if (piece == null) piece = prefab.AddComponent<Piece>();
            piece.m_name = reg.DisplayName;
            piece.m_description = reg.Description;
            piece.m_groundOnly = false;

            GameObject vfxPlace = null;
            GameObject sfxPlace = null;
            GameObject destroyVFX = null;
            GameObject destroySFX = null;

            // KEEP SFX/VFX NAMES **ORIGINAL** HERE:
            if (
                reg.PrefabName == "v1_CurvedWoodFence" ||
                reg.PrefabName == "v1_GardenGate" ||
                reg.PrefabName == "v1_Element04" ||
                reg.PrefabName == "v1_Element01" ||
                reg.PrefabName == "v1_Element0" ||
                reg.PrefabName == "v1_Element03" ||
                reg.PrefabName == "v1_GardenFence" ||
                reg.PrefabName == "v1_Element02a" ||
                reg.PrefabName == "v1_MoonforgedIronboundGate" ||
                reg.PrefabName == "v1_VikingGate"
            )
            {
                if (ZNetScene.instance != null)
                {
                    vfxPlace = ZNetScene.instance.GetPrefab("vfx_Place_wood");
                    sfxPlace = ZNetScene.instance.GetPrefab("sfx_build_hammer_wood");
                    destroyVFX = ZNetScene.instance.GetPrefab("vfx_destroyed_wood");
                    destroySFX = ZNetScene.instance.GetPrefab("sfx_wood_break");
                }
            }
            else if (
                reg.PrefabName == "v1_SilverGate" ||
                reg.PrefabName == "v1_DvergrForgedGate" ||
                reg.PrefabName == "v1_DvergrForgedGate3m" ||
                reg.PrefabName == "v1_DvergrForgedGate4m" ||
                reg.PrefabName == "v1_EmberlightGate" ||
                reg.PrefabName == "v1_ValkyriesGate" ||
                reg.PrefabName == "v1_GildedRingwatchFence" ||
                reg.PrefabName == "v1_SilverFence" ||
                reg.PrefabName == "v1_IroncrestWardenFenceBK" ||
                reg.PrefabName == "v1_IroncrestWardenFenceSilver" ||
                reg.PrefabName == "v1_VikingArchGate" ||
                reg.PrefabName == "v1_BlackFence" ||
                reg.PrefabName == "v1_Wrought_iron_fence_1" ||
                reg.PrefabName == "M_WolfGate" ||
                reg.PrefabName == "v1_chain_fence"
            )
            {
                if (ZNetScene.instance != null)
                {
                    vfxPlace = ZNetScene.instance.GetPrefab("vfx_Place_stone");
                    sfxPlace = ZNetScene.instance.GetPrefab("sfx_build_hammer_metal");
                    destroyVFX = ZNetScene.instance.GetPrefab("vfx_destroyed");
                    destroySFX = ZNetScene.instance.GetPrefab("sfx_metal_blocked");
                }
            }
            else
            {
                if (ZNetScene.instance != null)
                {
                    vfxPlace = ZNetScene.instance.GetPrefab("vfx_Place_stone");
                    sfxPlace = ZNetScene.instance.GetPrefab("sfx_build_hammer_stone");
                    destroyVFX = ZNetScene.instance.GetPrefab("vfx_destroyed");
                    destroySFX = ZNetScene.instance.GetPrefab("sfx_rock_destroyed");
                }
            }

            var placeList = new List<EffectList.EffectData>();
            if (vfxPlace != null) placeList.Add(new EffectList.EffectData { m_prefab = vfxPlace, m_enabled = true });
            if (sfxPlace != null) placeList.Add(new EffectList.EffectData { m_prefab = sfxPlace, m_enabled = true });
            var placeFX = new EffectList();
            placeFX.m_effectPrefabs = placeList.ToArray();
            piece.m_placeEffect = placeFX;

            WearNTear wear = prefab.GetComponent<WearNTear>();
            if (wear == null) wear = prefab.AddComponent<WearNTear>();
            wear.m_health = 1000f;
            wear.m_noRoofWear = true;

            var destroyList = new List<EffectList.EffectData>();
            if (destroyVFX != null) destroyList.Add(new EffectList.EffectData { m_prefab = destroyVFX, m_enabled = true });
            if (destroySFX != null) destroyList.Add(new EffectList.EffectData { m_prefab = destroySFX, m_enabled = true });
            var destroyFX = new EffectList();
            destroyFX.m_effectPrefabs = destroyList.ToArray();
            wear.m_destroyedEffect = destroyFX;

            // Load ICON with new lowercase name
            Sprite icon = bundle.LoadAsset<Sprite>(reg.PrefabName.ToLowerInvariant());
            if (icon != null)
                piece.m_icon = icon;

            var config = new PieceConfig();
            config.PieceTable = "Hammer";
            config.Category = CategoryToTab(reg.Category);
            config.CraftingStation = reg.CraftingStation;
            config.Requirements = reg.Requirements;

            PieceManager.Instance.AddPiece(new CustomPiece(prefab, true, config));
        }
    }
}