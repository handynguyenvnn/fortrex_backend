using System;
using System.Web.Script.Serialization;
using Lib.Data.Repository.Tasks;
using Lib.Data.Repository.Models.Packages;
using Web.SourceCoin.Common;

namespace Lib.Tasks.Packages
{
    public class BonusInvest : ITask
    {
        public BonusInvest()
        {
        }
        public void Execute()
        {
            BonusBuild();
        }

        public void BonusBuild()
        {
            TaskRepository _task = new TaskRepository();
            try
            {
                BNBClient bNBClient = new BNBClient();
                string config = _task.Setting_GetValueByName("Invest.Profit.OnDay.Range");
                decimal priceEth = bNBClient.PriceBuySymbol("ETHUSDT");// _task.EthereumPrice();

                var data = _task.Tool_Get_Packeges_Bonus();
                if (data.Count > 0)
                {
                    foreach (Packages_Bonus bonus in data)
                    {
                        try
                        {
                            Payment(bonus, config, priceEth, _task);
                        }
                        catch (Exception ex)
                        {
                            var json = new JavaScriptSerializer().Serialize(bonus);
                            _task.ErrorLog_Insert(null, "Payment -> " + ex.Message, json, 7);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                _task.ErrorLog_Insert(null, ex.Message, "BonusInvest-Exception", 7);
            }
        }

        private void Payment(Packages_Bonus bonus, string config, decimal priceEth, TaskRepository task)
        {
            decimal percent = PercentProfit(config, bonus.Invested);
            decimal ProfitUSD = bonus.Invested * percent / 100;
            decimal eth = task.Convert_USD_To_ETH(ProfitUSD, priceEth);


            var model = new Packages_Bonus_Transaction
            {
                UserId = bonus.UserId,
                Bonus = ProfitUSD,
                CreateDate = bonus.CurrentDate,
                PercentAmount = percent,
                PackagesId = bonus.Id,
                Type = 8,
                TotalBonus = eth,
                MaxBonusOnMonth = 0
            };
            int rel = task.Tool_Packages_Bonus_Transaction_Insert(model);
            if(eth <= 0)
            {
                var json = new JavaScriptSerializer().Serialize(model);
                task.ErrorLog_Insert(bonus.Id, json, "BonusInvest-Payment-Not_Get-Price-ETH", 7);
                return;
            }
            else if(rel != 1)
            {
                var json = new JavaScriptSerializer().Serialize(model);
                task.ErrorLog_Insert(bonus.Id, json, "BonusInvest-Payment", 7);
            }
        }

        private decimal PercentProfit(string config, decimal invest)
        {
            var config_list = config.Split('-');

            decimal _percent = 0;
            switch(invest)
            {
                case 1000:
                    _percent = decimal.Parse(config_list[0]);
                    break;
                case 2000:
                    _percent = decimal.Parse(config_list[1]);
                    break;
                case 3000:
                    _percent = decimal.Parse(config_list[2]);
                    break;
                case 4000:
                    _percent = decimal.Parse(config_list[3]);
                    break;
                case 5000:
                    _percent = decimal.Parse(config_list[4]);
                    break;
                case 6000:
                    _percent = decimal.Parse(config_list[5]);
                    break;
                case 7000:
                    _percent = decimal.Parse(config_list[6]);
                    break;
                case 8000:
                    _percent = decimal.Parse(config_list[7]);
                    break;
                case 9000:
                    _percent = decimal.Parse(config_list[8]);
                    break;
                case 10000:
                    _percent = decimal.Parse(config_list[9]);
                    break;
            }
            return _percent;
        }
    }
}
