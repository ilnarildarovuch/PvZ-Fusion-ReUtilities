using MelonLoader;

[assembly: MelonInfo(typeof(Odyssey_Buffs.Core), "Odyssey Buffs", "2.2.1", "tuananh291", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace Odyssey_Buffs
{
    public class Core : MelonMod
    {
        public static Core instance;

        //
        // Configuration entries
        public MelonPreferences_Entry<bool> configEnablePlant;
        public MelonPreferences_Entry<bool> configEnableEntries;
        public MelonPreferences_Entry<bool> configEnableTravel;
        public MelonPreferences_Entry<bool> configEnableIZ;

        public MelonPreferences_Entry<bool>[] boolArrayadvancedConfig;
        public MelonPreferences_Entry<bool>[] boolArrayultimateConfig;

        public static string[] advancedUpgradesKeys = new string[]
        {
            "Empress_Buff1", "Empress_Buff2",
            "GatlingDoom_Buff1", "GatlingDoom_Buff2",
            "TwinSolar_Buff1", "TwinSolar_Buff2",
            "Spikesidian_Buff1", "Spikesidian_Buff2",
            "DoubleSun", "GloveHalfCooldown",
            "PlantDMGIncrease", "ZombiesReceiveDMGIncrease",
            "ZombieInstanceDamage", "SelectPlantLoadout",
            "RerollPlus2", "EnflamedDMGMultiplier",
            "GloveNoCooldown",
            "GatlingIcicle_Buff1", "GatlingIcicle_Buff2",
            "MeteorHalfCooldown", "LumosMaxLevel",
            "LaserPumpkin_Buff1", "LaserPumpkin_Buff2",
            "MidasUmbrella_Buff1", "MidasUmbrella_Buff2",
            "Gain2000SunAndDoubled", "Mallet6SecCooldown",
            "PlantHalfRecharge",
            "Zombie1800DMGInstance", "Fission",
            "CharmedZombiesIncreasedDMG", "Phoenix_INFPierce",
            "ImitaterReducedRecharge", "IdenticalPlantDMGIncrease",
            "MagicCatDrone1","MAgicCatBasic2",
            "TrueCherryBomb","DoomMushroomNoCrafter",
            "HypnoMushroomSum","CherryGodxUltHighNut"
        };

        public static string[] ultimateUpgradesKeys = new string[]
        {
            "Cherrizilla_Buff1", "Cherrizilla_Buff2",
            "GatlingCherrybomber_Buff1", "GatlingCherrybomber_Buff2",
            "Doominator_Buff1", "Doominator_Buff2",
            "SquashedInfernowood_Buff1", "SquashedInfernowood_Buff2",
            "Magnetar_Buff1","Magnetar_Buff2",
            "Oblivion_Buff1", "Oblivion_Buff2",
            "Wither_Buff1", "Wither_Buff2",
            "CoblivionCannon_Buff1", "CoblivionCannon_Buff2",
            "Obsidian_Buff1", "Obsidian_Buff2",
            "Charmatron_Buff1", "Charmatron_Buff2",
            "UnknownBuffUltTower1","UltTower2"
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
            for (int i = 0; i < advancedUpgradesKeys.Length; i++)
            {
                boolArrayadvancedConfig[i] = advancedCategory.CreateEntry(advancedUpgradesKeys[i], true);
            }

            boolArrayultimateConfig = new MelonPreferences_Entry<bool>[ultimateUpgradesKeys.Length];
            for (int i = 0; i < ultimateUpgradesKeys.Length; i++)
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