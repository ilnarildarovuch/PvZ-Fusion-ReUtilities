using HarmonyLib;
using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace Utilities
{
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
                    __instance.theSun = 99999;

                if (Utility.GetActive(Utility.UtilityType.UnliCoins))
                    __instance.theMoney = 2147400000;

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

                // 
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    // IDK BUT I WANT MOREEEEEEEEEEEEEEEE
                    Utility.SpawnItem("Items/SuperMachine"); // 1
                    Utility.SpawnItem("Items/SuperMachine");
                    Utility.SpawnItem("Items/SuperMachine");
                    Utility.SpawnItem("Items/SuperMachine");
                    Utility.SpawnItem("Items/SuperMachine"); // 5
                    Utility.SpawnItem("Items/SuperMachine");
                    Utility.SpawnItem("Items/SuperMachine");
                    Utility.SpawnItem("Items/SuperMachine");
                    Utility.SpawnItem("Items/SuperMachine");
                    Utility.SpawnItem("Items/SuperMachine"); // 10
                    Utility.SpawnItem("Items/SuperMachine");
                    Utility.SpawnItem("Items/SuperMachine"); 
                    Utility.SpawnItem("Items/SuperMachine");
                    Utility.SpawnItem("Items/SuperMachine");
                    Utility.SpawnItem("Items/SuperMachine"); // 15
                }

                //
                if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.Z))
                {
                    // WTF IS THIS?
                    Utility.SpawnItem("Items/RedIronHead");

                }

                if (Input.GetKeyDown(KeyCode.Keypad7))
                {
                    //Board.Instance.CreateUltimateMateorite();
                    int successfulCreations = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        try
                        {
                            Board.Instance.CreateUltimateMateorite();
                            successfulCreations++;
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"Failed to create meteorite {i + 1}: {ex.Message}");
                        }
                    }

                    Debug.Log($"Created {successfulCreations} out of 5 Ultimate Meteorites");
                }


                if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    foreach (Zombie zombie in Board.Instance.zombieArray)
                    {
                        if (zombie != null)
                        {
                            zombie.SetMindControl();
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.Keypad9))
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

        //		[HarmonyPatch(typeof(SuperMachineNut))]
        //		public static class SuperMachineNutPatch 
        //		{
        //			[HarmonyPrefix]
        //			[HarmonyPatch("Summon")]
        //			private static void Summon(SuperMachineNut __instance, ref int theMaxHealth, GameObject gameObject)
        //			{
        //				Zombie zombie2 = gameObject.GetComponent<Zombie>();
        //                __instance.landSubmarine = zombie2;
        //                Zombie zombie3 = __instance.landSubmarine;
        //				zombie2.theMaxHealth = 3 * zombie2.theMaxHealth;
        //				zombie3.theMaxHealth = zombie2.theMaxHealth;
        //			}
        //		}
        //

//        [HarmonyPatch(typeof(Shulkflower))]
//        public static class Shulkflower_Patch
//        {
//            [HarmonyPrefix]
//            [HarmonyPatch("Awake")]
//            private static void Awake(Shulkflower __instance)
//            {
//                __instance.attributeCountdown = 0.01f;
//            }
//
//            [HarmonyPrefix]
//            [HarmonyPatch("AttributeEvent")]
//            private static void AttributeEvent(Shulkflower __instance)
//            {
//                __instance.SearchUpdate();
//                __instance.attributeCountdown = 0.01f;
//            }
//        }
//        [HarmonyPatch(typeof(TwinShulk))]
//        public static class TwinShulk_Patch
//        {
//            [HarmonyPostfix]
//            [HarmonyPatch("AnimShoot")]
//            private static bool AnimShoot(TwinShulk __instance, ref Bullet __result)
//            {
//                // The original method returns a Bullet, so use __result instead of a parameter
//                __instance.attributeCountdown = 0.1f;
//
//                __result.trackSpeed = 8f;
//                int num2 = __instance.attackDamage;
//                num2 += 5 * num2;
//                __result.theBulletDamage = num2;
//
//                return true; // Continue with original method
//            }
//        }



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
}
