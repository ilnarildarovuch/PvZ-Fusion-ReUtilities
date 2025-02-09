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
            if (Utility.IsFeatureActive(Utility.ReUtilityType.ShowUtilities) || DateTime.Now - _startTime < TimeSpan.FromSeconds(5))
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
                if (Utility.IsFeatureActive(Utility.ReUtilityType.NoCooldown))
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
                if (Utility.IsFeatureActive(Utility.ReUtilityType.NoCooldown))
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
                if (Utility.IsFeatureActive(Utility.ReUtilityType.NoCooldown))
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
                if (Utility.IsFeatureActive(Utility.ReUtilityType.UnlimitedSun))
                {
                    __instance.theSun = 99999;
                }

                if (Utility.IsFeatureActive(Utility.ReUtilityType.UnlimitedCoins))
                {
                    __instance.theMoney = 2147400000;
                }

                __instance.freeCD = Utility.IsFeatureActive(Utility.ReUtilityType.NoCooldown) || Utility.IsFeatureActive(Utility.ReUtilityType.DeveloperMode);

                if (Utility.IsFeatureActive(Utility.ReUtilityType.StopZombieSpawn))
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
        [HarmonyPatch(typeof(CreatePlant))]
        public static class CreatePlant_Patch
        {
            [HarmonyPostfix]
            [HarmonyPatch(nameof(CreatePlant.CheckBox))]
            private static void CheckBox(ref bool __result)
            {
                if (Utility.IsFeatureActive(Utility.ReUtilityType.PlantEverywhere))
                {
                    __result = true;
                }
            }

            [HarmonyPrefix]
            [HarmonyPatch(nameof(CreatePlant.SetPlant))]
            private static void SetPlant(ref bool isFreeSet)
            {
                if (Utility.IsFeatureActive(Utility.ReUtilityType.PlantEverywhere))
                {
                    isFreeSet = true;
                }
            }

            [HarmonyPrefix]
            [HarmonyPatch(nameof(CreatePlant.Lim))]
            private static bool Lim(ref bool __result)
            {
                __result = false;
                return false;
            }

            [HarmonyPrefix]
            [HarmonyPatch(nameof(CreatePlant.LimTravel))]
            private static bool LimTravel(ref bool __result)
            {
                __result = false;
                return false;
            }
        }


        [HarmonyPatch(typeof(Mouse))]
        public static class Mouse_Patch
        {
            [HarmonyPrefix]
            [HarmonyPatch(nameof(Mouse.TryToSetPlantByCard))]
            private static void TryToSetPlantByCard(Mouse __instance)
            {
                if (Utility.IsFeatureActive(Utility.ReUtilityType.ColumnPlants))
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
                if (Utility.IsFeatureActive(Utility.ReUtilityType.UnlimitedSun) || Utility.IsFeatureActive(Utility.ReUtilityType.DeveloperMode))
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
                if (Utility.IsFeatureActive(Utility.ReUtilityType.UnlimitedCoins) || Utility.IsFeatureActive(Utility.ReUtilityType.DeveloperMode))
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
                if (Utility.IsFeatureActive(Utility.ReUtilityType.InvulnerablePlants))
                {
                    damage = -500;
                }
            }

            [HarmonyPrefix]
            [HarmonyPatch(nameof(Plant.Die))]
            private static bool Die(Plant __instance)
            {
                if (Utility.IsFeatureActive(Utility.ReUtilityType.InvulnerablePlants))
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

        [HarmonyPatch(typeof(GameAPP))]
        public static class GameAPP_Patch
        {
            [HarmonyPrefix]
            [HarmonyPatch(nameof(GameAPP.Update))]
            private static void Update(GameAPP __instance)
            {
                GameAPP.developerMode = Utility.IsFeatureActive(Utility.ReUtilityType.DeveloperMode);
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
            private static bool OnTriggerEnter2D() => !Utility.IsFeatureActive(Utility.ReUtilityType.StopGameOver);
        }
        
        [HarmonyPatch(typeof(CreateBullet))]
        public static class CreateBullet_Patch
        {
            [HarmonyPrefix]
            [HarmonyPatch(nameof(CreateBullet.SetBullet))]
            static void SetBullet(ref int theBulletType) {
                if (Utility.IsFeatureActive(Utility.ReUtilityType.RandomBullets))
                {
                    theBulletType = UnityEngine.Random.Range(0, 100);
                }
            }
        }
    }

    // Main Utility Class
    internal static class Utility
    {
        public enum ReUtilityType
        {
            UnlimitedSun, UnlimitedCoins, NoCooldown, InvulnerablePlants,
            StopZombieSpawn, StopGameOver, PlantEverywhere, RandomBullets,
            DeveloperMode, ShowUtilities, ColumnPlants, ScaredyDream, SeedRain,
            GenerateTrophy, GenerateFertilizer, GenerateBucket, GenerateHelmet, GenerateJack,
            GeneratePickaxe, GenerateMecha, GenerateSuperMecha, GenerateMeteor, GenerateSprout,
            CharmAll, KillAllZombies, KillAllPlants
        }

        private static readonly Dictionary<ReUtilityType, Feature> _features = new()
        {
            { ReUtilityType.UnlimitedSun, new Feature("Unlimited Sun", KeyCode.F1, false) },
            { ReUtilityType.UnlimitedCoins, new Feature("Unlimited Coins", KeyCode.F2, false) },
            { ReUtilityType.NoCooldown, new Feature("No Cooldown", KeyCode.F3, false) },
            { ReUtilityType.InvulnerablePlants, new Feature("Invulnerable Plants", KeyCode.F4, false) },
            { ReUtilityType.StopZombieSpawn, new Feature("Stop Zombie Spawn", KeyCode.F8, false) },
            { ReUtilityType.StopGameOver, new Feature("Stop Game Over", KeyCode.F9, false) },
            { ReUtilityType.PlantEverywhere, new Feature("Plant Everywhere", KeyCode.F10, false) },
            { ReUtilityType.DeveloperMode, new Feature("Developer Mode", KeyCode.F11, false) },
            { ReUtilityType.ColumnPlants, new Feature("Column Plants", KeyCode.Semicolon, false) },
            { ReUtilityType.ScaredyDream, new Feature("Scaredy Dream", KeyCode.Quote, false) },
            { ReUtilityType.SeedRain, new Feature("Seed Rain", KeyCode.Backslash, false) },
            { ReUtilityType.GenerateTrophy, new Feature("Generate Trophy", KeyCode.Keypad0, false) },
            { ReUtilityType.GenerateFertilizer, new Feature("Generate Fertilizer", KeyCode.Keypad1, false) },
            { ReUtilityType.GenerateBucket, new Feature("Generate Bucket", KeyCode.Keypad2, false) },
            { ReUtilityType.GenerateHelmet, new Feature("Generate Helmet", KeyCode.Keypad3, false) },
            { ReUtilityType.GenerateJack, new Feature("Generate Jack", KeyCode.Keypad4, false) },
            { ReUtilityType.GeneratePickaxe, new Feature("Generate Pickaxe", KeyCode.Keypad5, false) },
            { ReUtilityType.GenerateMecha, new Feature("Generate Mecha", KeyCode.Keypad6, false) },
            { ReUtilityType.GenerateSuperMecha, new Feature("Generate Super Mecha", KeyCode.Keypad7, false) },
            { ReUtilityType.GenerateMeteor, new Feature("Generate Meteor", KeyCode.Keypad8, false) },
            { ReUtilityType.GenerateSprout, new Feature("Generate Sprout", KeyCode.Keypad9, false) },
            { ReUtilityType.CharmAll, new Feature("Charm All", KeyCode.KeypadMultiply, false) },
            { ReUtilityType.KillAllZombies, new Feature("Kill All Zombies", KeyCode.KeypadMinus, false) },
            { ReUtilityType.KillAllPlants, new Feature("Kill All Plants", KeyCode.KeypadPlus, false) },
            { ReUtilityType.RandomBullets, new Feature("Random Bullets", KeyCode.Alpha4, false) },
            { ReUtilityType.ShowUtilities, new Feature("Show Utilities", KeyCode.F12, true) }
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

        public static void ToggleFeature(ReUtilityType type)
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

        public static bool IsFeatureActive(ReUtilityType type)
        {
            return _features.TryGetValue(type, out var feature) && feature.IsActive;
        }

        public static void SetFeatureActive(ReUtilityType type, bool value)
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

        private static bool IsSpecialFeature(ReUtilityType type)
        {
            return type == ReUtilityType.GenerateTrophy || type == ReUtilityType.GenerateFertilizer ||
                   type == ReUtilityType.GenerateBucket || type == ReUtilityType.GenerateHelmet ||
                   type == ReUtilityType.GenerateJack || type == ReUtilityType.GeneratePickaxe ||
                   type == ReUtilityType.GenerateMecha || type == ReUtilityType.GenerateSuperMecha ||
                   type == ReUtilityType.GenerateMeteor || type == ReUtilityType.GenerateSprout ||
                   type == ReUtilityType.CharmAll || type == ReUtilityType.KillAllZombies || type == ReUtilityType.KillAllPlants;
        }

        private class Feature
        {
            public string Name { get; }
            public ReUtilityType Type { get; }
            public KeyCode KeyCode { get; }
            public bool IsActive { get; set; }

            public Feature(string name, KeyCode key, bool defaultValue = true)
            {
                Name = name;
                // Ensure the name matches exactly with the enum member by removing spaces or using explicit mapping
                Type = (ReUtilityType)Enum.Parse(typeof(ReUtilityType), name.Replace(" ", ""), true);
                KeyCode = key;
                IsActive = defaultValue;
            }

            public override string ToString()
            {
                if (IsSpecialFeature(Type) || Type == ReUtilityType.ShowUtilities)
                {
                    return $"[{KeyCode}] {Name}";
                }
                return $"[{KeyCode}] {Name} [{(IsActive ? "ON" : "OFF")}]";
            }
        }
    }
}