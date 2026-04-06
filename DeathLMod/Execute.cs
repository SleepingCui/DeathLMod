using System;
using System.IO;

namespace DeathLMod
{
    internal static class Execute
    {
        public static void execute() 
        {
            var settings = Main.settings;
            if (settings == null) return;

            Main.Logger.Log($"Launching Application mode={settings.ActionType}");

            try
            {
                if (settings.ActionType == DeathActionType.LaunchSteam && !string.IsNullOrEmpty(settings.SteamId))
                {
                    string steamExePath = Microsoft.Win32.Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamExe", null) as string;

                    if (!string.IsNullOrEmpty(steamExePath) && File.Exists(steamExePath))
                    {
                        System.Diagnostics.Process.Start(steamExePath, $"-applaunch {settings.SteamId}");
                    }
                    else
                    {
                        System.Diagnostics.Process.Start($"steam://rungameid/{settings.SteamId}");
                    }
                }
                else if (settings.ActionType == DeathActionType.LaunchExe && !string.IsNullOrEmpty(settings.ExePath))
                {
                    if (File.Exists(settings.ExePath))
                    {
                        var startInfo = new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = settings.ExePath,
                            WorkingDirectory = Path.GetDirectoryName(settings.ExePath),
                            UseShellExecute = true
                        };
                        System.Diagnostics.Process.Start(startInfo);
                    }
                }
            }
            catch (Exception e)
            {
                Main.Logger.Log($"ERROR: {e.Message}");
            }
        }
    }
}