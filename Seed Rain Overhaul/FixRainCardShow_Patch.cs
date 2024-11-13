using HarmonyLib;
using Il2Cpp;

namespace Seed_Rain_Overhaul
{
    [HarmonyPatch(typeof(DroppedCard))]
    internal class FixRainCardShow_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch("Update")]
        public static void RemoveText(DroppedCard __instance)
        {
            if (__instance.theSeedCost == 0)
            {
                __instance.text.text = "";
            }
        }
    }
}
