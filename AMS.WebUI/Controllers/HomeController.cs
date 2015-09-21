using System;
using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.IO;
using System.Web.Mvc;
using AMS.BLL;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.WebUI.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        AMS_UserBLL ams_useribll = new AMS_UserBLL();
        AMS_User ams_user = new AMS_User();
        AMS_OrganizationBLL ams_organizationibll = new AMS_OrganizationBLL();
        AMS_Organization ams_organization = new AMS_Organization();
        AMS_SysLogBLL ams_syslogibll = new AMS_SysLogBLL();
        AMS_IPBlacklistBLL ams_ipblacklistibll = new AMS_IPBlacklistBLL();
        IPScanerHelper objScan = new IPScanerHelper();

        [Description("用户登陆")]
        public ActionResult Login()
        {
            return View();
        }

        [Description("用户登陆")]
        public ActionResult OutLogin()
        {
            string UserId = RequestSession.GetSessionUser().UserId;
            CacheHelper.RemoveAllCache("Module" + UserId);
            CacheHelper.RemoveAllCache("Button" + UserId);
            CacheHelper.RemoveAllCache("Data" + UserId);
            Session.Abandon();  //取消当前会话
            Session.Clear();    //清除当前浏览器所以Session
            return View("Login");
        }

        [HttpPost]
        [Description("验证用户")]
        public JsonResult CheckLogin()
        {
            string Account = Request.Form["Account"];          //账户
            string Pwd = Request.Form["Password"];                    //密码
            BASE_USERBLL BASE_USERibll = new BASE_USERBLL();
            BASE_USER BASE_USER = new BASE_USER();
            BASE_COMPANYBLL BASE_COMPANYibll = new BASE_COMPANYBLL();
            BASE_COMPANY BASE_COMPANY = new BASE_COMPANY();
            AMS_SysLogBLL ams_syslogibll = new AMS_SysLogBLL();
            AMS_IPBlacklistBLL ams_ipblacklistibll = new AMS_IPBlacklistBLL();
            IPScanerHelper objScan = new IPScanerHelper();
            string Msg = "";
            #region 登录
            try
            {
                string IPAddress = RequestHelper.GetIPAddress();
                objScan.IP = IPAddress;
                objScan.DataPath = Server.MapPath("../Themes/IPScaner/QQWry.Dat");
                string IPAddressName = objScan.IPLocation();
                //系统管理
                if (Account == ConfigHelper.GetValue("CurrentUserName") && Md5Helper.MD5(Pwd, 32) == ConfigHelper.GetValue("CurrentPassword"))
                {
                    SessionUser user = new SessionUser();
                    user.UserId = "System";
                    user.Account = "System";
                    user.UserName = "超级管理员";
                    user.Gender = 1;
                    user.Password = ams_user.Password;
                    user.DepartmentId = "超级管理员";
                    user.DepartmentName = "超级管理员";
                    RequestSession.AddSessionUser(user);
                    Msg = "3";//验证成功
                    ams_syslogibll.AddSysLoginLog(ams_user.Account, "登录成功", IPAddress, IPAddressName);
                }
                else
                {
                    #region 验证
                    ams_ipblacklistibll.TheIpIsRange(IPAddress);
                    string outmsg;
                    ams_user = ams_useribll.UserLogin(Account.Trim(), Pwd.Trim(), out outmsg);
                    if (outmsg != "-1")
                    {
                        if (outmsg == "succeed")
                        {
                            if (ams_user.Enabled == 1)
                            {
                                string DepartmentName = "";
                                ams_organization = ams_organizationibll.GetEntity(ams_user.DepartmentId);
                                if (ams_organization != null)
                                {
                                    DepartmentName = ams_organization.FullName;
                                }
                                SessionUser user = new SessionUser();
                                user.UserId = ams_user.UserId;
                                user.Account = ams_user.Account;
                                user.UserName = ams_user.RealName;
                                user.Gender = ams_user.Gender;
                                user.Password = ams_user.Password;
                                user.Secretkey = ams_user.Secretkey;
                                user.DepartmentId = ams_user.DepartmentId;
                                user.DepartmentName = DepartmentName;
                                RequestSession.AddSessionUser(user);
                                Msg = "3";//验证成功
                                ams_syslogibll.AddSysLoginLog(ams_user.Account, "登录成功", IPAddress, IPAddressName);
                            }
                            else
                            {
                                Msg = "2";//账户锁定
                                ams_syslogibll.AddSysLoginLog(ams_user.Account, "账户锁定", IPAddress, IPAddressName);
                            }
                        }
                        else
                        {
                            Msg = "4";//账户或者密码有错误
                            ams_syslogibll.AddSysLoginLog(ams_user.Account, "登录失败", IPAddress, IPAddressName);
                        }
                    }
                    else if (outmsg == "-1")
                    {
                        Msg = "-1";
                    }
                    else
                    {
                        Msg = DbErrorMsg.ReturnMsg;//服务连接不上
                    }
                    #endregion
                }
            }

            catch (Exception ex)
            {
                Msg = ex.Message;
            }
            #endregion
            return Json(Msg);
        }
        [Description("获取用户菜单（树形无限级手风琴）")]
        public JsonResult LoadTreeMenu()
        {
            string strlan = Request["lan"];
            if (string.IsNullOrEmpty(strlan))
            {
                if (Session["Language"] != null)
                {
                    strlan = Session["Language"] as string;
                }
            }
            var UserId = RequestSession.GetSessionUser().UserId;
            IList list = (IList)StorePermission.Instance.GetModulePermission(UserId, strlan, "SM.parentid='9f8ce93a-fc2d-4914-a59c-a6b49494108f'");
            return Json(JsonHelper.ListToJson<AMS_ModulePermission>(list, "Date"));
        }
        /// <summary>
        /// 获取下拉列表树
        /// </summary>
        /// <returns></returns>
        /// </summary>
        public ActionResult LoadTreeSubMenu()
        {
            string strlan = Request["lan"];
            string parentId = Request["ModuleId"];
            if (string.IsNullOrEmpty(parentId))
            {
                return Json(new List<TreeView>());
            }
            if (string.IsNullOrEmpty(strlan))
            {
                if (Session["Language"] != null)
                {
                    strlan = Session["Language"] as string;
                }
            }
            var UserId = RequestSession.GetSessionUser().UserId;
            IList list = (IList)StorePermission.Instance.GetSubModulePermission(UserId, strlan, parentId);
            return Json(list);
        }
        [Description("获取用户菜单（Windos开始菜单）")]
        public JsonResult LoadStartMenu()
        {
            string strlan = Request["lan"];
            if (string.IsNullOrEmpty(strlan))
            {
                if (Session["Language"] != null)
                {
                    strlan = Session["Language"] as string;
                }
            }
            var UserId = RequestSession.GetSessionUser().UserId;
            IList list = (IList)StorePermission.Instance.GetModulePermission(UserId, strlan, null);
            return Json(JsonHelper.ListToJson<AMS_ModulePermission>(list, "Date"));
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            return View();
        }

        public ActionResult MainIndex()
        {
            return View();
        }

        [Description("首页框架（树形无限级手风琴）")]
        public ActionResult TreeIndex()
        {
            var UserId = RequestSession.GetSessionUser().UserId;
            string strKey = "Module" + UserId;
            if (!CacheHelper.IsExist(strKey))
            {
                CacheHelper.RemoveAllCache(strKey);
            }
            return View();
        }
        [Description("首页框架（Windos开始菜单）")]
        public ActionResult StartIndex()
        {
            var UserId = RequestSession.GetSessionUser().UserId;
            string strKey = "Module" + UserId;
            if (!CacheHelper.IsExist(strKey))
            {
                CacheHelper.RemoveAllCache(strKey);
            }
            return View();
        }
        [Description("默认首页")]
        public ActionResult MainPage()
        {
            return View();
        }

        #region Tab页面切换
        [HttpPost]
        public JsonResult SetModuleId()
        {
            string ModuleId = Request.Form["ModuleId"];          //账户
            string ModuleName = Request.Form["ModuleName"];
            Session["SystemId"] = ModuleId;
            return Json(true);
        }

        [HttpPost]
        [Description("获取用户快捷菜单")]
        public JsonResult ShortcutsListJson()
        {
            Stream stream = FileHelper.FileToStream(Server.MapPath("~/JsonData/ShortcutsListJson.json"));
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            return Json(text);
        }

        [HttpPost]
        [Description("获取用户菜单（抽屉式手风琴）")]
        public JsonResult LoadAccordionMenu()
        {
            string strlan = Request["lan"];
            if (string.IsNullOrEmpty(strlan))
            {
                if (Session["Language"] != null)
                {
                    strlan = Session["Language"] as string;
                }
            }
            var UserId = RequestSession.GetSessionUser().UserId;
            IList list = (IList)StorePermission.Instance.GetModulePermission(UserId, strlan, null);
            return Json(JsonHelper.ListToJson<AMS_ModulePermission>(list, "Date"));
        }

        [HttpPost]
        public JsonResult SetLeave()
        {
            string ModuleId = Request.Form["ModuleId"];          //账户
            string ModuleName = Request.Form["ModuleName"];
            Session["SystemId"] = ModuleId;
            return Json(true);
        }
        #endregion

        #region 换肤
        [Description("切换皮肤")]
        public ActionResult SkinIndex()
        {
            ViewData["theme"] = "MainIndex";
            //读出自定义皮肤
            //代码？？
            return View();
        }
        [HttpPost]
        [Description("切换UI风格")]
        public JsonResult SwitchTheme()
        {
            string UItheme = Convert.ToString(Request.Form["UItheme"]);
            //把用户的UI风格写入用户个性配置表中
            //代码？？
            return Json(true);
        }
        #endregion

        #region 个人中心
        public ActionResult PersonCenter()
        {
            return View();
        }
        #endregion
    }
}