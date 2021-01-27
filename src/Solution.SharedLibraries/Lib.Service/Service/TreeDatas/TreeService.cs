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
using Lib.Data.Repository.TreeDatas;
using Lib.Domain.Trees;

namespace Lib.Service.Service.TreeDatas
{
    public interface ITreeService
    {
        bool CheckUserIdExistsRoot(int rootId, int userId);
        List<ShowTree> T_TreeData_ShowTree(int userId, int level);
        List<UserIntroduction> Tree_GetAllUserByManageId(int userId);
        int T_TreeData_AddNode(ShowTree model);
        List<TreeData> T_TreeData_GetUserByParent(int parentId, int level);
        List<Parents> T_TreeData_GetParentByUserId(int userId, int level = 0, int type = 0);
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

    public class TreeService : ITreeService
    {
        private readonly ITreeRepository _treeRepository;
        public TreeService(ITreeRepository treeRepository)
        {
            _treeRepository = treeRepository;
        }

        public float Sysn_Data_Tab_Insert(SyncDataTab model)
        {
            return _treeRepository.Sysn_Data_Tab_Insert(model);
        }

        public List<Parents> MUser_GetParentByUserId(int userId)
        {
            return _treeRepository.MUser_GetParentByUserId(userId);
        }

        public int Tranfer_XRP(int id)
        {
            return _treeRepository.Tranfer_XRP(id);
        }
        public List<TreeData> MUser_GetListUserByParent(int parentId, int level)
        {
            return _treeRepository.MUser_GetListUserByParent(parentId, level);
        }

        public List<TreeData> MUser_GetListUserByParent_V2(int parentId, int level)
        {
            return _treeRepository.MUser_GetListUserByParent_V2(parentId, level);
        }

        public List<TreeData> MUser_GetUserByParent(int parentId, int level)
        {
            return _treeRepository.MUser_GetUserByParent(parentId, level);
        }
        public bool Check_UId_Exists_Tree(int uid)
        {
            return _treeRepository.Check_UId_Exists_Tree(uid);
        }
        public int User_Branch_Balance_Insert(User_Branch_Balance branch)
        {
            return _treeRepository.User_Branch_Balance_Insert(branch);
        }
        public bool T_TreeData_Check_Exists(int userId)
        {
            return _treeRepository.T_TreeData_Check_Exists(userId);
        }
        public bool CheckUserIdExistsRoot(int rootId, int userId)
        {
            return _treeRepository.CheckUserIdExistsRoot(rootId, userId);
        }
        public List<ShowTree> T_TreeData_ShowTree(int userId, int level)
        {
            return _treeRepository.T_TreeData_ShowTree(userId, level);
        }
        public List<UserIntroduction> Tree_GetAllUserByManageId(int userId)
        {
            return _treeRepository.Tree_GetAllUserByManageId(userId);
        }
        public int T_TreeData_AddNode(ShowTree model)
        {
            return _treeRepository.T_TreeData_AddNode(model);
        }
        public List<TreeData> T_TreeData_GetUserByParent(int parentId, int level)
        {
            return _treeRepository.T_TreeData_GetUserByParent(parentId, level);
        }

        public List<Parents> T_TreeData_GetParentByUserId(int userId, int level = 0, int type = 0)
        {
            return _treeRepository.T_TreeData_GetParentByUserId(userId, level, type);
        }

        public List<Parents> T_TreeData_GetUserIdByParentId(int userId)
        {
            return _treeRepository.T_TreeData_GetUserIdByParentId(userId);
        }
    }
}