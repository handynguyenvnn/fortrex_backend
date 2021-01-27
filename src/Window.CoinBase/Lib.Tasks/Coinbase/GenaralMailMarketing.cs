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
    public class GenaralMailMarketing : ITask
    {
        public GenaralMailMarketing()
        {
            
        }
        public void Execute()
        {
            GenaralMail();
        }
        public void GenaralMail()
        {
            TaskRepository _task = new TaskRepository();
            try
            {
                var dataMail = _task.Tool_GetAddressMail(true);
                if(dataMail.Count > 0)
                {
                    if (dataMail.Any(x => x.IsTest == true))
                    {
                        var testMail = dataMail.Where(x => x.IsTest == true).FirstOrDefault();
                        var send_test = new Genaral_Marketing_Mail
                        {
                            Email = testMail.Email,
                            DisplayName = testMail.DisplayName,
                            Host = testMail.Host,
                            Port = testMail.Port,
                            Username = testMail.Username,
                            Password = testMail.Password,
                            EnableSsl = testMail.EnableSsl,
                            UseDefaultCaredential = testMail.UseDefaultCredentials,
                            ToMail = testMail.EmailTest,
                            Title = testMail.Title,
                            Body = testMail.Body,
                            IsSend = false,
                            MarketingId = testMail.MarketingId
                        };
                        TaskHelper.SendNotificationAsync(send_test);
                        _task.Tool_UpdateLastIdOrFinish(testMail.MarketingId, null, false);
                    }
                    else
                    {
                        foreach (MailMarketing data in dataMail)
                        {
                            if (data.Type == (int)MarketingEmailType.SendTo_ALL_Users_In_List_Table_Temp)
                            {
                                Genaral_User_team(_task, data);
                            }
                            else if (data.Type == (int)MarketingEmailType.SendAll)
                            {
                                Genaral_User(_task, data);
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
        
        private void Genaral_User_team(TaskRepository _task, MailMarketing data)
        {
            int lastId = 0;
            List<AddressMail> mails = new List<AddressMail>();
            while (lastId >= 0)
            {
                try
                {
                    mails = _task.Tool_GetAddressAllMailUser(50, lastId);
                    int totalMail = mails.Count();
                    if (totalMail > 0)
                    {
                        lastId = mails.Max(x => x.Id);
                        foreach (AddressMail mail in mails)
                        {
                            try
                            {
                                var genaral = new Genaral_Marketing_Mail
                                {
                                    Email = data.Email,
                                    DisplayName = data.DisplayName,
                                    Host = data.Host,
                                    Port = data.Port,
                                    Username = data.Username,
                                    Password = data.Password,
                                    EnableSsl = data.EnableSsl,
                                    UseDefaultCaredential = data.UseDefaultCredentials,
                                    ToMail = mail.Email,
                                    Title = data.Title,
                                    Body = data.Body,
                                    IsSend = false,
                                    MarketingId = data.MarketingId
                                };
                                _task.Genaral_Marketing_Mail_Insert(genaral);
                            }
                            catch
                            { }
                        }
                    }
                    else
                    {
                        _task.Tool_UpdateLastIdOrFinish(data.MarketingId, null, false);
                        lastId = -1;
                    }
                }
                catch (Exception)
                {
                    _task.Tool_UpdateLastIdOrFinish(data.MarketingId, null, false);
                    lastId = -1;
                }
            }
        }

        private void Genaral_User(TaskRepository _task, MailMarketing data)
        {
            int lastId = 0;
            List<AddressMail> mails = new List<AddressMail>();
            while (lastId >= 0)
            {
                try
                {
                    mails = _task.Users_List_Mail_Marketing(50, lastId);
                    int totalMail = mails.Count();
                    if (totalMail > 0)
                    {
                        lastId = mails.Max(x => x.Id);
                        foreach (AddressMail mail in mails)
                        {
                            try
                            {
                                var genaral = new Genaral_Marketing_Mail
                                {
                                    Email = data.Email,
                                    DisplayName = data.DisplayName,
                                    Host = data.Host,
                                    Port = data.Port,
                                    Username = data.Username,
                                    Password = data.Password,
                                    EnableSsl = data.EnableSsl,
                                    UseDefaultCaredential = data.UseDefaultCredentials,
                                    ToMail = mail.Email,
                                    Title = data.Title,
                                    Body = data.Body,
                                    IsSend = false,
                                    MarketingId = data.MarketingId
                                };
                                _task.Genaral_Marketing_Mail_Insert(genaral);
                            }
                            catch
                            { }
                        }
                    }
                    else
                    {
                        _task.Tool_UpdateLastIdOrFinish(data.MarketingId, null, false);
                        lastId = -1;
                    }
                }
                catch (Exception)
                {
                    _task.Tool_UpdateLastIdOrFinish(data.MarketingId, null, false);
                    lastId = -1;
                }
            }
        }
    }
}
