using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Lib.Cache;
using System.Web.Script.Serialization;
using System.Collections;
using System.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Lib.Data.Repository.Marketings;
using Lib.Domain.Marketings;

namespace Lib.Service.Service.Marketings
{
    public interface IMarketingService
    {
        List<MailTemplate> Marketing_GetAll(int pageIndex, int pageSize, out int total, string whereClause);
        MailTemplate Marketing_GetDetail(int id);
        int Marketing_Insert(MailTemplate detail);
        int Marketing_Update(MailTemplate detail);
        List<MailAccount> MailAccount_List();
        int Manage_Delete_MarketingById(int[] ids);
    }

    public class MarketingService : IMarketingService
    {
        private readonly IMarketingRepository _marketingRepository;
        public MarketingService(IMarketingRepository marketingRepository)
        {
            _marketingRepository = marketingRepository;
        }

        public List<MailTemplate> Marketing_GetAll(int pageIndex, int pageSize, out int total, string whereClause)
        {
            return _marketingRepository.Marketing_GetAll(pageIndex, pageSize, out total, whereClause);
        }
        public MailTemplate Marketing_GetDetail(int id)
        {
            return _marketingRepository.Marketing_GetDetail(id);
        }
        public int Marketing_Insert(MailTemplate detail)
        {
            return _marketingRepository.Marketing_Insert(detail);
        }
        public int Marketing_Update(MailTemplate detail)
        {
            return _marketingRepository.Marketing_Update(detail);
        }
        public List<MailAccount> MailAccount_List()
        {
            return _marketingRepository.MailAccount_List();
        }
        public int Manage_Delete_MarketingById(int[] ids)
        {
            return _marketingRepository.Manage_Delete_MarketingById(string.Join(",", ids));
        }
    }
}