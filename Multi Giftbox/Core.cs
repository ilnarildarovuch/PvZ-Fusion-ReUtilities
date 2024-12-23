using MelonLoader;
using HarmonyLib;
using Il2Cpp;

[assembly: MelonInfo(typeof(Multi_Giftbox.Core), "Multi Giftbox", "216.0.0", "dynaslash", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace Multi_Giftbox
{
    public class Core : MelonMod
    {
        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Multi Giftbox is loaded!");
        }
    }

    [HarmonyPatch(typeof(PresentCard))]
    public class PresentCard_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch("Start")]
        private static bool SetActive()
        {
            return false;
        }
    }
}