using MelonLoader;

[assembly: MelonInfo(typeof(Odyssey_Buffs.Core), "Odyssey Buffs", "1.0.0", "dynaslash", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace Odyssey_Buffs
{
	public class Core : MelonMod
	{
		public static Core instance;
		
		// Configuration entries
		public MelonPreferences_Entry<bool> configEnablePlant;
		public MelonPreferences_Entry<bool> configEnableEntries;
		public MelonPreferences_Entry<bool> configEnableTravel;
		public MelonPreferences_Entry<bool> configEnableIZ;
		
		public MelonPreferences_Entry<bool>[] boolArrayadvancedConfig;
		public MelonPreferences_Entry<bool>[] boolArrayultimateConfig;

		public static string[] advancedUpgradesKeys = new string[]
		{
			"Empress shroom Summon Interval 10 Seconds", "Empress shroom Can now summon Ultimate Zombies", 
			"Gatling Doom No more rest between every attack", "Gatling Doom Can now fire bullets of doom between every attack", 
			"Twin Solar nut Can now restore to full health instead of 1500 each time", "Twin Solar nut Toughness reduces by 10 for each hit instead of 15", 
			"Spikesidian Damage multiplied by 5", "Spikesidian Gains 95% DMG Reduction", 
			"Each Sun now gives double the value", "Glove's cooldown is halved",
			"Plants' recharge are halved", "Mallet's cooldown is reduced to 1 10", 
			"At the beginning of each round a charmed Michael Zomboni is generated in each row", 
			"Gain 2000 Sun then double your total amount of Sun after", "Ignited peas will now add the Enflamed debuff to zombies", 
			"Enflamed Debuff damage increased to 150%", "Glove no longer has a cooldown", 
			"Gatling Icicle shroom Increased icicles' penetration by 10 times", "Gatling Icicle shroom Increased damage to Frozen zombies by 10 times",
			"Stardrop Meteor Interval is halved", "The Lumos Level for the entire field is set to max", 
			"Laser Pumpkin Wingmen Amount 3", "Laser Pumpkin Can now carry out ground attacks",
			"Midas Umbrella Activates ultimate automatically if zombies are near 7 5 seconds cooldown", 
			"Midas Umbrella Now bounce bullets with other Midas Umbrella"
		};

		public static string[] ultimateUpgradesKeys = new string[]
		{
			"Cherrizilla Can now kill zombies with less than 33 percent health", "Cherrizilla Immunity to all explosions", 
			"Gatling Cherrybomber Bombs now deal 900 DMG", "Gatling Cherrybomber Attack Interval 1 Second", 
			"Doominator shroom Attack Damage 300", "Doominator shroom Increased Freezing speed by 3 times",
			"Squashed Infernowood Bullets required to spawn a Spicy Squash 10", "Squashed Infernowood Now spawns two Spicy Squash at a time", 
			"Magnetar Attack Interval 0 5 Seconds","Magnetar Damage Limiter 120 is removed", 
			"Oblivion shroom Damage on Death 1 000 000", "Oblivion shroom Now absorbs explosion damage and releases it back to the enemies",
			"Wither pult Spread fire is now a 100 percent chance", "Wither pult Melon Pot's traits are now inherited",
			"Cob blivion Cannon Damage multiplied by 3", "Cob blivion Cannon Reload Time reduced to a third of the original",
			"Obsidian nut Recovers 10 000 HP when affected by Ice Mushrooms and recover full HP when affected by Jalapeno",
			"Obsidian nut Can now mitigate the damage directed to the two plants on its left and right"
		};


		public override void OnInitializeMelon()
		{

			MelonLogger.Msg("Odyssey Buffs is loaded!");

			instance = this;
			LoadConfig();
		}

		private void LoadConfig()
		{
			// Create a preference category for your mod
			var mainCategory = MelonPreferences.CreateCategory("Odyssey Buffs - Main");
			var coverageCategory = MelonPreferences.CreateCategory("Odyssey Buffs - Coverage");
			var advancedCategory = MelonPreferences.CreateCategory("Odyssey Buffs - Advanced");
			var ultimateCategory = MelonPreferences.CreateCategory("Odyssey Buffs - Ultimate");

			// Initialize each configuration entry with a default value if it doesn’t already exist
			configEnablePlant = mainCategory.CreateEntry("Enable Odyssey Plants", true);
			configEnableEntries = mainCategory.CreateEntry("Enable Odyssey Buffs", true);
			configEnableTravel = coverageCategory.CreateEntry("Override Odyssey", true);
			configEnableIZ = coverageCategory.CreateEntry("Enable in Other Modes", true);

			// Initialize lists for advanced and ultimate upgrades as needed
			boolArrayadvancedConfig = new MelonPreferences_Entry<bool>[advancedUpgradesKeys.Length];
			for (int i = 0 ; i < advancedUpgradesKeys.Length ; i++)
			{
				boolArrayadvancedConfig[i] = advancedCategory.CreateEntry(advancedUpgradesKeys[i], true);
			}

			boolArrayultimateConfig = new MelonPreferences_Entry<bool>[ultimateUpgradesKeys.Length];
			for (int i = 0 ; i < ultimateUpgradesKeys.Length; i++)
			{
				boolArrayultimateConfig[i] = ultimateCategory.CreateEntry(ultimateUpgradesKeys[i], true);
			}

			MelonPreferences.Save();
		}

		public void ReloadConfig()
		{
			MelonPreferences.Load();
		}
	}
}