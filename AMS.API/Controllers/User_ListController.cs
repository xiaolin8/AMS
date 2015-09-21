using System.Collections;
using System.Web.Http;
using AMS.BLL;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.API.Controllers
{
    public class User_ListController : ApiController
    {
        string state = "", message = "";
        int count = 0;
        protected string usersId = string.Empty;
        AMS_UserBLL bll = new AMS_UserBLL();
        //
        // GET: /User_List/
        //[HttpGet]
        //public string GetAllUsers()
        //{
        //    IList list = bll.GetList();
        //    if (list != null && list.Count > 0)
        //    {
        //        state = "100"; message = "获取成功";
        //        return JsonHelper.ListToJson<AMS_User>(list, "AMS_User");
        //    }
        //    else
        //    {
        //        state = "104";
        //        message = "暂无任何用户信息";
        //        return null;
        //    }
        //}

        [HttpGet]
        public AMS_User GetUserDetail(string Account)
        {
            AMS_User user = bll.GetEntityByAccount(Account);
            if (user != null)
            {
                state = "100"; message = "获取成功";
                return user;
            }
            else
            {
                state = "104";
                message = "暂无任何用户信息";
                return null;
            }
        }

        [HttpGet]
        public void Put()
        {
            IList list = bll.GetList();
            foreach (AMS_User user in list)
            {
                if (ValidateUtil.IsNumber(user.Account))
                {
                    user.Account = PinyinHelper.PinyinString(user.RealName);
                    bll.Update(user);
                }
            }
        }
    }
}