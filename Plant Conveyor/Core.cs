using Il2Cpp;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(Plant_Conveyor.Core), "Plant Conveyor", "1.0.0", "dynaslash", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace Plant_Conveyor
{
	public class Core : MelonMod
	{
		private static int cardid = 0;
		private static readonly int[] plantIndexes = new int[]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
			10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
			20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
			30, 31, 32, 249, 250,
			251, 252, 253, 254, 255, 256, 257, 
			900, 901, 902, 903, 904, 905, 906, 907, 908, 909, 
			910, 911, 912, 913, 914, 915, 916,
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

		public override void OnInitializeMelon()
		{
			MelonLogger.Msg("Plant Conveyor is loaded!");

			// Load custom plant prefabs
			GameAPP.plantPrefab[257] = Resources.Load<GameObject>("Plants/Peashooter/Electricpea/Electricpeaprefab");
			GameAPP.prePlantPrefab[257] = Resources.Load<GameObject>("Plants/Peashooter/Electricpea/Electricpeapreview");
			GameAPP.prePlantPrefab[255] = Resources.Load<GameObject>("Plants/WallNut/WallNutPreview");
			GameAPP.prePlantPrefab[251] = Resources.Load<GameObject>("Plants/_Mixer/SunNut/SunNutPreview");
		}

		public override void OnUpdate()
		{
			if (Board.Instance != null)
			{
				int num = 0;

				if (Input.GetKeyDown(KeyCode.K))
				{
					num--;
				}
					
				if (Input.GetKeyDown(KeyCode.L))
				{
					num++;
				}

				if (num != 0)
				{
					cardid += num;
					for (int i = 0; i < InGameUIMgr.Instance.cardOnBank.Length; i++)
					{
						if (InGameUIMgr.Instance.cardOnBank[i] == null) 
						{
							break;
						}

						int newCardIndex = (cardid + i) % plantIndexes.Length;
						if (newCardIndex < 0)
						{
							newCardIndex += plantIndexes.Length;
						}

						CardUI component = InGameUIMgr.Instance.cardOnBank[i].GetComponent<CardUI>();
						Mouse.Instance.ChangeCardSprite((MixData.PlantType)plantIndexes[newCardIndex], component);
						component.theSeedType = plantIndexes[newCardIndex];
						component.theSeedCost = 0;
					}
				}
			}
		}
	}
}