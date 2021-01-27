using System;
using System.Linq;
using System.Web.Script.Serialization;
using Lib.Data.Repository.Tasks;
using Lib.Data.Repository.Models.TronCoins;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace Lib.Tasks.Coinbase
{
    public class AutoPayment : ITask
    {
        public AutoPayment()
        {
            
        }
        public void Execute()
        {
            SendETH_Pay_ProfitDaily();
        }
        public async void SendETH_Pay_ProfitDaily()
        {

            try
            {
                var privateKey = "3daf548e5644f6d55a4326b72c3b55532939f6516736bdc31fb1cb8484ede240";
                var account = new Account(privateKey);
                var url = "https://mainnet.infura.io/v3/9bf70e8163e3469ebdaa3b3f23647f13";
                var web3 = new Web3(account, url);
                
                //b1: Dữ liệu này phải được admin duyệt trong phần manage trước rồi mới cho chạy code này.
                //b2: lấy danh sách ví từ bảng tbl_tempPayProfitDaily trả lãi hàng ngày. 
                //b3: Sau khi vừa chuyển coin  xong thì ghi log lại và xóa dòng dữ liệu đó trong bảng tạm tbl_tempPayProfitDaily_log.
                var toAddress = "0x3FEb9f35AB0c49fc3bf55693ce072327AbC33118";
                var transaction = await web3.Eth.GetEtherTransferService().TransferEtherAsync(toAddress, 0.002m);
                Console.WriteLine(transaction);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
       
    }
}
