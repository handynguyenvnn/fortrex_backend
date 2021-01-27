using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Lib.Domain.User;
using Newtonsoft.Json;
using Lib.Data.Repository.User;

namespace Lib.Service.Service.Trons
{
    public interface ITronService
    {
        string CreateAddressTrx(int userId);
    }

    public class TronService : ITronService
    {
        private string userHost = "https://api.tronscan.org/api";
        private readonly IUserRepository _userRepository;
        public TronService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public string CreateAddressTrx(int userId)
        {
            var restClient = new RestClient(userHost + "/account");
            var request = new RestRequest(Method.POST);
            var respone = restClient.Execute(request);
            if(respone.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = respone.Content;
                try
                {
                    var tronCoin = JsonConvert.DeserializeObject<TronCoin>(respone.Content.Trim());
                    tronCoin.UserId = userId;
                    return _userRepository.User_Tron_Create(tronCoin);
                }
                catch
                {

                }
            }
            return "";
        }
    }
}
