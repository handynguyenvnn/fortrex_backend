using System.Linq;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Lib.Data.MapBuilder;
using Lib.Data.ResultSetMapper;
using Lib.Domain.Simples;
using System;
using Lib.Domain.Trees;

namespace Lib.Data.Repository.TreeDatas
{
    public interface ITreeRepository
    {
        bool CheckUserIdExistsRoot(int rootId, int userId);
        List<ShowTree> T_TreeData_ShowTree(int userId, int level);
        List<UserIntroduction> Tree_GetAllUserByManageId(int userId);
        int T_TreeData_AddNode(ShowTree model);
        List<TreeData> T_TreeData_GetUserByParent(int parentId, int level);
        List<Parents> T_TreeData_GetParentByUserId(int userId, int level, int type);
        int User_Branch_Balance_Insert(User_Branch_Balance branch);
        bool Check_UId_Exists_Tree(int uid);
        List<TreeData> MUser_GetUserByParent(int parentId, int level);
        List<TreeData> MUser_GetListUserByParent(int parentId, int level);
        List<TreeData> MUser_GetListUserByParent_V2(int parentId, int level);
        int Tranfer_XRP(int id);
        List<Parents> T_TreeData_GetUserIdByParentId(int userId);
        bool T_TreeData_Check_Exists(int userId);
        List<Parents> MUser_GetParentByUserId(int userId);
        float Sysn_Data_Tab_Insert(SyncDataTab model);
    }

    public class TreeRepository : BaseRepository, ITreeRepository
    {
        public float Sysn_Data_Tab_Insert(SyncDataTab model)
        {
            var map = new LongResultSetMapper();
            return _db.CreateSprocAccessor("Sysn_Data_Tab_Insert", map).Execute(
                model.Status,
                model.ExtraData
            ).FirstOrDefault();
        }

        public List<Parents> MUser_GetParentByUserId(int userId)
        {
            var map = NewsMapBuilder<Parents>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("MUser_GetParentByUserId", map);

            return query.Execute(userId, 3).ToList();
        }

        public int Tranfer_XRP(int id)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("Tranfer_XRP", map).Execute(id).FirstOrDefault();
        }
        public List<TreeData> MUser_GetListUserByParent(int parentId, int level)
        {
            var map = NewsMapBuilder<TreeData>
                .MapAllProperties()
                .Map(m => m.TreeDataItem).WithFunc((row) =>
                {
                    if (row["TreeDataItem"] != DBNull.Value)
                    {
                        return GetValue<List<TreeDataItem>>(Convert.ToString(row["TreeDataItem"]));
                    }
                    else
                    {
                        return new List<TreeDataItem>();
                    }
                })
                .Build();

            var query = _db.CreateSprocAccessor("MUser_GetListUserByParent", map);

            return query.Execute(parentId, level).ToList();
        }

        public List<TreeData> MUser_GetListUserByParent_V2(int parentId, int level)
        {
            var map = NewsMapBuilder<TreeData>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("MUser_GetListUserByParent_v2", map);

            return query.Execute(parentId, level).ToList();
        }

        public List<TreeData> MUser_GetUserByParent(int parentId, int level)
        {
            var map = NewsMapBuilder<TreeData>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("MUser_GetUserByParent", map);
            return query.Execute(parentId, level).ToList();
        }

        public bool Check_UId_Exists_Tree(int uid)
        {
            var map = new BooleanResultSetMapper();
            return _db.CreateSprocAccessor("Check_UId_Exists_Tree", map).Execute(uid).FirstOrDefault();
        }
        public int User_Branch_Balance_Insert(User_Branch_Balance branch)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("User_Branch_Balance_Insert", map).Execute(
                branch.UserId,
                branch.LeftAmount,
                branch.RightAmount,
                branch.LeftReset,
                branch.RightReset,
                branch.Status,
                branch.CreateDate,
                branch.ByUid,
                branch.PackageId,
                branch.MaxInvest
                ).FirstOrDefault();
        }

        public bool T_TreeData_Check_Exists(int userId)
        {
            var map = new BooleanResultSetMapper();
            return _db.CreateSprocAccessor("T_TreeData_Check_Exists", map).Execute(userId).FirstOrDefault();
        }

        public bool CheckUserIdExistsRoot(int rootId, int userId)
        {
            var map = new BooleanResultSetMapper();
            return _db.CreateSprocAccessor("Tree_CheckUserIdExistsRoot", map).Execute(rootId, userId).FirstOrDefault();
        }
        public List<ShowTree> T_TreeData_ShowTree(int userId, int level)
        {
            var map = NewsMapBuilder<ShowTree>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("T_TreeData_ShowTree", map);
            return query.Execute(userId, level).ToList();
        }

        public List<UserIntroduction> Tree_GetAllUserByManageId(int userId)
        {
            var map = NewsMapBuilder<UserIntroduction>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("Tree_GetAllUserByManageId", map);
            return query.Execute(userId).ToList();
        }
        public int T_TreeData_AddNode(ShowTree model)
        {
            var map = new IntegerResultSetMapper();
            return _db.CreateSprocAccessor("T_TreeData_AddUserId", map).Execute(model.ManageId,
                model.ParentId,
                model.UserId,
                model.Level,
                model.Node).FirstOrDefault();
        }
        public List<TreeData> T_TreeData_GetUserByParent(int parentId, int level)
        {
            var map = NewsMapBuilder<TreeData>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("T_TreeData_GetUserByParent", map);
            return query.Execute(parentId, level).ToList();
        }
        public List<Parents> T_TreeData_GetParentByUserId(int userId, int level, int type)
        {
            var map = NewsMapBuilder<Parents>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("T_TreeData_GetParentByUserId", map);
            return query.Execute(userId, level, type).ToList();
        }

        public List<Parents> T_TreeData_GetUserIdByParentId(int userId)
        {
            var map = NewsMapBuilder<Parents>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("T_TreeData_GetUserIdByParentId", map);
            return query.Execute(userId).ToList();
        }
    }
}