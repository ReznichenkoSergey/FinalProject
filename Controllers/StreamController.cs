using System.Net.WebSockets;
using System.Threading.Tasks;
using FinalProject.Models.Classes;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class StreamController : Controller
    {
        public WebSocketsHandler _handler;

        public StreamController(WebSocketsHandler handler)
        {
            _handler = handler;
        }

        public async Task Get()
        {
            var context = ControllerContext.HttpContext;
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await _handler.HandleAsync(webSocket);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }
    }
}
