using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AMS.BLL;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.WebUI.Controllers
{
    public class UserController : Controller
    {
        AMS_User ams_user = new AMS_User();
        AMS_UserBLL ams_userbll = new AMS_UserBLL();
        AMS_UserRoleBLL ams_userrolebll = new AMS_UserRoleBLL();
        AMS_OrganizationBLL ams_organizationbll = new AMS_OrganizationBLL();
        AMS_PermissionBLL AMS_permissionibll = new AMS_PermissionBLL();
        public string[] Strconditio { get; set; }//显示操作按钮过滤条件
        StringBuilder sb_ButtonPermission = new StringBuilder();
        StringBuilder sb_contextmenu = new StringBuilder();
        StringBuilder sbRole = new StringBuilder();
        StringBuilder sb_contextmenuItem = new StringBuilder();
        StringBuilder sbCompany = new StringBuilder();
        public string _key, imgGender, strUserInfo;

        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserList()
        {
            GetToolBar();
            ViewData["sb_contextmenuItem"] = sb_contextmenuItem.ToString();
            ViewData["sb_ButtonPermission"] = sb_ButtonPermission.ToString();
            return View();
        }

        public ActionResult UserForm()
        {
            _key = Request["key"];//主键
            if (!string.IsNullOrEmpty(_key))
            {
                ViewData["user"] = ams_userbll.GetEntity(_key);
            }
            return View();
        }

        public ActionResult UserInfo()
        {
            _key = Request["key"];//主键
            InitControl();
            ViewData["imgGender"] = imgGender;
            ViewData["strUserInfo"] = strUserInfo;
            return View();
        }

        public ActionResult UpdateUserPwd()
        {
            return View();
        }

        public ActionResult UserRole()
        {
            _key = Request["key"];//主键
            InitControl();
            GetTree();
            ViewData["strUserInfo"] = strUserInfo;
            ViewData["sbCompany"] = sbCompany.ToString();
            ViewData["sbRole"] = sbRole.ToString();
            return View();
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void InitControl()
        {
            if (!string.IsNullOrEmpty(_key))
            {
                AMS_User user = ams_userbll.GetEntity(_key);
                if (user.Gender == 1)
                {
                    imgGender = "man.png";
                }
                else
                {
                    imgGender = "woman.png";
                }
                strUserInfo = user.RealName + "（" + user.Account + "）";
                Response.Write(JsonHelper.ObjectToJson<AMS_User>(user));
                Response.End();
            }
        }

        public void InitEducationDrop()
        {
            AMS_ItemDetailsBLL ams_itemdetailsbll = new AMS_ItemDetailsBLL();
            IList list = ams_itemdetailsbll.GetList();
            IList EducationList = IListHelper.IListToList<AMS_ItemDetails>(list).FindAll(t => t.ItemsId == "1559ff6d-8b04-48f7-952c-333305bc526d");
            Response.Write(JsonHelper.DropToJson<AMS_ItemDetails>(EducationList, "JSON"));
            Response.End();
        }

        public void InitDegreeDrop()
        {
            AMS_ItemDetailsBLL ams_itemdetailsbll = new AMS_ItemDetailsBLL();
            IList list = ams_itemdetailsbll.GetList();
            IList DegreeList = IListHelper.IListToList<AMS_ItemDetails>(list).FindAll(t => t.ItemsId == "964d6a06-5282-4fcb-b805-f192ae0de922");
            Response.Write(JsonHelper.DropToJson<AMS_ItemDetails>(DegreeList, "JSON"));
            Response.End();
        }

        public void InitTitleIdDrop()
        {
            AMS_ItemDetailsBLL ams_itemdetailsbll = new AMS_ItemDetailsBLL();
            IList list = ams_itemdetailsbll.GetList();
            IList TitleIdList = IListHelper.IListToList<AMS_ItemDetails>(list).FindAll(t => t.ItemsId == "2acba9e8-5fa7-4b6f-8ebd-56e753dd059a");
            Response.Write(JsonHelper.DropToJson<AMS_ItemDetails>(TitleIdList, "JSON"));
            Response.End();
        }

        public void InitTitleLevelDrop()
        {
            AMS_ItemDetailsBLL ams_itemdetailsbll = new AMS_ItemDetailsBLL();
            IList list = ams_itemdetailsbll.GetList();
            IList TitleLevelList = IListHelper.IListToList<AMS_ItemDetails>(list).FindAll(t => t.ItemsId == "b65809f0-3b7b-44d1-b2f5-c93ef9afa12d");
            Response.Write(JsonHelper.DropToJson<AMS_ItemDetails>(TitleLevelList, "JSON"));
            Response.End();
        }

        public void InitDutyIdDrop()
        {
            AMS_ItemDetailsBLL ams_itemdetailsbll = new AMS_ItemDetailsBLL();
            IList list = ams_itemdetailsbll.GetList();
            IList TitleLevelList = IListHelper.IListToList<AMS_ItemDetails>(list).FindAll(t => t.ItemsId == "137a2d97-d1d9-4752-9c5e-239097e2ed68");
            Response.Write(JsonHelper.DropToJson<AMS_ItemDetails>(TitleLevelList, "JSON"));
            Response.End();
        }

        public void InitNationDrop()
        {
            AMS_ItemDetailsBLL ams_itemdetailsbll = new AMS_ItemDetailsBLL();
            IList list = ams_itemdetailsbll.GetList();
            IList TitleLevelList = IListHelper.IListToList<AMS_ItemDetails>(list).FindAll(t => t.ItemsId == "e2e78aec-31f7-4de5-af7b-bb5bc7c09a61");
            Response.Write(JsonHelper.DropToJson<AMS_ItemDetails>(TitleLevelList, "JSON"));
            Response.End();
        }

        public void InitNationalityDrop()
        {
            AMS_ItemDetailsBLL ams_itemdetailsbll = new AMS_ItemDetailsBLL();
            IList list = ams_itemdetailsbll.GetList();
            IList TitleLevelList = IListHelper.IListToList<AMS_ItemDetails>(list).FindAll(t => t.ItemsId == "104bfd21-5bbe-4b96-b5c2-448b84dbe0d8");
            Response.Write(JsonHelper.DropToJson<AMS_ItemDetails>(TitleLevelList, "JSON"));
            Response.End();
        }

        public void InitPartyDrop()
        {
            AMS_ItemDetailsBLL ams_itemdetailsbll = new AMS_ItemDetailsBLL();
            IList list = ams_itemdetailsbll.GetList();
            IList PartyList = IListHelper.IListToList<AMS_ItemDetails>(list).FindAll(t => t.ItemsId == "44e210fb-5afd-4511-a56c-a438d947d5bc");
            Response.Write(JsonHelper.DropToJson<AMS_ItemDetails>(PartyList, "JSON"));
            Response.End();
        }

        /// <summary>
        /// 组织机构
        /// </summary>
        public void GetTree()
        {
            DataTable dt = ams_userrolebll.GetUserRoleList(_key);
            IList list = ams_organizationbll.GetList();
            List<AMS_Organization> itemNode = IListHelper.IListToList<AMS_Organization>(list).FindAll(t => t.ParentId == "0");
            foreach (AMS_Organization entity in itemNode)
            {
                sbCompany.Append("<li>");
                sbCompany.Append("<div id='" + entity.OrganizationId + "'><img src='/Themes/images/Icon16/house.png' style='vertical-align: middle;' alt=''/>" + entity.FullName + "</div>");
                //创建子节点
                sbCompany.Append(GetTreeNode(list, dt));
                sbCompany.Append("</li>");
            }
        }

        /// <summary>
        /// 创建子节点
        /// </summary>
        /// <param name="list">list</param>
        /// <param name="dt">list</param>
        /// <returns></returns>
        public string GetTreeNode(IList list, DataTable dt)
        {
            StringBuilder sb_TreeNode = new StringBuilder();
            List<AMS_Organization> itemNode = IListHelper.IListToList<AMS_Organization>(list).FindAll(t => t.Category == "公司");
            int index = 0;
            string strclass = "";
            if (itemNode.Count > 0)
            {
                sb_TreeNode.Append("<ul>");
                foreach (AMS_Organization entity in itemNode)
                {
                    if (index == 0)
                        strclass = "collapsableselected";
                    else
                        strclass = "";
                    sb_TreeNode.Append("<li>");
                    sb_TreeNode.Append("<div class='" + strclass + "' id='" + entity.OrganizationId + "'><img src='/Themes/images/Icon16/chart_organisation.png' style='vertical-align: middle;' alt=''/>" + entity.FullName + "</div>");
                    sb_TreeNode.Append("</li>");
                    NodeRole(dt, entity.OrganizationId);
                    index++;
                }
                sb_TreeNode.Append("</ul>");
            }
            return sb_TreeNode.ToString();
        }

        /// <summary>
        /// 加载角色
        /// </summary>
        /// <param name="ListNotMember"></param>
        /// <param name="DepartmentId"></param>
        public void NodeRole(DataTable RoleList, string OrganizationId)
        {
            DataView dv = new DataView(RoleList);
            dv.RowFilter = " OrganizationId = '" + OrganizationId + "'";
            sbRole.Append("<div id='Role_" + OrganizationId + "' class='UserRole'  style='display:none;'>");
            if (dv.Count > 0)
            {
                foreach (DataRowView drv in dv)
                {
                    string checkbuttonNo = "checkbuttonNo";
                    string triangleNo = "triangleNo";
                    string IsExist = drv["IsExist"].ToString();
                    if (!string.IsNullOrEmpty(IsExist))
                    {
                        checkbuttonNo = "checkbuttonOk";
                        triangleNo = "triangleOk";
                    }
                    sbRole.Append("<div class=\"" + checkbuttonNo + "  panelcheck\">");
                    sbRole.Append("<div id=\"" + drv["RoleId"] + "\" class=\"checktext\">");
                    sbRole.Append("<img src=\"../../Themes/Images/Icon16/AllotRole.png\" />" + drv["FullName"] + "");
                    sbRole.Append("</div>");
                    sbRole.Append("<div class=\"" + triangleNo + "\"></div>");
                    sbRole.Append("</div>");
                }
            }
            else
            {
                sbRole.Append(" <span style='color:red;'>暂无角色</span>");
            }
            sbRole.Append("</div>");
        }

        public void GetToolBar()
        {
            string MenuId = Session["SystemId"].ToString();//模块菜单ID
            string UserId = RequestSession.GetSessionUser().UserId;
            IList list = (IList)StorePermission.Instance.GetButtonPermission(UserId);
            List<AMS_ButtonPermission> itemNode = IListHelper.IListToList<AMS_ButtonPermission>(list).FindAll(t => t.MenuId == MenuId);

            sb_ButtonPermission.Append("<div class=\"tools_bar\">");
            int index = 0;
            if (itemNode.Count > 0)
            {
                foreach (AMS_ButtonPermission entity in itemNode)
                {
                    if (entity.Category == "工具栏")
                    {
                        if (Strconditio != null)
                        {
                            foreach (string item in Strconditio)
                            {
                                if (item == entity.Control_ID)
                                {
                                    sb_ButtonPermission.Append("<a title=\"" + entity.Description + "\" onclick=\"" + entity.Event + ";\" class=\"tools_btn\"><span><b style=\"background: url('/Themes/images/Icon16/" + entity.Img + "') 50% 4px no-repeat;\">" + entity.FullName + "</b></span></a>");
                                }
                            }
                        }
                        else
                        {
                            sb_ButtonPermission.Append("<a title=\"" + entity.Description + "\" onclick=\"" + entity.Event + ";\" class=\"tools_btn\"><span><b style=\"background: url('/Themes/images/Icon16/" + entity.Img + "') 50% 4px no-repeat;\">" + entity.FullName + "</b></span></a>");
                            if (entity.Split == "1")
                            {
                                sb_ButtonPermission.Append("<div class=\"tools_separator\"></div>");
                            }
                        }
                    }
                    else if (entity.Category == "右击菜单栏")
                    {
                        sb_contextmenuItem.Append("{");
                        sb_contextmenuItem.Append("text: '" + entity.FullName + "',");
                        sb_contextmenuItem.Append("icon: '/Themes/images/Icon16/" + entity.Img + "',");
                        sb_contextmenuItem.Append("action: function (target) {");
                        sb_contextmenuItem.Append(entity.Event);
                        sb_contextmenuItem.Append("}");
                        sb_contextmenuItem.Append("},");
                        index++;
                    }
                }
                if (index > 0)
                {
                    sb_contextmenu.Append("$('#grid_paging').contextmenu({ items: [");
                    sb_contextmenu.Append(sb_contextmenuItem.ToString());
                    sb_contextmenu = sb_contextmenu.Remove(sb_contextmenu.Length - 1, 1);
                    sb_contextmenu.Append("] });");
                }
            }
            else
            {
                sb_ButtonPermission.Append("<a title=\"刷新当前页面\" onclick=\"Replace();\" class=\"tools_btn\"><span><b style=\"background: url('/Themes/images/Icon16/arrow_refresh.png') 50% 4px no-repeat;\">刷新</b></span></a>");
            }
            sb_ButtonPermission.Append("</div>");
        }

        public void LoadAction()
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "No-Cache");

            string active = HttpContext.Request["action"];                                          //提交类型
            int pageIndex = CommonHelper.GetInt(HttpContext.Request["pqGrid_PageIndex"]);           //当前页
            int pageSize = CommonHelper.GetInt(HttpContext.Request["pqGrid_PageSize"]);             //当前页大小
            string orderField = HttpContext.Request["pqGrid_OrderField"];                           //排序字段  
            string orderType = HttpContext.Request["pqGrid_OrderType"];                             //排序类型
            string pqGrid_Sort = HttpContext.Request["pqGrid_Sort"];                                //要显示字段
            string key = HttpContext.Request["key"];                                                //主键
            string Category = HttpContext.Request["Category"];                                      //机构分类
            string OrganizationId = HttpContext.Request["OrganizationId"];                          //机构主键
            string Parm_Key_Value = HttpContext.Request["Parm_Key_Value"];                          //搜索条件键值
            string search = HttpContext.Request["search"];                                          //模糊查询条件
            switch (active)
            {
                case "GridBindList"://加载列表
                    int count = 0;
                    Hashtable parm = HashtableHelper.String_Key_ValueToHashtable(Parm_Key_Value);
                    parm["Category"] = Category;
                    parm["OrganizationId"] = OrganizationId;
                    Response.Write(JsonHelper.PqGridPageJson<AMS_User>(ams_userbll.GetPageList(parm, orderField, orderType, pageIndex, pageSize, ref count), pageIndex, pqGrid_Sort, count));
                    Response.End();
                    break;
                case "Delete":    //删除数据
                    Response.Write(ams_userbll.Delete(key));
                    Response.End();
                    break;
                case "autocomplete"://员工自动补全，返回JSON
                    Response.Write(JsonHelper.ListToJson<AMS_User>(ams_userbll.AutoComplete(search), "JSON"));
                    Response.End();
                    break;
                default:
                    break;
            }
        }

        public string Insert_Update(AMS_User user)
        {
            bool IsOk = false;
            if (!string.IsNullOrEmpty(user.UserId))//判断是否编辑
            {
                IsOk = ams_userbll.Update(user);
                if (IsOk) { return ShowMsgHelper.AlertParmCallback(MessageHelper.MSG0006); }
            }
            else
            {
                user.UserId = CommonHelper.GetGuid;
                user.CreateUserId = RequestSession.GetSessionUser().UserId.ToString();
                user.CreateUserName = RequestSession.GetSessionUser().UserName.ToString();
                IsOk = ams_userbll.Insert(user);
                if (IsOk) { return ShowMsgHelper.AlertParmCallback(MessageHelper.MSG0005); }
            }
            if (!IsOk)
                return ShowMsgHelper.Alert_Error(MessageHelper.MSG0022);
            return null;
        }
    }
}