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
							CreatePlant.Instance.SetPlant(__instance.theMouseColumn, i, __instance.thePlantTypeOnMouse, null, default(Vector2), false, 0f);
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
					theDamage *= 10;
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

				if (Input.GetKeyDown(KeyCode.Alpha5))
				{
					Utility.SpawnItem("Items/SuperMachine");
				}

				if (Input.GetKeyDown(KeyCode.Keypad7))
				{
					Board.Instance.CreateUltimateMateorite();
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
