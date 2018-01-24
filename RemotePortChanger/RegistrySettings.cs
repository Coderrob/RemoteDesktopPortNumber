using System;
using Microsoft.Win32;
using NLog;

namespace RemotePortChanger
{
    internal class RegistrySettings
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public static RemoteDesktopSettings Settings { get; }

        static RegistrySettings()
        {
            Settings = new RemoteDesktopSettings();
        }

        public static void SetRemoteDesktopPortNumber(int portNumber)
        {
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(Settings.SubKey, true))
                {
                    if (key == null)
                    {
                        Logger.Warn("Unable to locate the remote desktop Tcp port number registry values.");
                        return;
                    }

                    key.SetValue(Settings.ValueKey, portNumber);
                    key.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to set remote desktop Tcp port number to {portNumber}. {ex.Message}");
            }
        }

        public static int GetRemoteDesktopPortNumber()
        {
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(Settings.SubKey))
                {
                    if (key == null)
                    {
                        Logger.Warn("Unable to locate the remote desktop Tcp port number registry values.");
                        return Settings.DefaultPort;
                    }

                    return Convert.ToInt32(key.GetValue(Settings.ValueKey, Settings.DefaultPort));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to get remote desktop Tcp port number. {ex.Message}");

                // Return default Windows RDP Tcp port number.
                return Settings.DefaultPort;
            }
        }

        internal class RemoteDesktopSettings
        {
            public int DefaultPort { get; } = 3389;
            public string SubKey { get; } = @"System\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp";
            public string ValueKey { get; } = "PortNumber";
        }
    }
}