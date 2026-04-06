using HarmonyLib;
using System;
using System.Threading.Tasks;
using UnityModManagerNet;

namespace DeathLMod
{
    [HarmonyPatch(typeof(scrController), "FailAction")]
    public static class DeathHook
    {
        private static float lastTriggerTime = 0f;

        static void Postfix()
        {
            if (!Main.enabled || Main.settings == null || Main.settings.ActionType == DeathActionType.DoNothing)
                return;
            if (UnityEngine.Time.realtimeSinceStartup - lastTriggerTime < 2f)
                return;
            lastTriggerTime = UnityEngine.Time.realtimeSinceStartup;
            Task.Run(() =>
            {
                Main.ExecuteAction();
            });
        }
    }
}