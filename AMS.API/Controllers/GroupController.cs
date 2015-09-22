using System.Collections.Generic;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using AMS.BLL;
using AMS.Entity;

namespace AMS.API.Controllers
{
    public class GroupController : ApiController
    {
        //
        // GET: /Group/

        /// <summary>
        /// 从服务器获取所有群组的基本信息，并更新本地数据库
        /// </summary>
        /// <returns></returns>
        public SimpleGroupsModel2[] getAllGroups()
        {
            //1.从服务器获取所有群组的基本信息，“owner”、“groupid”、“affiliations”、“groupname”
            HXService hxService = new HXService();
            string strJson = hxService.GetAllGroups();
            //2.更新本地数据库
            if (!string.IsNullOrEmpty(strJson))
            {
                //解析json字符串
                //List<SimpleGroupsModel1> groupList = fastJSON.JSON.ToObject<List<SimpleGroupsModel1>>(strJson);
                SimpleGroupsModel1 simpleGroupsModel = fastJSON.JSON.ToObject<SimpleGroupsModel1>(strJson);
                //foreach (SimpleGroupsModel2 model in simpleGroupsModel.data)
                //{
                //    string groupId = model.groupid;
                //    string groupName = model.groupname;
                //    //string groupOwner = model.owner;//这个一般不会被修改的
                //    int currentUsers = model.affiliations;
                //    string strSql = "UPDATE [dbo].[Tb_Group] SET [groupName] = '" + groupName + "',currentUsers=" + currentUsers + " WHERE [groupId] = '" + groupId + "'";
                //    sqlHelper.RunSQL(strSql);
                //}
                return simpleGroupsModel.data;
            }
            return null;
        }

        public JsonResult getAllMembersByGroupId(string groupId)
        {
            string strJson = getGroupDetailById(groupId);
            AMS_GroupDetailModel2 groupDetailModel = fastJSON.JSON.ToObject<AMS_GroupDetailModel1>(strJson).data[0];
            AMS_GroupMemberIdModel[] groupList = groupDetailModel.affiliations;
            List<string> userIdList = new List<string>();//获取用户ID
            string ownerId = "";
            foreach (AMS_GroupMemberIdModel user in groupList)
            {
                if (user.member != null)
                {
                    userIdList.Add(user.member);
                }
                else
                {
                    userIdList.Add(user.owner);
                    ownerId = user.owner;
                }
            }
            if (userIdList.Count > 0)
            {
                //根据ID查询用户信息
                
            }
            return null;
        }

        public string getGroupTree()
        {
            HXService hxService = new HXService();
            string strJson = hxService.GetAllGroups();
            SimpleGroupsModel1 group = fastJSON.JSON.ToObject<SimpleGroupsModel1>(strJson); //反序列化
            SimpleGroupsModel2[] groups = group.data;
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            foreach (SimpleGroupsModel2 g in groups)
            {
                builder.Append("{\"groupid\":\"" + g.groupid + "\",");
                builder.Append("\"icon\":\"../../Content/Themes/Images/16/users.png\",");
                builder.Append("\"leaf\":true,");
                builder.Append("\"groupname\":\"" + g.groupname + "\"},");
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append("]");
            return builder.ToString();
        }

        public string getGroupDetailById(string groupId)
        {
            HXService hxService = new HXService();
            return hxService.GetGroupDetailById(groupId);
        }

        public string editGroupDetail(string groupId, string groupname, string description, int maxusers)
        {
            HXService hxService = new HXService();
            return hxService.EditGroupById(groupId, groupname, description, maxusers);
        }

        //创建一个群组
        public string newGroup(string groupname, string description, int maxusers, string owner, string[] members)
        {
            HXService hxService = new HXService();
            return hxService.NewGroup(groupname, description, maxusers, owner, members);
        }

        //删除群组
        public string deleteGroup(string groupId)
        {
            HXService hxService = new HXService();
            return hxService.deleteGroup(groupId);
        }

        //群组加人[批量]
        public string addGroupMembers(string groupId, string[] members)
        {
            HXService hxService = new HXService();
            return hxService.addGroupMembers(groupId, members);
        }

        //群组减人
        public string deleteGroupMember(string groupId, string member)
        {
            HXService hxService = new HXService();
            return hxService.deleteGroupMember(groupId, member);
        }

        public string exec()
        {
            string[] s = { "liuj" };
            return deleteGroupMember("142638234269572", "liuj");
        }

    }
}