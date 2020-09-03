using Infrastructure.Handlers;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Websockets
{
    public class WebsocketMannager : IWebSocketMannager
    {
        public ConnectionHandler ConnectionHandler { get; set; }

        public WebsocketMannager(ConnectionHandler connectionManager)
        {
            ConnectionHandler = connectionManager;
        }

        public virtual async Task OnConnected(WebSocket socket)
        {
            ConnectionHandler.AddSocket(socket);
            var socketId = ConnectionHandler.GetId(socket);
            await SendMessageToAllAsync($"{socketId} is now connected");
        }

        public async Task SendMessageAsync(WebSocket socket, string message)
        {
            if (socket.State != WebSocketState.Open)
                return;

            await socket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message),
                                                                    offset: 0,
                                                                    count: message.Length),
                                    messageType: WebSocketMessageType.Text,
                                    endOfMessage: true,
                                    cancellationToken: CancellationToken.None);
        }

        public async Task SendMessageAsync(string socketId, string message)
        {
            await SendMessageAsync(ConnectionHandler.GetSocketById(socketId), message);
        }

        public async Task SendMessageToAllAsync(string message)
        {
            foreach (var socketEntity in ConnectionHandler.GetAll())
            {
                if (socketEntity.Socket.State == WebSocketState.Open)
                    await SendMessageAsync(socketEntity.Socket, message);
            }
        }

        public async Task<string> ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            return Encoding.UTF8.GetString(buffer, 0, result.Count);
        }
    }
}
