using System.Net.WebSockets;

namespace Domain.Entities
{
    public class SocketEntity :  BaseEntity
    {
        public string SocketId { get; set; }
        public virtual ServerInfo ServerInfo { get; set; }
        public virtual WebSocket Socket { get; set; }
    }
}
