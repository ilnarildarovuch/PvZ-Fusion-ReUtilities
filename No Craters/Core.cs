﻿using Il2Cpp;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(No_Craters.Core), "No Craters", "1.0.0", "dynaslash", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace No_Craters
{
    public class Core : MelonMod
    {
        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Better Pot Fusion is loaded!");
        }
    }
}