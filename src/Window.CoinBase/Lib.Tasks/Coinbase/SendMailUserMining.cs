using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Data;
using Lib.Data.Repository.Tasks;
using Lib.Data.Repository.Models;

namespace Lib.Tasks.Coinbase
{
    public class SendMailUserMining : ITask
    {
        public SendMailUserMining()
        {
            
        }
        public void Execute()
        {
            SendMail();
        }
        public void SendMail()
        {
            TaskRepository _task = new TaskRepository();
            try
            {
                var dataMail = _task.Tool_GetAddressMailSendUserMining();
                if(dataMail != null)
                {
                    if (dataMail.IsTest == true)
                    {
                        var mail = new AddressMail
                        {
                            Email = dataMail.EmailTest
                        };
                        TaskHelper.SendNotificationAsync(dataMail, mail);
                        _task.Tool_UpdateLastIdOrFinish(dataMail.MarketingId, 0, false);
                    }
                    else
                    {
                        List<AddressMail> mails = _task.Tool_GetAddressAllMailUser_Mining(50);
                        if (mails.Count > 0)
                        {
                            foreach (AddressMail mail in mails)
                            {
                                string body = string.Format(dataMail.Body, mail.FullName, float.Parse(mail.Bonus.ToString()).ToString() + (mail.Status==1? " BTC" :" ETH"), (mail.IsFinish ? "Finish" : mail.NextTimeOn.ToString()));
                                dataMail.Body = body;
                                try
                                {
                                    TaskHelper.SendNotificationAsync(dataMail, mail);
                                    _task.Mail_UserMining_Update(mail.Id);
                                }
                                catch
                                { }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _task.ErrorLog_Insert(null, ex.Message, null, 1);
            }
        }
    }
}
