using Il2Cpp;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(Plant_and_Zombie_Spawner.Core), "Plant and Zombie Spawner", "221.0.0", "dynaslash", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace Plant_and_Zombie_Spawner
{
    public class Core : MelonMod
    {
        private static MelonPreferences_Entry<bool> configMindControl;
        public MelonPreferences_Entry<KeyCode> plantKeybind;
        public MelonPreferences_Entry<KeyCode> zombieKeybind;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Plant and Zombie Spawner is loaded!");

            var category = MelonPreferences.CreateCategory("Plant_and_Zombie_Spawner");
            configMindControl = category.CreateEntry("MindControlled", true);
            plantKeybind = category.CreateEntry("PlantKeybind", KeyCode.C);
            zombieKeybind = category.CreateEntry("ZombieKeybind", KeyCode.B);

        }
        public override void OnUpdate()
        {
            if (Board.Instance != null)
            {
                if (Input.GetKeyDown(plantKeybind.Value))
                {
                    if (Patch.GetSeedType.SeedType != -1)
                    {
                        CreatePlant.Instance.SetPlant(
                            Mouse.Instance.theMouseColumn,
                            Mouse.Instance.theMouseRow,
                            (Il2Cpp.PlantType)Patch.GetSeedType.SeedType, // Force to PlantType
                            null,
                            default(Vector2),
                            false,
                            true
                        );
                    }
                }
                if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
                {
                    configMindControl.Value = !configMindControl.Value;
                }
                if (Input.GetKeyDown(zombieKeybind.Value))
                {
                    if (Patch.GetZombieType.ZombieType != -1)
                    {
                        if (configMindControl.Value)
                        {
                            CreateZombie.Instance.SetZombieWithMindControl(
                                Mouse.Instance.theMouseRow,
                                (Il2Cpp.ZombieType)Patch.GetZombieType.ZombieType, // Force to ZombieType
                                Mouse.Instance.mouseX,
                                false
                            );
                        }
                        else
                        {
                            CreateZombie.Instance.SetZombie(
                                Mouse.Instance.theMouseRow,
                                (Il2Cpp.ZombieType)Patch.GetZombieType.ZombieType, // Force to ZombieType
                                Mouse.Instance.mouseX,
                                false
                            );
                        }
                    }
                }
            }
        }

        //		public override void OnUpdate()
        //		{
        //			if (Board.Instance != null)
        //			{
        //				if (Input.GetKeyDown(plantKeybind.Value))
        //				{
        //					if (Patch.GetSeedType.SeedType != -1)
        //					{
        //						CreatePlant.Instance.SetPlant(Mouse.Instance.theMouseColumn, Mouse.Instance.theMouseRow, Patch.GetSeedType.SeedType, null, default(Vector2), false, true);
        //					}
        //				}
        //				if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        //				{
        //					configMindControl.Value = !configMindControl.Value;
        //				}
        //				if (Input.GetKeyDown(zombieKeybind.Value))
        //				{
        //					if (Patch.GetZombieType.ZombieType != -1)
        //					{
        //						if (configMindControl.Value)
        //						{
        //							CreateZombie.Instance.SetZombieWithMindControl(Mouse.Instance.theMouseRow, Patch.GetZombieType.ZombieType, Mouse.Instance.mouseX, false);
        //						}
        //						else
        //						{
        //							CreateZombie.Instance.SetZombie(Mouse.Instance.theMouseRow, Patch.GetZombieType.ZombieType, Mouse.Instance.mouseX, false);
        //						}
        //					}
        //				}
        //			}
        //		}
    }
}