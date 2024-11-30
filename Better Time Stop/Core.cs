using Il2Cpp;
using Il2CppTMPro;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(Better_Time_Stop.Core), "Better Time Stop", "1.0.1", "dynaslash", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace Better_Time_Stop
{
	public class Core : MelonMod
	{
		public static bool speedTrigger;
		public static MelonPreferences_Entry<float> configTime;
		public static MelonPreferences_Entry<bool> configEnable;

		public override void OnInitializeMelon()
		{
			MelonLogger.Msg("Better Time Stop is loaded!");

			// Configuration setup
			var category = MelonPreferences.CreateCategory("Better Time Stop", "");
			configTime = category.CreateEntry("Time", 0.2f, "Time Stop Duration");
			configEnable = category.CreateEntry("EnableKey", true, "Enable shortcut keys for adjusting time stop speed");

		}

		public override void OnUpdate()
		{
			// Toggle time stop with key press

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
		}
	}
}
