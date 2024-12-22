using HarmonyLib;
using Il2Cpp;

namespace Plant_and_Zombie_Spawner
{
    internal class Patch
    {
        [HarmonyPatch(typeof(AlmanacCard))]
        public class GetSeedType   
        {

            public static int SeedType = -1;

            [HarmonyPostfix]
            [HarmonyPatch("OnMouseDown")]
            public static void OnMouseDown(AlmanacCard __instance)
            {
                GetSeedType.SeedType = __instance.theSeedType;
            }
        }

        [HarmonyPatch(typeof(AlmanacCardZombie))]
        public class GetZombieType
        {

            public static int ZombieType = -1;

            [HarmonyPostfix]
            [HarmonyPatch("OnMouseDown")]
            public static void GetZombieTypePostfix(AlmanacCardZombie __instance)
            {
                GetZombieType.ZombieType = (int)__instance.theZombieType;
            }
        }
    }
}
