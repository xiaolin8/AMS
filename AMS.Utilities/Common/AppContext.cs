using System;
using System.Web;
using System.Web.Security;

namespace DotNet.Utilities
{
    /// <summary>
    /// 当前系统的运行上下文
    /// </summary>
    public class AppContext
    {
        private static AppContext _instance = null;
        private static readonly object lockHelper = new object();
        /// <summary>
        /// 单例模式
        /// </summary>
        public static AppContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockHelper)
                    {
                        if (_instance == null)
                        {
                            _instance = new AppContext();
                        }
                    }
                }
                return new AppContext();
            }
        }

        private AppContext()
        {
        }

        //private UserLogin currentUserLogin = null;
        /// <summary>
        /// 注册用户
        /// </summary>
        public void RegisterUserLogin(HttpContextBase context, LoginUser userLogin)
        {
            string sAuthTime = System.Configuration.ConfigurationManager.AppSettings["AuthenticationMinutes"];
            string sInfo = JsonHelper2.GetInstance.GetJsonStringByObject(userLogin);

            FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(1, sInfo, DateTime.Now, DateTime.Now.AddMinutes(Double.Parse(sAuthTime)), false, "", "/");
            //加密序列化验证票为字符串
            string HashTicket = FormsAuthentication.Encrypt(Ticket);
            //生成Cookie
            HttpCookie UserCookie = new HttpCookie(FormsAuthentication.FormsCookieName, HashTicket);
            context.Response.Cookies.Add(UserCookie); //输出Cookie

            //this.currentUserLogin = userLogin;
            //this.currentCulture = new ClientCultureModel(userLogin);
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public LoginUser GetLogin()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string userInfo = HttpContext.Current.User.Identity.Name;
                return JsonHelper2.GetInstance.GetObjectByJsonString<LoginUser>(userInfo);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取当前用户ID
        /// </summary>
        /// <returns></returns>
        public String GetLoginID()
        {
            LoginUser userLogin = GetLogin();
            if (!Object.Equals(userLogin, null))
            {
                return userLogin.UID;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取当前用户名称
        /// </summary>
        /// <returns></returns>
        public String GetLoginName()
        {
            LoginUser userLogin = GetLogin();
            if (!Object.Equals(userLogin, null))
            {
                return userLogin.UFullName;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取当前客户端环境
        /// </summary>
        /// <returns></returns>
        public ClientCultureModel GetCulture()
        {
            LoginUser userLogin = GetLogin();

            if (!Object.Equals(userLogin, null))
            {
                return new ClientCultureModel(userLogin); ;
            }
            else
            {
                return null;
            }
        }
    }
}