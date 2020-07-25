using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinalProject.Models.Classes
{
    public class WebSocketsHandler
    {
        List<string> _queries = new List<string>()
        {
            "I'm sure, we can resolve this problem!",
            "It's not a problem!",
            "We'll call back you later!",
            "We'll inform you about future propositions",
            "Goodluck!"
        };

        public ConcurrentDictionary<string, WebSocket> websocketConnections = new ConcurrentDictionary<string, WebSocket>();

        public async Task HandleAsync(WebSocket webSocket)
        {
            while (webSocket.State == WebSocketState.Open)
            {
                var message = await ReceiveMessage(webSocket);
                if (message != null)
                    await SendMessageToSockets(message);
            }
        }

        private async Task<MessageOutputInfo> ReceiveMessage(WebSocket webSocket)
        {
            var arraySegment = new ArraySegment<byte>(new byte[4096]);
            var receivedMessage = await webSocket.ReceiveAsync(arraySegment, CancellationToken.None);
            if (receivedMessage.MessageType == WebSocketMessageType.Text)
            {
                var message = Encoding.UTF8.GetString(arraySegment).TrimEnd('\0');
                var content = JsonConvert.DeserializeObject<MessageInputInfo>(message);
                content.Date = DateTime.Now;
                if (!content.Registered)
                {
                    if (!websocketConnections.ContainsKey(content.Login))
                    {
                        websocketConnections.TryAdd(content.Login, webSocket);
                        return new MessageOutputInfo()
                        {
                            Login = content.Login,
                            Content = $"<i>\"{content.Login}\", welcome to our chat! How can we help You?</i>"
                        };
                    }
                }
                else
                {
                    if (!websocketConnections.ContainsKey(content.Login))
                    {
                        websocketConnections.TryAdd(content.Login, webSocket);
                    }
                    return new MessageOutputInfo()
                    {
                        Login = content.Login,
                        Content = $"({content.Date.ToShortTimeString()}), <b>{content.Login}</b>: {content.Content}"
                    };
                }
            }
            return null;
        }

        private async Task SendMessageToSockets(MessageOutputInfo message)
        {
            var connection = websocketConnections[message.Login];
            if(connection!=null)
            {
                //client request
                var arraySegment = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message.Content));
                //
                await connection.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
                
                await Task.Delay(3000);
                
                //support response
                Random random = new Random();
                var content = $"({DateTime.Now.ToShortTimeString()}) <i>Support</i>: {_queries[random.Next(0, _queries.Count - 1)]}";
                arraySegment = new ArraySegment<byte>(Encoding.UTF8.GetBytes(content));
                //
                await connection.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        
    }
}
