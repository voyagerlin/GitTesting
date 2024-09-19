using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.SignalR;

namespace SignalRNotify.Hubs
{
    public class NotifyHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var connId = Context.ConnectionId;
            await Clients.Client(connId).SendAsync("SetConnId", connId, "hi");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public async Task AddToGroup(string groupName)
        {            
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public async Task RemoveFromGroup(string groupName)        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }
        public async Task SendNotify(string user, string message)
        {
            var connId = Context.ConnectionId;
            await Clients.Client(connId).SendAsync("ReceiveMessage", user, message);
        }
        public async Task TriggerModal(string user, bool canClose){
            //await Clients.Client(user).SendAsync("CloseModal", canClose);
            await Clients.Client(Context.ConnectionId).SendAsync("CloseModal", canClose);
        }
    }
}