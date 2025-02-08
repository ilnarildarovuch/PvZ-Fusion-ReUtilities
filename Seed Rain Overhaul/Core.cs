using Il2Cpp;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(Seed_Rain_Overhaul.Core), "Seed Rain Overhaul", "221.0.0", "dynaslash & TuanAnh2901", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace Seed_Rain_Overhaul
{
	public class Core : MelonMod
	{
		public static Core instance;

		public static string[] plantNames = new string[]
		{
			"Peashooter", "Sunflower", "Cherry_Bomb", "Wall-nut", "Potato_Mine", "Chomper", "Small_Puff-shroom", "Fume-shroom",
			"Hypno-shroom", "Scaredy-shroom", "Ice-shroom", "Doom-shroom", "Lily_Pad", "Squash", "Threepeater", "Tangle_Kelp",
			"Jalapeno", "Spikeweed", "Torchwood", "Sea-shroom", "Plantern", "Cactus", "Blover", "Starfruit", "Pumpkin",
			"Magnet-shroom", "Cabbage-pult", "Flower_Pot", "Kernel-pult", "Garlic", "Umbrella_Leaf", "Marigold", "Melon-pult",
			"Bladegrass",

			"Queen_Endoflame", "Burger_Blaster", "Jicamagic", "Imitater", "Neko_Squash", "Swordmaster_Starfruit",
			"Zombie_Giftbox", "Giant-nut_Spawn", "Cattail_Girl", "Barley", "Endoflame", "Giant-nut", "Plant_Giftbox",

			"Empress-shroom", "Gatling_Cherrybomber", "Squashed_Infernowood", "Cherrizilla", "Doominator-shroom", "Twin_Solar-nut",
			"Spikesidian", "Gatling_Doom", "Gatling_Icicle-shroom", "Magnetar", "Oblivion-shroom", "Laser_Pumpkin", "Laser_Drones", 
			"Obsidian_Tall-nut", "Wither-pult", "Cob-livion_Cannon", "Midas_Umbrella", "Enchantress-shroom", "Ashen_Threepeater", 
			"Phoenix_Threepeater", "Gravitron", "Garlizilla", "Blast_Pumpkin", "Aegis_Umbrella", "Charmatron-shroom", 
			"Obsidian_Mine", "Magicat_Nettle", "Apeacalypse_Turret", "Calamity-shroom", "Gatling_Sunburst-shroom", 
			"Gatling_Frenzy-shroom", "Obsidian_Wall-nut",

			"Pea_Sunflower", "Cherryshooter", "Solar_Bomb", "Cherry-nut", "Pea-nut", "Explod-o-shooter", "Solar-nut", "Pea-mine", 
			"Cherrypeater", "Solar_Mine", "Mine-nut", "Chomp-shooter", "Chomp-nut", "Chewzilla", "Solar_Chomper", "Chomp-mine", 
			"Cherry_Chomper", "Gatling_Cherry", "Pea-shroom", "Repeat-shroom", "Buckshooter", "Nut-shroom", "Mini_Hypno-shroom", 
			"Perfume-shroom", "Trippy-shroom", "Gutsy-shroom", "Charm-shroom", "Tall-nut", "Rugby-nut", "Buck-nut", "Repeater", 
			"Sun-shroom", "Gatling_Pea", "Twin_Sunflower", "Snow_Pea", "Icicle-shroom", "Mini_Ice-shroom", "Frost-shroom", 
			"Shivery-shroom", "Frost-nut", "Cryodoom-shroom", "Frostveil-shroom", "Frenzy-shroom", "Soot-shroom", "Mini_Doom-shroom", 
			"Curse-shroom", "Doomberg-shroom", "Squash-spreader", "Spicy_Kelp", "Mangle_Kelp", "Kelp-spreader", "Infernowood", 
			"Scorchwood", "Spicy_Squash", "Torchthree", "Torch-kelp", "Golden_Squash", "Scorched_Threepeater", "Squashed_Torchwood", 
			"Spikerock", "Torchweed", "Scorchweed", "Squashweed", "Weed-spreader", "Gatling_Pea-shroom", "Krakerberus", "Cattail", 
			"Frost_Cattail", "Flame_Cattail", "Gloom-shroom", "Embergloom-shroom", "Cryogloom-shroom", "Scorch-nut", "Spikecicle", 
			"Spikinferno", "Sea_Thorn-shroom", "Sea_Sun-shroom", "Sea_Lamp-shroom", "Lumicactus", "Glowver", "Starglow", "Thornball",
			"Sea_Star", "Starblover", "Starthorn", "Sea_Blow-shroom", "Jack_O_Lantern", "Thorngourd", "Stargourd", "Split_Pea", 
			"Windgourd", "Pumpkin_Lure", "Starlure", "Starjoker", "Starpick", "Starsteel", "Iron_Pumpkin", "Joker_Pumpkin", 
			"Miner_Pumpkin", "Magled-shroom", "Sea_Magne-shroom", "Magblover", "Magnethorn", "Stardrop", "Snow_Repeater", 
			"Gatling_Snow", "Split_Snow", "Split_Cherry", "Sniper_Pea", "Bloverthorn_Pumpkin", "Solar-pult", "Cabbage_Pot", 
			"Taco-pult", "Kernel-pot", "Umbrella_Husk", "Winter_Melon", "Clove-pult", "Garbage-pult", "Garlic-pult", "Cob_Cannon", 
			"Corn-pult", "Pyro_Cannon", "Cryo_Cannon", "Stuffed_Melon", "Melon_Pot", "Salad-pult", "Umbrella_Clove", "Umbrella_Kale", 
			"Mecha-nut", "Garlic_Pot", "Umbrella_Rind", "Melon_Cannon", "Umbrella_Pot", "Silver_Cabbage", "Golden_Cabbage", 
			"Silver_Pot", "Golden_Pot", "Silver_Kernel", "Golden_Kernel", "Twin_Marigold", "Silver_Melon", "Golden_Melon", 
			"Silver_Umbrella", "Golden_Umbrella", "Silver_Garlic", "Golden_Garlic", "Hypno-nut", "Alchemist_Umbrella", "Summer_Melon", 
			"Gold_Magnet", "Giga_Mecha-nut", "Buck-shroom", "Split_Pea-shroom", "Solar_Magnet", "Golden_Spikeweed", "Golden_Kelp", 
			"Charm_Magnet", "Lure-shroom", "Rust-shroom", "Rugby-shroom", "Titan_Pea_Turret", "Explod-o-tato_Mine", "Chili_Mine", 
			"PumpKaboom", "Snow_Commando", "Volatile_Magnet", "Blazer_Pea", "Pea_Commando", "Titan_Pumpkin_Bunker", "Nugget-shroom", 
			"Spuddy-shroom", "Foul-shroom", "Frostburnt_Jalapeno", "Titan_Chomper_Maw", "Hypnoblover", "Twin_Bladegrass", 
			"Boomwood", "Bamboom", "Frostweed", "Foul_Blover"
		};

		public static int[] plantIndexes = new int[]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 
			10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 
			20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 
			30, 31, 32, 33,

			242, 243, 244, 245, 248, 249, 250, 
			251, 252, 253, 254, 255, 256, 257, 258,

			900, 901, 902, 903, 904, 905, 906, 907, 908, 909, 
			910, 911, 912, 913, 914, 915, 916, 917, 918, 919, 
			920, 921, 922, 923, 924, 925, 926, 927, 928, 929, 
			930, 931,

			1000, 1001, 1002, 1003, 1004, 1005, 1006, 1007, 1008, 1009, 
			1010, 1011, 1012, 1013, 1014, 1015, 1016, 1017, 1018, 1019, 
			1020, 1021, 1022, 1023, 1024, 1025, 1026, 1027, 1028, 1029, 
			1030, 1031, 1032, 1033, 1034, 1035, 1036, 1037, 1038, 1039, 
			1040, 1041, 1042, 1043, 1044, 1045, 1046, 1047, 1048, 1049, 
			1050, 1051, 1052, 1053, 1054, 1055, 1056, 1057, 1058, 1059, 
			1060, 1061, 1062, 1063, 1064, 1065, 1066, 1067, 1068, 1069, 
			1070, 1071, 1072, 1073, 1074, 1075, 1076, 1077, 1078, 1079, 
			1080, 1081, 1082, 1083, 1084, 1085, 1086, 1087, 1088, 1089, 
			1090, 1091, 1092, 1093, 1094, 1095, 1096, 1097, 1098, 1099, 
			1100, 1101, 1102, 1103, 1104, 1105, 1106, 1107, 1108, 1109, 
			1110, 1111, 1112, 1113, 1114, 1115, 1116, 1117, 1118, 1119, 
			1120, 1121, 1122, 1123, 1124, 1125, 1126, 1127, 1128, 1129, 
			1130, 1131, 1132, 1133, 1134, 1135, 1136, 1137, 1138, 1139, 
			1140, 1141, 1142, 1143, 1144, 1145, 1146, 1147, 1148, 1149, 
			1150, 1151, 1152, 1153, 1154, 1155, 1156, 1157, 1158, 1159, 
			1160, 1161, 1162, 1163, 1164, 1165, 1166, 1167, 1168, 1169, 
			1170, 1171, 1172, 1173, 1174, 1176, 1177, 1178, 1179, 
			1180, 1181
		};


		public MelonPreferences_Entry<bool> configEnable;
		public MelonPreferences_Entry<int> configMode;
		public MelonPreferences_Entry<bool>[] boolArrayConfig;

		public override void OnInitializeMelon()
		{
			MelonLogger.Msg("Seed Rain Overhaul is loaded!");

			instance = this;
			LoadConfig();
		}

		private void LoadConfig()
		{
			var mainCategory = MelonPreferences.CreateCategory("Seed Rain Overhaul", " ");
			var enabledPlants = MelonPreferences.CreateCategory("Seed Rain Overhaul - Custom Enabled Plants");

			configEnable = mainCategory.CreateEntry("Enable", true);
			configMode = mainCategory.CreateEntry("Mode", 1, "0 - Default, 1 - Custom Seed Rain, 2 - Default but add Odyssey plants, 3 - Only Odyssey Plants, 4 - All Plants, 5 - All Plants but remove Aquatic Plants.");

			boolArrayConfig = new MelonPreferences_Entry<bool>[plantNames.Length];

			for (int i = 0; i < plantNames.Length; i++)
			{
				boolArrayConfig[i] = enabledPlants.CreateEntry(plantNames[i], true);
			}

			GameAPP.prePlantPrefab[255] = Resources.Load<GameObject>("Plants/WallNut/WallNutPreview");
			GameAPP.prePlantPrefab[251] = Resources.Load<GameObject>("Plants/_Mixer/SunNut/SunNutPreview");

			MelonPreferences.Save();
		}
		public void ReloadConfig()
		{
			MelonPreferences.Load();
		}
	}
}