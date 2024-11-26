using HarmonyLib;
using Il2Cpp;
using System.Text;
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
				{
					__instance.theSun = 9999;
				}

				if (Utility.GetActive(Utility.UtilityType.UnliCoins))
				{
					__instance.theMoney = 999999999;
				}

				__instance.freeCD = Utility.GetActive(Utility.UtilityType.DeveloperMode);

				if (Utility.GetActive(Utility.UtilityType.ColumnPlants))
				{
					Board.BoardTag newTag = __instance.boardTag;
					newTag.isColumn = true;
					__instance.boardTag = newTag;
				}

				if (Utility.GetActive(Utility.UtilityType.StopZombieSpawn))
				{
					__instance.newZombieWaveCountDown = 15f;
				}
			}
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
