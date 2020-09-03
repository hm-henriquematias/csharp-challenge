using Domain.Entities;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Handlers
{
    public class ConnectionHandler
    {
        private readonly BlockingCollection<SocketEntity> _sockets = new BlockingCollection<SocketEntity>();

        public WebSocket GetSocketById(string id)
        {
            return _sockets.FirstOrDefault(p => p.SocketId == id).Socket;
        }

        public BlockingCollection<SocketEntity> GetAll()
        {
            return _sockets;
        }

        public string GetId(WebSocket socket)
        {
            return _sockets.FirstOrDefault(p => p.Socket == socket).SocketId;
        }

        public bool IsSocketRegistred(WebSocket socket)
        {
            int amount = _sockets.Count(p => p.Socket == socket);
            return amount > 0;
        }

        public void AddSocket(WebSocket socket)
        {
            SocketEntity socketEntity = new SocketEntity()
            {
                SocketId = CreateConnectionId(),
                Socket = socket
            };
            _sockets.TryAdd(socketEntity);
        }

        public void AddServerInfoIntoSocket(WebSocket socket, ServerInfo serverInfo)
        {
            _sockets.FirstOrDefault(p => p.Socket == socket).ServerInfo = serverInfo;
        }

        public async Task RemoveSocket(string id)
        {
            SocketEntity socket = _sockets.FirstOrDefault(p => p.SocketId == id);
            _sockets.TryTake(out SocketEntity item);

            await socket.Socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                    statusDescription: "Closed by the ConnectionManager",
                                    cancellationToken: CancellationToken.None);
        }

        private string CreateConnectionId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
