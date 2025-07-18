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
        public static readonly List<RelicRegistration> AllRegistrations = new List<RelicRegistration>
        {
            new RelicRegistration("SilverGate", "Silver Gate", new[] {
                new RequirementConfig("BlackMetal", 5), new RequirementConfig("Bronze", 1)
            }, "A custom decorative piece.", "building", 0, "Forge"),

            new RelicRegistration("DvergrForgedGate", "Dvergr Forged Gate", new[] {
                new RequirementConfig("BlackMetal", 5), new RequirementConfig("Bronze", 1)
            }, "Dvergr Forged Gate.", "building", 0, "Forge"),

            new RelicRegistration("DvergrForgedGate3m", "Dvergr Forged Gate 3m Tall", new[] {
                new RequirementConfig("BlackMetal", 4), new RequirementConfig("Bronze", 1)
            }, "Dvergr Forged Gate.", "building", 0, "Forge"),

            new RelicRegistration("DvergrForgedGate4m", "Dvergr Forged Gate 4m Tall", new[] {
                new RequirementConfig("BlackMetal", 5), new RequirementConfig("Bronze", 1)
            }, "Dvergr Forged Gate.", "building", 0, "Forge"),

            new RelicRegistration("EmberlightGate", "Emberlight Gate", new[] {
                new RequirementConfig("BlackMetal", 5), new RequirementConfig("Bronze", 1), new RequirementConfig("Stone", 1)
            }, "Emberlight Gate.", "building", 0, "Forge"),

            new RelicRegistration("ValkyriesEntrance", "Valkyrie's Entrance", new[] {
                new RequirementConfig("BlackMetal", 5), new RequirementConfig("Bronze", 1), new RequirementConfig("Stone", 1)
            }, "A Gate to Valhalla.", "building", 0, "Forge"),

            new RelicRegistration("ValkyriesEntranceSmall", "Valkyrie's Entrance Small", new[] {
                new RequirementConfig("BlackMetal", 5), new RequirementConfig("Bronze", 1), new RequirementConfig("Stone", 1)
            }, "A side gate of Valhalla.", "building", 0, "Forge"),

            new RelicRegistration("NeoWall", "Valkyrie's New Wall", new[] {
                new RequirementConfig("BlackMetal", 4), new RequirementConfig("Tar", 4)
            }, "Something new.", "building", 0, "Forge"),

            new RelicRegistration("SilverFence", "Silver Fence", new[] {
                new RequirementConfig("Silver", 2), new RequirementConfig("Crystal", 2)
            }, "Silver Fence of the Howling Cavern.", "building", 0, "Forge"),

            new RelicRegistration("IroncrestWardenFenceBK", "Ironcrest Warden Fence Black", new[] {
                new RequirementConfig("BlackMetal", 2)
            }, "A custom decorative piece.", "building", 0, "Forge"),

            new RelicRegistration("IroncrestWardenFenceSilver", "Ironcrest Warden Fence Silver", new[] {
                new RequirementConfig("BlackMetal", 2)
            }, "A custom decorative piece.", "building", 0, "Forge"),

            new RelicRegistration("Stonewatch_Palisade", "Stonewatch Palisade", new[] {
                new RequirementConfig("BlackMetal", 2), new RequirementConfig("Stone", 4)
            }, "A custom decorative piece.", "building", 0, "Forge"),

            new RelicRegistration("GildedRingwatchFence", "Gilded Ringwatch Fence", new[] {
                new RequirementConfig("Iron", 2), new RequirementConfig("Bronze", 1)
            }, "A custom decorative piece.", "building", 0, "Forge"),

            new RelicRegistration("chain_fence", "Golden Chain Fence", new[] {
                new RequirementConfig("BlackMetal", 1), new RequirementConfig("Bronze", 1)
            }, "A custom decorative piece.", "building", 0, "Forge"),

            new RelicRegistration("GateToAsh", "Gate To Ash", new[] {
                new RequirementConfig("Stone", 5), new RequirementConfig("Wood", 5), new RequirementConfig("BlackMetal", 1)
            }, "A custom decorative piece.", "building", 0, "Workbench"),

            new RelicRegistration("Fence_01", "Echo Ring Barrier", new[] {
                new RequirementConfig("Iron", 2), new RequirementConfig("Wood", 2)
            }, "A custom decorative piece.", "building", 0, "Workbench"),

            new RelicRegistration("BlackFence", "Black Fence", new[] {
                new RequirementConfig("Iron", 2), new RequirementConfig("Bronze", 1)
            }, "A custom decorative piece.", "building", 0, "BlackForge"),

            new RelicRegistration("StoneBlackFence", "Stone Black Fence", new[] {
                new RequirementConfig("Iron", 2), new RequirementConfig("Bronze", 1), new RequirementConfig("Stone", 1)
            }, "A custom decorative piece.", "building", 0, "BlackForge"),

            new RelicRegistration("VikingGate", "Viking Gate", new[] {
                new RequirementConfig("BlackMetal", 2), new RequirementConfig("Wood", 10)
            }, "Old Viking Gate.", "building", 0, "Workbench"),

            new RelicRegistration("BlackFenceStonePillar", "Stone Black Fence Pillar", new[] {
                new RequirementConfig("BlackMetal", 1), new RequirementConfig("Bronze", 1), new RequirementConfig("Stone", 2)
            }, "Build a Black Fence.", "building", 0, "Forge")
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
                    return "Moonforged Structures";
                default:
                    return category;
            }
        }

        public static void RegisterAllRelics(AssetBundle bundle)
        {
            foreach (var reg in AllRegistrations)
                RegisterRelic(bundle, reg);
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

            if (
                reg.PrefabName == "Fence_01" ||
                reg.PrefabName == "VikingGate"
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
                reg.PrefabName == "SilverGate" ||
                reg.PrefabName == "DvergrForgedGate" ||
                reg.PrefabName == "DvergrForgedGate3m" ||
                reg.PrefabName == "DvergrForgedGate4m" ||
                reg.PrefabName == "EmberlightGate" ||
                reg.PrefabName == "ValkyriesEntrance" ||
                reg.PrefabName == "ValkyriesEntranceSmall" ||
                reg.PrefabName == "GildedRingwatchFence" ||
                reg.PrefabName == "SilverFence" ||
                reg.PrefabName == "IroncrestWardenFenceBK" ||
                reg.PrefabName == "IroncrestWardenFenceSilver" ||
                reg.PrefabName == "GateToAsh" ||
                reg.PrefabName == "BlackFence" ||
                reg.PrefabName == "chain_fence"
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
