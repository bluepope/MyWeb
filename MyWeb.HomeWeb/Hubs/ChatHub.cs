using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWeb.HomeWeb.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message, string targetId)
        {
            if (message == "exit")
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "SYSTEM", "접속이 종료되었습니다");
                Context.Abort();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(targetId))
                {
                    await Clients.All.SendAsync("ReceiveMessage", this.Context.ConnectionId, message);
                }
                else
                {
                    await Clients.Client(targetId.Trim()).SendAsync("ReceiveMessage", this.Context.ConnectionId, message);
                }
                
            }
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveMessage", "SYSTEM", $"{this.Context.ConnectionId}님 환영합니다");

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("ReceiveMessage", "SYSTEM", $"{this.Context.ConnectionId}님 안녕히가세요");

            await base.OnDisconnectedAsync(exception);
        }
    }
}
