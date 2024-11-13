using HarmonyLib;
using Il2Cpp;

namespace Better_Time_Stop
{
    [HarmonyPatch(typeof(InGameBtn))]
	[HarmonyPatch("SpeedTrigger")]
	public static class Patch
	{
		[HarmonyPrefix]

		public static bool BetterSpeedTrigger()
		{ 
			return false;
		}
	}
}
