using Microsoft.Win32;
using System;
using System.Management;

namespace WinServiceClassLib.Utils
{
    public class SecurityHelper
    {

        public bool IsActiveFirewall()
        {
            bool isActive = false;

            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\SharedAccess\\Parameters\\FirewallPolicy\\StandardProfile"))
                {
                    if (key == null)
                    {
                        isActive = false;
                    }
                    else
                    {
                        Object enableFirewall = key.GetValue("EnableFirewall");
                        if (enableFirewall == null)
                        {
                            isActive = false;
                        }
                        else
                        {
                            isActive = ((int)enableFirewall == 1);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
            }

            return isActive;
        }

        public bool IsActiveAntivirus()
        {
            bool isActive = false;

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"\\" + Environment.MachineName + @"\root\SecurityCenter", "SELECT * FROM AntivirusProduct");
                ManagementObjectCollection instances = searcher.Get();
                isActive = instances.Count > 0;
            }
            catch (Exception exception)
            {
            }

            return isActive;
        }
    }
}
