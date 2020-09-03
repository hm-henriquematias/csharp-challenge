using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WinServiceClassLib.Src;

namespace BattleRoyalleWinService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configurations;
        private readonly ServerInfo _serverInfo;
        private readonly string _webSocketURI;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IServerInfoService serverInfoService)
        {
            _logger = logger;
            _configurations = configuration;
            _serverInfo = serverInfoService.GetServerInfo();
            _webSocketURI = _configurations.GetSection("Websocket").GetSection("URI").Value.ToString();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var client = new ClientWebSocket();

            await client.ConnectAsync(new Uri(_webSocketURI), CancellationToken.None);

            var sending = Task.Run(async () =>
            {
                await SendMessage(client, _serverInfo.ToString());
            });

            while (true)
            {
                await RunWebSockets(client,sending);
                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task RunWebSockets(ClientWebSocket client, Task sending)
        {
            var receiving = ReceiveClientMessage(client);

            await Task.WhenAll(sending, receiving);
        }

        private async Task ReceiveClientMessage(ClientWebSocket client)
        {
            var buffer = new byte[1024 * 4];

            Command terminal = new Command();

            while (true)
            {
                var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    string command = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    string response = String.Join("\n", terminal.Execute(command).ToArray());
                    await SendMessage(client, response);
                }
            }
        }

        private async Task SendMessage(ClientWebSocket client, string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            await client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
            //await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
        }
    }
}
