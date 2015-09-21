using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNet.Common;

namespace DotNet.Portal.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 返回验证码图片
        /// </summary>
        public ActionResult getCheckCode()
        {
            //首先实例化验证码的类
            ValidateCode validateCode = new ValidateCode();
            //生成验证码指定的长度
            string code = validateCode.GetRandomString(4);
            //将验证码赋值给Session变量
            //Session["ValidateCode"] = code;
            this.TempData["ValidateCode"] = code;//TempData是一个字典类，作用是在Action执行过程之间传值
            //简单的说，你可以在执行某个Action的时候，将数据存放在TempData中，那么在下一次Action执行过程中可以使用TempData中的数据
            //参考：http://developer.51cto.com/art/200904/118494.htm
            //创建验证码的图片
            byte[] bytes = validateCode.CreateImage(code);
            //最后将验证码返回
            return File(bytes, @"image/jpeg");
        }

    }
}
