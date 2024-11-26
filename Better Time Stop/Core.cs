using Il2Cpp;
using Il2CppTMPro;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(Better_Time_Stop.Core), "Better Time Stop", "1.0.0", "dynaslash", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace Better_Time_Stop
{
	public class Core : MelonMod
	{
		private static bool speedTrigger;
		private static MelonPreferences_Entry<float> configTime;
		private static MelonPreferences_Entry<bool> configEnable;

		public override void OnInitializeMelon()
		{
			MelonLogger.Msg("Better Time Stop is loaded!");

			// Configuration setup
			var category = MelonPreferences.CreateCategory("Better Time Stop", "");
			configTime = category.CreateEntry("Time", 0.2f, "Time Stop Duration");
			configEnable = category.CreateEntry("EnableKey", true, "Enable shortcut keys for adjusting time stop speed");

		}

		public override void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
			HarmonyInstance.PatchAll();
		}

		public override void OnUpdate()
		{
			// Toggle time stop with key press
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				speedTrigger = !speedTrigger;
			}

			if (configEnable.Value)
			{
				if (Input.GetKeyDown(KeyCode.X) && configTime.Value < 0.2f)
				{
					configTime.Value += 0.01f;
					MelonPreferences.Save();
				}
				if (Input.GetKeyDown(KeyCode.Z) && configTime.Value > 0.01f)
				{
					configTime.Value -= 0.01f;
					MelonPreferences.Save();
				}    
			}

			// Clamping time stop duration within set bounds
			if (configTime.Value < 0.01f)
			{
				configTime.Value = 0.01f;
				MelonPreferences.Save();
			}
			if (configTime.Value > 0.2f)
			{
				configTime.Value = 0.2f;
				MelonPreferences.Save();
			}

			// Apply time stop effect or reset to normal speed
			Time.timeScale = speedTrigger ? configTime.Value : GameAPP.gameSpeed;

			UpdateSlowTriggerText();
		}

		private void UpdateSlowTriggerText()
		{
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
					text1.text = speedTrigger ? "Time Slowed" : "Slow Time";
					text2.text = speedTrigger ? "Time Slowed" : "Slow Time";
				}
			}
		}
	}
}
