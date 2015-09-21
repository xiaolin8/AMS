using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AMS.BLL;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.WebUI.Controllers
{
    public class RoleController : Controller
    {
        AMS_OrganizationBLL ams_organizationbll = new AMS_OrganizationBLL();
        AMS_RolesBLL ams_rolesbll = new AMS_RolesBLL();
        AMS_Roles ams_roles = new AMS_Roles();
        /// <summary>
        /// 显示操作按钮过滤条件
        /// </summary>
        string[] Strconditio { get; set; }//过滤条件
        StringBuilder strHtml = new StringBuilder();
        StringBuilder sb_ButtonPermission = new StringBuilder();
        StringBuilder sb_contextmenu = new StringBuilder();
        StringBuilder sb_contextmenuItem = new StringBuilder();

        //
        // GET: /Role/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CompanyTree()
        {
            GetTree();
            ViewData["strHtml"] = strHtml.ToString();
            return View();
        }

        public ActionResult RoleList()
        {
            GetToolBar();
            ViewData["sb_contextmenuItem"] = sb_contextmenuItem.ToString();
            ViewData["sb_ButtonPermission"] = sb_ButtonPermission.ToString();
            return View();
        }

        public ActionResult RoleForm()
        {
            return View();
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

        /// <summary>
        /// 组织机构
        /// </summary>
        public void GetTree()
        {
            IList list = ams_organizationbll.GetList();
            List<AMS_Organization> itemNode = IListHelper.IListToList<AMS_Organization>(list).FindAll(t => t.ParentId == "0");
            foreach (AMS_Organization entity in itemNode)
            {
                strHtml.Append("<li>");
                strHtml.Append("<div id='" + entity.OrganizationId + "'><img src='/Themes/images/Icon16/house.png' style='vertical-align: middle;' alt=''/><span>" + entity.FullName + "</span></div>");
                //创建子节点
                strHtml.Append(GetTreeNode(list, entity.OrganizationId));
                strHtml.Append("</li>");
            }
        }

        /// <summary>
        /// 创建子节点
        /// </summary>
        /// <param name="list">list</param>
        /// <returns></returns>
        public string GetTreeNode(IList list, string OrganizationId)
        {
            StringBuilder sb_TreeNode = new StringBuilder();
            List<AMS_Organization> itemNode = IListHelper.IListToList<AMS_Organization>(list).FindAll(t => t.ParentId == OrganizationId);
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
                    sb_TreeNode.Append("<div class='" + strclass + "' id='" + entity.OrganizationId + "'><img src='/Themes/images/Icon16/chart_organisation.png' style='vertical-align: middle;' alt=''/><span>" + entity.FullName + "</span></div>");
                    sb_TreeNode.Append("</li>");
                    index++;
                }
                sb_TreeNode.Append("</ul>");
            }
            return sb_TreeNode.ToString();
        }

        /// <summary>
        /// 初始化绑定下拉框
        /// </summary>
        public void InitRoleDrop()
        {
            AMS_ItemDetailsBLL ams_itemdetailsbll = new AMS_ItemDetailsBLL();
            IList list = ams_itemdetailsbll.GetListByItemsId("5fed1313-7355-4cc4-a7ec-73211a361fa6");
            Response.Write(JsonHelper.DropToJson<AMS_ItemDetails>(list, "JSON"));
            Response.End();
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void InitControl(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                AMS_Roles role = ams_rolesbll.GetEntity(key);
                Response.Write(JsonHelper.ObjectToJson<AMS_Roles>(role));
                Response.End();
            }
        }

        public string Insert_Update(AMS_Roles role, string OrganizationId, string key)
        {
            bool IsOk = false;
            if (!string.IsNullOrEmpty(key))//判断是否编辑
            {
                role.RoleId = key;
                role.ModifyDate = DateTime.Now;
                role.ModifyUserId = RequestSession.GetSessionUser().UserId.ToString();
                role.ModifyUserName = RequestSession.GetSessionUser().UserName.ToString();
                IsOk = ams_rolesbll.Update(role);
                if (IsOk) { return ShowMsgHelper.AlertParmCallback(MessageHelper.MSG0006); }
            }
            else
            {
                role.OrganizationId = OrganizationId;
                role.RoleId = CommonHelper.GetGuid;
                role.CreateDate = DateTime.Now;
                role.CreateUserId = RequestSession.GetSessionUser().UserId.ToString();
                role.CreateUserName = RequestSession.GetSessionUser().UserName.ToString();
                IsOk = ams_rolesbll.Insert(role);
                if (IsOk) { return ShowMsgHelper.AlertParmCallback(MessageHelper.MSG0005); }
            }
            if (!IsOk)
                return ShowMsgHelper.Alert_Error(MessageHelper.MSG0022);
            return null;
        }

        public void LoadAction()
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "No-Cache");

            string active = Request["action"];                                          //提交类型
            string orderField = Request["pqGrid_OrderField"];                           //排序字段  
            string orderType = Request["pqGrid_OrderType"];                             //排序类型
            string pqGrid_Sort = Request["pqGrid_Sort"];                                //要显示字段
            string key = Request["key"];                                                //主键
            string OrganizationId = Request["OrganizationId"];                          //公司主键
            switch (active)
            {
                case "GridBindList"://加载列表
                    Response.Write(JsonHelper.PqGridJson<AMS_Roles>(ams_rolesbll.GetList(OrganizationId), pqGrid_Sort));
                    Response.End();
                    break;
                case "InitRoles"://加载列表
                    Response.Write(JsonHelper.ListToJson<AMS_Roles>(ams_rolesbll.GetList(OrganizationId), "JSON"));
                    Response.End();
                    break;
                case "Delete":    //删除数据
                    ams_roles = ams_rolesbll.GetEntity(key);
                    if (ams_roles.AllowDelete == 0)
                    {
                        Response.Write(string.Format(MessageHelper.MSG0009, ams_roles.FullName));
                        Response.End();
                    }
                    else
                    {
                        Response.Write(ams_rolesbll.Delete(key));
                        Response.End();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}