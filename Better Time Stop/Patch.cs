using HarmonyLib;
using Il2Cpp;
using Il2CppTMPro;
using UnityEngine;
using static MelonLoader.MelonLogger;

namespace Better_Time_Stop
{
    [HarmonyPatch(typeof(InGameBtn))]
	public static class Patch
	{
		[HarmonyPatch("SpeedTrigger")]
		[HarmonyPostfix]
		private static void SpeedTrigger(InGameBtn __instance)
		{ 
			if (__instance.buttonNumber == 3)
			{
				Time.timeScale =  Core.configTime.Value;
				Core.speedTrigger = true;
			}

			var slowTrigger = GameObject.Find("SlowTrigger");
			if (slowTrigger != null)
			{
				bool isPaused = GameAPP.theGameStatus == 1;
				bool isAlmanac = GameAPP.theGameStatus == 4;

				var text1 = slowTrigger.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
				var text2 = slowTrigger.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();

				if (isPaused || isAlmanac)
				{
					text1.text = "Paused";
					text2.text = "Paused";
					Time.timeScale = 0f;
				}
				else
				{
					text1.text = Core.speedTrigger ? "Time Slowed" : "Slow Time";
					text2.text = Core.speedTrigger ? "Time Slowed" : "Slow Time";
				}
			}
		}
	}
}
