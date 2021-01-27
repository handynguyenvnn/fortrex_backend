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
    public class SendMailMarketing : ITask
    {
        public SendMailMarketing()
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
                var dataMail = _task.Tool_GetAddressMail(null);
                foreach (MailMarketing data in dataMail)
                {
                    int lastId = data.LastId.Value;
                    while (lastId >= 0)
                    {
                        try
                        {
                            List<Genaral_Marketing_Mail> mails = _task.Tool_GetExtensionMail(50, lastId, data.MarketingId);
                            int totalMail = mails.Count();
                            if (totalMail > 0)
                            {
                                lastId = mails.Max(x => x.Id);
                                _task.Tool_UpdateLastIdOrFinish(data.MarketingId, lastId, null);
                                foreach (Genaral_Marketing_Mail mail in mails)
                                {
                                    try
                                    {
                                        TaskHelper.SendNotificationAsync(mail);
                                        _task.Tool_GetExtensionMail_IsSend(mail.Id);
                                    }
                                    catch
                                    { }
                                }
                            }
                            else
                            {
                                lastId = -1;
                            }
                        }
                        catch (Exception)
                        {
                            lastId = -1;
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
