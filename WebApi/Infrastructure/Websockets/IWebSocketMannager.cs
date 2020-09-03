using Infrastructure.Handlers;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Websockets
{
    public interface IWebSocketMannager
    {
        public ConnectionHandler ConnectionHandler { get; set; }
        public Task SendMessageAsync(WebSocket socket, string message);
        public Task SendMessageAsync(string socketId, string message);
        public Task SendMessageToAllAsync(string message);
        public Task<string> ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);

    }
}
