using HarmonyLib;
using Il2Cpp;
using MelonLoader;

namespace Seed_Rain_Overhaul
{
    [HarmonyPatch(typeof(Board))]
	internal class Board_Awake_Patch
	{

		public static int[] aquaticplantIndexes = new int[]
		{
			12, 15, 19, 1049, 1050, 1051, 1056, 1066, 1067, 1068,
			1069, 1076, 1077, 1078, 1083, 1087, 1101
		};

		[HarmonyPostfix]
		[HarmonyPatch("Awake")]
		public static void FixRainCardPrefix(Board __instance)
		{
			Core.instance.ReloadConfig();
			// MelonLogger.Msg("Seed Rain Overhaul is loaded!");

			if (Core.instance.configEnable.Value)
			{
				switch (Core.instance.configMode.Value)
				{
					case 1:
						{
							if (Core.plantNames.Length != 0 )
							{
								__instance.seedPool.Clear();
								for (int i = 0 ; i < Core.plantNames.Length ; i++)
								{
									if (Core.instance.boolArrayConfig[i].Value)
									{
										__instance.seedPool.Add(Core.plantIndexes[i]);
									}
								}
							}
							break;
						}
					case 2:
						AddTravelPlant(__instance);
                        break;
					case 3:
						__instance.seedPool.Clear();
                        AddTravelPlant(__instance);
                        break;
					case 4:
						__instance.seedPool.Clear();
						foreach (int num in Core.plantIndexes)
						{
							__instance.seedPool.Add(num);
						}
						break;
					case 5:
						__instance.seedPool.Clear();
						AddnonaquaticPlant(__instance);
						break;
				}
			}
		}

		private static void AddTravelPlant(Board __instance)
		{
			for (int i = 900 ; i <= 924 ; i++)
			{
				__instance.seedPool.Add(i);
			}
		}

		private static void AddnonaquaticPlant(Board __instance)
		{
			HashSet<int> hashSet = new HashSet<int>(aquaticplantIndexes);
			foreach (int num in Core.plantIndexes)
			{
				if (!hashSet.Contains(num))
				{
					__instance.seedPool.Add(num);
				}
			}
		}
	}
}
