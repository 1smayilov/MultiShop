using Microsoft.AspNetCore.SignalR;
using MultiShop.SignalR.Services;
using MultiShop.SignalR.Services.SignalRCommentServices;
using MultiShop.SignalR.Services.SignalRMessageServices;

namespace MultiShop.SignalR.Hubs
{
    public class SignalRHub : Hub
    {
        //private readonly ISignalRMessageService _signalRMessageService;
        private readonly ISignalRCommentService _signalRCommentService;

        public SignalRHub(ISignalRCommentService signalRCommentService)
        {

            _signalRCommentService = signalRCommentService;
        }

        public async Task SendStatisticcount()
        {
            var getTotalCommentCount = await _signalRCommentService.GetTotalCommentCount();
            await Clients.All.SendAsync("ReceiveCommentCount", getTotalCommentCount); // O Çağrılanda bu gələcək



            //var getTotalMessageCount = await _signalRMessageService.GetTotalMessageCountByReceiverId(id);
            //await Clients.All.SendAsync("ReceiveMessageCount", getTotalMessageCount);
        }
    }
}
