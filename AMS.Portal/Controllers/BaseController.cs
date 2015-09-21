using AMS.BLL;
using AMS.Entity;
using DotNet.Utilities;
using System.Collections;
using System.Text;
using System.Web.Mvc;

namespace AMS.WebUI.Controllers
{
    /// <summary>
    /// 基类BaseController，过滤器
    /// </summary>
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //当Session过期自动跳出登录画面
            if (RequestSession.GetSessionUser() == null)
            {
                Session.Abandon();  //取消当前会话
                Session.Clear();
                Response.Redirect("/Index");
            }
            IsUrlPermission();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            // 标记异常已处理
            filterContext.ExceptionHandled = true;
            // 跳转到错误页
            filterContext.Result = new RedirectResult(Url.Action("Error", "Shared"));
        }

        #region URL权限验证,加强安全验证防止未授权匿名不合法的请求
        /// <summary>
        /// URL权限验证,加强安全验证防止未授权匿名不合法的请求
        /// </summary>
        public void IsUrlPermission()
        {
            bool IsOK = false;
            //获取当前访问页面地址
            string requestPath = RequestHelper.GetScriptName;
            string[] filterUrl = { "../FrameAccordion/HomeIndex", "../CommonModule/User/UpdateUserPwd" };//过滤特别页面
            for (int i = 0; i < filterUrl.Length; i++)
            {
                if (requestPath == filterUrl[i])
                {
                    IsOK = true;
                    break;
                }
            }
            if (!IsOK)
            {
                string lan = "en-US";
                string UserId = RequestSession.GetSessionUser().UserId;
                IList list = (IList)StorePermission.Instance.GetModulePermission(UserId, lan, null);
                IList itemNode = IListHelper.IListToList<AMS_ModulePermission>(list).FindAll(t => t.NavigateUrl == requestPath);
                if (itemNode.Count == 0)
                {
                    StringBuilder strHTML = new StringBuilder();
                    strHTML.Append("<div><script type=\"text/javascript\">alert('很抱歉！您的权限不足，访问被拒绝！')</script>");
                    strHTML.Append("</div>");
                    Response.Write(strHTML.ToString());
                    Response.End();
                }
            }
        }
        #endregion
    }
}
