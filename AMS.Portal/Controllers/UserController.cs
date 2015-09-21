using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using DotNet.Portal.Models;

namespace DotNet.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        UserBLL bll = new UserBLL();

        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetUsers()
        {
            //加载组织机构列表
            DataSet DSet = bll.GetList("");
            if (DSet != null)
            {
                if (DSet.Tables[0] != null)
                {
                    List<UserModel> litestModel = new List<UserModel>();
                    for (int i = 0; i < DSet.Tables[0].Rows.Count; i++)
                    {
                        DataRowView rowview = DSet.Tables[0].DefaultView[i];
                        litestModel.Add(new UserModel()
                        {
                            UserName = rowview["UserName"].ToString(),
                            UserId = rowview["UserId"].ToString(),
                            EnglishName = rowview["EnglishName"].ToString(),
                            Signature = rowview["Signature"].ToString(),
                            Status = Convert.ToInt32(rowview["Status"].ToString()),
                            Gender = rowview["Gender"].ToString() == String.Empty ? false : true,
                            CellPhone = rowview["CellPhone"].ToString(),
                            OfficePhone = rowview["OfficePhone"].ToString(),
                            Email = rowview["Email"].ToString(),
                            DeptId = rowview["DeptId"].ToString(),
                            Position = rowview["Position"].ToString(),
                            HeadPic = Convert.ToInt32(rowview["HeadPic"].ToString()),
                            DisplayIndex = Convert.ToInt32(rowview["DisplayIndex"].ToString()),
                            Comment = rowview["Comment"].ToString()
                        });
                    }
                    return Json(litestModel, JsonRequestBehavior.AllowGet);
                }
            }
            return null;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetUsersByDepId(string DepId)
        {
            DataTable dt = bll.GetUsersByDepId(DepId);
            List<UserModel> listModel = new List<UserModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRowView rowview = dt.DefaultView[i];
                listModel.Add(new UserModel()
                {
                    UserName = rowview["UserName"].ToString(),
                    UserId = rowview["UserId"].ToString(),
                    EnglishName = rowview["EnglishName"].ToString(),
                    Signature = rowview["Signature"].ToString(),
                    Status = Convert.ToInt32(rowview["Status"].ToString()),
                    Gender = rowview["Gender"].ToString() == String.Empty ? false : true,
                    CellPhone = rowview["CellPhone"].ToString(),
                    OfficePhone = rowview["OfficePhone"].ToString(),
                    Email = rowview["Email"].ToString(),
                    DeptId = rowview["DeptId"].ToString(),
                    Position = rowview["Position"].ToString(),
                    HeadPic = Convert.ToInt32(rowview["HeadPic"].ToString()),
                    DisplayIndex = Convert.ToInt32(rowview["DisplayIndex"].ToString()),
                    active = true,
                    Comment = rowview["Comment"].ToString()
                });
            }
            return Json(listModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取用户在云服务器上的详细信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string Details(string userName)
        {
            //除了一个"username" : "huxl"
            HXService hxService = new HXService();
            return hxService.AccountGet("huxl");
        }

        /// <summary>
        /// 获取云服务器上的消息记录
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string getChatMessages(string userName)
        {//除了一个"username" : "huxl"
            HXService hxService = new HXService();
            return hxService.getChatMessages("huxl");
        }

        public string getNickById(string userName)
        {
            return bll.GetNickById(userName);
        }

        public int GetPerPhoto(string UserId)
        {
            return bll.GetPerPhoto(UserId);
        }

        //
        // GET: /User/
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
