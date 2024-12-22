using HarmonyLib;
using Il2Cpp;

namespace No_Craters
{
    internal class Patch
    {
        [HarmonyPatch(typeof(DoomShroom))]
        public class DoomShroom_Patch
        {
            [HarmonyPrefix]
            [HarmonyPatch("AnimExplode")]
            private static void AnimExplode(DoomShroom __instance)
            {
                __instance.Die(Plant.DieReason.Default);
            }
        }

        [HarmonyPatch(typeof(IceDoom))]
        public class IceDoom_Patch
        {
            [HarmonyPrefix]
            [HarmonyPatch("AnimExplode")]
            private static void AnimExplode(IceDoom __instance)
            {
                __instance.Die(Plant.DieReason.Default);
            }
        }

        [HarmonyPatch(typeof(Board))]
        public class Pit
        {
            [HarmonyPrefix]
            [HarmonyPatch("SetDoom")]
            private static void SetDoom(ref bool setPit)
            {
                setPit = false;
            }
        }
    }
}
