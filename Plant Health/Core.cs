using Il2Cpp;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(Plant_Health.Core), "Plant Health", "1.0.0", "dynaslash", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace Plant_Health
{
	public class Core : MelonMod
	{
		private static GUIStyle guiStyle;
		private Camera mainCamera;
		public static bool opendrawbl = false;
		public static readonly List<ValueTuple<Plant, Transform>> plant_Shadow = new List<ValueTuple<Plant, Transform>>();

		public override void OnInitializeMelon()
		{
			// Set up initial styles and camera
			guiStyle = new GUIStyle
			{
				fontSize = 20,
				normal = { textColor = Color.green },
				alignment = TextAnchor.MiddleCenter
			};

			MelonLogger.Msg("Plant Health is loaded!");
		}

		public override void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
			if (Camera.main != null)
			{
				mainCamera = Camera.main;
			}
		}

		public override void OnUpdate()
		{
			if (Input.GetKeyDown(KeyCode.F9))
			{
				opendrawbl = !opendrawbl;
				MelonLogger.Msg($"Drawing toggled to {opendrawbl}");
			}
		}

		public override void OnGUI()
		{
			// Adjust font size relative to screen height
			if (mainCamera == null) return;

			guiStyle.fontSize = (int)((float)Screen.height / 42.75f * 5f / mainCamera.orthographicSize);

			if (plant_Shadow.Count == 0 || plant_Shadow != new List<ValueTuple<Plant, Transform>>())
			{
				plant_Shadow.Clear();
				plant_Shadow.AddRange(from o in GameObject.FindGameObjectsWithTag("Plant")
									  select new ValueTuple<Plant, Transform>(o.GetComponent<Plant>(), o.transform.Find("Shadow")));
			}

			if (opendrawbl && plant_Shadow.Count != 0)
			{
				if (GameAPP.theGameStatus == 0 || GameAPP.theGameStatus == 2 || GameAPP.theGameStatus == 3)
				{
					foreach (ValueTuple<Plant, Transform> valueTuple in plant_Shadow)
					{
						Plant item = valueTuple.Item1;
						Transform item2 = valueTuple.Item2;

						try
						{
							Vector3 position = item2.transform.position;
							Vector3 screenPosition = mainCamera.WorldToScreenPoint(position);
							screenPosition.y = Screen.height - screenPosition.y;

							Rect rect = GetLabelPosition(item, screenPosition);
							GUI.Label(rect, "Health: " + item.thePlantHealth.ToString(), guiStyle);
						}
						catch (Exception ex)
						{
							MelonLogger.Error($"Error in OnGUI drawing: {ex}");
						}
					}
				}
			}
		}

		private Rect GetLabelPosition(Plant plant, Vector3 screenPosition)
		{
			float xOffset = (float)Screen.height / 22.5f;
			float yOffset = 0f;

			if (plant.thePlantType == 12)
			{
				yOffset = 0.5f * guiStyle.fontSize;
			}
			else if (plant.plantTag.potPlant || plant.plantTag.pumpkinPlant)
			{
				yOffset = 0f;
			}
			else if (plant.plantTag.puffPlant)
			{
				yOffset = plant.puffPlace switch
				{
					0 => -guiStyle.fontSize,
					1 => -2 * guiStyle.fontSize,
					2 => -guiStyle.fontSize,
					_ => -guiStyle.fontSize
				};
			}
			else if (plant.plantTag.flyingPlant)
			{
				yOffset = -4 * guiStyle.fontSize;
			}
			else
			{
				yOffset = -2 * guiStyle.fontSize;
			}

			return new Rect(screenPosition.x - xOffset, screenPosition.y + yOffset, 100f, 30f);
		}
	}
}