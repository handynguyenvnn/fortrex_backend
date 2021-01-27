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
    public class SendMailPromotion : ITask
    {
        public SendMailPromotion()
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
                var dataMail = _task.Tool_GetAddressMail(4);
                if (dataMail.Count > 0)
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
                        string titleTemplate = string.Empty;
                        string bodyTemplate = string.Empty;
                        foreach (MailMarketing data in dataMail)
                        {
                            int lastId = 0;
                            titleTemplate = data.Title;
                            bodyTemplate = data.Body;
                            List<MailPromotion> mails = new List<MailPromotion>();
                            try
                            {
                                mails = _task.Get_All_MailPromotion();
                                int totalMail = mails.Count();
                                if (totalMail > 0)
                                {
                                    foreach (MailPromotion mail in mails)
                                    {
                                        AddressMail addMail = new AddressMail
                                        {
                                            Email = mail.Email
                                        };
                                        try
                                        {
                                            data.Title = ReplaceTitle(mail, titleTemplate);
                                            data.Body = ReplaceBody(mail, bodyTemplate);
                                            TaskHelper.SendNotificationAsync(data, addMail);
                                            _task.PromotionSendMail_Update(mail.Id);
                                        }
                                        catch
                                        { }
                                    }
                                }
                                else
                                {
                                    _task.Tool_UpdateLastIdOrFinish(data.MarketingId, lastId, false);
                                }
                            }
                            catch (Exception)
                            {
                                _task.Tool_UpdateLastIdOrFinish(data.MarketingId, lastId, false);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _task.ErrorLog_Insert(null, ex.Message, null, 2);
            }
        }
        public string ReplaceTitle(MailPromotion mail, string _title)
        {
            _title = _title.Replace("code_promotion", mail.Code);
            _title = _title.Replace("received_eth", float.Parse(mail.TotalReceivedEth.ToString()).ToString());
            _title = _title.Replace("min_eth", float.Parse(mail.MinValueEth.ToString()).ToString());
            return _title;
        }
        public string ReplaceBody(MailPromotion mail, string _body)
        {
            _body = _body.Replace("user_name", mail.Username);
            _body = _body.Replace("min_eth", float.Parse(mail.MinValueEth.ToString()).ToString());
            _body = _body.Replace("received_eth", float.Parse(mail.TotalReceivedEth.ToString()).ToString());
            _body = _body.Replace("min_btc", float.Parse(mail.MinValueBtc.ToString()).ToString());
            _body = _body.Replace("received_btc", float.Parse(mail.TotalReceivedBtc.ToString()).ToString());
            _body = _body.Replace("from_date", mail.FromDate.ToString("yyyy-MM-dd mm:hh:ss"));
            _body = _body.Replace("to_date", mail.EndDate.ToString("yyyy-MM-dd mm:hh:ss"));
            _body = _body.Replace("code_promotion", mail.Code);
            return _body;
        }
    }
}
