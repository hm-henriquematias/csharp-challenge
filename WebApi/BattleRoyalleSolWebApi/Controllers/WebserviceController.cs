using Infrastructure.Websockets;
using Microsoft.AspNetCore.Mvc;

namespace BattleRoyalleSolWebApi.Controllers
{
    [Route("api/[controller]")]
    public class WebserviceController : ControllerBase
    {
        private readonly IWebSocketService _websocketService;

        public WebserviceController(IWebSocketService websocketService) => _websocketService = websocketService;

        [HttpPost]
        public IActionResult SendCommand(string socketId, string message)
        {
            return Ok(_websocketService.SendCommand(socketId, message));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_websocketService.ListAllSockets());
        }
    }
}
