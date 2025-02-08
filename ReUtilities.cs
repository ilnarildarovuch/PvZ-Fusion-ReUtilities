using HarmonyLib;
using MelonLoader;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using Il2Cpp;

[assembly: MelonInfo(typeof(ReUtilities.Core), "ReUtilities", "221.0.0", "Open distribution", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace ReUtilities
{
    // Core Class
    public class Core : MelonMod
    {
        private static DateTime _startTime;
        private static DateTime? _toastStartTime;
        private static string _toastMessage;

        public static bool IsSeedRainEnabled { get; set; } = false;
        public static bool IsScaredyDreamEnabled { get; set; } = false;

        public override void OnEarlyInitializeMelon() => _startTime = DateTime.Now;

        public override void OnInitializeMelon()
        {
            base.OnInitializeMelon();
        }

        public override void OnLateInitializeMelon() => _startTime = DateTime.Now;

        public override void OnLateUpdate()
        {
            Utility.OnLateUpdate();
        }

        public override void OnGUI()
        {
            DisplayUtilityList();
            DisplayToastMessage();
        }

        private void DisplayUtilityList()
        {
            if (Utility.IsFeatureActive(Utility.UtilityType.ShowUtilities) || DateTime.Now - _startTime < TimeSpan.FromSeconds(5))
            {
                string utilityText = Utility.GetUtilityStatus();
                int maxLineLength = 0;
                int lineCount = 0;

                foreach (string line in utilityText.Split('\n'))
                {
                    maxLineLength = Math.Max(maxLineLength, line.Length);
                    lineCount++;
                }

                GUI.Button(new Rect(10f, 30f, maxLineLength * 10f, lineCount * 16f + 15f), utilityText);
            }
        }

        private void DisplayToastMessage()
        {
            if (_toastStartTime != null)
            {
                GUI.Button(new Rect(10f, 10f, 200f, 20f), $"\n{_toastMessage}\n");

                if (DateTime.Now - _toastStartTime > TimeSpan.FromSeconds(2))
                {
                    _toastStartTime = null;
                }
            }
        }

        public static void ShowToast(string message)
        {
            _toastMessage = message;
            _toastStartTime = DateTime.Now;
        }
    }

    // Cheat Patches
    internal static class Patches
    {
        [HarmonyPatch(typeof(CardUI))]
        public static class CardUI_Patch
        {
            [HarmonyPostfix]
            [HarmonyPatch(nameof(CardUI.Update))]
            private static void Update(CardUI __instance)
            {
                if (Utility.IsFeatureActive(Utility.UtilityType.NoCooldown))
                {
                    __instance.CD = __instance.fullCD;
                    __instance.isAvailable = true;
                }
            }
        }

        [HarmonyPatch(typeof(GloveMgr))]
        public static class GloveMgr_Patch
        {
            [HarmonyPrefix]
            [HarmonyPatch(nameof(GloveMgr.CDUpdate))]
            private static void CDUpdate(GloveMgr __instance)
            {
                if (Utility.IsFeatureActive(Utility.UtilityType.NoCooldown))
                {
                    __instance.CD = __instance.fullCD;
                    __instance.avaliable = true;
                }
            }
        }

        [HarmonyPatch(typeof(HammerMgr))]
        public static class HammerMgr_Patch
        {
            [HarmonyPrefix]
            [HarmonyPatch(nameof(HammerMgr.CDUpdate))]
            private static void CDUpdate(HammerMgr __instance)
            {
                if (Utility.IsFeatureActive(Utility.UtilityType.NoCooldown))
                {
                    __instance.CD = __instance.fullCD;
                    __instance.avaliable = true;
                }
            }
        }

        [HarmonyPatch(typeof(Board))]
        public static class Board_Patch
        {
            [HarmonyPostfix]
            [HarmonyPatch(nameof(Board.Update))]
            private static void Update(Board __instance)
            {
                if (Utility.IsFeatureActive(Utility.UtilityType.UnlimitedSun))
                {
                    __instance.theSun = 99999;
                }

                if (Utility.IsFeatureActive(Utility.UtilityType.UnlimitedCoins))
                {
                    __instance.theMoney = 2147400000;
                }

                __instance.freeCD = Utility.IsFeatureActive(Utility.UtilityType.NoCooldown) || Utility.IsFeatureActive(Utility.UtilityType.DeveloperMode);

                if (Utility.IsFeatureActive(Utility.UtilityType.StopZombieSpawn))
                {
                    __instance.newZombieWaveCountDown = 15f;
                }

                HandleCustomFeatures(__instance);
            }

            private static void HandleCustomFeatures(Board __instance)
            {
                if (Input.GetKeyDown(KeyCode.Quote))
                {
                    Core.IsScaredyDreamEnabled = !Core.IsScaredyDreamEnabled;
                    __instance.boardTag = new Board.BoardTag
                    {
                        isScaredyDream = Core.IsScaredyDreamEnabled
                    };
                }

                if (Input.GetKeyDown(KeyCode.Backslash))
                {
                    Core.IsSeedRainEnabled = !Core.IsSeedRainEnabled;
                    __instance.boardTag = new Board.BoardTag
                    {
                        isNight = Core.IsSeedRainEnabled,
                        isScaredyDream = Core.IsScaredyDreamEnabled,
                        isSeedRain = Core.IsSeedRainEnabled
                    };
                }
            }
        }

        [HarmonyPatch(typeof(Mouse))]
        public static class Mouse_Patch
        {
            [HarmonyPrefix]
            [HarmonyPatch(nameof(Mouse.TryToSetPlantByCard))]
            private static void TryToSetPlantByCard(Mouse __instance)
            {
                if (Utility.IsFeatureActive(Utility.UtilityType.ColumnPlants))
                {
                    for (int i = 0; i < Board.Instance.rowNum; i++)
                    {
                        if (i != __instance.theMouseRow)
                        {
                            CreatePlant.Instance.SetPlant(__instance.theMouseColumn, i, __instance.thePlantTypeOnMouse, null, default(Vector2), false, true);
                        }
                    }
                }
            }
        }

        [HarmonyPatch(typeof(InGameUIMgr))]
        public static class InGameUIMgr_Patch
        {
            [HarmonyPostfix]
            [HarmonyPatch(nameof(InGameUIMgr.Update))]
            private static void Update(InGameUIMgr __instance)
            {
                if (Utility.IsFeatureActive(Utility.UtilityType.UnlimitedSun) || Utility.IsFeatureActive(Utility.UtilityType.DeveloperMode))
                {
                    __instance.sun.text = "∞";
                }
            }
        }

        [HarmonyPatch(typeof(Money))]
        public static class Money_Patch
        {
            [HarmonyPostfix]
            [HarmonyPatch(nameof(Money.Update))]
            private static void Update(Money __instance)
            {
                if (Utility.IsFeatureActive(Utility.UtilityType.UnlimitedCoins) || Utility.IsFeatureActive(Utility.UtilityType.DeveloperMode))
                {
                    __instance.textMesh.text = "∞";
                    __instance.beanCount.text = "∞";
                    __instance.beanCount2.text = "∞";
                }
            }
        }

        [HarmonyPatch(typeof(Plant))]
        public static class Plant_Patch
        {
            [HarmonyPrefix]
            [HarmonyPatch(nameof(Plant.TakeDamage))]
            private static void TakeDamage(ref int damage)
            {
                if (Utility.IsFeatureActive(Utility.UtilityType.InvulnerablePlants))
                {
                    damage = -500;
                }
            }

            [HarmonyPrefix]
            [HarmonyPatch(nameof(Plant.Die))]
            private static bool Die(Plant __instance)
            {
                if (Utility.IsFeatureActive(Utility.UtilityType.InvulnerablePlants))
                {
                    return false;
                }
                return true;
            }

            [HarmonyPostfix]
            [HarmonyPatch(nameof(Plant.Update))]
            private static void Update(Plant __instance)
            {
                if (Input.GetKeyDown(KeyCode.KeypadPlus))
                {
                    __instance.Die(0);
                }
            }
        }

        [HarmonyPatch(typeof(Zombie))]
        [HarmonyPrefix]
        [HarmonyPatch(nameof(Zombie.Die))]
        private static bool TakeDamage(Zombie __instance)
        {
            if (Utility.IsFeatureActive(Utility.UtilityType.InvulnerableZombies))
            {
                return false;
            }
            return true;
        }

        [HarmonyPatch(typeof(GameAPP))]
        public static class GameAPP_Patch
        {
            [HarmonyPrefix]
            [HarmonyPatch(nameof(GameAPP.Update))]
            private static void Update(GameAPP __instance)
            {
                GameAPP.developerMode = Utility.IsFeatureActive(Utility.UtilityType.DeveloperMode);
                GenerateItems();
            }

            private static void GenerateItems()
            {
                if (Input.GetKeyDown(KeyCode.Keypad0)) Utility.SpawnItem("Board/Award/TrophyPrefab");
                if (Input.GetKeyDown(KeyCode.Keypad1)) Utility.SpawnItem("Items/fertilize/Ferilize");
                if (Input.GetKeyDown(KeyCode.Keypad2)) Utility.SpawnItem("Items/Bucket");
                if (Input.GetKeyDown(KeyCode.Keypad3)) Utility.SpawnItem("Items/Helmet");
                if (Input.GetKeyDown(KeyCode.Keypad4)) Utility.SpawnItem("Items/JackBox");
                if (Input.GetKeyDown(KeyCode.Keypad5)) Utility.SpawnItem("Items/Pickaxe");
                if (Input.GetKeyDown(KeyCode.Keypad6)) Utility.SpawnItem("Items/Machine");
                if (Input.GetKeyDown(KeyCode.Keypad7)) Utility.SpawnItem("Items/SuperMachine");
                if (Input.GetKeyDown(KeyCode.Keypad8)) Board.Instance.CreateUltimateMateorite();
                if (Input.GetKeyDown(KeyCode.Keypad9)) Utility.SpawnItem("Items/SproutPotPrize/SproutPotPrize");
                if (Input.GetKeyDown(KeyCode.KeypadMultiply)) MindControlAllZombies();
                if (Input.GetKeyDown(KeyCode.KeypadMinus)) KillAllZombies();
            }

            private static void MindControlAllZombies()
            {
                foreach (Zombie zombie in Board.Instance.zombieArray)
                {
                    zombie?.SetMindControl();
                }
            }

            private static void KillAllZombies()
            {
                foreach (Zombie zombie in Board.Instance.zombieArray)
                {
                    if (zombie != null && !zombie.isMindControlled)
                    {
                        zombie.Die(1);
                    }
                }
            }
        }

        [HarmonyPatch(typeof(GameLose))]
        public static class GameLose_Patch
        {
            [HarmonyPrefix]
            [HarmonyPatch(nameof(GameLose.OnTriggerEnter2D))]
            private static bool OnTriggerEnter2D() => !Utility.IsFeatureActive(Utility.UtilityType.StopGameOver);
        }
    }

    // Main Utility Class
    internal static class Utility
    {
        public enum UtilityType
        {
            UnlimitedSun, UnlimitedCoins, NoCooldown, InvulnerablePlants, InvulnerableZombies,
            StopZombieSpawn, StopGameOver, PlantEverywhere,
            DeveloperMode, ShowUtilities, ColumnPlants, ScaredyDream, SeedRain,
            GenerateTrophy, GenerateFertilizer, GenerateBucket, GenerateHelmet, GenerateJack,
            GeneratePickaxe, GenerateMecha, GenerateSuperMecha, GenerateMeteor, GenerateSprout,
            CharmAll, KillAllZombies, KillAllPlants
        }

        private static readonly Dictionary<UtilityType, Feature> _features = new()
        {
            { UtilityType.UnlimitedSun, new Feature("Unlimited Sun", KeyCode.F1) },
            { UtilityType.UnlimitedCoins, new Feature("Unlimited Coins", KeyCode.F2) },
            { UtilityType.NoCooldown, new Feature("No Cooldown", KeyCode.F3) },
            { UtilityType.InvulnerablePlants, new Feature("Invulnerable Plants", KeyCode.F4) },
            { UtilityType.InvulnerableZombies, new Feature("Invulnerable Zombies", KeyCode.F5) },
            { UtilityType.StopZombieSpawn, new Feature("Stop Zombie Spawn", KeyCode.F8) },
            { UtilityType.StopGameOver, new Feature("Stop Game Over", KeyCode.F9) },
            { UtilityType.PlantEverywhere, new Feature("Plant Everywhere", KeyCode.F10) },
            { UtilityType.DeveloperMode, new Feature("Developer Mode", KeyCode.F11) },
            { UtilityType.ColumnPlants, new Feature("Column Plants", KeyCode.Semicolon) },
            { UtilityType.ScaredyDream, new Feature("Scaredy Dream", KeyCode.Quote) },
            { UtilityType.SeedRain, new Feature("Seed Rain", KeyCode.Backslash) },
            { UtilityType.GenerateTrophy, new Feature("Generate Trophy", KeyCode.Keypad0) },
            { UtilityType.GenerateFertilizer, new Feature("Generate Fertilizer", KeyCode.Keypad1) },
            { UtilityType.GenerateBucket, new Feature("Generate Bucket", KeyCode.Keypad2) },
            { UtilityType.GenerateHelmet, new Feature("Generate Helmet", KeyCode.Keypad3) },
            { UtilityType.GenerateJack, new Feature("Generate Jack", KeyCode.Keypad4) },
            { UtilityType.GeneratePickaxe, new Feature("Generate Pickaxe", KeyCode.Keypad5) },
            { UtilityType.GenerateMecha, new Feature("Generate Mecha", KeyCode.Keypad6) },
            { UtilityType.GenerateSuperMecha, new Feature("Generate Super Mecha", KeyCode.Keypad7) },
            { UtilityType.GenerateMeteor, new Feature("Generate Meteor", KeyCode.Keypad8) },
            { UtilityType.GenerateSprout, new Feature("Generate Sprout", KeyCode.Keypad9) },
            { UtilityType.CharmAll, new Feature("Charm All", KeyCode.KeypadMultiply) },
            { UtilityType.KillAllZombies, new Feature("Kill All Zombies", KeyCode.KeypadMinus) },
            { UtilityType.KillAllPlants, new Feature("Kill All Plants", KeyCode.KeypadPlus) },
            { UtilityType.ShowUtilities, new Feature("Show Utilities", KeyCode.F12, false) }
        };

        public static string GetUtilityStatus()
        {
            StringBuilder status = new StringBuilder("ReUtilities:\n");
            foreach (var feature in _features.Values)
            {
                status.AppendLine(feature.ToString());
            }
            return status.ToString();
        }

        public static void ToggleFeature(UtilityType type)
        {
            if (_features.TryGetValue(type, out var feature))
            {
                feature.IsActive = !feature.IsActive;
                if (!IsSpecialFeature(type))
                {
                    Core.ShowToast($"{feature.Name} [{(feature.IsActive ? "ON" : "OFF")}]");
                }
            }
        }

        public static bool IsFeatureActive(UtilityType type)
        {
            return _features.TryGetValue(type, out var feature) && feature.IsActive;
        }

        public static void SetFeatureActive(UtilityType type, bool value)
        {
            if (_features.TryGetValue(type, out var feature))
            {
                feature.IsActive = value;
            }
        }

        public static void OnLateUpdate()
        {
            foreach (var feature in _features.Values)
            {
                if (feature.KeyCode != KeyCode.None && Input.GetKeyDown(feature.KeyCode))
                {
                    ToggleFeature(feature.Type);
                }
            }
        }

        public static void SpawnItem(string resourcePath)
        {
            GameObject item = Resources.Load<GameObject>(resourcePath) as GameObject;
            if (item != null)
            {
                UnityEngine.Object.Instantiate(item, Vector2.zero, Quaternion.identity, GameAPP.board.transform);
            }
        }

        private static bool IsSpecialFeature(UtilityType type)
        {
            return type == UtilityType.GenerateTrophy || type == UtilityType.GenerateFertilizer ||
                   type == UtilityType.GenerateBucket || type == UtilityType.GenerateHelmet ||
                   type == UtilityType.GenerateJack || type == UtilityType.GeneratePickaxe ||
                   type == UtilityType.GenerateMecha || type == UtilityType.GenerateSuperMecha ||
                   type == UtilityType.GenerateMeteor || type == UtilityType.GenerateSprout ||
                   type == UtilityType.CharmAll || type == UtilityType.KillAllZombies || type == UtilityType.KillAllPlants;
        }

        private class Feature
        {
            public string Name { get; }
            public UtilityType Type { get; }
            public KeyCode KeyCode { get; }
            public bool IsActive { get; set; }

            public Feature(string name, KeyCode key, bool defaultValue = true)
            {
                Name = name;
                // Ensure the name matches exactly with the enum member by removing spaces or using explicit mapping
                Type = (UtilityType)Enum.Parse(typeof(UtilityType), name.Replace(" ", ""), true);
                KeyCode = key;
                IsActive = defaultValue;
            }

            public override string ToString()
            {
                if (IsSpecialFeature(Type) || Type == UtilityType.ShowUtilities)
                {
                    return $"[{KeyCode}] {Name}";
                }
                return $"[{KeyCode}] {Name} [{(IsActive ? "ON" : "OFF")}]";
            }
        }
    }
}