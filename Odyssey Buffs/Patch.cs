using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using MelonLoader;
using System.Reflection;
using UnityEngine;

namespace Odyssey_Buffs
{
    [HarmonyPatch(typeof(Board))]
    internal class Patch
    {
        [HarmonyPatch("Awake")]
        [HarmonyPostfix]
        private static void TravelPostfix(Board __instance)
        {
            // Ensure the config is loaded correctly
            Core.instance.ReloadConfig();
            MelonLogger.Msg("Odyssey Buffs is loaded!");

            if (Core.instance.configEnablePlant.Value)
            {
                // Enable travel plant feature in board tag
                object obj = __instance.boardTag;
                Type type = obj.GetType();
                FieldInfo field = type.GetField("enableTravelPlant");
                // MelonLogger.Msg($"configEnablePlant Value = {Core.instance.configEnablePlant.Value}");

                if (field != null)
                {
                    object obj2 = Convert.ChangeType(true, field.FieldType);
                    field.SetValue(obj, obj2);
                    // MelonLogger.Msg("Field Null");
                }
                __instance.boardTag = (Board.BoardTag)obj;
            }

            GameObject gameObject = new GameObject("GameAPP");
            // MelonLogger.Msg("GameAPP created");
            TravelMgr travelMgr = gameObject.AddComponent<TravelMgr>(); // Thay đổi từ GetComponent sang AddComponent
                                                                        //TravelMgr travelMgr = gameObject.GetComponent<TravelMgr>();
                                                                        // MelonLogger.Msg("TravelMgr found");

            // Kiểm tra null trước khi sử dụng
            if (travelMgr == null)
            {
                MelonLogger.Error("Failed to create TravelMgr component!");
                return;
            }

            // Handle IZ and travel entries configuration
            if (Core.instance.configEnableEntries.Value)
            {
                // MelonLogger.Msg("configEnableEntries Value = " + Core.instance.configEnableEntries.Value);
                if (GameAPP.theBoardLevel == 8 && GameAPP.theBoardType == 3)
                {
                    // MelonLogger.Msg("Board Level and Board Type: " + GameAPP.theBoardLevel + " " + GameAPP.theBoardType);
                    if (!Core.instance.configEnableTravel.Value)
                    {
                        // MelonLogger.Msg("configEnableTravel Value = " + Core.instance.configEnableTravel.Value);
                        return;
                    }
                }

                if (GameAPP.theBoardType == 2)
                {
                    // MelonLogger.Msg("Board Type: " + GameAPP.theBoardType);
                    if (!Core.instance.configEnableIZ.Value)
                    {
                        // MelonLogger.Msg("configEnableIZ Value = " + Core.instance.configEnableIZ.Value);
                        return;
                    }
                }

                // Khởi tạo arrays với Il2Cpp
                if (travelMgr.advancedUpgrades == null)
                {
                    travelMgr.advancedUpgrades = new Il2CppStructArray<bool>(Core.instance.boolArrayadvancedConfig.Length);
                }
                if (travelMgr.ultimateUpgrades == null)
                {
                    travelMgr.ultimateUpgrades = new Il2CppStructArray<bool>(Core.instance.boolArrayultimateConfig.Length);
                }

                // Khởi tạo arrays với kích thước chính xác
                if (travelMgr.advancedUpgrades == null || travelMgr.advancedUpgrades.Length == 0)
                {
                    // Giả sử advancedUpgradesKeys cũng có định nghĩa tương tự
                    travelMgr.advancedUpgrades = new Il2CppStructArray<bool>(Core.instance.boolArrayadvancedConfig.Length);
                }

                if (travelMgr.ultimateUpgrades == null || travelMgr.ultimateUpgrades.Length == 0)
                {
                    // Đảm bảo kích thước là 22 theo ultimateUpgradesKeys
                    travelMgr.ultimateUpgrades = new Il2CppStructArray<bool>(22);
                }

                MelonLogger.Msg($"Ultimate Upgrades Array Size: {travelMgr.ultimateUpgrades.Length}");
                MelonLogger.Msg($"Ultimate Config Array Size: {Core.instance.boolArrayultimateConfig.Length}");

                // Apply ultimate upgrades với kiểm tra kích thước
                if (Core.instance.boolArrayultimateConfig.Length != 22)
                {
                    MelonLogger.Warning($"Config size mismatch! Expected 22 but got {Core.instance.boolArrayultimateConfig.Length}");
                }

                for (int i = 0; i < Math.Min(travelMgr.ultimateUpgrades.Length, Core.instance.boolArrayultimateConfig.Length); i++)
                {
                    travelMgr.ultimateUpgrades[i] = Core.instance.boolArrayultimateConfig[i].Value;
                }

                // Apply advanced and ultimate upgrade configurations

                MelonLogger.Msg("");
                MelonLogger.Msg("Loading advanced upgrades...");
                for (int i = 0; i < travelMgr.advancedUpgrades.Count; i++)
                {
                    travelMgr.advancedUpgrades[i] = Core.instance.boolArrayadvancedConfig[i].Value;
                    MelonLogger.Msg($"Advanced Upgrade {Core.instance.boolArrayadvancedConfig[i].DisplayName} = {Core.instance.boolArrayadvancedConfig[i].Value} applied.");
                }
                MelonLogger.Msg("Advanced upgrades loaded!");

                MelonLogger.Msg("");
                MelonLogger.Msg("Loading ultimate upgrades...");
                for (int i = 0; i < travelMgr.ultimateUpgrades.Count; i++)
                {
                    travelMgr.ultimateUpgrades[i] = Core.instance.boolArrayultimateConfig[i].Value;
                    MelonLogger.Msg($"Ultimate Upgrade {Core.instance.boolArrayultimateConfig[i].DisplayName} = {Core.instance.boolArrayultimateConfig[i].Value} applied.");
                }
                MelonLogger.Msg("Ultimate upgrades loaded!");

            }
        }
    }
}