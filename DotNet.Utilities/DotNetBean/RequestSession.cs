using System.Web;

namespace AMS.Utilities
{
    /// <summary>
    /// Session 帮助类
    /// </summary>
    public class RequestSession
    {
        public RequestSession()
        {

        }
        private static string SESSION_USER = "SESSION_USER";
        public static void AddSessionUser(SessionUser user)
        {
            HttpContext rq = HttpContext.Current;
            rq.Session[SESSION_USER] = user;
        }
        public static SessionUser GetSessionUser()
        {
            HttpContext rq = HttpContext.Current;
            return (SessionUser)rq.Session[SESSION_USER];
        }

        /// <summary>
        /// 将登录用户实体存入Session by yhb 2014-10-15 add
        /// </summary>
        /// <param name="userName"></param>
        public static void AddLoginUser(LoginUser user)
        {
            HttpContext rq = HttpContext.Current;
            rq.Session[SESSION_USER] = user;
        }

        /// <summary>
        /// 取登录用户  by yhb 2014-10-15 add
        /// </summary>
        /// <returns></returns>
        public static LoginUser GetLoginUser()
        {
            HttpContext rq = HttpContext.Current;
            return (LoginUser)rq.Session[SESSION_USER];
        }
    }
}
