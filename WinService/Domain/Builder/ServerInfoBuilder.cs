using Domain.Entities;
using WinServiceClassLib.Utils;

namespace Domain.Builder
{
    public class ServerInfoBuilder : IServerInfoBuilder
    {
        private ServerInfo _serverInfo = new ServerInfo();

        public ServerInfoBuilder()
        {
            _serverInfo = new ServerInfo();
        }

        public void BuildDiskInfo()
        {
            DiskHelper diskInfoHelper = new DiskHelper();
            _serverInfo.AvailableDiskStorage = diskInfoHelper.Free;
            _serverInfo.UsedDiskStorage = diskInfoHelper.Used;
        }

        public void BuildEnvironmentInfo()
        {
            EnvironmentHelper environmentInfoHelper = new EnvironmentHelper();
            _serverInfo.Name = environmentInfoHelper.GetName();
            _serverInfo.Ip = environmentInfoHelper.GetIp();
            _serverInfo.WindowsVersion = environmentInfoHelper.GetOsVersion();
            _serverInfo.DotNetFrameworkVersion = environmentInfoHelper.GetVersion();
        }

        public void BuildSecurityInfo()
        {
            SecurityHelper securityInfoHelper = new SecurityHelper();
            _serverInfo.Antivirus = securityInfoHelper.IsActiveAntivirus();
            _serverInfo.Firewall = securityInfoHelper.IsActiveFirewall();
        }

        public ServerInfo GetServerInfo()
        {
            ServerInfo serverInfo = _serverInfo;
            _serverInfo = new ServerInfo();
            return serverInfo;
        }
    }
}
