using Domain.Entities;
using System.Collections.Concurrent;

namespace Infrastructure.Websockets
{
    public interface IWebSocketService
    {
        WebsocketMannager GetWebsocketMannager();
        string SendCommand(string socketId, string message);
        BlockingCollection<SocketEntity> ListAllSockets();
    }
}
