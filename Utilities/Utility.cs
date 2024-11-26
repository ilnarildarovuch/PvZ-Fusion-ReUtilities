using HarmonyLib;
using MelonLoader;
using UnityEngine;
using Il2Cpp;
using System.Text;


namespace Utilities
{
	internal class Utility
	{
		public enum UtilityType
		{
			UnliSun, // F1 Done
			UnliCoins, // F2 Done
			NoCooldown, // F3 Done
			ColumnPlants, // F4 Done
			InvulPlants, // F5 Done
			InvulZombies, // F6 Done
			DoubleDamage, // F7 Done
			SuperDamage, // F8 Done
			StopZombieSpawn, // F9 Done
			StopGameOver, // F10 Done
			DeveloperMode, // F11 Done
			ShowUtilities // F12 Done
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
				if (this.UtilityType == UtilityType.ShowUtilities)
					return;

				Core.ShowToast(string.Format("{0} [{1}]", this.Name, IsActive ? "ON" : "OFF"));
			}

            public override string ToString()
            {
                return string.Format("[{0}]{1} [{2}]", this.KeyCode.ToString(), this.Name, IsActive ? "ON" : "OFF");
            }
        }

		private static Dictionary<UtilityType, UtilityFeature> utilityLists = new Dictionary<UtilityType, UtilityFeature>()
		{
			{UtilityType.UnliSun, new UtilityFeature("Unlimited Sun", UtilityType.UnliSun, KeyCode.F1)},
			{UtilityType.UnliCoins, new UtilityFeature("Unlimited Coins", UtilityType.UnliCoins, KeyCode.F2)},
			{UtilityType.NoCooldown, new UtilityFeature("No Cooldown", UtilityType.NoCooldown, KeyCode.F3)},
			{UtilityType.ColumnPlants, new UtilityFeature("Column Plants", UtilityType.ColumnPlants, KeyCode.F4)},
			{UtilityType.InvulPlants, new UtilityFeature("Invulnerable Plants", UtilityType.InvulPlants, KeyCode.F5)},
			{UtilityType.InvulZombies, new UtilityFeature("Invulnerable Zombies", UtilityType.InvulZombies, KeyCode.F6)},	
			{UtilityType.DoubleDamage, new UtilityFeature("Double Plant Damage", UtilityType.DoubleDamage, KeyCode.F7)},
			{UtilityType.SuperDamage, new UtilityFeature("10x Plant Damage", UtilityType.SuperDamage, KeyCode.F8)},
			{UtilityType.StopZombieSpawn, new UtilityFeature("Stop Zombie Spawn", UtilityType.StopZombieSpawn, KeyCode.F9)},
			{UtilityType.StopGameOver, new UtilityFeature("Stop Game Over", UtilityType.StopGameOver, KeyCode.F10)},
			{UtilityType.DeveloperMode, new UtilityFeature("Developer Mode", UtilityType.DeveloperMode, KeyCode.F11)},

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
			if (!utilityLists.ContainsKey(UtilityType))
				return;

			utilityLists[UtilityType].ToggleUtility();
		}

		public static bool GetActive(UtilityType UtilityType)
		{
			if (!utilityLists.ContainsKey(UtilityType))
				return false;

			return utilityLists[UtilityType].IsActive;
		}

		public static void SetActive(UtilityType UtilityType, bool value)
		{
			if (!utilityLists.ContainsKey(UtilityType))
				return;

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
	}
}
