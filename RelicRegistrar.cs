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

        private static readonly HashSet<string> MetalDoorSoundPrefabs = new HashSet<string>
        {
            "v1_SilverGate",
            "v1_DvergrForgedGate",
            "v1_DvergrForgedGate3m",
            "v1_DvergrForgedGate4m",
            "v1_EmberlightGate",
            "v1_ValkyriesGate",
            "v1_MoonforgedIronboundGate",
            "M_Gargoyle_Gate",
            "M_WolfGate"
        };

        private static readonly HashSet<string> NormalDoorSoundPrefabs = new HashSet<string>
        {
            "v1_GardenGate",
            "M_Garden_Gate_Small",
            "M_Garden_Gate",
            "v1_Element03",
            "v1_Element04",
            "M_Garden_Gate_Arch",
            "M_MedievalMetalDoor",
            "v1_VikingArchGate"
        };

        public static readonly List<RelicRegistration> AllRegistrations = new List<RelicRegistration>
        {
            new RelicRegistration("v1_SilverGate", "Silver Gate", new[] {
                new RequirementConfig("Silver", 5), new RequirementConfig("Tin", 2)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_DvergrForgedGate", "Dvergr Forged Gate", new[] {
                new RequirementConfig("BlackMetal", 5), new RequirementConfig("Bronze", 5)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_DvergrForgedGate3m", "Dvergr Forged Gate 3m Tall", new[] {
                new RequirementConfig("BlackMetal", 2), new RequirementConfig("Bronze", 1)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_DvergrForgedGate4m", "Dvergr Forged Gate 4m Tall", new[] {
                new RequirementConfig("BlackMetal", 2), new RequirementConfig("Bronze", 1)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_EmberlightGate", "Moonforged Emberlight Gate", new[] {
                new RequirementConfig("BlackMetal", 5), new RequirementConfig("Bronze", 1), new RequirementConfig("Stone",5), new RequirementConfig("Resin", 2)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_ValkyriesGate", "Valkyrie's Gate", new[] {
                new RequirementConfig("Iron", 3), new RequirementConfig("Bronze", 1), new RequirementConfig("Silver", 2)
            }, "A side gate to Valhalla.", "building", 0, "Forge"),

            new RelicRegistration("v1_NeoWall", "Valkyrie's New Wall", new[] {
                new RequirementConfig("BlackMetal", 1), new RequirementConfig("Tar", 1),new RequirementConfig("Bronze", 1)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_SilverFence", "Silver Fence", new[] {
                new RequirementConfig("Silver", 2)
            }, "Silver Fence of the Howling Cavern.", "building", 0, "Forge"),

            new RelicRegistration("v1_IroncrestWardenFenceBK", "Ironcrest Warden Fence Black", new[] {
                new RequirementConfig("Iron", 1)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_IroncrestWardenFenceSilver", "Ironcrest Warden Fence Silver", new[] {
                new RequirementConfig("Silver", 1)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_Stonewatch_Palisade", "Stonewatch Palisade", new[] {
                new RequirementConfig("BlackMetal", 2), new RequirementConfig("Stone", 4)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_GildedRingwatchFence", "Gilded Ringwatch Fence", new[] {
                new RequirementConfig("Iron", 2), new RequirementConfig("Bronze", 1)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_chain_fence", "Golden Chain Fence", new[] {
                new RequirementConfig("Bronze", 1)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_VikingArchGate", "Viking Castle Arch Gate", new[] {
                new RequirementConfig("Stone", 5), new RequirementConfig("Wood", 5), new RequirementConfig("Coal", 5)
            }, "", "building", 0, "Workbench"),

            new RelicRegistration("v1_CurvedWoodFence", "Curved Wood Fence", new[] {
                new RequirementConfig("Wood", 2), new RequirementConfig("FineWood", 2)
            }, "", "building", 0, "Workbench"),

            new RelicRegistration("v1_BlackFence", "Black Fence", new[] {
                new RequirementConfig("Iron", 2), new RequirementConfig("Bronze", 1)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_Wrought_iron_fence_1", "Wrought Iron Fence I", new[] {
                new RequirementConfig("Iron", 2), new RequirementConfig("Bronze", 1)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_StoneBlackFence", "Stone Black Fence", new[] {
                new RequirementConfig("Bronze", 1), new RequirementConfig("Stone", 1)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("v1_VikingGate", "Moonforged Viking Gate", new[] {
                new RequirementConfig("Coal", 4), new RequirementConfig("Wood", 10), new RequirementConfig("RoundLog", 15),new RequirementConfig("LeatherScraps", 10),
            }, "Old Viking Gate.", "building", 0, "Workbench"),

            new RelicRegistration("v1_GardenGate", "Moonforged Garden Gate", new[] {
                new RequirementConfig("Coal", 2), new RequirementConfig("Wood", 2)
            }, "", "building", 0, "Workbench"),

            new RelicRegistration("v1_GardenFence", "Moonforged Garden Fence", new[] {
                new RequirementConfig("Coal", 2), new RequirementConfig("Wood", 2)
            }, "", "building", 0, "Workbench"),

            new RelicRegistration("M_Garden_Fence1x1", "Small Garden Fence 1x1", new[] {
                new RequirementConfig("Wood", 2)
            }, "", "building", 0, "Workbench"),

            new RelicRegistration("M_Garden_Fence_Medium", "Medium Garden Fence", new[] {
                new RequirementConfig("Wood", 3)
            }, "", "building", 0, "Workbench"),

            new RelicRegistration("M_Garden_Pole1x1", "Small Garden Pole 1x1", new[] {
                new RequirementConfig("Wood", 1)
            }, "", "building", 0, "Workbench"),

            new RelicRegistration("M_Garden_Gate_Small", "Small Garden Gate", new[] {
                new RequirementConfig("Wood", 12)
            }, "", "building", 0, "Workbench"),

            new RelicRegistration("M_Garden_Fence2x2", "Garden Fence 2x2", new[] {
                new RequirementConfig("Wood", 4)
            }, "", "building", 0, "Workbench"),

            new RelicRegistration("M_Garden_Fence3x2", "Garden Fence 3x2", new[] {
                new RequirementConfig("Wood", 6)
            }, "", "building", 0, "Workbench"),

            new RelicRegistration("M_Garden_Pole2x2", "Garden Pole 2x2", new[] {
                new RequirementConfig("Wood", 2)
            }, "", "building", 0, "Workbench"),

            new RelicRegistration("M_Garden_Gate", "Garden Gate", new[] {
                new RequirementConfig("Wood", 24)
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
                new RequirementConfig("Iron", 10),new RequirementConfig("Stone", 40),new RequirementConfig("GreydwarfEye", 10)
            }, "Twin wolves guard the threshold, forged beneath the moon’s cold fire. Their silent howls echo through the ages", "building", 0, "Forge"),


            new RelicRegistration("M_Hobbit_Door", "Hobbit House Door", new[] {
                new RequirementConfig("Iron", 5),new RequirementConfig("Stone", 20),new RequirementConfig("GreydwarfEye", 10)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("M_Garden_Gate_Arch", "Moonforged Arched Garden Gate", new[] {
                new RequirementConfig("Dandelion", 5),new RequirementConfig("Thistle", 5),new RequirementConfig("Mushroom", 10)
            }, "", "building", 0, "Workbench"),

             new RelicRegistration("M_Gargoyle_Gate", "Moonforged Gargoyle Gate", new[] {
                new RequirementConfig("Iron", 5),new RequirementConfig("Stone", 20),new RequirementConfig("GreydwarfEye", 10)
            }, "", "building", 0, "Forge"),

                          new RelicRegistration("M_GargoyleFence_5m", "Moonforged Gargoyle Fence 5m", new[] {
                new RequirementConfig("Iron", 5),new RequirementConfig("Stone", 20),new RequirementConfig("GreydwarfEye", 10)
            }, "", "building", 0, "Forge"),

            new RelicRegistration("M_MedievalMetalDoor", "Medieval Metal Door", new[] {
                new RequirementConfig("Iron", 5),new RequirementConfig("Stone", 20),new RequirementConfig("GreydwarfEye", 10)
            }, "", "building", 0, "piece_stonecutter"),

                        new RelicRegistration("Moonforged_Silent_Hill_Gate", "Moonforged Swamp Gate", new[] {
                new RequirementConfig("Wood", 15),new RequirementConfig("ElderBark", 15)
            }, "", "building", 0, "Workbench"),


        new RelicRegistration("Wolf_Door", "Moonforged Stone Wolf Door", new[] {
                new RequirementConfig("Stone", 30),new RequirementConfig("Crystal", 10),new RequirementConfig("Iron", 10),
            }, "", "building", 0, "Forge"),

        new RelicRegistration("M_Twin_Dragon_Castle_Gate", "Moonforged Twin Dragon Castle Gate", new[] {
                new RequirementConfig("RoundLog", 10),new RequirementConfig("Iron", 10),new RequirementConfig("Wood", 10),new RequirementConfig("Coal", 10),
            }, "", "building", 0, "Forge"),

        new RelicRegistration("M_Twin_Dragon_Door", "Moonforged Twin Dragon Door", new[] {
                new RequirementConfig("RoundLog", 5),new RequirementConfig("Iron", 2),new RequirementConfig("Wood", 5),new RequirementConfig("Coal", 2),
            }, "", "building", 0, "Forge"),

        new RelicRegistration("M_Dragon_Door", "Moonforged Dragon Door", new[] {
                new RequirementConfig("RoundLog", 2),new RequirementConfig("Iron", 1),new RequirementConfig("Wood", 2),new RequirementConfig("Coal", 2),
            }, "", "building", 0, "Forge"),

        new RelicRegistration("M_Green_Twig_Fence", "Moonforged Green Twig Fence", new[] {
                new RequirementConfig("Wood", 2), new RequirementConfig("Dandelion", 2),
            }, "", "building", 0, "Workbench"),

        new RelicRegistration("M_Twig_Fence", "Moonforged Twig Fence", new[] {
                new RequirementConfig("Wood", 2),
            }, "", "building", 0, "Workbench")
        };

        public static IEnumerable<string> GetAllCategories()
        {
            return AllRegistrations.Select(r => CategoryToTab(r.Category)).Distinct();
        }

        private static string CategoryToTab(string category)
        {
            if (string.IsNullOrWhiteSpace(category)) return MoonforgedGates.PlayerPreferredCategory.Value;

            var lower = category.ToLowerInvariant();
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

            // Prevent duplicate prefab registration if the event is raised again.
            if (PrefabManager.Instance.GetPrefab(reg.PrefabName) != null) return;

            GameObject prefab = bundle.LoadAsset<GameObject>(reg.PrefabName);
            if (prefab == null)
            {
                Debug.LogWarning($"[Moonforged Gates & Fences] Missing prefab in AssetBundle: {reg.PrefabName}");
                return;
            }

            prefab.name = reg.PrefabName;

            // Every registered build prefab receives its network components from code.
            var znv = prefab.GetComponent<ZNetView>() ?? prefab.AddComponent<ZNetView>();
            znv.m_persistent = true;
            znv.m_syncInitialScale = true;

            var zsync = prefab.GetComponent<ZSyncTransform>() ?? prefab.AddComponent<ZSyncTransform>();
            zsync.m_syncPosition = false;
            zsync.m_syncRotation = false;
            zsync.m_syncScale = true;
            zsync.m_syncBodyVelocity = false;
            zsync.m_characterParentSync = false;

            // Every registered prefab receives a Piece component from code if it does not already have one.
            Piece piece = prefab.GetComponent<Piece>() ?? prefab.AddComponent<Piece>();
            piece.m_name = reg.DisplayName;
            piece.m_description = reg.Description;
            piece.m_enabled = true;
            piece.m_canBeRemoved = true;
            piece.m_canRotate = true;
            piece.m_groundOnly = false;

            GameObject vfxPlace = null;
            GameObject sfxPlace = null;
            GameObject destroyVFX = null;
            GameObject destroySFX = null;

            // KEEP SFX/VFX NAMES **ORIGINAL** HERE:
            if (
                reg.PrefabName == "v1_CurvedWoodFence" ||
                reg.PrefabName == "v1_GardenGate" ||
                reg.PrefabName == "M_Hobbit_Door" ||
                reg.PrefabName == "v1_Element04" ||
                reg.PrefabName == "v1_Element01" ||
                reg.PrefabName == "v1_Element0" ||
                reg.PrefabName == "v1_Element03" ||
                reg.PrefabName == "v1_GardenFence" ||
                reg.PrefabName == "v1_Element02a" ||
                reg.PrefabName == "v1_MoonforgedIronboundGate" ||
                reg.PrefabName == "Moonforged_Silent_Hill_Gate" ||
                reg.PrefabName == "v1_VikingGate" ||
                reg.PrefabName == "M_Garden_Fence2x2" ||
                reg.PrefabName == "M_Garden_Fence3x2" ||
                reg.PrefabName == "M_Garden_Pole2x2" ||
                reg.PrefabName == "M_Garden_Gate" ||
                reg.PrefabName == "M_Garden_Fence1x1" ||
                reg.PrefabName == "M_Garden_Fence_Medium" ||
                reg.PrefabName == "M_Garden_Pole1x1" ||
                reg.PrefabName == "M_Garden_Gate_Small" ||
                reg.PrefabName == "M_Green_Twig_Fence" ||
                reg.PrefabName == "M_Twig_Fence" ||
                reg.PrefabName == "M_Twin_Dragon_Castle_Gate" ||
                reg.PrefabName == "M_Twin_Dragon_Door" ||
                reg.PrefabName == "M_Dragon_Door"
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

            WearNTear wear = prefab.GetComponent<WearNTear>() ?? prefab.AddComponent<WearNTear>();
            wear.m_health = 1000f;
            wear.m_noRoofWear = true;

            var destroyList = new List<EffectList.EffectData>();
            if (destroyVFX != null) destroyList.Add(new EffectList.EffectData { m_prefab = destroyVFX, m_enabled = true });
            if (destroySFX != null) destroyList.Add(new EffectList.EffectData { m_prefab = destroySFX, m_enabled = true });
            var destroyFX = new EffectList();
            destroyFX.m_effectPrefabs = destroyList.ToArray();
            wear.m_destroyedEffect = destroyFX;

            if (reg.Comfort > 0)
                piece.m_comfort = reg.Comfort;

            Sprite icon = bundle.LoadAsset<Sprite>(reg.PrefabName);
            if (icon == null)
                icon = bundle.LoadAsset<Sprite>(reg.PrefabName.ToLowerInvariant());
            if (icon != null)
                piece.m_icon = icon;

            ApplyRuntimeDoorSounds(prefab, reg.PrefabName);

            var config = new PieceConfig
            {
                PieceTable = PieceTables.Hammer,
                Category = CategoryToTab(reg.Category),
                Usage = new[] { PieceUsages.Building },
                CraftingStation = reg.CraftingStation,
                Requirements = reg.Requirements ?? new RequirementConfig[0]
            };

            PieceManager.Instance.AddPiece(new CustomPiece(prefab, false, config));
        }

        private static void ApplyRuntimeDoorSounds(GameObject prefab, string prefabName)
        {
            if (prefab == null || string.IsNullOrEmpty(prefabName)) return;

            // Wolf_Door keeps the custom open/close effects authored for that prefab.
            if (prefabName == "Wolf_Door") return;

            Door[] doors = prefab.GetComponentsInChildren<Door>(true);
            if (doors == null || doors.Length == 0) return;

            string sourceDoorPrefab;
            string[] fallbackOpenEffects;
            string[] fallbackCloseEffects;

            if (MetalDoorSoundPrefabs.Contains(prefabName))
            {
                sourceDoorPrefab = "iron_grate";
                fallbackOpenEffects = new[] { "v2_sfx_metalgate_open", "sfx_metalgate_open" };
                fallbackCloseEffects = new[] { "v2_sfx_metalgate_close", "sfx_metalgate_close" };
            }
            else if (NormalDoorSoundPrefabs.Contains(prefabName))
            {
                sourceDoorPrefab = "wood_door";
                fallbackOpenEffects = new[] { "v2_sfx_door_open", "sfx_door_open" };
                fallbackCloseEffects = new[] { "v2_sfx_door_close", "sfx_door_close" };
            }
            else
            {
                sourceDoorPrefab = "darkwood_gate";
                fallbackOpenEffects = new[] { "v2_sfx_darkwood_door_open", "sfx_darkwood_door_open" };
                fallbackCloseEffects = new[] { "v2_sfx_darkwood_door_close", "sfx_darkwood_door_close" };
            }

            Door sourceDoor = GetRuntimeDoor(sourceDoorPrefab);
            EffectList openEffects = sourceDoor != null && HasEffects(sourceDoor.m_openEffects)
                ? sourceDoor.m_openEffects
                : CreateRuntimeEffectList(fallbackOpenEffects);
            EffectList closeEffects = sourceDoor != null && HasEffects(sourceDoor.m_closeEffects)
                ? sourceDoor.m_closeEffects
                : CreateRuntimeEffectList(fallbackCloseEffects);

            if (!HasEffects(openEffects) || !HasEffects(closeEffects))
            {
                Debug.LogWarning($"[Moonforged Gates & Fences] Could not resolve runtime door sounds for {prefabName} using {sourceDoorPrefab}.");
                return;
            }

            foreach (Door door in doors)
            {
                if (door == null) continue;
                door.m_openEffects = openEffects;
                door.m_closeEffects = closeEffects;
            }
        }

        private static Door GetRuntimeDoor(string prefabName)
        {
            GameObject sourcePrefab = GetRuntimePrefab(prefabName);
            if (sourcePrefab == null) return null;

            Door sourceDoor = sourcePrefab.GetComponent<Door>();
            if (sourceDoor == null)
                sourceDoor = sourcePrefab.GetComponentInChildren<Door>(true);

            return sourceDoor;
        }

        private static GameObject GetRuntimePrefab(string prefabName)
        {
            if (string.IsNullOrEmpty(prefabName)) return null;

            GameObject runtimePrefab = ZNetScene.instance != null
                ? ZNetScene.instance.GetPrefab(prefabName)
                : null;

            if (runtimePrefab == null && PrefabManager.Instance != null)
                runtimePrefab = PrefabManager.Instance.GetPrefab(prefabName);

            return runtimePrefab;
        }

        private static EffectList CreateRuntimeEffectList(string[] effectPrefabNames)
        {
            if (effectPrefabNames == null)
                return new EffectList { m_effectPrefabs = new EffectList.EffectData[0] };

            foreach (string effectPrefabName in effectPrefabNames)
            {
                GameObject effectPrefab = GetRuntimePrefab(effectPrefabName);
                if (effectPrefab == null) continue;

                return new EffectList
                {
                    m_effectPrefabs = new[]
                    {
                        new EffectList.EffectData
                        {
                            m_prefab = effectPrefab,
                            m_enabled = true
                        }
                    }
                };
            }

            return new EffectList { m_effectPrefabs = new EffectList.EffectData[0] };
        }

        private static bool HasEffects(EffectList effects)
        {
            if (effects == null || effects.m_effectPrefabs == null || effects.m_effectPrefabs.Length == 0)
                return false;

            foreach (EffectList.EffectData effectData in effects.m_effectPrefabs)
            {
                if (effectData != null && effectData.m_prefab != null)
                    return true;
            }

            return false;
        }
    }
}