using Domain.Entities;
using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Threading;

namespace Infrastructure.Websockets
{
    public class WebSocketService : IWebSocketService
    {
        private readonly WebsocketMannager WebsocketMannager;

        public WebSocketService()
        {
            WebsocketMannager = new WebsocketMannager(new Handlers.ConnectionHandler());
        }

        public WebsocketMannager GetWebsocketMannager()
        {
            return WebsocketMannager;
        }

        public string SendCommand(string socketId, string message)
        {
            WebSocket socket = WebsocketMannager.ConnectionHandler.GetSocketById(socketId);
            SendCommandMessage(socket, message);
            return GetResponseOfCommand(socket);
        }

        private void SendCommandMessage(WebSocket socket, string message)
        {
            WebsocketMannager.SendMessageAsync(socket, message).GetAwaiter();
        }

        private string GetResponseOfCommand(WebSocket socket)
        {
            var buffer = new byte[1024 * 4];

            var result = socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer), 
                cancellationToken: CancellationToken.None
            ).GetAwaiter().GetResult();
            
            return WebsocketMannager.ReceiveAsync(socket, result, buffer).GetAwaiter().GetResult();
        }

        public BlockingCollection<SocketEntity> ListAllSockets()
        {
            return GetWebsocketMannager().ConnectionHandler.GetAll();
        }
    }
}
