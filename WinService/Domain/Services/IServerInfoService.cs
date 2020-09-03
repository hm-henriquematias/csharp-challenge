using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public interface IServerInfoService
    {
        ServerInfo GetServerInfo();
    }
}
