using System.Web;
using System.Web.UI;

namespace DotNet.Utilities
{
    /// <summary>
    /// 客户端提示信息帮助类
    /// </summary>
    public class ShowMsgHelper
    {
        /// <summary>
        /// 默认成功提示
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string Alert(string message)
        {
            return string.Format("showTipsMsg('{0}','4000','4');windowload();", message);
        }
        /// <summary>
        /// 默认成功提示，刷新父窗口函数关闭页面
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string AlertCallback(string message)
        {
            return string.Format("showTipsMsg('{0}','4000','4');top.$('#' + Current_iframeID())[0].contentWindow.windowload();OpenClose();", message);
        }
        /// <summary>
        /// 默认成功提示，刷新父窗口函数关闭页面
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string AlertParmCallback(string message)
        {
            return string.Format("showTipsMsg('{0}','4000','4');top.$('#' + Current_iframeID())[0].contentWindow.windowload();OpenClose();", message);//.target_right
        }
        /// <summary>
        /// 默认错误提示
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string Alert_Error(string message)
        {
            return string.Format("showTipsMsg('{0}','5000','5');", message);
        }
        /// <summary>
        /// 默认警告提示
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string Alert_Wern(string message)
        {
            return string.Format("showTipsMsg('{0}','5000','3');", message);
        }
        /// <summary>
        /// 提示警告信息
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string showFaceMsg(string message)
        {
            return string.Format("showFaceMsg('{0}');", message);
        }
        /// <summary>
        /// 提示警告信息
        /// </summary>
        /// <param name="message">显示消息</param>
        public static string showWarningMsg(string message)
        {
            return string.Format("showWarningMsg('{0}');", message);
        }

        /// <summary>
        /// 后台调用JS函数
        /// </summary>
        /// <param name="obj"></param>
        public static void ShowScript(string strobj)
        {
            Page p = HttpContext.Current.Handler as Page;
            p.ClientScript.RegisterStartupScript(p.ClientScript.GetType(), "myscript", "<script>" + strobj + "</script>");
        }
    }
}
