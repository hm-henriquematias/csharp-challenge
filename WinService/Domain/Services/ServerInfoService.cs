using Domain.Builder;
using Domain.Entities;

namespace Domain.Services
{
    public class ServerInfoService : IServerInfoService
    {
        public ServerInfo GetServerInfo()
        {
            ServerInfoDirector director = new ServerInfoDirector();
            ServerInfoBuilder builder = new ServerInfoBuilder();

            director.Builder = builder;
            director.BuildServerInfo();

            return builder.GetServerInfo();
        }
    }
}
