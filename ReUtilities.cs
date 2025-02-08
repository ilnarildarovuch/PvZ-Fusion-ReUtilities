using HarmonyLib;
using MelonLoader;
using UnityEngine;
using Il2Cpp;
using System.Collections.Generic;
using System.Text;

[assembly: MelonInfo(typeof(Utilities.Core), "ReUtilities", "221.0.0", "", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace Utilities
{
    // Core Class
    public class Core : MelonMod
    {
        private static DateTime dtStart;
        private static DateTime? dtStartToast;
        private static string toast_txt;
        public static bool isSeedRain = false;
        public static bool isScaredyDream = false;

        public override void OnEarlyInitializeMelon() => dtStart = DateTime.Now;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Utilities Addon is loaded!");
        }

        public override void OnLateInitializeMelon() => dtStart = DateTime.Now;

        public override void OnLateUpdate()
        {
            Utility.OnLateUpdate();
        }

        public override void OnGUI()
        {
            if (Utility.GetActive(Utility.UtilityType.ShowUtilities) || DateTime.Now - dtStart < new TimeSpan(0, 0, 0, 5))
            {
                string text = Utility.GetUtilities();
                int num = 0;
                int num2 = 20;
                foreach (string text2 in text.Split('\n', StringSplitOptions.None))
                {
                    if (text2.Length > num2)
                    {
                        num2 = text2.Length;
                    }
                    num++;
                }
                bool flag = GUI.Button(new Rect(10f, 30f, (float)num2 * 10f, (float)num * 16f + 15f), text);
            }

            if (dtStartToast != null)
            {
                GUI.Button(new Rect(10f, 10f, 200f, 20f), "\n" + toast_txt + "\n");
                TimeSpan? timeSpan = DateTime.Now - dtStartToast;
                TimeSpan t = new TimeSpan(0, 0, 0, 2);
                if (timeSpan > t)
                {
                    dtStartToast = null;
                }
            }
        }

        public static void ShowToast(string message)
        {
            toast_txt = message;
            dtStartToast = new DateTime?(DateTime.Now);
        }
    }

    // Cheat Patches
    internal class Patches
    {
        [HarmonyPatch(typeof(CardUI))]
        public static class CardUI_Patch
        {
            [HarmonyPostfix]
            [HarmonyPatch("Update")]
            private static void Update(CardUI __instance)
            {
                if (Utility.GetActive(Utility.UtilityType.NoCooldown))
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
            [HarmonyPatch("CDUpdate")]
            private static void CDUpdate(GloveMgr __instance)
            {
                if (Utility.GetActive(Utility.UtilityType.NoCooldown))
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
            [HarmonyPatch("CDUpdate")]
            private static void CDUpdate(HammerMgr __instance)
            {
                if (Utility.GetActive(Utility.UtilityType.NoCooldown))
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
            [HarmonyPatch("Update")]
            private static void Update(Board __instance)
            {
                if (Utility.GetActive(Utility.UtilityType.UnliSun))
                {
                    __instance.theSun = 99999;
                }

                if (Utility.GetActive(Utility.UtilityType.UnliCoins))
                {
                    __instance.theMoney = 2147400000;
                }

                __instance.freeCD = Utility.GetActive(Utility.UtilityType.NoCooldown) || Utility.GetActive(Utility.UtilityType.DeveloperMode);

                if (Utility.GetActive(Utility.UtilityType.StopZombieSpawn))
                {
                    __instance.newZombieWaveCountDown = 15f;
                }

                if (Input.GetKeyDown(KeyCode.Quote))
                {
                    Core.isScaredyDream = !Core.isScaredyDream;
                    Board.BoardTag boardTag = Board.Instance.boardTag;
                    boardTag.isScaredyDream = Core.isScaredyDream;
                    Board.Instance.boardTag = boardTag;
                }

                if (Input.GetKeyDown(KeyCode.Backslash))
                {
                    Core.isSeedRain = !Core.isSeedRain;
                    Board.BoardTag boardTag = Board.Instance.boardTag;
                    boardTag.isSeedRain = Core.isSeedRain;
                    boardTag.isNight = Core.isSeedRain;
                    Board.Instance.boardTag = boardTag;
                }
            }
        }

        [HarmonyPatch(typeof(Mouse))]
        public static class Mouse_Patch
        {
            [HarmonyPrefix]
            [HarmonyPatch("TryToSetPlantByCard")]
            private static void TryToSetPlantByCard(Mouse __instance)
            {
                if (Utility.GetActive(Utility.UtilityType.ColumnPlants))
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

        [HarmonyPatch(typeof(CreatePlant))]
        public static class CreatePlant_Patch
        {
#if X
            [HarmonyPostfix]
            [HarmonyPatch("CheckBox")]
            private static void CheckBox(ref bool __result)
            {
                if (Utility.GetActive(Utility.UtilityType.PlantEverywhere))
                {
                    __result = true;
                }
            }
#endif

            [HarmonyPrefix]
            [HarmonyPatch("SetPlant")]
            private static void SetPlant(ref bool isFreeSet)
            {
                if (Utility.GetActive(Utility.UtilityType.PlantEverywhere))
                {
                    isFreeSet = true;
                }
            }

#if TESTING
            [HarmonyPrefix]
            [HarmonyPatch("Lim")]
            private static bool Lim(ref bool __result)
            {
                __result = false;
                return false;
            }

            [HarmonyPrefix]
            [HarmonyPatch("LimTravel")]
            private static bool LimTravel(ref bool __result)
            {
                __result = false;
                return false;
            }
#endif
        }

        [HarmonyPatch(typeof(InGameUIMgr))]
        public static class InGameUIMgr_Patch
        {
            [HarmonyPostfix]
            [HarmonyPatch("Update")]
            private static void Update(InGameUIMgr __instance)
            {
                if (Utility.GetActive(Utility.UtilityType.UnliSun))
                {
                    __instance.sun.text = "∞";
                }

                if (Utility.GetActive(Utility.UtilityType.DeveloperMode))
                {
                    __instance.sun.text = "∞";
                }
            }
        }

        [HarmonyPatch(typeof(Money))]
        public static class Money_Patch
        {
            [HarmonyPostfix]
            [HarmonyPatch("Update")]
            private static void Update(Money __instance)
            {
                if (Utility.GetActive(Utility.UtilityType.UnliCoins))
                {
                    __instance.textMesh.text = "∞";
                    __instance.beanCount.text = "∞";
                    __instance.beanCount2.text = "∞";
                }

                if (Utility.GetActive(Utility.UtilityType.DeveloperMode))
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
            [HarmonyPatch("TakeDamage")]
            private static void TakeDamage(ref int damage)
            {
                if (Utility.GetActive(Utility.UtilityType.InvulPlants))
                {
                    damage = 0;
                }
            }

            [HarmonyPostfix]
            [HarmonyPatch("Update")]
            private static void Update(Plant __instance)
            {
                if (Input.GetKeyDown(KeyCode.KeypadPlus))
                {
                    __instance.Die(0);
                }
            }
        }

        [HarmonyPatch(typeof(Zombie))]
        public static class Zombie_Patch
        {
            [HarmonyPrefix]
            [HarmonyPatch("TakeDamage")]
            private static void TakeDamage(ref int theDamage)
            {
                if (Utility.GetActive(Utility.UtilityType.InvulZombies))
                {
                    theDamage = 0;
                }

                if (Utility.GetActive(Utility.UtilityType.DoubleDamage))
                {
                    theDamage *= 2;
                }

                if (Utility.GetActive(Utility.UtilityType.SuperDamage))
                {
                    theDamage *= 100;
                }
            }
        }

        [HarmonyPatch(typeof(GameAPP))]
        public static class GameAPP_Patch
        {
            [HarmonyPrefix]
            [HarmonyPatch("Update")]
            private static void Update(GameAPP __instance)
            {
                GameAPP.developerMode = Utility.GetActive(Utility.UtilityType.DeveloperMode);
                Generate();
            }

            private static void Generate()
            {
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    Utility.SpawnItem("Board/Award/TrophyPrefab");
                }

                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    Utility.SpawnItem("Items/fertilize/Ferilize");
                }

                if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    Utility.SpawnItem("Items/Bucket");
                }

                if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    Utility.SpawnItem("Items/Helmet");
                }

                if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    Utility.SpawnItem("Items/JackBox");
                }

                if (Input.GetKeyDown(KeyCode.Keypad5))
                {
                    Utility.SpawnItem("Items/Pickaxe");
                }

                if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    Utility.SpawnItem("Items/Machine");
                }

                if (Input.GetKeyDown(KeyCode.Keypad7))
                {
                    Utility.SpawnItem("Items/SuperMachine");
                }

                if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    Board.Instance.CreateUltimateMateorite();
                }

                if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    Utility.SpawnItem("Items/SproutPotPrize/SproutPotPrize");
                }

                if (Input.GetKeyDown(KeyCode.KeypadMultiply))
                {
                    foreach (Zombie zombie in Board.Instance.zombieArray)
                    {
                        if (zombie != null)
                        {
                            zombie.SetMindControl();
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.KeypadMinus))
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
        }

        [HarmonyPatch(typeof(GameLose))]
        public static class GameLose_Patch
        {
            [HarmonyPrefix]
            [HarmonyPatch("OnTriggerEnter2D")]
            private static bool OnTriggerEnter2D()
            {
                return !Utility.GetActive(Utility.UtilityType.StopGameOver);
            }
        }
    }

    // Main MelonLoader cheat-mod things
    internal class Utility
    {
        public enum UtilityType
        {
            UnliSun, UnliCoins, NoCooldown, InvulPlants, InvulZombies, DoubleDamage, SuperDamage,
            StopZombieSpawn, StopGameOver, PlantEverywhere, DeveloperMode, ShowUtilities,
            ColumnPlants, ScaredyDream, SeedRain, GenerateTrophy, GenerateFertilizer,
            GenerateBucket, GenerateHelmet, GenerateJack, GeneratePickaxe, GenerateMecha,
            GenerateSuperMecha, GenerateMeteor, GenerateSprout, CharmAll, KillAllZombies, KillAllPlants
        }

        private class UtilityFeature
        {
            public string Name { get; private set; }
            public UtilityType UtilityType { get; private set; }
            public KeyCode KeyCode { get; private set; }
            public bool IsActive { get; set; }

            public UtilityFeature(string Name, UtilityType UtilityType, KeyCode KeyCode, bool defaultValue = false)
            {
                this.Name = Name;
                this.UtilityType = UtilityType;
                this.KeyCode = KeyCode;
                this.IsActive = defaultValue;
            }

            public void ToggleUtility()
            {
                IsActive = !IsActive;
                if (this.UtilityType == UtilityType.GenerateTrophy || this.UtilityType == UtilityType.GenerateFertilizer ||
                    this.UtilityType == UtilityType.GenerateBucket || this.UtilityType == UtilityType.GenerateHelmet ||
                    this.UtilityType == UtilityType.GenerateJack || this.UtilityType == UtilityType.GeneratePickaxe ||
                    this.UtilityType == UtilityType.GenerateMecha || this.UtilityType == UtilityType.GenerateSuperMecha ||
                    this.UtilityType == UtilityType.GenerateMeteor || this.UtilityType == UtilityType.GenerateSprout)
                {
                    return;
                }

                if (this.UtilityType == UtilityType.CharmAll || this.UtilityType == UtilityType.KillAllPlants || this.UtilityType == UtilityType.KillAllZombies)
                {
                    return;
                }

                if (this.UtilityType == UtilityType.ShowUtilities)
                {
                    return;
                }

                Core.ShowToast(string.Format("{0} [{1}]", this.Name, IsActive ? "ON" : "OFF"));
            }

            public override string ToString()
            {
                if (this.UtilityType == UtilityType.ShowUtilities)
                {
                    return string.Format("[{0}] {1}", this.KeyCode.ToString(), this.Name);
                }

                if (this.UtilityType == UtilityType.GenerateTrophy || this.UtilityType == UtilityType.GenerateFertilizer ||
                    this.UtilityType == UtilityType.GenerateBucket || this.UtilityType == UtilityType.GenerateHelmet ||
                    this.UtilityType == UtilityType.GenerateJack || this.UtilityType == UtilityType.GeneratePickaxe ||
                    this.UtilityType == UtilityType.GenerateMecha || this.UtilityType == UtilityType.GenerateSuperMecha ||
                    this.UtilityType == UtilityType.GenerateMeteor || this.UtilityType == UtilityType.GenerateSprout)
                {
                    return string.Format("[{0}] {1}", this.KeyCode.ToString(), this.Name);
                }

                if (this.UtilityType == UtilityType.CharmAll || this.UtilityType == UtilityType.KillAllPlants || this.UtilityType == UtilityType.KillAllZombies)
                {
                    return string.Format("[{0}] {1}", this.KeyCode.ToString(), this.Name);
                }

                return string.Format("[{0}] {1} [{2}]", this.KeyCode.ToString(), this.Name, IsActive ? "ON" : "OFF");
            }
        }

        private static Dictionary<UtilityType, UtilityFeature> utilityLists = new Dictionary<UtilityType, UtilityFeature>()
        {
            {UtilityType.UnliSun, new UtilityFeature("Unlimited Sun", UtilityType.UnliSun, KeyCode.F1)},
            {UtilityType.UnliCoins, new UtilityFeature("Unlimited Coins", UtilityType.UnliCoins, KeyCode.F2)},
            {UtilityType.NoCooldown, new UtilityFeature("No Cooldown", UtilityType.NoCooldown, KeyCode.F3)},
            {UtilityType.InvulPlants, new UtilityFeature("Invulnerable Plants", UtilityType.InvulPlants, KeyCode.F4)},
            {UtilityType.InvulZombies, new UtilityFeature("Invulnerable Zombies", UtilityType.InvulZombies, KeyCode.F5)},
            {UtilityType.DoubleDamage, new UtilityFeature("Double Plant Damage", UtilityType.DoubleDamage, KeyCode.F6)},
            {UtilityType.SuperDamage, new UtilityFeature("10x Plant Damage", UtilityType.SuperDamage, KeyCode.F7)},
            {UtilityType.StopZombieSpawn, new UtilityFeature("Stop Zombie Spawn", UtilityType.StopZombieSpawn, KeyCode.F8)},
            {UtilityType.StopGameOver, new UtilityFeature("Stop Game Over", UtilityType.StopGameOver, KeyCode.F9)},
            {UtilityType.PlantEverywhere, new UtilityFeature("Plant Everywhere", UtilityType.PlantEverywhere, KeyCode.F10)},
            {UtilityType.DeveloperMode, new UtilityFeature("Developer Mode", UtilityType.DeveloperMode, KeyCode.F11)},
            {UtilityType.ColumnPlants, new UtilityFeature("Column Plants", UtilityType.ColumnPlants, KeyCode.Semicolon)},
            {UtilityType.ScaredyDream, new UtilityFeature("Scaredy Dream", UtilityType.ScaredyDream, KeyCode.Quote)},
            {UtilityType.SeedRain, new UtilityFeature("Seed Rain", UtilityType.SeedRain, KeyCode.Backslash)},
            {UtilityType.GenerateTrophy, new UtilityFeature("Generate Trophy", UtilityType.GenerateTrophy, KeyCode.Keypad0)},
            {UtilityType.GenerateFertilizer, new UtilityFeature("Generate Fertilizer", UtilityType.GenerateFertilizer, KeyCode.Keypad1)},
            {UtilityType.GenerateBucket, new UtilityFeature("Generate Bucket", UtilityType.GenerateBucket, KeyCode.Keypad2)},
            {UtilityType.GenerateHelmet, new UtilityFeature("Generate Helmet", UtilityType.GenerateHelmet, KeyCode.Keypad3)},
            {UtilityType.GenerateJack, new UtilityFeature("Generate Jack-in-the-Box", UtilityType.GenerateJack, KeyCode.Keypad4)},
            {UtilityType.GeneratePickaxe, new UtilityFeature("Generate Pickaxe", UtilityType.GeneratePickaxe, KeyCode.Keypad5)},
            {UtilityType.GenerateMecha, new UtilityFeature("Generate Mecha Fragment", UtilityType.GenerateMecha, KeyCode.Keypad6)},
            {UtilityType.GenerateSuperMecha, new UtilityFeature("Generate Giga Mecha Fragment", UtilityType.GenerateSuperMecha, KeyCode.Keypad7)},
            {UtilityType.GenerateMeteor, new UtilityFeature("Generate Meteor", UtilityType.GenerateMeteor, KeyCode.Keypad8)},
            {UtilityType.GenerateSprout, new UtilityFeature("Generate Sprout", UtilityType.GenerateSprout, KeyCode.Keypad9)},
            {UtilityType.CharmAll, new UtilityFeature("Charm All Zombies", UtilityType.CharmAll, KeyCode.KeypadMultiply)},
            {UtilityType.KillAllZombies, new UtilityFeature("Kill All Zombies", UtilityType.KillAllZombies, KeyCode.KeypadMinus)},
            {UtilityType.KillAllPlants, new UtilityFeature("Kill All Plants", UtilityType.KillAllPlants, KeyCode.KeypadPlus)},
            {UtilityType.ShowUtilities, new UtilityFeature("Utilities List", UtilityType.ShowUtilities, KeyCode.F12, false)},
        };

        public static string GetUtilities()
        {
            StringBuilder status = new StringBuilder();
            status.AppendLine("Utilities: ");
            foreach (var utility in utilityLists.Values)
            {
                status.AppendLine(utility.ToString());
            }
            return status.ToString();
        }

        public static void ToggleUtility(UtilityType UtilityType)
        {
            if (!utilityLists.ContainsKey(UtilityType)) return;
            utilityLists[UtilityType].ToggleUtility();
        }

        public static bool GetActive(UtilityType UtilityType)
        {
            if (!utilityLists.ContainsKey(UtilityType)) return false;
            return utilityLists[UtilityType].IsActive;
        }

        public static void SetActive(UtilityType UtilityType, bool value)
        {
            if (!utilityLists.ContainsKey(UtilityType)) return;
            utilityLists[UtilityType].IsActive = value;
        }

        public static void OnLateUpdate()
        {
            foreach (var (type, utility) in utilityLists)
            {
                if (utility.KeyCode != KeyCode.None && Enum.IsDefined(typeof(KeyCode), utility.KeyCode))
                {
                    if (Input.GetKeyDown(utility.KeyCode))
                    {
                        utility.ToggleUtility();
                    }
                }
            }
        }

        public static void SpawnItem(string resourcePath)
        {
            GameObject gameObject = Resources.Load(resourcePath);
            if (gameObject != null)
            {
                UnityEngine.Object.Instantiate(gameObject, new Vector2(0f, 0f), Quaternion.identity, GameAPP.board.transform);
            }
        }
    }
}