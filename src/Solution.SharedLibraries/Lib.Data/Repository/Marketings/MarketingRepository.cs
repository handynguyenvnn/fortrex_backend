using System.Linq;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Lib.Data.MapBuilder;
using Lib.Data.ResultSetMapper;
using Lib.Domain.Simples;
using System;
using Lib.Core.Data;
using Lib.Domain.Marketings;

namespace Lib.Data.Repository.Marketings
{
    public interface IMarketingRepository
    {
        List<MailTemplate> Marketing_GetAll(int pageIndex, int pageSize, out int total, string whereClause);
        MailTemplate Marketing_GetDetail(int id);
        int Marketing_Insert(MailTemplate detail);
        int Marketing_Update(MailTemplate detail);
        List<MailAccount> MailAccount_List();
        int Manage_Delete_MarketingById(string ids);
    }

    public class MarketingRepository : BaseRepository, IMarketingRepository
    {
        public List<MailTemplate> Marketing_GetAll(int pageIndex, int pageSize, out int total, string whereClause)
        {
            var map = NewsMapBuilder<MailTemplate>.MapAllProperties().Build();
            var parameters = new[] {
                    _db.CreateParameter("PageIndex", pageIndex, DbType.Int32),
                    _db.CreateParameter("PageSize", pageSize, DbType.Int32),
                    _db.CreateParameter("TotalCounts", 0, DbType.Int32, ParameterDirection.Output),
                    _db.CreateParameter("WhereClause", whereClause, DbType.String)
            };
            var data = _db.Execute("Marketing_GetAll", map, parameters).ToList();
            total = parameters[2].Value != DBNull.Value ? Convert.ToInt32(parameters[2].Value) : 0;

            return data;
        }
        public MailTemplate Marketing_GetDetail(int id)
        {
            var map = NewsMapBuilder<MailTemplate>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Marketing_GetDetail", map);
            return query.Execute(id).FirstOrDefault();
        }
        public int Marketing_Insert(MailTemplate detail)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Marketing_Insert", map).Execute(detail.AccountId,
                detail.Title,
                detail.Body,
                detail.IsActive,
                detail.CreateBy,
                detail.Type,
                detail.Email,
                detail.IsTest).FirstOrDefault();
        }
        public int Marketing_Update(MailTemplate detail)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Marketing_Update", map).Execute(detail.Id,
                detail.AccountId,
                detail.Title,
                detail.Body,
                detail.IsActive,
                detail.Type,
                detail.Email,
                detail.IsTest).FirstOrDefault();
        }
        public List<MailAccount> MailAccount_List()
        {
            var map = NewsMapBuilder<MailAccount>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("MailAccount_List", map);
            return query.Execute().ToList();
        }
        public int Manage_Delete_MarketingById(string ids)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Manage_Delete_MarketingById", map).Execute(ids).FirstOrDefault();
        }
    }
}