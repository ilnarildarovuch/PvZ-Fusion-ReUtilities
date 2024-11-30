using HarmonyLib;
using MelonLoader;
using UnityEngine;
using Il2Cpp;

[assembly: MelonInfo(typeof(Utilities.Core), "Utilities Addon", "1.0.0", "dynaslash", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace Utilities
{
    public class Core : MelonMod
    {

        private static DateTime dtStart;
        private static DateTime? dtStartToast;
        private static string toast_txt;

        public override void OnEarlyInitializeMelon() => dtStart = DateTime.Now;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Utilities Addon is loaded!");
        }

        public override void OnLateInitializeMelon() => dtStart = DateTime.Now;

        public override void OnLateUpdate()
        {
            Utility.OnLateUpdate();
        }

        public override void OnGUI()
        {
            if (Utility.GetActive(Utility.UtilityType.ShowUtilities) || DateTime.Now - dtStart < new TimeSpan(0 , 0, 0, 5))
            {
                string text = Utility.GetUtilities();
                int num = 0;
                int num2 = 20;
                foreach (string text2 in text.Split('\n', StringSplitOptions.None))
                {
                    if (text2.Length > num2)
                    {
                        num2 = text2.Length;
                    }
                    num++;
                }
                bool flag = GUI.Button(new Rect(10f, 30f, (float)num2 * 10f, (float)num * 16f + 15f), text);
            }

            if (dtStartToast != null)
            {
                GUI.Button(new Rect(10f, 10f, 200f, 20f), "\n" + toast_txt + "\n");
                TimeSpan? timeSpan = DateTime.Now - dtStartToast;
                TimeSpan t = new TimeSpan(0, 0, 0, 2);
                if (timeSpan > t)
                {
                    dtStartToast = null;
                }
            }
        }

        public static void ShowToast(string message)
        {
            toast_txt = message;
            dtStartToast = new DateTime?(DateTime.Now);
        }
    }
}