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
			InvulPlants, // F4 Done
			InvulZombies, // F5 Done
			DoubleDamage, // F6 Done
			SuperDamage, // F7 Done
			StopZombieSpawn, // F8 Done
			StopGameOver, // F9 Done
			PlantEverywhere, // F10 Done
			DeveloperMode, // F11 Done
			ShowUtilities, // F12 Done
			ColumnPlants, // Semicolon Done
			ScaredyDream, // Quote 
			SeedRain, // Backslash 
			
			GenerateTrophy, // 0
			GenerateFertilizer, // 1
			GenerateBucket, // 2
			GenerateHelmet, // 3
			GenerateJack, // 4
			GeneratePickaxe, // 5
			GenerateMecha, // 6
			GenerateMeteor, // 7
			CharmAll, // 8
			KillAllZombies, // 9
			KillAllPlants, // +
		}

		public enum GenerateType
		{
			GenerateTrophy, // 0
			GenerateFertilizer, // 1
			GenerateBucket, // 2
			GenerateHelmet, // 3
			GenerateJack, // 4
			GeneratePickaxe, // 5
			GenerateMecha, // 6
			GenerateMeteor, // 7
			CharmAll, // 8
			KillAllZombies, // 9
			KillAllPlants, // +
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

				if (Enum.IsDefined(typeof(GenerateType), (GenerateType)this.UtilityType))
				{
					return;
				}

				if (this.UtilityType == UtilityType.ShowUtilities)
					return;

				Core.ShowToast(string.Format("{0} [{1}]", this.Name, IsActive ? "ON" : "OFF"));
			}

			public override string ToString()
			{
				if (this.UtilityType == UtilityType.ShowUtilities)
				{
					return string.Format("[{0}] {1}", this.KeyCode.ToString(), this.Name);
				}

				if (this.UtilityType == UtilityType.GenerateTrophy || this.UtilityType == UtilityType.GenerateFertilizer || this.UtilityType == UtilityType.GenerateBucket || this.UtilityType == UtilityType.GenerateHelmet || this.UtilityType == UtilityType.GenerateJack || this.UtilityType == UtilityType.GeneratePickaxe || this.UtilityType == UtilityType.GenerateMecha || this.UtilityType == UtilityType.GenerateMeteor)
				{
					return string.Format("[{0}] {1}", this.KeyCode.ToString(), this.Name);
				}

				if (this.UtilityType == UtilityType.CharmAll || this.UtilityType == UtilityType.KillAllPlants || this.UtilityType == UtilityType.KillAllZombies)
				{
					return string.Format("[{0}] {1}", this.KeyCode.ToString(), this.Name);
				}

				return string.Format("[{0}] {1} [{2}]", this.KeyCode.ToString(), this.Name, IsActive ? "ON" : "OFF");
			}
		}

		private static Dictionary<UtilityType, UtilityFeature> utilityLists = new Dictionary<UtilityType, UtilityFeature>()
		{
			{UtilityType.UnliSun, new UtilityFeature("Unlimited Sun", UtilityType.UnliSun, KeyCode.F1)},
			{UtilityType.UnliCoins, new UtilityFeature("Unlimited Coins", UtilityType.UnliCoins, KeyCode.F2)},
			{UtilityType.NoCooldown, new UtilityFeature("No Cooldown", UtilityType.NoCooldown, KeyCode.F3)},
			{UtilityType.InvulPlants, new UtilityFeature("Invulnerable Plants", UtilityType.InvulPlants, KeyCode.F4)},
			{UtilityType.InvulZombies, new UtilityFeature("Invulnerable Zombies", UtilityType.InvulZombies, KeyCode.F5)},	
			{UtilityType.DoubleDamage, new UtilityFeature("Double Plant Damage", UtilityType.DoubleDamage, KeyCode.F6)},
			{UtilityType.SuperDamage, new UtilityFeature("10x Plant Damage", UtilityType.SuperDamage, KeyCode.F7)},
			{UtilityType.StopZombieSpawn, new UtilityFeature("Stop Zombie Spawn", UtilityType.StopZombieSpawn, KeyCode.F8)},
			{UtilityType.StopGameOver, new UtilityFeature("Stop Game Over", UtilityType.StopGameOver, KeyCode.F9)},
			{UtilityType.PlantEverywhere, new UtilityFeature("Plant Everywhere", UtilityType.PlantEverywhere, KeyCode.F10)},
			{UtilityType.DeveloperMode, new UtilityFeature("Developer Mode", UtilityType.DeveloperMode, KeyCode.F11)},

			{UtilityType.ColumnPlants, new UtilityFeature("Column Plants", UtilityType.ColumnPlants, KeyCode.Semicolon)},
			{UtilityType.ScaredyDream, new UtilityFeature("Scaredy Dream", UtilityType.ScaredyDream, KeyCode.Quote)},
			{UtilityType.SeedRain, new UtilityFeature("Seed Rain", UtilityType.SeedRain, KeyCode.Backslash)},

			{UtilityType.GenerateTrophy, new UtilityFeature("Generate Trophy", UtilityType.GenerateTrophy, KeyCode.Keypad0)},
			{UtilityType.GenerateFertilizer, new UtilityFeature("Generate Fertilizer", UtilityType.GenerateFertilizer, KeyCode.Keypad1)},
			{UtilityType.GenerateBucket, new UtilityFeature("Generate Bucket", UtilityType.GenerateBucket, KeyCode.Keypad2)},
			{UtilityType.GenerateHelmet, new UtilityFeature("Generate Helmet", UtilityType.GenerateHelmet, KeyCode.Keypad3)},
			{UtilityType.GenerateJack, new UtilityFeature("Generate Jack-in-the-Box", UtilityType.GenerateJack, KeyCode.Keypad4)},
			{UtilityType.GeneratePickaxe, new UtilityFeature("Generate Pickaxe", UtilityType.GeneratePickaxe, KeyCode.Keypad5)},
			{UtilityType.GenerateMecha, new UtilityFeature("Generate Mecha Fragment", UtilityType.GenerateMecha, KeyCode.Keypad6)},
			{UtilityType.GenerateMeteor, new UtilityFeature("Generate Meteor", UtilityType.GenerateMeteor, KeyCode.Keypad7)},
			{UtilityType.CharmAll, new UtilityFeature("Charm All Zombies", UtilityType.CharmAll, KeyCode.Keypad8)},
			{UtilityType.KillAllZombies, new UtilityFeature("Kill All Zombies", UtilityType.KillAllZombies, KeyCode.Keypad9)},
			{UtilityType.KillAllPlants, new UtilityFeature("Kill All Plants", UtilityType.KillAllPlants, KeyCode.KeypadPlus)},

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
		public static void SpawnItem(string resourcePath)
		{
			GameObject gameObject = Resources.Load<GameObject>(resourcePath);
			if (gameObject != null)
			{
				UnityEngine.Object.Instantiate<GameObject>(gameObject, new Vector2(0f, 0f), Quaternion.identity, GameAPP.board.transform);
				return;
			}
		}
	}
}
