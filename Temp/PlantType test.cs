public static string[] plantNames = new string[]
{
    "Peashooter", "Sunflower", "Cherry_Bomb", "Wall-nut", "Potato_Mine", "Chomper", "Small_Puff-shroom", "Fume-shroom", 
    "Hypno-shroom", "Scaredy-shroom", "Ice-shroom", "Doom-shroom", "Lily_Pad", "Squash", "Threepeater", "Tangle_Kelp",
    "Jalapeno", "Spikeweed", "Torchwood", "Sea-shroom", "Plantern", "Cactus", "Blover", "Starfruit", "Pumpkin",
    "Magnet-shroom", "Cabbage-pult", "Flower_Pot", "Kernel-pult", "Garlic", "Umbrella_Leaf", "Marigold", "Melon-pult",
    "Shulkflower", "Prismflower",
    
    "EndoFlame_Girl", "Hamburger", "Mix_Bomb", "Imitater", "Magnet_Box", "Magnet_Interface", "Squalour", "Sword_Star",
    "Present_Zombie", "Big_Sun-nut", "Cattail_Girl", "Wheat", "EndoFlame", "Big_Wall-nut", "Present", "Pit", "Refresh",
    
    "Hypno_Emperor", "Ultimate_Gatling", "Ultimate_Torch", "Ultimate_Chomper", "Ultimate_Fume", "Super_Sun-nut",
    "Obsidian_Spike", "Doom_Gatling", "Snow_Gatling-puff", "Ultimate_Star", "Ultimate_Gloom", "Final_Pumpkin",
    "Ultimate_Fly", "Ultimate_Tall-nut", "Ultimate_Melon", "Ultimate_Cannon", "Emerald_Umbrella", "Hypno_Queen",
    "Ash_Threepeater", "Super_Threepeater", "Ultimate_Blover", "Garlic_Ultimate-chomper", "Cherry_Ultimate-pumpkin",
    "Red_Emerald-umbrella", "Ultimate_Hypno", "Ultimate_Potato-nut", "Cattail_Lour", "Ultimate_Big-gatling",
    "Super_Hypno-doom", "Sun_Gatling-puff", "Gatling_Doom-scaredy", "Obsidian_Wall-nut",
    
    "Pea_Sunflower", "Cherryshooter", "Sun_Bomb", "Cherry-nut", "Pea-nut", "Super_Cherry-shooter", "Sun-nut", 
    "Pea_Mine", "Double_Cherry", "Sun_Mine", "Potato-nut", "Pea_Chomper", "Nut_Chomper", "Super_Chomper", 
    "Sun_Chomper", "Potato_Chomper", "Cherry_Chomper", "Cherry_Gatling", "Pea_Puff", "Double_Puff", "Iron_Pea",
    "Puff-nut", "Hypno_Puff", "Hypno_Fume", "Scaredy_Hypno", "Scaredy_Fume", "Super_Hypno", "Tall-nut", 
    "Tall-nut_Football", "Iron-nut", "Double_Shooter", "Sun-shroom", "Gatling_Pea", "Twin_Flower", "Snow_Pea-shooter",
    "Ice_Puff", "Small_Ice-shroom", "Ice_Fume-shroom", "Ice_Scaredy-shroom", "Tall_Ice-nut", "Ice_Doom", "Ice_Hypno",
    "Scaredy_Doom", "Doom_Fume", "Puff_Doom", "Hypno_Doom", "Super_Fume", "Three_Squash", "Elite_Torchwood",
    "Jala_Kelp", "Squash_Kelp", "Three_Kelp", "Super_Torch", "Jala_Torch", "Jala_Squash", "Three_Torch", "Kelp_Torch",
    "Fire_Squash", "Dark_Threepeater", "Squash_Torch", "Spike_Rock", "Torch_Spike", "Jala_Spike", "Squash_Spike",
    "Three_Spike", "Gatling_Puff", "Super_Kelp", "Cattail_Plant", "Ice_Cattail", "Fire_Cattail", "Gloom-shroom",
    "Fire_Gloom", "Ice_Gloom", "Tall_Fire-nut", "Ice_Spike-rock", "Fire_Spike-rock", "Sea_Cactus", "Sea_Sun-shroom",
    "Sea_Lantern", "Lantern_Cactus", "Lantern_Blover", "Lantern_Star", "Cactus_Blover", "Sea_Starfruit", 
    "Star_Blover", "Cacstus_Star", "Sea_Blover", "Lantern_Pumpkin", "Cactus_Pumpkin", "Star_Pumpkin", "Split_Pea",
    "Blower_Pumpkin", "Magnet_Pumpkin", "Magnet_Star", "Jackbox_Star", "Pickaxe_Star", "Iron_Star", "Iron_Pumpkin",
    "Jackbox_Pumpkin", "Pickaxe_Pumpkin", "Lantern_Magnet", "Sea_Magnet", "Magnet_Blover", "Magnet_Cactus",
    "Super_Star", "Double_Snow", "Snow_Gatling", "Snow_Split", "Cherry_Split", "Sniper_Pea", "Super_Pumpkin",
    "Sun_Cabbage", "Cabbage_Pot", "Corn_Cabbage", "Corn_Pot", "Corn_Umbrella", "Winter_Melon", "Garlic_Corn",
    "Garlic_Cabbage", "Garlic_Melon", "Cob_Cannon", "Corn_Melon", "Fire_Cannon", "Ice_Cannon", "Cabbage_Melon",
    "Melon_Pot", "Super_Melon", "Garlic_Umbrella", "Cabbage_Umbrella", "Machine_Nut", "Garlic_Pot", "Melon_Umbrella",
    "Melon_Cannon", "Umbrella_Pot", "Silver_Cabbage", "Gold_Cabbage", "Silver_Pot", "Gold_Pot", "Silver_Corn",
    "Gold_Corn", "Twin_Marigold", "Silver_Melon", "Gold_Melon", "Silver_Umbrella", "Gold_Umbrella", "Silver_Garlic",
    "Gold_Garlic", "Hypno-nut", "Super_Umbrella", "Fire_Melon", "Gold_Magnet", "Super_Machine-nut", "Iron_Puff",
    "Split_Puff", "Sun_Magnet", "Fire_Caltrop", "Fire_Kelp", "Hypno_Magnet", "Magnet_Fume", "Iron_Fume",
    "Helmet_Fume", "Big_Gatling", "Cherry_Mine", "Jala_Mine", "Cherry_Pumpkin", "Super_Snow-gatling", "Cherry_Magnet",
    "Fire_Sniper", "Super_Gatling", "Big_Pumpkin", "Potato_Puff", "Scaredy_Potato", "Garlic_Fume", "Obsidian_Jalapeno",
    "Big_Chomper", "Super_Sea-shroom", "Hypno_Blover", "Twin_Shulk", "Cherry_Torch", "Cherry_Jalapeno", "Ice_Caltrop",
    "Garlic_Blover"
};

public static int[] plantIndexes = new int[]
{
    0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
    31, 32, 33, 34,
    
    242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 257, 258,
    
    900, 901, 902, 903, 904, 905, 906, 907, 908, 909, 910, 911, 912, 913, 914, 915, 916, 917, 918, 919, 920, 921, 922,
    923, 924, 925, 926, 927, 928, 929, 930, 931,
    
    1000, 1001, 1002, 1003, 1004, 1005, 1006, 1007, 1008, 1009, 1010, 1011, 1012, 1013, 1014, 1015, 1016, 1017, 1018,
    1019, 1020, 1021, 1022, 1023, 1024, 1025, 1026, 1027, 1028, 1029, 1030, 1031, 1032, 1033, 1034, 1035, 1036, 1037,
    1038, 1039, 1040, 1041, 1042, 1043, 1044, 1045, 1046, 1047, 1048, 1049, 1050, 1051, 1052, 1053, 1054, 1055, 1056,
    1057, 1058, 1059, 1060, 1061, 1062, 1063, 1064, 1065, 1066, 1067, 1068, 1069, 1070, 1071, 1072, 1073, 1074, 1075,
    1076, 1077, 1078, 1079, 1080, 1081, 1082, 1083, 1084, 1085, 1086, 1087, 1088, 1089, 1090, 1091, 1092, 1093, 1094,
    1095, 1096, 1097, 1098, 1099, 1100, 1101, 1102, 1103, 1104, 1105, 1106, 1107, 1108, 1109, 1110, 1111, 1112, 1113,
    1114, 1115, 1116, 1117, 1118, 1119, 1120, 1121, 1122, 1123, 1124, 1125, 1126, 1127, 1128, 1129, 1130, 1131, 1132,
    1133, 1134, 1135, 1136, 1137, 1138, 1139, 1140, 1141, 1142, 1143, 1144, 1145, 1146, 1147, 1148, 1149, 1150, 1151,
    1152, 1153, 1154, 1155, 1156, 1157, 1158, 1159, 1160, 1161, 1162, 1163, 1164, 1165, 1166, 1167, 1168, 1169, 1170,
    1171, 1172, 1173, 1174, 1175, 1176, 1177, 1178, 1179, 1180, 1181
};
