using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using AMS.BLL;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.WebUI.Controllers
{
    public class OrganizeController : Controller
    {
        public StringBuilder strHtml = new StringBuilder();
        AMS_OrganizationBLL ams_organizationbll = new AMS_OrganizationBLL();
        AMS_UserBLL ams_userbll = new AMS_UserBLL();
        AMS_Organization ams_organization = new AMS_Organization();
        public StringBuilder strHtml_OrgChart = new StringBuilder();
        /// <summary>
        /// 显示操作按钮过滤条件
        /// </summary>
        public string[] Strconditio { get; set; }//过滤条件
        public StringBuilder sb_ButtonPermission = new StringBuilder();
        public StringBuilder sb_contextmenu = new StringBuilder();
        StringBuilder sb_contextmenuItem = new StringBuilder();
        AMS_PermissionBLL AMS_permissionibll = new AMS_PermissionBLL();

        //
        // GET: /Organize/

        public ActionResult OrganizeTree()
        {
            GetTree();
            ViewData["strHtml"] = strHtml.ToString();
            return View();
        }

        public ActionResult OrganizeList()
        {
            GetToolBar();
            ViewData["sb_contextmenuItem"] = sb_contextmenuItem.ToString();
            ViewData["sb_ButtonPermission"] = sb_ButtonPermission.ToString();
            return View();
        }

        public ActionResult OrganizeChart()
        {
            GetTreeOrgChart();
            ViewData["strHtml_OrgChart"] = strHtml_OrgChart.ToString();
            return View();
        }

        public ActionResult OrganizeForm()
        {
            return View();
        }

        #region 组织架构图
        /// <summary>
        /// 机构列表
        /// </summary>
        public void GetTreeOrgChart()
        {
            IList list = ams_organizationbll.GetList();
            List<AMS_Organization> itemNode = IListHelper.IListToList<AMS_Organization>(list).FindAll(t => t.ParentId == "0");
            foreach (AMS_Organization entity in itemNode)
            {
                string itemid = "v" + entity.OrganizationId.Replace("-", "");
                strHtml_OrgChart.Append("var " + itemid + " = new OrgNode();");
                strHtml_OrgChart.Append("" + itemid + ".Text = \"" + entity.FullName + "\";");
                strHtml_OrgChart.Append("" + itemid + ".Description = \"" + entity.ShortName + "\";");
                //strHtml_OrgChart.Append("" + itemid + ".Link = \"#\";");
                //创建子节点
                strHtml_OrgChart.Append(GetTreeNodeOrgChart(entity.OrganizationId, list));
            }
        }
        /// <summary>
        /// 创建子节点
        /// </summary>
        /// <param name="parentID">父节点主键</param>
        /// <param name="list">菜单集合</param>
        /// <returns></returns>
        public string GetTreeNodeOrgChart(string ParentId, IList list)
        {
            StringBuilder sb_TreeNode = new StringBuilder();
            List<AMS_Organization> itemNode = IListHelper.IListToList<AMS_Organization>(list).FindAll(t => t.ParentId == ParentId);
            if (itemNode.Count > 0)
            {
                foreach (AMS_Organization entity in itemNode)
                {
                    string itemid = "v" + entity.OrganizationId.Replace("-", "");
                    string itemParentId = "v" + ParentId.Replace("-", "");
                    sb_TreeNode.Append("var " + itemid + " = new OrgNode();");
                    sb_TreeNode.Append("" + itemid + ".Text = \"" + entity.FullName + "\";");
                    sb_TreeNode.Append("" + itemid + ".Description = \"" + entity.ShortName + "\";");
                    //sb_TreeNode.Append("" + itemid + ".Link = \"#\";");
                    sb_TreeNode.Append("" + itemParentId + ".Nodes.Add(" + itemid + ");");
                    //创建子节点
                    sb_TreeNode.Append(GetTreeNodeOrgChart(entity.OrganizationId, list));
                }
            }
            return sb_TreeNode.ToString();
        }
        #endregion

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
                strHtml.Append("<div class='divTree' id='" + entity.OrganizationId + "'><img src='/Themes/images/Icon16/house.png' style='vertical-align: middle;' alt=''/><span>" + entity.FullName + "</span></div>");
                //创建子节点
                strHtml.Append(GetTreeNode(entity.OrganizationId, list));
                strHtml.Append("</li>");
            }
        }

        /// <summary>
        /// 创建子节点
        /// </summary>
        /// <param name="parentID">父节点主键</param>
        /// <param name="list">菜单集合</param>
        /// <returns></returns>
        public string GetTreeNode(string ParentId, IList list)
        {
            StringBuilder sb_TreeNode = new StringBuilder();
            List<AMS_Organization> itemNode = IListHelper.IListToList<AMS_Organization>(list).FindAll(t => t.ParentId == ParentId);
            if (itemNode.Count > 0)
            {
                sb_TreeNode.Append("<ul>");
                foreach (AMS_Organization entity in itemNode)
                {
                    sb_TreeNode.Append("<li title='" + entity.Category + "'>");
                    sb_TreeNode.Append("<div class='divTree' Category='" + entity.Category + "' id='" + entity.OrganizationId + "'><img src='/Themes/images/Icon16/" + CategoryToImg(entity.Category) + "' style='vertical-align: middle;' alt=''/><span>" + entity.FullName + "</span></div>");
                    //创建子节点
                    sb_TreeNode.Append(GetTreeNode(entity.OrganizationId, list));
                    sb_TreeNode.Append("</li>");
                }
                sb_TreeNode.Append("</ul>");
            }
            return sb_TreeNode.ToString();
        }

        public string CategoryToImg(string Category)
        {
            string img = "";
            if (Category == "区域")
            {
                img = "house_star.png";
            }
            if (Category == "公司")
            {
                img = "chart_organisation.png";
            }
            if (Category == "子公司")
            {
                img = "flag_green.png";
            }
            if (Category == "部门")
            {
                img = "wand.png";
            }
            if (Category == "工作组")
            {
                img = "users.png";
            }
            return img;
        }

        #region 组织架构表
        /// <summary>
        /// 组织架构表
        /// </summary>
        public string GetTreeTable()
        {
            StringBuilder TableTreeList = new StringBuilder();
            IList list = ams_organizationbll.GetList();
            int eRowIndex = 0;
            foreach (AMS_Organization entity in list)
            {
                if (entity.ParentId == "0")
                {
                    string trID = "node-" + eRowIndex.ToString();
                    TableTreeList.Append("<tr id='" + trID + "'>");
                    TableTreeList.Append("<td style='width: 230px;padding-left:20px;'><img src='/Themes/images/Icon16/house.png' style='vertical-align: middle;' alt=''/><span style='padding-left:8px;'>" + entity.FullName + "</span></td>");
                    TableTreeList.Append("<td style='width: 130px;'>" + entity.Code + "</td>");
                    TableTreeList.Append("<td style='width: 60px;text-align:center;'>" + entity.Category + "</td>");
                    TableTreeList.Append("<td style='width: 120px;text-align:center;'>" + entity.ShortName + "</td>");
                    TableTreeList.Append("<td style='width: 100px;text-align:center;'>" + entity.Manager + "</td>");
                    TableTreeList.Append("<td style='width: 60px;text-align:center;'>" + IsEnabled(entity.Enabled) + "</td>");
                    TableTreeList.Append("<td style='width: 250px;text-align:center;'>" + entity.Description + "</td>");
                    TableTreeList.Append("<td style='display:none'>" + entity.OrganizationId + "</td>");
                    TableTreeList.Append("</tr>");
                    //创建子节点
                    TableTreeList.Append(GetTableTreeNode(entity.OrganizationId, list, trID));
                    eRowIndex++;
                }
            }
            return TableTreeList.ToString();
        }
        /// <summary>
        /// 创建子节点
        /// </summary>
        /// <param name="parentID">父节点主键</param>
        /// <param name="list">菜单集合</param>
        /// <returns></returns>
        public string GetTableTreeNode(string parentID, IList list, string parentTRID)
        {
            StringBuilder sb_TreeNode = new StringBuilder();
            int i = 1;
            foreach (AMS_Organization entity in list)
            {
                if (entity.ParentId == parentID)
                {
                    string trID = parentTRID + "-" + i.ToString();
                    sb_TreeNode.Append("<tr id='" + trID + "' class='child-of-" + parentTRID + "'>");
                    sb_TreeNode.Append("<td style='padding-left:20px;'><img src='/Themes/images/Icon16/" + CategoryToImg(entity.Category) + "' style='vertical-align: middle;' alt=''/><span style='padding-left:8px;'>" + entity.FullName + "</span></td>");
                    sb_TreeNode.Append("<td style='width: 130px;'>" + entity.Code + "</td>");
                    sb_TreeNode.Append("<td style='width: 60px;text-align:center;'>" + entity.Category + "</td>");
                    sb_TreeNode.Append("<td style='width: 120px;text-align:center;'>" + entity.ShortName + "</td>");
                    sb_TreeNode.Append("<td style='width: 100px;text-align:center;'>" + entity.Manager + "</td>");
                    sb_TreeNode.Append("<td style='width: 60px;text-align:center;'>" + IsEnabled(entity.Enabled) + "</td>");
                    sb_TreeNode.Append("<td style='width: 250px;text-align:center;'>" + entity.Description + "</td>");
                    sb_TreeNode.Append("<td style='display:none'>" + entity.OrganizationId + "</td>");
                    sb_TreeNode.Append("</tr>");
                    //创建子节点
                    sb_TreeNode.Append(GetTableTreeNode(entity.OrganizationId, list, trID));
                    i++;
                }
            }
            return sb_TreeNode.ToString();
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        /// <param name="Enabled">是否有效</param>
        /// <returns></returns>
        public string IsEnabled(int? Enabled)
        {
            if (Enabled == 1)
            {
                return "<img src='/Themes/Images/checkmark.gif'/>";
            }
            else
            {
                return "<img src='/Themes/Images/checknomark.gif'/>";
            }
        }
        #endregion

        #region 机构列表
        /// <summary>
        /// 机构列表
        /// </summary>
        public string GetTreeList(IList list)
        {
            StringBuilder strHtml = new StringBuilder();
            List<AMS_Organization> itemNode = IListHelper.IListToList<AMS_Organization>(list).FindAll(t => t.ParentId == "0");
            strHtml.Append("<li>");
            strHtml.Append("<div id='0'><img src='/Themes/images/Icon16/house.png' style='vertical-align: middle;' alt=''/><span>父节点</span></div>");
            strHtml.Append("</li>");
            foreach (AMS_Organization entity in itemNode)
            {
                strHtml.Append("<li>");
                strHtml.Append("<div id='" + entity.OrganizationId + "'><img src='/Themes/images/Icon16/house.png' style='vertical-align: middle;' alt=''/><span>" + entity.FullName + "</span></div>");
                //创建子节点
                strHtml.Append(GetTreeNode(entity.OrganizationId, list));
                strHtml.Append("</li>");
            }
            return strHtml.ToString();
        }
        #endregion

        public void LoadAction()
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "No-Cache");

            string active = HttpContext.Request["action"];                                          //提交类型
            string key = HttpContext.Request["key"];                                                //主键
            string search = HttpContext.Request["search"];                                          //模糊查询条件
            AMS_SysMenuBLL ams_sysmenuibll = new AMS_SysMenuBLL();
            AMS_SysMenu ams_sysmenu = new AMS_SysMenu();
            switch (active)
            {
                case "OrgTreeList"://加载组织架构列表
                    Response.Write(GetTreeTable());
                    Response.End();
                    break;
                case "LoadTree":
                    IList list = ams_organizationbll.GetList();
                    Response.Write(GetTreeList(list));
                    Response.End();
                    break;
                case "autocomplete"://员工自动补全，返回JSON
                    Response.Write(JsonHelper.ListToJson<AMS_User>(ams_userbll.AutoComplete(search), "JSON"));
                    Response.End();
                    break;
                case "Delete":
                    if (ams_organizationbll.IsBelowMenu(key))
                    {
                        ams_organization = ams_organizationbll.GetEntity(key);
                        Response.Write(string.Format(MessageHelper.MSG0010, ams_organization.FullName));
                        Response.End();
                    }
                    else
                    {
                        Response.Write(ams_organizationbll.Delete(key));
                        Response.End();
                    }
                    break;
                case "LoadBindDrop"://绑定下拉框，公司，部门，工作组
                    Response.Write(JsonHelper.DropToJson<AMS_Organization>(ams_organizationbll.GetList(), "JSON"));
                    Response.End();
                    break;
                default:
                    break;
            }
        }

        public string Insert_Update(AMS_Organization ams_organization, string OrganizationId)
        {
            bool IsOk = false;
            if (!string.IsNullOrEmpty(OrganizationId))//判断是否编辑
            {
                ams_organization.OrganizationId = OrganizationId;
                ams_organization.ModifyDate = DateTime.Now;
                ams_organization.ModifyUserId = RequestSession.GetSessionUser().UserId.ToString();
                ams_organization.ModifyUserName = RequestSession.GetSessionUser().UserName.ToString();
                IsOk = ams_organizationbll.Update(ams_organization);
                if (IsOk) { return ShowMsgHelper.AlertParmCallback(MessageHelper.MSG0006); }
            }
            else
            {
                ams_organization.OrganizationId = CommonHelper.GetGuid;
                ams_organization.ModifyUserId = RequestSession.GetSessionUser().UserId.ToString();
                ams_organization.ModifyUserName = RequestSession.GetSessionUser().UserName.ToString();
                IsOk = ams_organizationbll.Insert(ams_organization);
                if (IsOk) { return ShowMsgHelper.AlertParmCallback(MessageHelper.MSG0005); }
            }
            if (!IsOk)
                return ShowMsgHelper.Alert_Error(MessageHelper.MSG0022);
            return null;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void InitControl(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                AMS_Organization organization = ams_organizationbll.GetEntity(key);
                Response.Write(JsonHelper.ObjectToJson<AMS_Organization>(organization));
                Response.End();
            }
        }

        /// <summary>
        /// 根据ParentId得到FullName
        /// </summary>
        public void GetOrganizationNameById(string ParentId)
        {
            if (!string.IsNullOrEmpty(ParentId))
            {
                AMS_Organization organization = ams_organizationbll.GetEntity(ParentId);
                Response.Write(organization.FullName);
                Response.End();
            }
        }
    }
}