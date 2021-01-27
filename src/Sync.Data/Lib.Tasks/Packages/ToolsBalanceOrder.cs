using System;
using System.Threading.Tasks;
using Lib.Data.Repository.Tasks;
using Lib.Data.Domain.Trade;
using Web.SourceCoin.Common;
using System.Threading;

namespace Lib.Tasks.Packages
{
    public class ToolsBalanceOrder : ITask
    {
        public ToolsBalanceOrder()
        {
        }
        public void Execute()
        {
            HandleOrders();
        }
        private void HandleOrders()
        {
            TaskRepository task = new TaskRepository();
            var grporders = task.Tool_OpeningOrderGroupName();
            var orders = task.Tool_OpeningOrders();
            foreach (var groupName in grporders)
            {
                var handleOrder = orders.FindAll(p => p.MarketName.Equals(groupName.MarketName));
                string typeIsWIN = "";
                decimal _amountOrderbyPair = 0;
                if (handleOrder.Count >= 2)
                {
                    if (handleOrder[0].AMOUNT > handleOrder[1].AMOUNT)
                    {
                        if (handleOrder[1].ISCALL)
                        {
                            typeIsWIN = "BUY";
                        }
                        else
                        {
                            typeIsWIN = "SELL";
                        }
                    }
                    else if (handleOrder[0].AMOUNT < handleOrder[1].AMOUNT)
                    {
                        if (handleOrder[0].ISCALL)
                        {
                            typeIsWIN = "BUY";
                        }
                        else
                        {
                            typeIsWIN = "SELL";
                        }
                    }
                    else
                    {
                        typeIsWIN = "";
                    }
                    // get giá max hoặc min theo loại giao dịch được cho thắng
                    var resultPrice = task.Tool_OpeningOrders_Price_getBy_Type_Is_WIN(groupName.MarketName, typeIsWIN);
                    /// typeIsWIN = BUY thì push giá mới vào làm sao cho giá mới CAP hơn với giá trần của SELL (> resultPrice)
                    /// Ngược lại typeIsWIN = SELL thì push giá mới vào làm sao cho giá mới THẤP hơn với giá SÀN của BUY (< resultPrice)
                    decimal newLastPrice = 0;
                    if (typeIsWIN.Equals("BUY"))
                    {
                        Random rd = new Random();
                        var percentRandom = (decimal)Math.Round(rd.NextDouble() / 3, 2);
                        newLastPrice = resultPrice + ((decimal)percentRandom * resultPrice / 100);
                    }
                }
                else
                {

                }

            }
        }



    }
}
