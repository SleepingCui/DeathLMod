using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using UnityEngine;
using UnityModManagerNet;

namespace DeathLMod
{
    public enum DeathActionType
    {
        DoNothing,
        LaunchSteam,
        LaunchExe
    }

    public class Settings : UnityModManager.ModSettings
    {
        public DeathActionType ActionType = DeathActionType.LaunchSteam;
        public string SteamId = "1144400";
        public string ExePath = ""; 

        public override void Save(UnityModManager.ModEntry modEntry)
        {
            Save(this, modEntry);
        }

        public void DrawUI(UnityModManager.ModEntry modEntry)
        {
            GUILayout.BeginVertical("box");
            GUILayout.BeginHorizontal();
            GUILayout.Label("死亡执行动作：", GUILayout.Width(120));
            ActionType = (DeathActionType)GUILayout.SelectionGrid((int)ActionType,new string[] { "禁用", "Steam启动", "可执行文件" }, 3);
            GUILayout.EndHorizontal();
            GUILayout.Space(10);

            if (ActionType == DeathActionType.LaunchSteam)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Steam AppID:", GUILayout.Width(100));
                SteamId = GUILayout.TextField(SteamId, GUILayout.Width(150));
                GUILayout.EndHorizontal();
            }
            else if (ActionType == DeathActionType.LaunchExe)
            {
                GUILayout.Label("路径：");
                GUILayout.BeginHorizontal();
                ExePath = GUILayout.TextField(ExePath);
                GUILayout.EndHorizontal();
                if (!string.IsNullOrEmpty(ExePath) && !File.Exists(ExePath))
                {
                    GUILayout.Label("未发现文件");
                }
            }
            GUILayout.EndVertical();
        }

       

        //private void AutoDetect()
        //{
        //    string steamPath = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamPath", null) as string;
        //    if (string.IsNullOrEmpty(steamPath)) return;

        //    string vdfPath = Path.Combine(steamPath.Replace("/", "\\"), "steamapps", "libraryfolders.vdf");
        //    if (!File.Exists(vdfPath)) return;

        //    try
        //    {
        //        string vdfContent = File.ReadAllText(vdfPath);
        //        var matches = Regex.Matches(vdfContent, @"""path""\s+""([^""]+)""");

        //        foreach (Match match in matches)
        //        {
        //            string libPath = match.Groups[1].Value.Replace(@"\\", @"\");
        //            string potentialPath = Path.Combine(libPath, "steamapps", "common", "SenrenBanka", "SenrenBanka.exe");
        //            if (File.Exists(potentialPath))
        //            {
        //                ExePath = potentialPath;
        //                return;
        //            }
        //        }
        //    }
        //    catch {}
        //}
    }
}