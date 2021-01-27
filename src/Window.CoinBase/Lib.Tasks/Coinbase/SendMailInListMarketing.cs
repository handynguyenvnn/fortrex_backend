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
    public class SendMailInListMarketing : ITask
    {
        public SendMailInListMarketing()
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
                var dataMail = _task.Tool_GetAddressMail();
                if(dataMail.Count > 0)
                {
                    if (dataMail.Any(x => x.IsTest == true))
                    {
                        var testMail = dataMail.Where(x => x.IsTest == true).FirstOrDefault();
                        var mail = new AddressMail
                        {
                            Email = testMail.EmailTest
                        };
                        TaskHelper.SendNotificationAsync(testMail, mail);
                        _task.Tool_UpdateLastIdOrFinish(testMail.MarketingId, 0, false);
                    }
                    else
                    {
                        foreach (MailMarketing data in dataMail)
                        {
                            int lastId = 0;
                            List<AddressMail> mails = new List<AddressMail>();

                            //while (lastId >= 0)
                            //{
                                try
                                {
                                    mails = _task.Users_List_Mail_Marketing();
                                    int totalMail = mails.Count();
                                    if (totalMail > 0)
                                    {
                                        lastId = mails.Max(x => x.Id);

                                        foreach (AddressMail mail in mails)
                                        {
                                            try
                                            {
                                                TaskHelper.SendNotificationAsync(data, mail);
                                            }
                                            catch
                                            { }
                                        }
                                        _task.Tool_UpdateLastIdOrFinish(data.MarketingId, 0, false);
                                    }
                                    else
                                    {
                                        _task.Tool_UpdateLastIdOrFinish(data.MarketingId, lastId, false);
                                        lastId = -1;
                                    }
                                }
                                catch (Exception)
                                {
                                    _task.Tool_UpdateLastIdOrFinish(data.MarketingId, lastId, false);
                                    lastId = -1;
                                }
                            //}
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
