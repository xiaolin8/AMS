using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNet.BLL;
using DotNet.Portal.Models;

namespace DotNet.Portal.Controllers
{
    public class MeetingRoomController : Controller
    {
        //
        // GET: /MeetingRoom/

        public ActionResult Index()
        {
            return View();
        }

        // 获取所有的MeetingRoom
        // GET: /MeetingRoom/
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetRoomListJson()
        {
            List<MeetingRoomModel> myData = new List<MeetingRoomModel>();
            MeetingRoomBLL bll = new MeetingRoomBLL();
            DataSet ds = bll.GetList("");
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MeetingRoomModel model = new MeetingRoomModel();
                        DataRowView view = dt.DefaultView[i];
                        model.RoomId = Convert.ToInt32(view["RoomId"].ToString());
                        model.RoomName = view["RoomName"].ToString();
                        model.RoomAddr = view["RoomAddr"].ToString();
                        model.RoomCapacity = Convert.ToInt32(view["RoomCapacity"].ToString());
                        model.RoomDesc = view["RoomDesc"].ToString();
                        model.CompId = view["CompId"].ToString();
                        model.Phone = view["Phone"].ToString();
                        model.Equipments = view["Equipments"].ToString();
                        myData.Add(model);
                    }
                }
                return Json(myData, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

    }
}
