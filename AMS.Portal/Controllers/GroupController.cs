using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Mvc;
using DotNet.BLL;
using DotNet.Entity;
using DotNet.Portal.Models;

namespace DotNet.Portal.Controllers
{
    public class GroupController : Controller
    {
        //
        // GET: /Group/
        GroupBLL bll = new GroupBLL();
        UserBLL userBll = new UserBLL();

        public ActionResult Index()
        {
            return View();
        }

        public string getCompanyTree()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            builder.Append("{\"id\":\"BJ\",");
            builder.Append("\"icon\":\"../../Content/Themes/Images/16/chart_organisation.png\",");
            builder.Append("\"leaf\":true,");
            builder.Append("\"text\":\"北京侏罗纪\"},");
            builder.Append("{\"id\":\"WH\",");
            builder.Append("\"icon\":\"../../Content/Themes/Images/16/chart_organisation.png\",");
            builder.Append("\"leaf\":true,");
            builder.Append("\"text\":\"武汉侏罗纪\"},");
            builder.Append("{\"id\":\"XA\",");
            builder.Append("\"icon\":\"../../Content/Themes/Images/16/chart_organisation.png\",");
            builder.Append("\"leaf\":true,");
            builder.Append("\"text\":\"西安侏罗纪\"},");
            builder.Append("{\"id\":\"YDKJ\",");
            builder.Append("\"icon\":\"../../Content/Themes/Images/16/chart_organisation.png\",");
            builder.Append("\"leaf\":true,");
            builder.Append("\"text\":\"北京雅丹科技\"},");
            builder.Append("{\"id\":\"YDSY\",");
            builder.Append("\"icon\":\"../../Content/Themes/Images/16/chart_organisation.png\",");
            builder.Append("\"leaf\":true,");
            builder.Append("\"text\":\"北京雅丹石油\"}");
            builder.Append("]");
            return builder.ToString();
        }

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

        /// <summary>
        /// 在获取到List<SimpleGroupsModel2>的基础上，找出符合要求的Model并返回序列化json
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult getAllGroupsByDepID(string depId)
        {
            SimpleGroupsModel2[] list = getAllGroups();
            List<SimpleGroupsModel2> myData = new List<SimpleGroupsModel2>();
            foreach (SimpleGroupsModel2 model in list)
            {
                string groupId = model.groupid;
                string groupName = model.groupname;
                int currentUsers = model.affiliations;
                string groupOwner = model.owner.Substring(model.owner.LastIndexOf('_') + 1);//这个一般不会被修改的
                //1.根据groupOwner获取姓名、所在部门
                Tb_User user = userBll.GetModel(groupOwner);
                model.owner = user.UserName;
                model.leaf = true;
                //if (user.DeptId.Substring(0, 2) == depId)//如果部门是北京、武汉、西安
                //{
                //    //如果数据库中有此条信息，而服务器不存在此条信息，那么删除该条记录
                //    //如果数据库中存在此条信息，那么更新数据库
                //    string sqlString = "UPDATE Tb_Group SET groupName = '" + groupName + "',currentUsers=" + currentUsers + " WHERE groupId = '" + groupId + "'";
                //    int result = sqlHelper.RunSQL(sqlString);//result代表受影响的行数
                //    if (result == 0)//说明是数据库中不存在此条信息，那么执行插入数据库操作
                //    {
                //        string insertSql = "INSERT INTO Tb_Group(groupId,groupName,groupDesc,groupOwner,groupIcon,members,allowInvite,maxUsers,isPublic)VALUES('" + groupId + "', N'" + groupName + "',N'','" + groupOwner + "',0,'" + currentUsers + "',NULL,80,NULL)";
                //        sqlHelper.RunSQL(sqlString);//result代表受影响的行数
                //    }
                //    myData.Add(model);
                //}
                //else if (user.DeptId.Substring(0, 4) == depId)
                //{
                //    string sqlString = "UPDATE Tb_Group SET groupName = '" + groupName + "',currentUsers=" + currentUsers + " WHERE groupId = '" + groupId + "'";
                //    sqlHelper.RunSQL(sqlString);
                //    myData.Add(model);
                //}
            }
            //返回json字符串
            return Json(myData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult getAllMembersByGroupId(string groupId)
        {
            string strJson = getGroupDetailById(groupId);
            GroupDetailModel2 groupDetailModel = fastJSON.JSON.ToObject<GroupDetailModel1>(strJson).data[0];
            GroupMemberIdModel[] groupList = groupDetailModel.affiliations;
            List<string> userIdList = new List<string>();//获取用户ID
            string ownerId = "";
            foreach (GroupMemberIdModel user in groupList)
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
                UserBLL bll = new UserBLL();
                DataTable DTable = bll.GetUsersByUserIds(userIdList);
                UserModel model = new UserModel();
                if (DTable != null)
                {
                    List<UserModel> litestModel = new List<UserModel>();
                    for (int i = 0; i < DTable.Rows.Count; i++)
                    {
                        DataRowView rowview = DTable.DefaultView[i];
                        litestModel.Add(new UserModel()
                        {
                            UserName = rowview["UserName"].ToString(),
                            EnglishName = rowview["EnglishName"].ToString(),
                            UserId = rowview["UserId"].ToString(),
                            isOwner = rowview["UserId"].ToString() == ownerId ? true : false,
                            Status = Convert.ToInt32(rowview["Status"].ToString()),
                            Gender = Convert.ToBoolean(rowview["Gender"].ToString()),
                            Signature = rowview["Signature"].ToString(),
                            HeadPic = Convert.ToInt32(rowview["HeadPic"].ToString()),
                            CellPhone = rowview["CellPhone"].ToString(),
                            OfficePhone = rowview["OfficePhone"].ToString(),
                            Email = rowview["Email"].ToString(),
                            DeptId = rowview["DeptId"].ToString(),
                            Position = rowview["Position"].ToString(),
                            DisplayIndex = Convert.ToInt32(rowview["DisplayIndex"].ToString()),
                            active = true,
                            Comment = rowview["Comment"].ToString()
                        });
                    }
                    return Json(litestModel, JsonRequestBehavior.AllowGet);
                }
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
