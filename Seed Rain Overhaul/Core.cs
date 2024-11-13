using Il2Cpp;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(Seed_Rain_Overhaul.Core), "Seed Rain Overhaul", "1.0.0", "dynaslash", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace Seed_Rain_Overhaul
{
    public class Core : MelonMod
    {
		public static Core instance;

        public static string[] plantNames = new string[]
        {
            "Peashooter", "Sunflower", "Cherry Bomb", "Wall-nut", "Potato Mine", "Chomper", "Puff-shroom", "Fume-shroom", "Hypno-shroom", "Scaredy-shroom",
			"Ice-shroom", "Doom-shroom", "Lily Pad", "Squash", "Threepeater", "Tangle Kelp", "Jalapeno", "Spikeweed", "Torchwood", "Sea-shroom",
			"Plantern", "Cactus", "Blover", "Starfruit", "Pumpkin", "Magnet-shroom", "Cabbage-pult", "Flower Pot", "Kernel-pult", "Garlic", "Umbrella Leaf", "Marigold", "Melon-pult", "Swordmaster Starfruit", "Zombie Giftbox", "Giant-nut Spawn", "Cattail Girl",
			"Barley", "Endoflame", "Giant-nut", "Plant Giftbox", "Empress-shroom", "Gatling Cherrybomber", "Squashed Infernowood", "Cherrizilla", "Doominator-shroom", "Twin Solar-nut",
			"Spikesidian", "Gatling Doom", "Gatling Icicle-shroom", "Magnetar", "Oblivion-shroom", "Laser Pumpkin", "Laser Drones", "Obsidian-nut", "Wither-pult", "Cob-livion Cannon", "Midas Umbrella", "Pea Sunflower", "Cherryshooter",
			"Solar Bomb", "Cherry-nut", "Pea-nut", "Explod-o-shooter", "Solar-nut", "Pea-mine", "Cherrypeater", "Solar Mine", "Mine-nut", "Chomp-shooter",
			"Chomp-nut", "Chewzilla", "Solar Chomper", "Chomp-mine", "Cherry Chomper", "Gatling Cherry", "Pea-shroom", "Repeat-shroom", "Buckshooter", "Nut-shroom",
			"Mini Hypno-shroom", "Perfume-shroom", "Trippy-shroom", "Gutsy-shroom", "Charm-shroom", "Tall-nut", "Rugby-nut", "Buck-nut", "Repeater", "Sun-shroom",
			"Gatling Pea", "Twin Sunflower", "Snow Pea", "Icicle-shroom", "Mini Ice-shroom", "Frost-shroom", "Shivery-shroom", "Frost-nut", "Cryodoom-shroom", "Frostveil-shroom",
			"Frenzy-shroom", "Soot-shroom", "Mini Doom-shroom", "Curse-shroom", "Doomberg-shroom", "Squash-spreader", "Spicy Kelp", "Mangle Kelp", "Kelp-spreader", "Infernowood",
			"Scorchwood", "Spicy Squash", "Torchthree", "Torch-kelp", "Golden Squash", "Scorched Threepeater", "Squashed Torchwood", "Spikerock", "Torchweed", "Scorchweed",
			"Squashweed", "Weed-spreader", "Gatling Pea-shroom", "Krakerberus", "Cattail", "Frost Cattail", "Flame Cattail", "Gloom-shroom", "Embergloom-shroom", "Cryogloom-shroom",
			"Scorch-nut", "Spikecicle", "Spikinferno", "Sea Thorn-shroom", "Sea Sun-shroom", "Sea Lamp-shroom", "Lumicactus", "Glowver", "Starglow", "Thornball",
			"Sea Star", "Starblover", "Starthorn", "Sea Blow-shroom", "Jack O' Lantern", "Thorngourd", "Stargourd", "Split Pea", "Windgourd", "Pumpkin Lure",
			"Starlure", "Starjoker", "Starpick", "Starsteel", "Iron Pumpkin", "Joker Pumpkin", "Miner Pumpkin", "Magled-shroom", "Sea Magne-shroom", "Magblover",
			"Magnethorn", "Stardrop", "Snow Repeater", "Gatling Snow", "Split Snow", "Split Cherry", "Sniper Pea", "Bloverthorn Pumpkin", 
			"Solar-pult", "Cabbage Pot", "Taco-pult", "Kernel-pot", "Umbrella Husk", "Winter Melon", "Clove-pult", "Garbage-pult", 
			"Garlic-pult", "Cob Cannon", "Corn-pult", "Pyro Cannon", "Cryo Cannon", "Stuffed Melon", "Melon Pot", "Salad-pult", 
			"Umbrella Clove", "Umbrella Kale", "Mecha-nut", "Garlic Pot", "Umbrella Rind", "Melon Cannon", "Umbrella Pot", 
			"Silver Cabbage", "Golden Cabbage", "Silve Pot", "Golden Pot", "Silver Kernel", "Golden Kernel", "Twin Marigold",
			"Silver Melon", "Golden Melon", "Silver Umbrella", "Golden Umbrella", "Silver Garlic", "Golden Garlic", "Hypno-nut", "Alchemist Umbrella"
        };

		public static int[] plantIndexes = new int[]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
			11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
			21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
			31, 32, 249, 250, 251,
			252, 253, 254, 255, 256, 257, 900, 901, 902, 903,
			904, 905, 906, 907, 908, 909, 910, 911, 912, 913,
			914, 915, 916,
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
            1140, 1141, 1142, 1143, 1144, 1145, 1146, 1147, 1148
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
			var mainCategory = MelonPreferences.CreateCategory("Seed Rain Overhaul");
			var enabledPlants = MelonPreferences.CreateCategory("Seed Rain Overhaul - Enabled Plants");

			configEnable = mainCategory.CreateEntry("Enable", true);
			configMode = mainCategory.CreateEntry("Mode", 1, "0 - Default, 1 - Custom seed rain drop cards, 2 - Default drop cards but add Odyssey plants, 3 - Only Odyssey plants, 4 - All plants, 5 - All plants but remove aquatic plants.");
			
			boolArrayConfig = new MelonPreferences_Entry<bool>[plantNames.Length];

			for (int i = 0 ; i < plantNames.Length ; i++)
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