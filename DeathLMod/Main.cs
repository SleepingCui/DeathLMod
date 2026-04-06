using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;
using UnityModManagerNet;

namespace DeathLMod
{
    public static class Main
    {
        public static Settings settings;
        public static bool enabled;
        public static UnityModManager.ModEntry mod;
        public static UnityModManager.ModEntry.ModLogger Logger;

        static bool Load(UnityModManager.ModEntry modEntry)
        {
            mod = modEntry;
            Logger = modEntry.Logger;
            settings = UnityModManager.ModSettings.Load<Settings>(modEntry);
            var harmony = new Harmony(modEntry.Info.Id);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            modEntry.OnToggle = OnToggle;
            modEntry.OnGUI = OnGUI;
            modEntry.OnSaveGUI = OnSaveGUI;
            return true;
        }
        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            enabled = value;
            return true;
        }
        static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            settings.DrawUI(modEntry);
        }
        static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            settings.Save(modEntry);
        }

        public static void ExecuteAction()
        {
            if (enabled && settings != null)
            {
                if (settings.ActionType == DeathActionType.DoNothing) return;
                Execute.execute();
            }
        }
    }
}