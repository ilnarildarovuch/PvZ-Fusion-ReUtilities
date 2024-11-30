using Il2Cpp;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(Zombie_Health.Core), "Zombie Health", "1.0.1", "dynaslash", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace Zombie_Health
{
    public class Core : MelonMod
    {
        private static GUIStyle guiStyle;
        private Camera mainCamera;
        private static readonly List<ValueTuple<Zombie, Transform>> zombie_Shadow = new List<ValueTuple<Zombie, Transform>>();
        private static bool opendrawbl = false;

        public override void OnInitializeMelon()
        {

            guiStyle = new GUIStyle
            {
                fontSize = 20,
                normal = { textColor = Color.red },
                alignment = TextAnchor.MiddleCenter
            };

            MelonLogger.Msg("Zombie Health is loaded!");
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (Camera.main != null)
            {
                mainCamera = Camera.main;
            }
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.KeypadMultiply))
            {
                opendrawbl = !opendrawbl;
                MelonLogger.Msg($"Drawing toggled to {opendrawbl}");
            }
        }

        public override void OnGUI()
        {
            if (mainCamera == null) return;

            guiStyle.fontSize = (int)(Screen.height / 42.75f * 5f / mainCamera.orthographicSize);

            if (zombie_Shadow.Count == 0 || zombie_Shadow != new List<ValueTuple<Zombie, Transform>>())
            {
                zombie_Shadow.Clear();
                zombie_Shadow.AddRange(from o in GameObject.FindGameObjectsWithTag("Zombie")
                                       select new ValueTuple<Zombie, Transform>(o.GetComponent<Zombie>(), o.transform.Find("Shadow")));
            }

            if (opendrawbl && zombie_Shadow.Count > 0)
            {
                if (GameAPP.theGameStatus == 0 || GameAPP.theGameStatus == 2 || GameAPP.theGameStatus == 3)
                {
                    foreach (var valueTuple in zombie_Shadow)
                    {
                        Zombie item = valueTuple.Item1;
                        Transform item2 = valueTuple.Item2;

                        try
                        {
                            Vector3 position = item2.position;
                            Vector3 screenPos = mainCamera.WorldToScreenPoint(position);
                            screenPos.y = Screen.height - screenPos.y;

                            int num = 0;
                            int theSecondArmorHealth = item.theSecondArmorHealth;
                            int theFirstArmorHealth = item.theFirstArmorHealth;
                            float freezeCountdown = (float)Math.Round(item.theFreezeCountDown, 2);
                            float slowCountdown = (float)Math.Round(item.theSlowCountDown, 2);

                            num -= guiStyle.fontSize;

                            if (freezeCountdown != 0f)
                            {
                                Rect freezeRect = new Rect(screenPos.x - (float)((int)(Screen.height / 22.5)), screenPos.y + num, 100f, 30f);
                                GUI.Label(freezeRect, "Freeze: " + freezeCountdown.ToString() + "s", guiStyle);
                                num -= guiStyle.fontSize;
                            }

                            if (slowCountdown != 0f)
                            {
                                Rect slowRect = new Rect(screenPos.x - (float)((int)(Screen.height / 22.5)), screenPos.y + num, 100f, 30f);
                                GUI.Label(slowRect, "Slow: " + slowCountdown.ToString() + "s", guiStyle);
                                num -= guiStyle.fontSize;
                            }

                            if ((item.theHealth + theFirstArmorHealth + theSecondArmorHealth) > 0)
                            {
                                Rect healthRect = new Rect(screenPos.x - (float)((int)(Screen.height / 22.5)), screenPos.y + num, 100f, 30f);
                                GUI.Label(healthRect, "Total HP: " + (item.theHealth + theFirstArmorHealth + theSecondArmorHealth).ToString(), guiStyle);
                                num -= guiStyle.fontSize;
                            }

                            if (theSecondArmorHealth > 0)
                            {
                                Rect secondArmorRect = new Rect(screenPos.x - (float)((int)(Screen.height / 22.5)), screenPos.y + num, 100f, 30f);
                                GUI.Label(secondArmorRect, "2nd Armor: " + theSecondArmorHealth.ToString(), guiStyle);
                                num -= guiStyle.fontSize;
                            }

                            if (theFirstArmorHealth > 0)
                            {
                                Rect firstArmorRect = new Rect(screenPos.x - (float)((int)(Screen.height / 22.5)), screenPos.y + num, 100f, 30f);
                                GUI.Label(firstArmorRect, "1st Armor: " + theFirstArmorHealth.ToString(), guiStyle);
                                num -= guiStyle.fontSize;
                            }
                            if (item.theHealth > 0)
                            {
                                Rect baseHealthRect = new Rect(screenPos.x - (float)((int)(Screen.height / 22.5)), screenPos.y + num, 100f, 30f);
                                GUI.Label(baseHealthRect, "Base HP: " + ((int)item.theHealth).ToString(), guiStyle);
                            }
                        }
                        catch (Exception e)
                        {
                            MelonLogger.Msg($"Error in ONGUI Drawing: {e}");
                        }
                    }
                }
            }
        }
    }
}
