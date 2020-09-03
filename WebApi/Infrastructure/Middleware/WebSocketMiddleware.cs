using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Infrastructure.Websockets;
using System.Net.WebSockets;
using System.Threading;
using Domain.Entities;
using Newtonsoft.Json;

namespace Infrastructure.Middleware
{
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate _next;
        private WebsocketMannager WebSocketMannager { get; set; }

        public WebSocketMiddleware(RequestDelegate next, WebsocketMannager webSocketMannager)
        {
            _next = next;
            WebSocketMannager = webSocketMannager;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
                return;

            var socket = await context.WebSockets.AcceptWebSocketAsync();

            if (!WebSocketMannager.ConnectionHandler.IsSocketRegistred(socket))
            {
                await WebSocketMannager.OnConnected(socket);

                await Receive(socket, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        string serverInfo = await WebSocketMannager.ReceiveAsync(socket, result, buffer);
                        try
                        {
                            ServerInfo serverInfoObj = JsonConvert.DeserializeObject<ServerInfo>(serverInfo);
                            WebSocketMannager.ConnectionHandler.AddServerInfoIntoSocket(socket, serverInfoObj);
                        }
                        catch (Exception exception)
                        {

                        }
                        return;
                    }
                });
            }
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer), cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }
    }
}
