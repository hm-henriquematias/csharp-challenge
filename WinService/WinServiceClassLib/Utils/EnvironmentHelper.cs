using Microsoft.Win32;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace WinServiceClassLib.Utils
{
    public class EnvironmentHelper
    {
        public string GetName()
        {
            return Environment.MachineName.ToString();
        }

        public string GetIp()
        {
            return Dns.GetHostAddresses(Dns.GetHostName()).Where(address => address.AddressFamily == AddressFamily.InterNetwork).First().ToString();
        }

        public string GetOsVersion()
        {
            string versionName = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName", "").ToString();
            string versionId = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString();
            return $"{versionName} - Release: {versionId}";
        }

        public string GetVersion()
        {
            return Environment.Version.ToString();
        }
    }
}
