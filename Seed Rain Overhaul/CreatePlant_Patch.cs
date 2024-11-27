using Il2Cpp;
using HarmonyLib;
using UnityEngine;

namespace Seed_Rain_Overhaul
{
	internal class CreatePlant_Patch
	{
		[HarmonyPatch(typeof(CreatePlant))]
		public static class Patch
		{
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
		}
	}
}
