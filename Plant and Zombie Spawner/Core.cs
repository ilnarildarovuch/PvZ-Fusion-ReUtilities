using Il2Cpp;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(Plant_and_Zombie_Spawner.Core), "Plant and Zombie Spawner", "1.1.0", "dynaslash", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace Plant_and_Zombie_Spawner
{
    public class Core : MelonMod
    {
        private static MelonPreferences_Entry<bool> configMindControl;
        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Plant and Zombie Spawner is loaded!");
            configMindControl = MelonPreferences.CreateEntry("Plant and Zombie Spawner", "Mind Controlled", true);
        }

        public override void OnUpdate()
        {
            if (Board.Instance != null)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    if (Patch.GetSeedType.SeedType != -1)
                    {
                        CreatePlant.Instance.SetPlant(Mouse.Instance.theMouseColumn, Mouse.Instance.theMouseRow, Patch.GetSeedType.SeedType, null, default(Vector2), false, 0f);
                    }
                }
                if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
                {
                    configMindControl.Value = !configMindControl.Value;
                }
                if (Input.GetKeyDown(KeyCode.V))
                {
                    if (Patch.GetZombieType.ZombieType != -1)
                    {
                        if (configMindControl.Value)
                        {
                            CreateZombie.Instance.SetZombieWithMindControl(Mouse.Instance.theMouseRow, Patch.GetZombieType.ZombieType, Mouse.Instance.mouseX, false);
                        }
                        else
                        {
                            CreateZombie.Instance.SetZombie(Mouse.Instance.theMouseRow, Patch.GetZombieType.ZombieType, Mouse.Instance.mouseX, false);
                        }
                    }
                }
            }
        }
    }
}