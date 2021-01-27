using Lib.Domain.Packages.Trades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RealtimeRealtimeDatabaseSubscriptionSubscription.Hubs
{
    [Authorize]
    //[Authorize(Roles = "USER")]
    public class RealtimeHub : Hub
    {
        private readonly RealtimeDatabaseSubscription _repository;

        public RealtimeHub(RealtimeDatabaseSubscription repository)
        {
            _repository = repository;
        }
        //public void setPairname(string connectionId="",string pair = "")
        //{
        //    if (!string.IsNullOrEmpty(connectionId))
        //    {
        //        //_repository._pairname = pair;
        //        var UserconnectionId = _repository.userIds.Where(p => p.ConnectionId == connectionId).FirstOrDefault();
        //        _repository.userIds.Remove(UserconnectionId);
        //        UserconnectionId.PairName = pair;
        //        _repository.userIds.Add(UserconnectionId);
        //    }
        //}
        public void setPairname(string pair = "")
        {
            if (!string.IsNullOrEmpty(Context.ConnectionId) && !string.IsNullOrEmpty(pair))
            {
                //_repository._pairname = pair;
                //var UserconnectionId = _repository.userIds.Where(p => p.ConnectionId == Context.ConnectionId).FirstOrDefault();
                //if (UserconnectionId== null)
                //{
                //    //_repository.userIds.Remove(UserconnectionId);
                //    UserconnectionId.PairName = pair;
                //    _repository.userIds.Add(UserconnectionId);
                //}
              
            }
        }
        //public string GetConnectionId()
        //{
        //    return Context.ConnectionId;
        //}
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //var UserconnectionId = _repository.userIds.Find(p => p.ConnectionId == Context.ConnectionId);
            //if (UserconnectionId != null)
            //{
            //    _repository.userIds.Remove(UserconnectionId);
            //}

            await base.OnDisconnectedAsync(exception);
        }
        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    var UserconnectionId = _repository.userIds.Where(p => p.ConnectionId == Context.ConnectionId).FirstOrDefault();
        //    _repository.userIds.Remove(UserconnectionId);
        //    await base.OnDisconnectedAsync(null);
        //}
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            var context = Context.User.Claims.Where(p => p.Type.Equals("AccountId")).FirstOrDefault();
            if (context != null)
            {
                var userId = context.Value ?? "0";
                UserId_ConnectionId_Map userId_Connection = new UserId_ConnectionId_Map();
                userId_Connection.Userid = int.Parse(userId);
                userId_Connection.ConnectionId = Context.ConnectionId;
                //userId_Connection.PairName = "BTC_USD";
                _repository.userIds.Add(userId_Connection);
               // _repository._connectionIds.Add(Context.ConnectionId);
                //await Clients.Client(Context.ConnectionId).SendAsync(Context.ConnectionId, "by user 2: ", $"{Context.User.Identity.Name} joined. connectionId: {Context.ConnectionId}");
                //Console.WriteLine("connect", $"{Context.User.Identity.Name} joined.");
            }
        }
       
    }
}
